using log4net;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using WallPoster.Assets;
using WallPoster.Helper;
using WallPoster.Models;
using WallPoster.Models.Service;

namespace WallPoster.ViewModels
{
    /// <summary>
    /// 处理解析完成的数据
    /// </summary>
    public class WeatherViewModel
    {
        private static ILog log = LogManager.GetLogger("WeatherViewModel");

        HttpHelper httpHelper = new();

        SQLiteHelper<AreaModel> helper = SQLiteHelper<AreaModel>.GetInstance();

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
            catch (Exception e)
            {
                log.Debug("和风天气连接异常:" + e.Message);
                return nowWeather;
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
            WeatherModel weatherAqi = null;
            try
            {
                weatherAqi = JsonConvert.DeserializeObject<WeatherModel>(HttpHelper.Get(Consts.Air, dic));
            }
            catch (Exception e)
            {
                log.Debug("和风天气获取AQI异常:" + e.Message);
                return weatherAqi;
            }

            return weatherAqi;
        }

        public CityModel CityQuery(string city, string key, string? adm = null)
        {
            Dictionary<string, string> dic = new();
            dic.Add("location", city);
            dic.Add("key", key);
            if (adm != null)
            {
                dic.Add("adm", adm);
            }
            CityModel cityModel = null;
            try
            {
                cityModel = JsonConvert.DeserializeObject<CityModel>(HttpHelper.Get(Consts.CityInfof, dic));
            }
            catch (Exception e)
            {
                log.Debug("和风天气获取城市列表异常:" + e.Message);
                return cityModel;
            }
            if (cityModel.code == "200")
            {
                if (cityModel.location.Count > 1)
                {
                    return cityModel;
                }
                string cityName = cityModel.location[0].name;
                string adm1 = cityModel.location[0].adm1;
                try
                {
                    var citys = helper.GetFristDefault<AreaModel>(i => i.ProvinceName == adm1 && i.CountyName.Contains(cityName));
                    cityModel.location[0].name = citys.CountyName;
                }
                catch (Exception e)
                {
                    log.Debug(e.Message);
                    cityModel.location[0].name = cityModel.location[0].country.Equals("中国")
                        && cityModel.location[0].name == cityModel.location[0].adm2
                        ? cityName + "市" : cityModel.location[0].country + cityName;
                    return cityModel;
                }
            }
            return cityModel;
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
            catch (Exception e)
            {
                log.Debug("和风天气获取生活指数异常:" + e.Message);
            }
            if (weather.code == "200")
            {
                return weather;
            }
            return null;
        }
    }
}
