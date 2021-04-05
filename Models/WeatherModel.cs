using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WallPoster.Models
{
    public class WeatherModel
    {
        public Basic basic { get; set; }
        public Update update { get; set; }
        public string status { get; set; }
        public Now now { get; set; }
    }

    public class Basic
    {
        public string Cid { get; set; }
        public string Location { get; set; }
        public string ParentCity { get; set; }
        public string admin_area { get; set; }
        public string Country { get; set; }
        public string Lat { get; set; }
        public string Lon { get; set; }
        public string Tz { get; set; }
    }

    public class Update
    {
        public DateTime loc { get; set; }
        public DateTime utc { get; set; }
    }

    public class Now
    {
        public string cloud { get; set; }
        public string cond_code { get; set; }
        public string cond_txt { get; set; }
        public string fl { get; set; }
        public string hum { get; set; }
        public string pcpn { get; set; }
        public string pres { get; set; }
        public string tmp { get; set; }
        public string vis { get; set; }
        public string wind_deg { get; set; }
        public string wind_dir { get; set; }
        public string wind_sc { get; set; }
        public string wind_spd { get; set; }
    }

}
