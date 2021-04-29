using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WallPoster.Helper.Video
{
    /// <summary>
    /// 表示单个视频文件
    /// </summary>
    public class VideoFileInfo
    {
        public VideoFileInfo(string name, string path, string? container, int? year = default, ExtraType? extraType = default, ExtraRule? extraRule = default, bool isStub = default, string? stubType = default, bool isDirectory = default)
        {
            Path = path;
            Container = container;
            Name = name;
            Year = year;
            ExtraType = extraType;
            ExtraRule = extraRule;
            IsStub = isStub;
            StubType = stubType;
            IsDirectory = isDirectory;
        }

        /// <summary>
        /// 获取或设置路径
        /// </summary>
        /// <value>The path.</value>
        public string Path { get; set; }

        /// <summary>
        /// 获取或设置视频容器
        /// </summary>
        /// <value>视频容器</value>
        public string? Container { get; set; }

        /// <summary>
        /// 获取或设置名字
        /// </summary>
        /// <value>影片名</value>
        public string Name { get; set; }

        /// <summary>
        /// 获取或设置年份
        /// </summary>
        /// <value>年份</value>
        public int? Year { get; set; }

        /// <summary>
        /// 获取或设置额外内容的类型，例如预告片、主题曲、幕后等等
        /// </summary>
        /// <value>额外的类型</value>
        public ExtraType? ExtraType { get; set; }

        /// <summary>
        /// 获取或设置额外规则
        /// </summary>
        /// <value>额外的规则</value>
        public ExtraRule? ExtraRule { get; set; }

        /// <summary>
        /// 获取或设置一个值，该值指示此实例是否为存根
        /// </summary>
        /// <value><c>true</c> if this instance is stub; otherwise, <c>false</c>.</value>
        public bool IsStub { get; set; }

        /// <summary>
        /// 获取或设置存根的类型
        /// </summary>
        /// <value>存根类型</value>
        public string? StubType { get; set; }

        /// <summary>
        /// 获取或设置一个值，该值指示此实例是否为目录
        /// </summary>
        /// <value>类型</value>
        public bool IsDirectory { get; set; }

        /// <summary>
        /// 获取不带扩展名的文件名
        /// </summary>
        /// <value>没有扩展名的文件名</value>
        public string FileNameWithoutExtension => !IsDirectory
            ? System.IO.Path.GetFileNameWithoutExtension(Path)
            : System.IO.Path.GetFileName(Path);

        /// <inheritdoc />
        public override string ToString()
        {
            return "VideoFileInfo(Name: '" + Name + "')";
        }
    }
}
