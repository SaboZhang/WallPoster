using ModernWpf.Controls;
using System.Windows.Controls;
using static WallPoster.Assets.Helper;

namespace WallPoster.Views
{
    /// <summary>
    /// Settings.xaml 的交互逻辑
    /// </summary>
    public partial class SystemSettings : UserControl
    {
        public SystemSettings()
        {
            InitializeComponent();
            LoadInitialSettings();
        }

        private void LoadInitialSettings()
        {
            cmbPaneDisplay.SelectedItem = Settings.PaneDisplayMode;
            MainWindow.Instance.navView.PaneDisplayMode = Settings.PaneDisplayMode;

        }

        private void cmbPaneDisplay_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var mode = (NavigationViewPaneDisplayMode)cmbPaneDisplay.SelectedItem;
            if (mode != Settings.PaneDisplayMode)
            {
                Settings.PaneDisplayMode = mode;
                MainWindow.Instance.navView.PaneDisplayMode = mode;
            }
        }
    }
}
