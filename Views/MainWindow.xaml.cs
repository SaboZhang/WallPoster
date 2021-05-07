using HandyControl.Controls;
using HandyControl.Themes;
using HandyControl.Tools;
using ModernWpf.Controls;
using ModernWpf.Controls.Primitives;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using WallPoster.Assets.Strings;
using static WallPoster.Assets.Helper;

namespace WallPoster
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow
    {
        internal static MainWindow Instance;
        public MainWindow()
        {
            InitializeComponent();
            Instance = this;
            LoadSettings();
            #region 启动时显示大小
            Rect rc = SystemParameters.WorkArea; //获取工作区大小
            Width = rc.Width - 300;
            Height = rc.Height - 200;
            #endregion
        }

        private void LoadSettings()
        {
            if (Settings.IsFirstRun)
            {
                navView.SelectedItem = navView.MenuItems[0];
                Settings.IsFirstRun = false;
            }

            navView.PaneDisplayMode = Settings.PaneDisplayMode;

            navView.IsBackButtonVisible = Settings.IsBackEnabled ? NavigationViewBackButtonVisible.Visible : NavigationViewBackButtonVisible.Collapsed;


        }

        private void ApplicationTheme_Click(object sender, RoutedEventArgs e)
        {
            if (e.OriginalSource is AppBarButton button && button.Tag is ApplicationTheme tag)
            {
                if (tag.Equals(Settings.Theme)) return;

                Settings.Theme = tag;
                ((App)Application.Current).UpdateTheme(tag);
            }
            else if (e.OriginalSource is AppBarButton btn && (string)btn.Tag is "Accent")
            {
                var picker = SingleOpenHelper.CreateControl<ColorPicker>();
                var window = new PopupWindow
                {
                    PopupElement = picker,
                    WindowStartupLocation = WindowStartupLocation.CenterScreen,
                    AllowsTransparency = true,
                    WindowStyle = WindowStyle.None,
                    MinWidth = 0,
                    MinHeight = 0,
                    Title = Lang.ResourceManager.GetString("Accent")
                };

                if (Settings.Accent != null)
                {
                    picker.SelectedBrush = new SolidColorBrush(ColorHelper.GetColorFromBrush(Settings.Accent));
                }

                picker.SelectedColorChanged += delegate
                {
                    ((App)Application.Current).UpdateAccent(picker.SelectedBrush);
                    Settings.Accent = picker.SelectedBrush;
                    window.Close();
                };
                picker.Canceled += delegate { window.Close(); };
                window.Show();
            }
        }

        private void OpenFlyout(string resourceKey, FrameworkElement element)
        {
            var cmdBarFlyout = (CommandBarFlyout)Resources[resourceKey];
            var paneMode = Settings.PaneDisplayMode;
            switch (paneMode)
            {
                case NavigationViewPaneDisplayMode.Auto:
                case NavigationViewPaneDisplayMode.Left:
                case NavigationViewPaneDisplayMode.LeftCompact:
                case NavigationViewPaneDisplayMode.LeftMinimal:
                    cmdBarFlyout.Placement = FlyoutPlacementMode.RightEdgeAlignedTop;
                    break;
                case NavigationViewPaneDisplayMode.Top:
                    cmdBarFlyout.Placement = FlyoutPlacementMode.BottomEdgeAlignedRight;
                    break;
            }

            cmdBarFlyout.ShowAt(element);
        }

        private void navChangeLanguage_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            OpenFlyout("LanguageCommandBar", navChangeLanguage);
        }

        private void navChangeTheme_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            OpenFlyout("ThemeCommandBar", navChangeTheme);
        }

        private void LanguageChange_Click(object sender, RoutedEventArgs e)
        {
            if (e.OriginalSource is AppBarButton button && button.Tag is string tag)
            {
                if (tag.Equals(Settings.InterfaceLanguage))
                    return;
                Settings.InterfaceLanguage = tag;
                ConfigHelper.Instance.SetLang(tag);
            }
        }
    }
}
