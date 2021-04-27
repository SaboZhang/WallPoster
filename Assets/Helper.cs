using nucs.JsonSettings;
using nucs.JsonSettings.Autosave;

namespace WallPoster.Assets
{
    public class Helper
    {
        public static ISettings Settings = JsonSettings.Load<ISettings>().EnableAutosave();

        public enum Category { Movie, Tv }
    }
}
