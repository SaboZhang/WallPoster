using System.Windows.Controls;
using static WallPoster.Assets.Helper;

namespace WallPoster.Views
{
    /// <summary>
    /// Home.xaml 的交互逻辑
    /// </summary>
    public partial class Home : UserControl
    {
        public Home()
        {
            InitializeComponent();
            LoadInitialSettings();
        }

        private void LoadInitialSettings()
        {
            string key = Settings.AppSecret;
        }
    }
}
