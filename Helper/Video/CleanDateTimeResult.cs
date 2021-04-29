using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WallPoster.Helper.Video
{
    /// <summary>
    /// 名称与年份的结构体
    /// </summary>
    public readonly struct CleanDateTimeResult
    {
        public CleanDateTimeResult(string name, int? year = null)
        {
            Name = name;
            Year = year;
        }

        public string Name { get; }

        public int? Year { get; }
    }
}
