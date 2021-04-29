using System.Collections.Generic;

namespace WallPoster.Helper.Video
{
    /// <summary>
    /// 从<see cref="Format3DParser"/>返回数据的Helper对象
    /// </summary>
    public class Format3DResult
    {
        public Format3DResult()
        {
            Tokens = new List<string>();
        }

        public bool Is3D { get; set; }

        public string? Format3D { get; set; }

        public List<string> Tokens { get; set; }
    }
}
