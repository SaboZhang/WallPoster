using HandyControl.Controls;
using HandyControl.Data;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Regions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media.Imaging;
using WallPoster.Assets;
using WallPoster.Assets.Strings;
using WallPoster.Helper;
using WallPoster.Models;
using static WallPoster.Assets.Helper;
using MessageBox = HandyControl.Controls.MessageBox;

namespace WallPoster.ViewModels
{
    public class HomeViewModel : BindableBase
    {
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

        public string Saying
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

        private string _searchText;

        public string SearchText
        {
            get => _searchText;
            set => SetProperty(ref _searchText, value);
        }

        private string _status;

        public string Status
        {
            get => _status;
            set => SetProperty(ref _status, value);
        }

        private string _cityCode;

        public string CityCode
        {
            get => _cityCode;
            set => SetProperty(ref _cityCode, value);
        }

        private List<string> _dataList;

        public List<string> DataList
        {
            get => _dataList;
            set => SetProperty(ref _dataList, value);
        }

        private string _provinces;

        public string Provinces
        {
            get => _provinces;
            set => SetProperty(ref _provinces, value);
        }

        private DelegateCommand<FunctionEventArgs<string>> _onSearchStartedCommand;
        public DelegateCommand<FunctionEventArgs<string>> OnSearchStartedCommand =>
                _onSearchStartedCommand ?? (_onSearchStartedCommand = new DelegateCommand<FunctionEventArgs<string>>(OnSearchStarted));

        private DelegateCommand<object> _provincesCommand;
        public DelegateCommand<object> ProvincesCommand =>
                _provincesCommand ?? (_provincesCommand = new DelegateCommand<object>(ProvincesInfo));


        public HomeViewModel()
        {
            Status = "Visible";
            InitProvincesData();
            LoadingCityInfo(Settings.Location, Settings.AppSecret, Consts.DefaultCity);
            LoadingWeatherCard(Settings.Location, Settings.AppSecret);
            LoadingAirQuality(Settings.Location, Settings.AppSecret);
            LoadingSaying();
            LoadingWeeklyWeather();
            Status = "Hidden";
        }

        WeatherViewModel weatherModel = new WeatherViewModel();

        /// <summary>
        /// 加载实时天气
        /// </summary>
        /// <param name="location"></param>
        /// <param name="key"></param>
        private void LoadingWeatherCard(string location, string key)
        {
            Task.Run(() =>
            {
                var nowWeather = weatherModel.LoadWeather(location, key);
                return nowWeather;
            }).ContinueWith(weather =>
            {
                Application.Current.Dispatcher.BeginInvoke(new Action(() =>
                {
                    if (weather.Result == null)
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
                    UpdateTime = "更新时间:" + weatherResult.updateTime.ToString("yyyy-MM-dd HH':'mm");
                    var icon = new BitmapImage(
                        new Uri(new StringBuilder("pack://application:,,,/WallPoster;component/Resources/Weather/color-128/")
                        .Append(weatherResult.now.icon).Append(".png").ToString()));
                    WeatherIcon = icon;
                    Temp = weatherResult.now.temp + "°";
                    Clime = weatherResult.now.text;
                    WindDir = weatherResult.now.windDir;
                    WindScale = weatherResult.now.windScale + "级";
                    Humidity = weatherResult.now.humidity + "%\n相对湿度";
                    Vis = weatherResult.now.vis + "KM\n能见度";
                    Pressure = weatherResult.now.pressure + "hpa\n大气压";
                }));
            });
        }

        /// <summary>
        /// 加载空气质量
        /// </summary>
        /// <param name="location"></param>
        /// <param name="key"></param>
        private void LoadingAirQuality(string location, string key)
        {
            Task.Run(() =>
            {
                var nowAqi = weatherModel.LoadWeatherAqi(location, key);
                return nowAqi;
            }).ContinueWith(aqi =>
            {
                Application.Current.Dispatcher.BeginInvoke(new Action(() =>
                {
                    if (aqi.Result == null)
                    {
                        MessageBox.Error(Lang.ResourceManager.GetString("WeatherAqiNullError"));
                        return;
                    }
                    var aqiResult = aqi.Result;
                    if (aqiResult.code != "200")
                    {
                        AirQuality = aqiResult.code == "403"
                        ? Lang.ResourceManager.GetString("NoSupport")
                        : Lang.ResourceManager.GetString("WeatherAqiNullError");
                        return;
                    }
                    AirQuality = "AQI" + aqiResult.now.category;
                    string[] colors = { "#95B359", "#D3CF63", "#E0991D", "#D96161", "#A257D0", "#D94371" };
                    int color = int.Parse(aqiResult.now.level) - 1;
                    AqiBakcground = colors[color];
                }));
            });
        }

        private void LoadingCityInfo(string location, string key, string adm)
        {
            Task.Run(() =>
            {
                var city = weatherModel.CityQuery(location, key, adm);
                return city;
            }).ContinueWith(city =>
            {
                Application.Current.Dispatcher.BeginInvoke(new Action(() =>
                {
                    if (city.Result == null)
                    {
                        MessageBox.Error(Lang.ResourceManager.GetString("WeatherCityError"));
                        return;
                    }
                    var cityResult = city.Result;
                    if (cityResult.code != "200")
                    {
                        Growl.WarningGlobal(Lang.ResourceManager.GetString("CityError") + $"  error code:{cityResult.code}");
                        System.Diagnostics.Process.Start("explorer.exe", Consts.StatusCode);
                        return;
                    }
                    City = cityResult.location[0].name.ToString();
                }));
            });
        }

        private void OnSearchStarted(FunctionEventArgs<string> e)
        {
            if (string.IsNullOrEmpty(SearchText)) return;
            if (string.IsNullOrEmpty(Provinces))
            {
                Growl.InfoGlobal(Lang.ResourceManager.GetString("NeedChoice"));
                return;
            }
            Provinces = Provinces == "海外国家" ? null : Provinces;
            var cityModel = weatherModel.CityQuery(SearchText, Settings.AppSecret, Provinces);
            if (cityModel != null && cityModel.code == "200")
            {
                string cityCode = cityModel.location[0].id;
                City = cityModel.location[0].country.Equals("中国") 
                    ? cityModel.location[0].name
                    : cityModel.location[0].country + cityModel.location[0].name;
                LoadingWeatherCard(cityCode, Settings.AppSecret);
                LoadingAirQuality(cityCode, Settings.AppSecret);
                return;
            }
            Growl.WarningGlobal(Lang.ResourceManager.GetString("CityError") + $"  error code:{cityModel.code}");
        }

        private void ProvincesInfo(object provinces)
        {
            // 获取所选省份
            if (string.IsNullOrEmpty(provinces.ToString())) return;
            Provinces = provinces.ToString();
        }

        private void InitProvincesData()
        {
            var helper = SQLiteHelper<AreaModel>.GetInstance();
            var lists = helper.Areas.OrderBy(p => p.LocationId).Select(p => p.Adm1).ToList().Distinct();
            var ls = new List<string>();
            ls.Add(Lang.ResourceManager.GetString("PleaseSelect"));
            foreach (var p in lists)
            {
                string name = p;
                ls.Add(name);
            }
            ls.Add(Lang.ResourceManager.GetString("Overseas"));
            DataList = ls;
        }

        private void LoadingSaying()
        {
            string say = SayingViewModel.LoadSaying();
            Saying = say;
        }

        private void LoadingWeeklyWeather()
        {
            weatherModel.GetWeeklyWeather(Settings.Location, Settings.AppSecret);
            
        }


    }
}
