using System;
using System.IO;

namespace WallPoster.Assets
{
    public abstract class Consts
    {
        public static readonly string AppName = "WallPoster";

        public static readonly string RootPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), AppName);
        public static readonly string ConfigPath = Path.Combine(RootPath, "Config.json");
        public static readonly string CachePath = Path.Combine(RootPath, "Cache");

        #region 和风天气参数以及接口
        public const string WeatherKey = "e4128d214e47471ea020c5630ebce2d0";
        public static readonly string NowWeather = "https://devapi.qweather.com/v7/weather/now";
        public static readonly string CityInfof = "https://geoapi.qweather.com/v2/city/lookup";
        public static readonly string TheLife = "https://devapi.qweather.com/v7/indices/1d";
        public static readonly string Warning = "https://devapi.qweather.com/v7/warning/now";
        public static readonly string Air = "https://devapi.qweather.com/v7/air/now";
        public static readonly string FutureWeather = "https://devapi.qweather.com/v7/weather/7d";
        public static readonly string StatusCode = "https://dev.qweather.com/docs/start/status-code/";
        #endregion

        public static readonly string SayingUrl = "https://api.xygeng.cn/one";
        public const string MovieCategory = "0";
        public const string TVCategory = "1";
        public const string DefaultCity = "天津";
    }
}
