using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WallPoster.Constans
{
    public class WeatherEnum
    {
        public enum Aqi 
        {
            [StringValue("#95B359")] Green,
            [StringValue("#D3CF63")] Yellow,
            [StringValue("#E0991D")] Orange,
            [StringValue("#D96161")] Red,
            [StringValue("#A257D0")] Violet,
            [StringValue("#D94371")] Maroon
        };
    }
}
