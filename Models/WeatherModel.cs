using System;
using System.Collections.Generic;

namespace WallPoster.Models
{
    /// <summary>
    /// 和风天气实体类 https://dev.qweather.com/docs/api/
    /// </summary>
    public class WeatherModel
    {
        /// <summary>
        /// 状态码
        /// </summary>
        public string code { get; set; }

        /// <summary>
        /// 更新时间
        /// </summary>
        public DateTime updateTime { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string fxLink { get; set; }

        /// <summary>
        /// 实时天气
        /// </summary>
        public Now now { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public Refer refer { get; set; }

        /// <summary>
        /// 生活指数
        /// </summary>
        public List<Daily> daily { get; set; }

    }

    public class Now
    {
        /// <summary>
        /// 
        /// </summary>
        public string obsTime { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string temp { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string feelsLike { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string icon { get; set; }

        /// <summary>
        /// 多云
        /// </summary>
        public string text { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string wind360 { get; set; }

        /// <summary>
        /// 东风
        /// </summary>
        public string windDir { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string windScale { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string windSpeed { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string humidity { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string precip { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string pressure { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string vis { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string cloud { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string dew { get; set; }

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
        /// 
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

    public class Daily
    {
        /// <summary>
        /// 预报日期
        /// </summary>
        public DateTime date { get; set; }
        /// <summary>
        /// 生活指数类型ID
        /// </summary>
        public string type { get; set; }
        /// <summary>
        /// 生活指数类型名称
        /// </summary>
        public string name { get; set; }
        /// <summary>
        /// 生活指数预报等级
        /// </summary>
        public string level { get; set; }
        /// <summary>
        /// 生活等级预报级别名称
        /// </summary>
        public string category { get; set; }
        /// <summary>
        /// 生活指数预报的详细描述
        /// </summary>
        public string text { get; set; }
    }


}
