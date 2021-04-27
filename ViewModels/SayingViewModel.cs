using log4net;
using Newtonsoft.Json;
using Prism.Mvvm;
using System;
using WallPoster.Assets;
using WallPoster.Models;
using WallPoster.Models.Service;

namespace WallPoster.ViewModels
{
    public class SayingViewModel : BindableBase
    {
        private static ILog log = LogManager.GetLogger("SayingViewModel");

        public static string LoadSaying()
        {
            SayingModel sayingModel = null;
            try
            {
                sayingModel = JsonConvert.DeserializeObject<SayingModel>(HttpHelper.Get(Consts.SayingUrl));
            }
            catch (Exception e)
            {
                log.Debug(e);
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
