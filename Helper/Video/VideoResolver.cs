using System;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Linq;
using WallPoster.Helper.Common;

namespace WallPoster.Helper.Video
{
    /// <summary>
    /// 从文件路径解析<see cref=“VideoFileInfo”/>
    /// </summary>
    public class VideoResolver
    {
        private readonly NamingOptions _options;

        public VideoResolver(NamingOptions options)
        {
            _options = options;
        }

        public VideoFileInfo? ResolveDirectory(string? path)
        {
            return Resolve(path, true);
        }

        public VideoFileInfo? ResolveFile(string? path)
        {
            return Resolve(path, false);
        }

        /// <summary>
        /// 解析指定的路径
        /// </summary>
        /// <param name="path">路径</param>
        /// <param name="isDirectory">如果是true表示是文件夹</param>
        /// <param name="parseName">是否应分析视频名称以获取信息</param>
        /// <returns>VideoFileInfo</returns>
        public VideoFileInfo? Resolve(string? path, bool isDirectory, bool parseName = true)
        {
            if (string.IsNullOrEmpty(path))
            {
                return null;
            }

            bool isStub = false;
            string? container = null;
            string? stubType = null;

            if (!isDirectory)
            {
                var extension = Path.GetExtension(path);
                //检查支持的扩展名称
                if (!_options.VideoFileExtensions.Contains(extension, StringComparer.OrdinalIgnoreCase))
                {
                    //不支持，检查存根扩展
                    if (!StubResolver.TryResolveFile(path, _options, out stubType))
                    {
                        return null;
                    }

                    isStub = true;
                }

                container = extension.TrimStart('.');
            }
            var flags = new FlagParser(_options).GetFlags(path);
            var format3DResult = new Format3DParser(_options).Parse(flags);
            var extraResult = new ExtraResolver(_options).GetExtraInfo(path);
            var name = isDirectory
                ? Path.GetFileName(path)
                : Path.GetFileNameWithoutExtension(path);
            int? year = null;

            if (parseName)
            {
                var cleanDateTimeResult = CleanDateTime(name);
                name = cleanDateTimeResult.Name;
                year = cleanDateTimeResult.Year;
                if (extraResult.ExtraType == null
                    && TryCleanString(name, out ReadOnlySpan<char> newName))
                {
                    name = newName.ToString();
                }
            }

            return new VideoFileInfo(
                path: path,
                container: container,
                isStub: isStub,
                name: name,
                year: year,
                stubType: stubType,
                is3D: format3DResult.Is3D,
                format3D: format3DResult.Format3D,
                extraType: extraResult.ExtraType,
                isDirectory: isDirectory,
                extraRule: extraResult.Rule);
        }

        /// <summary>
        /// 根据扩展名确定路径是否为视频文件
        /// </summary>
        /// <param name="path">路径</param>
        /// <returns></returns>
        public bool IsVideoFile(string path)
        {
            var extension = Path.GetExtension(path);
            return _options.VideoFileExtensions.Contains(extension, StringComparer.OrdinalIgnoreCase);
        }

        public bool TryCleanString([NotNullWhen(true)] string? name, out ReadOnlySpan<char> newName)
        {
            return CleanStringParser.TryClean(name, _options.CleanStringRegexes, out newName);
        }

        public CleanDateTimeResult CleanDateTime(string name)
        {
            return CleanDateTimeParser.Clean(name, _options.CleanDateTimeRegexes);
        }
    }
}
