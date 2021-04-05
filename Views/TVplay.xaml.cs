using System.Collections.Generic;
using System.Windows.Controls;
using HandyControl.Controls;
using WallPoster.Models.Service;

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
            Dictionary<string, string> dictionary = new Dictionary<string, string>();
            dictionary.Add("location", "天津");
            dictionary.Add("key", "");
            var result = HttpService.Post("https://geoapi.qweather.com/v2/city/lookup", dictionary);
            
            MessageBox.Show(result);
        }
    }
}
