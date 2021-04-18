using Newtonsoft.Json;
using System;
using System.Collections.Generic;
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

        HttpHelper httpHelper = new HttpHelper();
        public async Task<WeatherModel> LoadWeather(string location, string key)
        {
            Dictionary<string, string> dic = new();
            dic.Add("location", location);
            dic.Add("key", key);
            WeatherModel nowWeather = null;
            try
            {
                var result = await httpHelper.GetAsync(Consts.NowWeather, dic);
                nowWeather = JsonConvert.DeserializeObject<WeatherModel>(result);
            }
            catch(Exception e)
            {
                var result = await httpHelper.GetAsync(Consts.NowWeather, dic);
                nowWeather = JsonConvert.DeserializeObject<WeatherModel>(result);
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
            Dictionary<string, string> dic = new();
            dic.Add("location", location);
            dic.Add("key", key);
            WeatherModel WeatherAqi = null;
            try
            {
                WeatherAqi = JsonConvert.DeserializeObject<WeatherModel>(HttpHelper.Get(Consts.Air, dic));
            }
            catch
            {
                WeatherAqi = JsonConvert.DeserializeObject<WeatherModel>(HttpHelper.Get(Consts.Air, dic));
            }

            return WeatherAqi;
        }

        public string CityQuery(string city, string key)
        {
            Dictionary<string, string> dic = new();
            dic.Add("location", city);
            dic.Add("key", key);
            CityModel cityModel = null;
            string cityInfo = "";
            try
            {
                cityModel = JsonConvert.DeserializeObject<CityModel>(HttpHelper.Get(Consts.CityInfof, dic));
            }
            catch
            {
                cityModel = JsonConvert.DeserializeObject<CityModel>(HttpHelper.Get(Consts.CityInfof, dic));
            }
            #region 判断是否城市ID
            if (cityModel.code == "200" && Regex.IsMatch(city, @"^[+-]?\d*[.]?\d*$"))
            {
                cityInfo = cityModel.location[0].name;
                return cityInfo;
            }
            if (cityModel.code == "200" && Regex.IsMatch(city, @"^[\u4e00-\u9fa5A-Za-z]+$") && cityModel.location[0].country.Equals("中国"))
            {
                cityInfo = cityModel.location[0].id;
                return cityInfo;
            }
            #endregion
            if (cityModel.code == "404")
            {
                cityInfo = "Error";
                return cityInfo;
            }

            return cityInfo;
        }

        public WeatherModel LifeIndex(string location, string key, string type)
        {
            Dictionary<string, string> dic = new();
            dic.Add("type", type);
            dic.Add("location", location);
            dic.Add("key", key);
            WeatherModel weather = null;
            try 
            {
                weather = JsonConvert.DeserializeObject<WeatherModel>(HttpHelper.Get(Consts.TheLife, dic));
            }
            catch
            {
                weather = JsonConvert.DeserializeObject<WeatherModel>(HttpHelper.Get(Consts.TheLife, dic));
            }
            if (weather.code == "200")
            {
                return weather;
            }
            return null;
        }
    }
}
