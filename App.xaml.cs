using HandyControl.Themes;
using HandyControl.Tools;
using System.Net;
using System.Windows;
using System.Windows.Media;
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
            
            ConfigHelper.Instance.SetLang(Settings.InterfaceLanguage);
            
            UpdateTheme(Settings.Theme);
            UpdateAccent(Settings.Accent);

            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;

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
                ModernWpf.ThemeManager.Current.AccentColor = accent == null ? null : ColorHelper.GetColorFromBrush(accent);
            }
        }
    }
}
