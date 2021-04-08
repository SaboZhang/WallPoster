using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WallPoster.Models;
using WallPoster.Models.Service;

namespace WallPoster.ViewModels
{
    /// <summary>
    /// 处理解析完成的数据
    /// </summary>
    public class WeatherViewModel
    {
        public void LoadWeather()
        {
            string nowURL = "https://devapi.qweather.com/v7/weather/now";
            Dictionary<string, string> dic = new Dictionary<string, string>();
            dic.Add("location", "101030500");
            dic.Add("key", "");
            WeatherModel nowWeather = null;
            try
            {
                nowWeather = JsonConvert.DeserializeObject<WeatherModel>(HttpService.Get(nowURL, dic));
            }
            catch(Exception e)
            {
                nowWeather = JsonConvert.DeserializeObject<WeatherModel>(HttpService.Get(nowURL, dic));
            }
            if (nowWeather.code == "200")
            {
                string icon = nowWeather.now.icon;
            }

        }
    }
}
