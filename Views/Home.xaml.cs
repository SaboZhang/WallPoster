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

namespace WallPoster.Views
{
    /// <summary>
    /// Home.xaml 的交互逻辑 weather.now.temp;
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
            
            WeatherModel weather  = weatherViewModel.LoadWeather(location, key);
            if(weather.code == "200")
            {
                UpdateTime.Text = "更新时间：" + weather.updateTime.ToString("yyyy-MM-dd HH':'mm':'ss");
                City.Text = weatherViewModel.CityQuery(location, key);
                BitmapImage icon = new BitmapImage(new Uri(new StringBuilder("pack://application:,,,/WallPoster;component/Resources/Weather/color-64/").Append(weather.now.icon).Append(".png").ToString()));
                WeatherIcon.Source = icon;
                Temp.Text = weather.now.temp + "°";
                Clime.Text = weather.now.text;
                WeatherAqi(weatherViewModel, location);
                string windDir = weather.now.windDir;
                string windScale = weather.now.windScale;
                string windSpeed = weather.now.windSpeed;
                string humidity = weather.now.humidity;
                string vis = weather.now.vis;
                des.Text = "风向：" + windDir + "\t风力等级：" + windScale + "\t风速：" + windSpeed + "\t湿度:" + humidity + "%\t能见度：" + vis;
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
                        binding.Source = "#FFD700";
                        break;
                    case "3":
                        binding.Source = "#FF8C00";
                        break;
                    case "4":
                        binding.Source = "#FF0000";
                        break;
                    case "5":
                        binding.Source = "#59181A";
                        break;
                    case "6":
                        binding.Source = "#4B0204";
                        break;
                }
                BindingOperations.SetBinding(AqiBorder, Border.BackgroundProperty, binding);
            }

        }

        private void WeatherQuery(object sender, HandyControl.Data.FunctionEventArgs<string> e)
        {
            string city = SearchWeather.Text;
            if (Regex.IsMatch(city, @"^[\u4e00-\u9fa5A-Za-z]+$"))
            {
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
                return;
            }
            MessageBox.Warning(Lang.ResourceManager.GetString("QueryWarning"));
        }
    }
}
