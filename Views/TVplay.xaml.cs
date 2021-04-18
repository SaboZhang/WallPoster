using System.Collections.ObjectModel;
using System.Windows.Controls;
using HandyControl.Controls;
using WallPoster.Models;
using WallPoster.ViewModels;
using Prism.Mvvm;

namespace WallPoster.Views
{
    /// <summary>
    /// TVplay.xaml 的交互逻辑
    /// </summary>
    public partial class TVplay : UserControl
    {
        SayingViewModel say = new SayingViewModel();

        public TVplay()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, System.Windows.RoutedEventArgs e)
        {

            MessageBox.Show("正在努力conding");
        }

        
    }
}
