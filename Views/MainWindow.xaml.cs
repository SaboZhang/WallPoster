using HandyControl.Controls;
using HandyControl.Themes;
using HandyControl.Tools;
using ModernWpf.Controls;
using ModernWpf.Controls.Primitives;
using System;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Navigation;
using WallPoster.Assets.Strings;
using WallPoster.Views;
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

        private void NavigationView_SelectionChanged(NavigationView sender, NavigationViewSelectionChangedEventArgs args)
        {
            var selectedItem = (NavigationViewItem)args.SelectedItem;
            if (selectedItem != null)
            {
                var tag = selectedItem.Tag.ToString();
                switch (selectedItem.Tag)
                {
                    case "Home":
                        Navigate(typeof(Home), tag);
                        break;
                    case "Movie":
                        Navigate(typeof(Movie), tag);
                        break;
                    case "TVplay":
                        Navigate(typeof(TVplay), tag);
                        break;
                    case "General":
                        Navigate(typeof(General), tag);
                        break;
                    case "About":
                        Navigate(typeof(About), tag);
                        break;
                }
            }
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


        private void navView_BackRequested(NavigationView sender, NavigationViewBackRequestedEventArgs args)
        {
            if (contentFrame.CanGoBack)
            {
                contentFrame.GoBack();
            }
        }

        private void contentFrame_Navigated(object sender, NavigationEventArgs e)
        {
            var pageName = contentFrame.Content.GetType().Name;
            var menuItem = navView.MenuItems
                                     .OfType<NavigationViewItem>()
                                     .Where(item => item.Tag.ToString() == pageName)
                                     .FirstOrDefault();
            if (menuItem != null)
            {
                navView.SelectedItem = menuItem;
            }

            if (contentFrame.CanGoBack)
            {
                navView.IsBackEnabled = true;
            }
            else
            {
                navView.IsBackEnabled = false;
            }
        }

        private void contentFrame_Navigating(object sender, NavigatingCancelEventArgs e)
        {
            if (e.NavigationMode == NavigationMode.Back)
            {
                contentFrame.RemoveBackEntry();
            }
        }

        public void NavigateTo(Type page, object parameter)
        {
            contentFrame?.Navigate(page, parameter);
        }

        public void Navigate(Type page, string tag)
        {
            var pageName = contentFrame?.Content?.GetType()?.Name;

            if (pageName != tag)
            {
                contentFrame?.Navigate(page);
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
