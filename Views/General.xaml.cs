﻿using System.Windows;
using HandyControl.Controls;
using ModernWpf.Controls;
using System.Windows.Controls;
using static WallPoster.Assets.Helper;
using HandyControl.Tools;

namespace WallPoster.Views
{
    /// <summary>
    /// Settings.xaml 的交互逻辑
    /// </summary>
    public partial class General : UserControl
    {
        private readonly string weatherKEY = "e4128d214e47471ea020c5630ebce2d0";
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
            AppSecretText.Text = weatherKEY.Equals(key) ? null : key;
            

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
            if (key != Settings.AppSecret)
            {
                Settings.AppSecret = key;
            }
            
        }

        private void ChangeDefaultCity(object sender, RoutedEventArgs e)
        {
            
        }
    }
}
