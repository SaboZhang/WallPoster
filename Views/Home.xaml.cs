using System.Windows.Controls;
using WallPoster.ViewModels;
using WallPoster.Models;
using static WallPoster.Assets.Helper;
using System.Windows.Media.Imaging;
using System;
using System.Text;
using System.Windows.Data;
using WallPoster.Assets.Strings;
using HandyControl.Controls;
using WallPoster.Assets;
using System.Text.RegularExpressions;
using System.Collections.ObjectModel;

namespace WallPoster.Views
{
    /// <summary>
    /// Home.xaml 的交互逻辑
    /// </summary>
    public partial class Home : UserControl
    {
        public string key;
        public string location;
        WeatherViewModel weatherViewModel = new WeatherViewModel();
        

        public Home()
        {
            InitializeComponent();
            LoadInitialSettings();
            WeatherCard(weatherViewModel,location);
        }

        private void LoadInitialSettings()
        {
            key = Settings.AppSecret;
            location = Settings.Location;
        }

        private void WeatherCard(WeatherViewModel weatherViewModel, string location)
        {
            
            WeatherModel weather = weatherViewModel.LoadWeather(location, key);
            WeatherModel life = weatherViewModel.LifeIndex(location, key, "8");
            if (weather.code == "200")
            {
                UpdateTime.Text = "更新时间：" + weather.updateTime.ToString("yyyy-MM-dd HH':'mm");
                City.Text = weatherViewModel.CityQuery(location, key);
                BitmapImage icon = new BitmapImage(new Uri(new StringBuilder("pack://application:,,,/WallPoster;component/Resources/Weather/color-128/").Append(weather.now.icon).Append(".png").ToString()));
                WeatherIcon.Source = icon;
                Temp.Text = weather.now.temp + "°";
                Clime.Text = weather.now.text;
                WeatherAqi(weatherViewModel, location);
                WindDir.Text = weather.now.windDir;
                WindScale.Text = weather.now.windScale + "级";
                Humidity.Text = weather.now.humidity + "%\n相对湿度";
                vis.Text = weather.now.vis + "KM\n能见度";
                Pressure.Text = weather.now.pressure + "hpa\n大气压";
                des.Text = SayingViewModel.LoadSaying();

                return;
            }

            Dispatcher.BeginInvoke(new Action(() => MessageBox.Error(Lang.ResourceManager.GetString("WeatherError"), $"error code {weather.code}")));
            System.Diagnostics.Process.Start("explorer.exe", Consts.StatusCode);

        }

        private void WeatherAqi(WeatherViewModel weatherViewModel,string location)
        {
            WeatherModel weatherAqi = weatherViewModel.LoadWeatherAqi(location, key);
            if (weatherAqi.code == "200")
            {
                Aqi.Text = "AQI" + weatherAqi.now.category;
                Binding binding = new Binding();
                switch (weatherAqi.now.level)
                {
                    case "1":
                        binding.Source = "#95B359";
                        break;
                    case "2":
                        binding.Source = "#D3CF63";
                        break;
                    case "3":
                        binding.Source = "#E0991D";
                        break;
                    case "4":
                        binding.Source = "#D96161";
                        break;
                    case "5":
                        binding.Source = "#A257D0";
                        break;
                    case "6":
                        binding.Source = "#D94371";
                        break;
                }
                BindingOperations.SetBinding(AqiBorder, Border.BackgroundProperty, binding);
            }

        }

        private void WeatherQuery(object sender, HandyControl.Data.FunctionEventArgs<string> e)
        {
            string city = SearchWeather.Text;
            string loaction = weatherViewModel.CityQuery(city, key);
            if (Regex.IsMatch(loaction, @"^[+-]?\d*[.]?\d*$") && loaction != "" && loaction != "Error")
            {
                WeatherCard(weatherViewModel, loaction);
                return;
            }
            if (loaction == "Error")
            {
                MessageBox.Warning(Lang.ResourceManager.GetString("QueryWarning"));
                return;
            }
            MessageBox.Warning(Lang.ResourceManager.GetString("Nonsupport"));

        }
    }
}
