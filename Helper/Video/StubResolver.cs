using System;
using System.IO;
using System.Linq;
using WallPoster.Helper.Common;

namespace WallPoster.Helper.Video
{
    /// <summary>
    /// 解析文件是否为存根(.disc)
    /// </summary>
    public class StubResolver
    {
        /// <summary>
        /// 尝试解析文件是否为存根(.disc)
        /// </summary>
        /// <param name="path"></param>
        /// <param name="options"></param>
        /// <param name="stubType"></param>
        /// <returns></returns>
        public static bool TryResolveFile(string path, NamingOptions options, out string? stubType)
        {
            stubType = default;

            if (string.IsNullOrEmpty(path))
            {
                return false;
            }

            var extension = Path.GetExtension(path);

            if (!options.StubFileExtensions.Contains(extension, StringComparer.OrdinalIgnoreCase))
            {
                return false;
            }

            path = Path.GetFileNameWithoutExtension(path);
            var token = Path.GetExtension(path).TrimStart('.');

            foreach (var rule in options.StubTypes)
            {
                if (string.Equals(rule.Token, token, StringComparison.OrdinalIgnoreCase))
                {
                    stubType = rule.StubType;
                    return true;
                }
            }

            return true;
        }
    }
}
