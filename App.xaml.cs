using DryIoc;
using HandyControl.Themes;
using HandyControl.Tools;
using Prism.DryIoc;
using Prism.Ioc;
using System.Net;
using System.Windows;
using System.Windows.Media;
using WallPoster.ViewModels;
using WallPoster.Views;
using static WallPoster.Assets.Helper;

namespace WallPoster
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : PrismApplication
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

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            /*containerRegistry.RegisterForNavigation<BlankPage, BlankViewModel>(PageKeys.Blank);
            containerRegistry.RegisterForNavigation<MainPage, MainViewModel>(PageKeys.Main);*/
            containerRegistry.RegisterForNavigation<MainWindow, MainViewModel>();
            containerRegistry.RegisterForNavigation<MovieInfo>();
        }

        protected override Window CreateShell()

         => Container.Resolve<MainWindow>();
    }
}
