using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WallPoster.Helper.Video
{
    /// <summary>
    /// 数据类保存关于存根类型规则的信息
    /// </summary>
    public class StubTypeRule
    {
        public StubTypeRule(string token, string stubType)
        {
            Token = token;
            StubType = stubType;
        }


        public string Token { get; set; }

        public string StubType { get; set; }
    }
}
