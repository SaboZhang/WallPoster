using System;
using System.Collections.Generic;

namespace WallPoster.Helper.Video
{
    /// <summary>
    /// 表示一个完整的视频，包括所有部分和字幕
    /// </summary>
    public class VideoInfo
    {
        public VideoInfo(string? name)
        {
            Name = name;

            Files = Array.Empty<VideoFileInfo>();
            Extras = Array.Empty<VideoFileInfo>();
            AlternateVersions = Array.Empty<VideoFileInfo>();
        }

        /// <summary>
        /// 影片名称
        /// </summary>
        public string? Name { get; set; }

        /// <summary>
        /// 影片年份
        /// </summary>
        public int? Year { get; set; }

        /// <summary>
        /// 影片文件信息列表
        /// </summary>
        public IReadOnlyList<VideoFileInfo> Files { get; set; }

        /// <summary>
        /// 视频文件扩展名列表
        /// </summary>
        public IReadOnlyList<VideoFileInfo> Extras { get; set; }

        /// <summary>
        /// 替代版本
        /// </summary>
        public IReadOnlyList<VideoFileInfo> AlternateVersions { get; set; }

    }
}
