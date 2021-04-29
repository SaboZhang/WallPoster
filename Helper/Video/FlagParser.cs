using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WallPoster.Helper.Common;

namespace WallPoster.Helper.Video
{
    /// <summary>
    /// 基于分隔符从文件名解析标志列表
    /// </summary>
    public class FlagParser
    {
        private readonly NamingOptions _options;

        public FlagParser(NamingOptions options)
        {
            _options = options;
        }

        public string[] GetFlags(string path)
        {
            return GetFlags(path, _options.VideoFlagDelimiters);
        }

        public string[] GetFlags(string path, char[] delimiters)
        {
            if (string.IsNullOrEmpty(path))
            {
                return Array.Empty<string>();
            }

            //注意：标签需要用空格（），连字符-，点包围或下划线
            var file = Path.GetFileName(path);
            return file.Split(delimiters, StringSplitOptions.RemoveEmptyEntries);
        }
    }
}
