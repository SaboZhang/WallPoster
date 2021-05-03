using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
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
                        var j = i + 1;
                        while (j < list.Count)
                        {
                            var file2 = list[j];
                            if (file1.IsDirectory != file2.IsDirectory)
                            {
                                j++;
                                continue;
                            }

                            // (Title)(Volume)(Ignore)(Extension)
                            var match2 = FindMatch(file2, exp, offset);
                            if (match2.Success)
                            {
                                var title2 = match2.Groups[1].Value;
                                var volume2 = match2.Groups[2].Value;
                                var ignore2 = match2.Groups[3].Value;
                                var extension2 = match2.Groups[4].Value;
                                if (string.Equals(title1, title2, StringComparison.OrdinalIgnoreCase))
                                {
                                    if (!string.Equals(volume1, volume2, StringComparison.OrdinalIgnoreCase))
                                    {
                                        if (string.Equals(ignore1, ignore2, StringComparison.OrdinalIgnoreCase))
                                        {
                                            if (stack.Files.Count == 0)
                                            {
                                                stack.Name = title1 + ignore1;
                                                stack.IsDirectoryStack = file1.IsDirectory;
                                                stack.Files.Add(file1.FullName);
                                            }

                                            stack.Files.Add(file2.FullName);
                                        }
                                        else
                                        {
                                            offset = 0;
                                            expressionIndex++;
                                            break;
                                        }
                                    }
                                    else if (!string.Equals(ignore1, ignore2, StringComparison.OrdinalIgnoreCase))
                                    {
                                        //使用偏移量再次尝试
                                        offset = match1.Groups[3].Index;
                                        break;
                                    }
                                    else
                                    {
                                        //扩展名不匹配
                                        offset = 0;
                                        expressionIndex++;
                                        break;
                                    }

                                }
                                else
                                {
                                    //title 不匹配
                                    offset = 0;
                                    expressionIndex++;
                                    break;
                                }
                            }
                            else
                            {
                                //不匹配 match2 下一个表达式
                                offset = 0;
                                expressionIndex++;
                                break;
                            }
                            j++;
                        }
                        if (j == list.Count)
                        {
                            expressionIndex = experssions.Length;
                        }
                    }
                    else
                    {
                        offset = 0;
                        expressionIndex++;
                    }

                    if (stack.Files.Count > 1)
                    {
                        yield return stack;
                        i += stack.Files.Count - 1;
                        break;
                    }
                }
            }
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
