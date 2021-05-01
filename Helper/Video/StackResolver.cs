using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using WallPoster.Helper.Common;
using WallPoster.Models.IO;

namespace WallPoster.Helper.Video
{
    public class StackResolver
    {
        private readonly NamingOptions _options;

        public StackResolver(NamingOptions options)
        {
            _options = options;
        }

        /// <summary>
        /// 从路径中解析目录
        /// </summary>
        /// <param name="files">路径列表</param>
        /// <returns>Enumerable</returns>
        public IEnumerable<FileStack> ResolveDirectories(IEnumerable<string> files)
        {
            return Resolve(files.Select(i => new FileSystemMetadata { FullName = i, IsDirectory = true }));
        }

        public IEnumerable<FileStack> ResolveFiles(IEnumerable<string> files)
        {
            return Resolve(files.Select(i => new FileSystemMetadata { FullName = i, IsDirectory = false }));
        }

        public IEnumerable<FileStack> Resolve(IEnumerable<FileSystemMetadata> files)
        {
            var resolve = new VideoResolver(_options);
            var list = files
                .Where(i => i.IsDirectory || resolve.IsVideoFile(i.FullName) || resolve.IsStubFile(i.FullName))
                .OrderBy(i => i.FullName)
                .ToList();
            var experssions = _options.VideoFileStackingRegexes;
            for (var i = 0; i < list.Count; i++)
            {
                var offset = 0;
                var file1 = list[i];
                var expressionIndex = 0;

                while (expressionIndex < experssions.Length)
                {
                    var exp = experssions[expressionIndex];
                    var stack = new FileStack();
                    //(Title)(Volume)(Ignore)(Extension)
                    var match1 = FindMatch(file1, exp, offset);
                    if (match1.Success)
                    {
                        var title1 = match1.Groups["title"].Value;
                        var volume1 = match1.Groups["volume"].Value;
                        var ignore1 = match1.Groups["ignore"].Value;
                        var extension1 = match1.Groups["extension"].Value;
                    }
                }
            }

            return null;
        }

        private static Match FindMatch(FileSystemMetadata input, Regex regex, int offset)
        {
            var regxInput = GetRegexInput(input);
            if (offset < 0 || offset >= regxInput.Length)
            {
                return Match.Empty;
            }
            return regex.Match(regxInput, offset);
        }

        private static string GetRegexInput(FileSystemMetadata file)
        {
            // 对于目录，将扩展设置为空，否则表达式将失败
            var input = !file.IsDirectory
                ? file.FullName
                : file.FullName + ".mkv";

            return Path.GetFileName(input);
        }
    }
}
