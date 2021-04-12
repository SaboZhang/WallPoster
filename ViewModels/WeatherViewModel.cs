using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using WallPoster.Assets;
using WallPoster.Models;
using WallPoster.Models.Service;

namespace WallPoster.ViewModels
{
    /// <summary>
    /// 处理解析完成的数据
    /// </summary>
    public class WeatherViewModel
    {
        
        
        public WeatherModel LoadWeather(string location, string key)
        {
            Dictionary<string, string> dic = new Dictionary<string, string>();
            dic.Add("location", location);
            dic.Add("key", key);
            WeatherModel nowWeather = null;
            try
            {
                nowWeather = JsonConvert.DeserializeObject<WeatherModel>(HttpService.Get(Consts.NowWeather, dic));
            }
            catch(Exception e)
            {
                nowWeather = JsonConvert.DeserializeObject<WeatherModel>(HttpService.Get(Consts.NowWeather, dic));
            }
            if (nowWeather.code == "200")
            {
                string icon = nowWeather.now.icon;
            }

            return nowWeather;

        }

        public static string LoadBackground()
        {
            
            return null;
        }

        public WeatherModel LoadWeatherAqi(string location, string key)
        {
            Dictionary<string, string> dic = new Dictionary<string, string>();
            dic.Add("location", location);
            dic.Add("key", key);
            WeatherModel WeatherAqi = null;
            try
            {
                WeatherAqi = JsonConvert.DeserializeObject<WeatherModel>(HttpService.Get(Consts.Air, dic));
            }
            catch
            {
                WeatherAqi = JsonConvert.DeserializeObject<WeatherModel>(HttpService.Get(Consts.Air, dic));
            }

            return WeatherAqi;
        }

        public string CityQuery(string city, string key)
        {
            Dictionary<string, string> dic = new Dictionary<string, string>();
            dic.Add("location", city);
            dic.Add("key", key);
            CityModel cityModel = null;
            string cityInfo = "";
            try
            {
                cityModel = JsonConvert.DeserializeObject<CityModel>(HttpService.Get(Consts.CityInfof, dic));
            }
            catch
            {
                cityModel = JsonConvert.DeserializeObject<CityModel>(HttpService.Get(Consts.CityInfof, dic));
            }
            if (cityModel.code == "200" && Regex.IsMatch(city, @"^[+-]?\d*[.]?\d*$"))
            {
                cityInfo = cityModel.location[0].name;
                return cityInfo;
            }
            if (cityModel.code == "200" && Regex.IsMatch(city, @"^[\u4e00-\u9fa5A-Za-z]+$"))
            {
                cityInfo = cityModel.location[0].id;
                return cityInfo;
            }

            return cityInfo;
        }
    }
}
