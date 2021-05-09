using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WallPoster.Models
{
    public class CityModel
    {
        /// <summary>
        /// 
        /// </summary>
        public string code { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public List<Location> location { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public Refer refer { get; set; }

    }

    public class Location
    {
        /// <summary>
        /// 西青
        /// </summary>
        public string name { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string id { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string lat { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string lon { get; set; }
        /// <summary>
        /// 天津
        /// </summary>
        public string adm2 { get; set; }
        /// <summary>
        /// 天津市
        /// </summary>
        public string adm1 { get; set; }
        /// <summary>
        /// 中国
        /// </summary>
        public string country { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string tz { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string utcOffset { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string isDst { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string type { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string rank { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string fxLink { get; set; }
    }

}
