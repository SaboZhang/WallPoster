﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WallPoster.Models;
using WallPoster.Models.Service;
using WallPoster.Assets;
using Prism.Mvvm;
using System.Collections.ObjectModel;

namespace WallPoster.ViewModels
{
    public class SayingViewModel : BindableBase
    {
        public static string LoadSaying()
        {
            SayingModel sayingModel = null;
            try
            {
                sayingModel = JsonConvert.DeserializeObject<SayingModel>(HttpHelper.Get(Consts.SayingUrl));
            }
            catch
            {
                sayingModel = JsonConvert.DeserializeObject<SayingModel>(HttpHelper.Get(Consts.SayingUrl));
            }
            if (sayingModel.code == "200")
            {
                string saying = sayingModel.data.content;

                return saying;
            }
            return "获取名言失败";
        }

        

        private string _content;
        public string content
        {
            get { return _content; }
            set => SetProperty(ref _content, value);
        }

        public SayingViewModel()
        {
            content = "123456";
        }

    }
}
