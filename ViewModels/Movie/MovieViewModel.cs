using HandyControl.Controls;
using HandyControl.Data;
using HandyControl.Tools.Command;
using log4net;
using Prism.Commands;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using WallPoster.Helper;
using WallPoster.Models;
using WallPoster.Views;

namespace WallPoster.ViewModels
{
    public class MovieViewModel : ViewModelBase<MoviesModel>
    {

        public DelegateCommand<object> ClickCover { get; private set; }
        public DelegateCommand<object> ClickInfo { get; private set; }
        private static ILog log = LogManager.GetLogger("MovieViewModel");

        SQLiteHelper<FilesModel> helper = SQLiteHelper<FilesModel>.GetInstance();

        public MovieViewModel()
        {
            _ = GetMoviesAsync();
            ClickCover = new DelegateCommand<object>(ShowClick);
            ClickInfo = new DelegateCommand<object>(Info_click);
        }

        private void ShowClick(object parm)
        {
            MessageBox.Show($"执行命令{parm}");
        }
        

        public async Task GetMoviesAsync()
        {
            var models = await Task.Factory.StartNew(() =>
            {
                int total = helper.Files.Where(m => m.Category == "0").Count();
                List<MoviesModel> models = new List<MoviesModel>();
                MaxPageCount = (int)total / 50;
                PageIndex = (int)1;
                List<FilesModel> filesModels = helper.LoadPageItems<FilesModel, DateTime>(50, PageIndex, out total, c => c.Category == "0", u => u.AddTime, false).ToList();
                foreach (var path in filesModels)
                {
                    /*BitmapImage image = new BitmapImage();
                    image.BeginInit();  var orderlist = WeichatManageApiHelper
                    .LoadPageItems<View_Mkxx_OrderStatistics, DateTime>(pageSize, pageIndex, out total, c => c.OrderDate <= end && c.OrderDate >= start, u => u.OrderDate, false).ToList();
                    image.CacheOption = BitmapCacheOption.OnLoad;*/
                    string url = path.StoreSite + @"\" + path.FileName + ".jpg";
                    string loca = File.Exists(url) ? url : path.StoreSite + @"\poster.jpg";
                    /*image.UriSource = new Uri(loca);*/
                    if (!File.Exists(loca))
                    {
                        continue;
                    }
                    MoviesModel movies = new MoviesModel
                    {
                        Content = loca,
                        Header = path.Caption,
                        Footer = path.FileName
                    };
                    /*image.EndInit();*/

                    models.Add(movies);
                }
                return models;

            });
            DataList = models;
        }

        private void Info_click(object p)
        {
            MainWindow.Instance.NavigateTo(typeof(MovieInfo), p);
            /*MessageBox.Show($"info{p}");*/
        }

        /// <summary>
        ///     页码改变命令
        /// </summary>
        public RelayCommand<FunctionEventArgs<int>> PageUpdatedCmd =>
            new Lazy<RelayCommand<FunctionEventArgs<int>>>(() =>
                new RelayCommand<FunctionEventArgs<int>>(PageUpdated)).Value;

        /// <summary>
        ///     页码改变
        /// </summary>
        private void PageUpdated(FunctionEventArgs<int> info)
        {
            var models = new List<MoviesModel>();
            int total = helper.Files.Where(m => m.Category == "0").Count();
            List<FilesModel> filesModels = helper.LoadPageItems<FilesModel, DateTime>(50, info.Info, out total, c => c.Category == "0", u => u.AddTime, false).ToList();
            foreach (var path in filesModels)
            {
                string url = path.StoreSite + @"\" + path.FileName + ".jpg";
                string loca = File.Exists(url) ? url : path.StoreSite + @"\poster.jpg";
                if (!File.Exists(loca))
                {
                    continue;
                }
                MoviesModel movies = new MoviesModel
                {
                    Content = loca,
                    Header = path.Caption,
                    Footer = path.FileName
                };

                models.Add(movies);
            }
            DataList = models;
        }

    }

}
