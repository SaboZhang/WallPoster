using HandyControl.Controls;
using HandyControl.Themes;
using HandyControl.Tools;
using System.Net;
using System.Windows;
using System.Windows.Media;
using WallPoster.Assets;
using static WallPoster.Assets.Helper;

namespace WallPoster
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public static string[] WindowsContextMenuArgument = { string.Empty, string.Empty };


        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            LocalizationManager.Instance.LocalizationProvider = new Provider();
            ConfigHelper.Instance.SetLang(Settings.InterfaceLanguage);
            /*if (!Settings.Version.Equals(RegistryHelper.GetValue<int>(Consts.VersionKey, Consts.AppName)))
            {
                if (File.Exists(Consts.ConfigPath))
                {
                    File.Delete(Consts.ConfigPath);
                }
                RegistryHelper.AddOrUpdateKey(Consts.VersionKey, Consts.AppName, Settings.Version);
            }*/

            UpdateTheme(Settings.Theme);
            UpdateAccent(Settings.Accent);

            /*if (e.Args.Length > 0)
            {
                var NameFromContextMenu = RemoveJunkString(Path.GetFileNameWithoutExtension(e.Args[0]));

                WindowsContextMenuArgument[0] = NameFromContextMenu;
                WindowsContextMenuArgument[1] = e.Args[0].Replace(Path.GetFileName(e.Args[0]), "");
            }*/

            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;

            /*AppCenter.Start(Consts.AppSecret, typeof(Analytics), typeof(Crashes));*/
        }

        internal void UpdateTheme(ApplicationTheme theme)
        {
            if (ThemeManager.Current.ApplicationTheme != theme)
            {
                ThemeManager.Current.ApplicationTheme = theme;
                ModernWpf.ThemeManager.Current.ApplicationTheme = theme == ApplicationTheme.Light ? (ModernWpf.ApplicationTheme?)ModernWpf.ApplicationTheme.Light : (ModernWpf.ApplicationTheme?)ModernWpf.ApplicationTheme.Dark;
            }
        }

        internal void UpdateAccent(Brush accent)
        {
            if (ThemeManager.Current.AccentColor != accent)
            {
                ThemeManager.Current.AccentColor = accent;
                ModernWpf.ThemeManager.Current.AccentColor = accent == null ? null : ApplicationHelper.GetColorFromBrush(accent);
            }
        }
    }
}
