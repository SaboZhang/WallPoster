using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using WallPoster.Helper.Common;
using WallPoster.Models.IO;

namespace WallPoster.Helper.Video
{
    /// <summary>
    /// 解决视频文件列表中的替代版本和其他内容
    /// </summary>
    public class VideoListResolver
    {
        private readonly NamingOptions _options;

        public VideoListResolver(NamingOptions options)
        {
            _options = options;
        }

        /// <summary>
        /// 解决视频文件列表中的替代版本和其他内容
        /// </summary>
        /// <param name="files"></param>
        /// <param name="supportMultiVersion"></param>
        /// <returns></returns>
        public IEnumerable<VideoInfo> Resolve(List<FileSystemMetadata> files, bool supportMultiVersion = true)
        {
            var videoResolver = new VideoResolver(_options);
            var videoInfos = files
                .Select(i => videoResolver.Resolve(i.FullName, i.IsDirectory))
                .OfType<VideoFileInfo>()
                .ToList();

            // 过滤掉所有额外内容，否则它们可能导致堆栈无法解析
            var nonExteras = videoInfos
                .Where(i => i.ExtraType == null)
                .Select(i => new FileSystemMetadata { FullName = i.Path, IsDirectory = i.IsDirectory });

            var stackResult = new StackResolver(_options)
                .Resolve(nonExteras).ToList();

            var remainingFiles = videoInfos
                .Where(i => !stackResult.Any(s => i.Path != null && s.ContainsFile(i.Path, i.IsDirectory)))
                .ToList();

            var list = new List<VideoInfo>();

            foreach (var stack in stackResult)
            {
                var info = new VideoInfo(stack.Name)
                {
                    Files = stack.Files.Select(i => videoResolver.Resolve(i, stack.IsDirectoryStack))
                    .OfType<VideoFileInfo>()
                    .ToList()
                };
                info.Year = info.Files[0].Year;
                var extraBaseNames = new List<string> { stack.Name, Path.GetFileNameWithoutExtension(stack.Files[0]) };
                var extras = GetExtras(remainingFiles, extraBaseNames);
                if (extras.Count > 0)
                {
                    remainingFiles = remainingFiles
                        .Except(extras)
                        .ToList();

                    info.Extras = extras;
                }

                list.Add(info);
            }

            var standaloneMedia = remainingFiles
                .Where(i => i.ExtraType == null)
                .ToList();

            foreach (var media in standaloneMedia)
            {
                var info = new VideoInfo(media.Name) { Files = new List<VideoFileInfo> { media } };
                info.Year = info.Files[0].Year;
                var extras = GetExtras(remainingFiles, new List<string> { media.FileNameWithoutExtension });
                remainingFiles = remainingFiles
                    .Except(extras.Concat(new[] { media }).ToList())
                    .ToList();
                info.Extras = extras;
                list.Add(info);
            }
            if (supportMultiVersion)
            {
                list = GetVideosGroupedByVersion(list).ToList();
            }

            // 如果只有一个已解析的视频，同时使用文件夹名称查找其他内容
            if (list.Count == 1)
            {
                var info = list[0];
                var videoPath = list[0].Files[0].Path;
                var parentPath = Path.GetDirectoryName(videoPath);

                if (!string.IsNullOrEmpty(parentPath))
                {
                    var folderName = Path.GetFileName(parentPath);
                    if (!string.IsNullOrEmpty(folderName))
                    {
                        var extras = GetExtras(remainingFiles, new List<string> { folderName });
                        remainingFiles = remainingFiles
                            .Except(extras)
                            .ToList();
                        extras.AddRange(info.Extras);
                        info.Extras = extras;
                    }
                }

                // 添加只是基于文件名的额外内容
                var extrasByFileName = remainingFiles
                    .Where(i => i.ExtraRule != null && i.ExtraRule.RuleType == ExtraRuleType.Filename)
                    .ToList();
                remainingFiles = remainingFiles
                    .Except(extrasByFileName)
                    .ToList();
                extrasByFileName.AddRange(info.Extras);
                info.Extras = extrasByFileName;
            }
            // 如果只有一个视频，接受所有的预告片
            if (list.Count == 1)
            {
                var trailers = remainingFiles
                    .Where(i => i.ExtraType == ExtraType.Trailer)
                    .ToList();
                trailers.AddRange(list[0].Extras);
                list[0].Extras = trailers;

                remainingFiles = remainingFiles
                    .Except(trailers)
                    .ToList();
            }

            // 无论剩下什么文件，只需添加它们
            list.AddRange(remainingFiles.Select(i => new VideoInfo(i.Name) { Files = new List<VideoFileInfo> { i }, Year = i.Year }).ToList());

            return list;
        }

        private IEnumerable<VideoInfo> GetVideosGroupedByVersion(List<VideoInfo> videos)
        {
            if (videos.Count == 0)
            {
                return videos;
            }

            var list = new List<VideoInfo>();

            var folderName = Path.GetFileName(Path.GetDirectoryName(videos[0].Files[0].Path));

            if (!string.IsNullOrEmpty(folderName)
                && folderName.Length > 1
                && videos.All(i => i.Files.Count == 1
                    && IsEligibleForMultiVersion(folderName, i.Files[0].Path))
                    && HaveSameYear(videos))
            {
                var ordered = videos.OrderBy(i => i.Name).ToList();

                list.Add(ordered[0]);

                var alternateVersionsLen = ordered.Count - 1;
                var alternateVersions = new VideoFileInfo[alternateVersionsLen];
                for (int i = 0; i < alternateVersionsLen; i++)
                {
                    alternateVersions[i] = ordered[i + 1].Files[0];
                }

                list[0].AlternateVersions = alternateVersions;
                list[0].Name = folderName;
                var extras = ordered.Skip(1).SelectMany(i => i.Extras).ToList();
                extras.AddRange(list[0].Extras);
                list[0].Extras = extras;

                return list;
            }

            return videos;
        }

        private bool IsEligibleForMultiVersion(string folderName, string testFilePath)
        {
            string testFileName = Path.GetFileNameWithoutExtension(testFilePath);
            if (testFileName.StartsWith(folderName, StringComparison.OrdinalIgnoreCase))
            {
                // 在清理之前删除文件夹名称，因为我们不清理该部分
                if (folderName.Length <= testFileName.Length)
                {
                    testFileName = testFileName.Substring(folderName.Length).Trim();
                }

                if (CleanStringParser.TryClean(testFileName, _options.CleanStringRegexes, out var cleanName))
                {
                    testFileName = cleanName.Trim().ToString();
                }
                // CleanStringParser应该删除了通用关键字等
                return string.IsNullOrEmpty(testFileName)
                       || testFileName[0] == '-'
                       || Regex.IsMatch(testFileName, @"^\[([^]]*)\]");
            }

            return false;
        }

        private bool HaveSameYear(List<VideoInfo> videos)
        {
            return videos.Select(i => i.Year ?? -1).Distinct().Count() < 2;
        }

        private List<VideoFileInfo> GetExtras(IEnumerable<VideoFileInfo> remainingFiles, List<string> baseNames)
        {
            foreach (var name in baseNames.ToList())
            {
                var trimmedName = name.TrimEnd().TrimEnd(_options.VideoFlagDelimiters).TrimEnd();
                baseNames.Add(trimmedName);
            }

            return remainingFiles
                .Where(i => i.ExtraType != null)
                .Where(i => baseNames.Any(b =>
                    i.FileNameWithoutExtension.StartsWith(b, StringComparison.OrdinalIgnoreCase)))
                .ToList();
        }
    }
}
