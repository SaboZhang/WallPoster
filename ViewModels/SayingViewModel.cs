using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WallPoster.Models;
using WallPoster.Models.Service;
using WallPoster.Assets;

namespace WallPoster.ViewModels
{
    public class SayingViewModel
    {
        public static string LoadSaying()
        {
            SayingModel sayingModel = null;
            try
            {
                sayingModel = JsonConvert.DeserializeObject<SayingModel>(HttpService.Get(Consts.SayingUrl));
            }
            catch
            {
                sayingModel = JsonConvert.DeserializeObject<SayingModel>(HttpService.Get(Consts.SayingUrl));
            }
            if (sayingModel.code == "200")
            {
                string saying = sayingModel.data.content;

                return saying;
            }
            return "获取名言失败";
        }
    }
}
