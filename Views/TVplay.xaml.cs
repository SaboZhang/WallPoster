using System.Collections.ObjectModel;
using System.Windows.Controls;
using HandyControl.Controls;
using WallPoster.Models;
using WallPoster.ViewModels;

namespace WallPoster.Views
{
    /// <summary>
    /// TVplay.xaml 的交互逻辑
    /// </summary>
    public partial class TVplay : UserControl
    {
        ObservableCollection<Data> Sayings = new ObservableCollection<Data>();
        public TVplay()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            

            MessageBox.Show("正在努力conding");
        }

        private void ShowInfo()
        {
            Sayings?.Clear();
            var item = new Data
            {
                content = "78"
            };
            /*string saying = SayingViewModel.LoadSaying();*/
            Sayings.Add(item);
            saying.ItemsSource = Sayings;
        }
    }
}
