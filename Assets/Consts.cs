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
        public const string WeatherKey = "e4128d214e47471ea020c5630ebce2d0";
    }
}
