using System;

namespace WallPoster.Models.IO
{
    public class FileSystemMetadata
    {
        /// <summary>
        /// 
        /// </summary>
        /// <value>判断是否存在存在返回 true 否则 false</value>
        public bool Exists { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <value>全名</value>
        public string FullName { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <value>名字</value>
        public string Name { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <value>扩展名</value>
        public string Extension { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <value>大小</value>
        public long Length { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <value>文件夹名</value>
        public string DirectoryName { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <value>最后更改时间UTC</value>
        public DateTime LastWriteTimeUtc { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <value>创建时间UTC</value>
        public DateTime CreationTimeUtc { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <value>如果是文件夹返回true 否则 fasle</value>
        public bool IsDirectory { get; set; }
    }
}
