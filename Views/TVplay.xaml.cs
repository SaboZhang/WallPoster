using System.Windows.Controls;
using HandyControl.Controls;
using WallPoster.ViewModels;

namespace WallPoster.Views
{
    /// <summary>
    /// TVplay.xaml 的交互逻辑
    /// </summary>
    public partial class TVplay : UserControl
    {
        public TVplay()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            WeatherViewModel weather = new WeatherViewModel();
            weather.LoadWeather();
            MessageBox.Show("测试");
        }
    }
}
