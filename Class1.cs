using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WallPoster
{
    class Class1
    {

        public class Now
        {
            /// <summary>
            /// 
            /// </summary>
            public string pubTime { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public string aqi { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public string level { get; set; }
            /// <summary>
            /// 良
            /// </summary>
            public string category { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public string primary { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public string pm10 { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public string pm2p5 { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public string no2 { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public string so2 { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public string co { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public string o3 { get; set; }
        }

        public class Station
        {
            /// <summary>
            /// 
            /// </summary>
            public string pubTime { get; set; }
            /// <summary>
            /// 昌平镇
            /// </summary>
            public string name { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public string id { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public string aqi { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public string level { get; set; }
            /// <summary>
            /// 良
            /// </summary>
            public string category { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public string primary { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public string pm10 { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public string pm2p5 { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public string no2 { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public string so2 { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public string co { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public string o3 { get; set; }
        }

        public class Refer
        {
            /// <summary>
            /// 
            /// </summary>
            public List<string> sources { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public List<string> license { get; set; }
        }

        public class Root
        {
            /// <summary>
            /// 
            /// </summary>
            public string code { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public string updateTime { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public string fxLink { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public Now now { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public List<Station> station { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public Refer refer { get; set; }
        }

    }
}
