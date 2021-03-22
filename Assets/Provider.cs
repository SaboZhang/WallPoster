using HandyControl.Controls;
using WallPoster.Assets.Strings;

namespace WallPoster.Assets
{
    class Provider : ILocalizationProvider
    {
        public object Localize(string key)
        {
            return Lang.ResourceManager.GetObject(key);
        }
    }
}
