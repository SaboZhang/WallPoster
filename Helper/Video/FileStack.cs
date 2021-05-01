using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WallPoster.Helper.Video
{
    /// <summary>
    /// 包含带有附加信息的文件路径列表
    /// </summary>
    public class FileStack
    {
        public FileStack()
        {
            Files = new List<string>();
        }

        public bool IsDirectoryStack { get; set; }

        public List<string> Files { get; set; }

        public string Name { get; set; } = string.Empty;

        /// <summary>
        /// 帮助函数来确定路径是否在堆栈中
        /// </summary>
        /// <param name="file"></param>
        /// <param name="isDirectory"></param>
        /// <returns></returns>
        public bool ContainsFile(string file, bool isDirectory)
        {
            if (IsDirectoryStack == isDirectory)
            {
                return Files.Contains(file, StringComparer.OrdinalIgnoreCase);
            }

            return false;
        }
    }
}
