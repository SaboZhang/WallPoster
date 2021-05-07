using HandyControl.Themes;
using HandyControl.Tools;
using Prism.Ioc;
using Prism.Regions;
using Prism.Unity;
using System.Net;
using System.Windows;
using System.Windows.Controls.Primitives;
using System.Windows.Media;
using WallPoster.CustomerRegionAdapters;
using WallPoster.Infrastructure;
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
            //注册全局命令
            containerRegistry.RegisterSingleton<IApplicationCommands, ApplicationCommands>();

            //注册导航
            containerRegistry.RegisterForNavigation<MainWindow, MainViewModel>();
            containerRegistry.RegisterForNavigation<MovieInfo>();
            containerRegistry.RegisterForNavigation<Movie>();
            containerRegistry.RegisterForNavigation<TVplay>();
            containerRegistry.RegisterForNavigation<Home>();
            containerRegistry.RegisterForNavigation<General>();
            containerRegistry.RegisterForNavigation<About>();
        }

        protected override void ConfigureRegionAdapterMappings(RegionAdapterMappings regionAdapterMappings)
        {
            base.ConfigureRegionAdapterMappings(regionAdapterMappings);
            regionAdapterMappings.RegisterMapping(typeof(UniformGrid), Container.Resolve<UniformGridRegionAdapter>());
        }

        protected override Window CreateShell()

         => Container.Resolve<MainWindow>();
    }
}
