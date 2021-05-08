using HandyControl.Controls;
using Prism.Mvvm;
using Prism.Regions;
using System;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media.Imaging;
using WallPoster.Assets;
using WallPoster.Assets.Strings;
using WallPoster.Models.Service;
using MessageBox = HandyControl.Controls.MessageBox;
using static WallPoster.Assets.Helper;

namespace WallPoster.ViewModels
{
    public class HomeViewModel : BindableBase
    {
        private readonly IRegionManager _regionManager;

        #region 天气字段
        private string _updateTime;
        private string _city;
        private BitmapImage _weatherIcon;
        private string _temp;
        private string _clime;
        private string _weatherAqi;
        private string _windDir;
        private string _windScale;
        private string _humidty;
        private string _vis;
        private string _pressure;
        private string _saying;
        private string _aqiBackground;
        private string _airQuality;

        public string UpdateTime
        {
            get => _updateTime;
            set => SetProperty(ref _updateTime, value);
        }

        public string City
        {
            get => _city;
            set => SetProperty(ref _city, value);
        }

        public BitmapImage WeatherIcon
        {
            get => _weatherIcon;
            set => SetProperty(ref _weatherIcon, value);
        }

        public string Temp
        {
            get => _temp;
            set => SetProperty(ref _temp, value);
        }

        public string Clime
        {
            get => _clime;
            set => SetProperty(ref _clime, value);
        }

        public string WeatherAqi
        {
            get => _weatherAqi;
            set => SetProperty(ref _weatherAqi, value);
        }

        public string WindDir
        {
            get => _windDir;
            set => SetProperty(ref _windDir, value);
        }

        public string WindScale
        {
            get => _windScale;
            set => SetProperty(ref _windScale, value);
        }

        public string Humidity
        {
            get => _humidty;
            set => SetProperty(ref _humidty, value);
        }

        public string Vis
        {
            get => _vis;
            set => SetProperty(ref _vis, value);
        }

        public string Pressure
        {
            get => _pressure;
            set => SetProperty(ref _pressure, value);
        }

        public string Syaing
        {
            get => _saying;
            set => SetProperty(ref _saying, value);
        }

        public string AqiBakcground
        {
            get => _aqiBackground;
            set => SetProperty(ref _aqiBackground, value);
        }

        public string AirQuality
        {
            get => _airQuality;
            set => SetProperty(ref _airQuality, value);
        }
        #endregion

        public HomeViewModel(IRegionManager regionManager) : this()
        {
            _regionManager = regionManager;
        }

        public HomeViewModel()
        {
            LoadWeatherCard(Settings.Location, Settings.AppSecret);
        }

        WeatherViewModel weatherModel = new WeatherViewModel();

        /// <summary>
        /// 加载实时天气
        /// </summary>
        /// <param name="location"></param>
        /// <param name="key"></param>
        private void LoadWeatherCard(string location, string key)
        {
            Task.Run(() =>
            {
                var nowWeather = weatherModel.LoadWeather(location, key);
                return nowWeather;
            }).ContinueWith(weather =>
            {
                Application.Current.Dispatcher.BeginInvoke(new Action(() =>
                {
                    if (weather == null)
                    {
                        MessageBox.Error(Lang.ResourceManager.GetString("WeatherNullError"));
                        return;
                    }
                    var weatherResult = weather.Result;
                    if (weatherResult.code != "200")
                    {
                        MessageBox.Error(Lang.ResourceManager.GetString("WeatherError"), $"error code{weatherResult.code}");
                        System.Diagnostics.Process.Start("explorer.exe", Consts.StatusCode);
                        return;
                    }
                    UpdateTime = weatherResult.updateTime.ToString("yyyy-MM-dd HH':'mm");
                    var icon = new BitmapImage(
                        new Uri(new StringBuilder("pack://application:,,,/WallPoster;component/Resources/Weather/color-128/")
                        .Append(weatherResult.now.icon).Append(".png").ToString()));
                    WeatherIcon = icon;
                    Temp = weatherResult.now.temp + "°";
                    Clime = weatherResult.now.text;
                    WindDir = weatherResult.now.windDir;
                    WindScale = weatherResult.now.windDir + "级";
                    Humidity = weatherResult.now.humidity + "%\n相对湿度";
                    Vis = weatherResult.now.vis + "KM\n能见度";
                    Pressure = weatherResult.now.pressure + "hpa\n大气压";
                }));
            });
        }

        private void LoadAirQuality(string location, string key)
        {
            Task.Run(() =>
            {
                var nowAqi = weatherModel.LoadWeatherAqi(location, key);
                return nowAqi;
            }).ContinueWith(aqi =>
            {
                Application.Current.Dispatcher.BeginInvoke(new Action(() =>
                {
                    var aqiResult = aqi.Result;
                    AirQuality = aqiResult.now.category;

                }));
            });
        }
    }
}
