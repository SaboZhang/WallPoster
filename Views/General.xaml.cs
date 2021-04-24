using System.Windows;
using ModernWpf.Controls;
using System.Windows.Controls;
using static WallPoster.Assets.Helper;
using WallPoster.Assets;
using WallPoster.Helper;
using System.Data;
using WallPoster.Models;
using WallPoster.ViewModels;
using System.Collections.Generic;

namespace WallPoster.Views
{
    /// <summary>
    /// Settings.xaml 的交互逻辑
    /// </summary>
    public partial class General : UserControl
    {

        public General()
        {
            InitializeComponent();
            LoadInitialSettings();
        }

        private void LoadInitialSettings()
        {
            cmbPaneDisplay.SelectedItem = Settings.PaneDisplayMode;
            MainWindow.Instance.navView.PaneDisplayMode = Settings.PaneDisplayMode;
            string key = Settings.AppSecret;
            AppSecretText.Text = Consts.WeatherKey.Equals(key) ? null : key;
            
        }
        /// <summary>
        /// 设置导航栏显示方式
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmbPaneDisplay_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var mode = (NavigationViewPaneDisplayMode)cmbPaneDisplay.SelectedItem;
            if (mode != Settings.PaneDisplayMode)
            {
                Settings.PaneDisplayMode = mode;
                MainWindow.Instance.navView.PaneDisplayMode = mode;
            }
        }
        /// <summary>
        /// 设置AppSecret
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ChangeAppSecret(object sender, RoutedEventArgs e)
        {
            string key = AppSecretText.Text;
            if (key != Settings.AppSecret && key != "")
            {
                Settings.AppSecret = key;
            }
            
        }

        private void ChangeDefaultCity(object sender, RoutedEventArgs e)
        {
            
        }

        private void MediaStoreLocation_Click(object sender, RoutedEventArgs e)
        {
            using (var dialog = new System.Windows.Forms.FolderBrowserDialog())
            {
                if (dialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    string path = dialog.SelectedPath;
                    int pathCount = Settings.MovieLocation.Count;
                    Settings.MovieLocation.Add(path);
                    Settings.MovieLocation = new List<string>(Settings.MovieLocation);
                    using (var helper = new SQLiteHelper())
                    {
                        var model = new PathModel()
                        {
                            MoviePath = path,
                            TVPath = ""
                        };
                        helper.Paths.Add(model);
                        helper.SaveChanges();
                    }
                }
            }
            
        }
    }
}
