using HandyControl.Controls;
using HandyControl.Tools.Extension;
using WallPoster.Assets.Strings;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;
using static WallPoster.Assets.Helper;

namespace WallPoster.Views
{
    /// <summary>
    /// About.xaml 的交互逻辑
    /// </summary>
    public partial class About : UserControl
    {
        string Version = string.Empty;

        public About()
        {
            InitializeComponent();
            LoadInitialSettings();
        }

        private void LoadInitialSettings()
        {
            Version = Assembly.GetExecutingAssembly().GetName().Version?.ToString();
            MainWindow.Instance.navView.PaneDisplayMode = Settings.PaneDisplayMode;
            currentVersion.Text = Lang.ResourceManager.GetString("CurrentVersion").Format(Version);

            /*txtLocation.Text = Lang.ResourceManager.GetString("CurrentLocation").Format(Settings.StoreLocation);*/
        }

        private void CheckUpdate_Click(object sender, RoutedEventArgs e)
        {
            //async 更新使用异步更新
            
            Growl.InfoGlobal(Lang.ResourceManager.GetString("LatestVersion"));
        }
    }
}
