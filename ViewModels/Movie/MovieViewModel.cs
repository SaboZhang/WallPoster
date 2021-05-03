using HandyControl.Controls;
using log4net;
using Prism.Commands;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Media.Imaging;
using WallPoster.Helper;
using WallPoster.Models;
using WallPoster.Views;
using static WallPoster.Assets.Helper;

namespace WallPoster.ViewModels
{
    public class MovieViewModel : ViewModelBase<MoviesModel>
    {

        public DelegateCommand<object> ClickCover { get; private set; }
        public DelegateCommand<object> ClickInfo { get; private set; }
        private static ILog log = LogManager.GetLogger("MovieViewModel");
        List<MoviesModel> models = new List<MoviesModel>();

        /*internal ObservableCollection<MoviesModel> GetMovieDataList()
        {
            return new ObservableCollection<MoviesModel>
            {
                new MoviesModel
            {
                Header = "Atomic",
                Content = "/WallPoster;component/Resources/Album/1.jpg",
                Footer = "Stive Morgan"
            },
            new MoviesModel
            {
                Header = "Zinderlong",
                Content = "/WallPoster;component/Resources/Album/2.jpg",
                Footer = "Zonderling"
            },
            new MoviesModel
            {
                Header = "Busy Doin' Nothin'",
                Content = "/WallPoster;component/Resources/Album/3.jpg",
                Footer = "Ace Wilder"
            },
            new MoviesModel
            {
                Header = "Wrong",
                Content = "/WallPoster;component/Resources/Album/4.jpg",
                Footer = "Blaxy Girls"
            },
            new MoviesModel
            {
                Header = "The Lights",
                Content = "/WallPoster;component/Resources/Album/5.jpg",
                Footer = "Panda Eyes"
            },
            new MoviesModel
            {
                Header = "EA7-50-Cent Disco",
                Content = "/WallPoster;component/Resources/Album/6.jpg",
                Footer = "еяхат музыка"
            },
            new MoviesModel
            {
                Header = "Monsters",
                Content = "/WallPoster;component/Resources/Album/7.jpg",
                Footer = "Different Heaven"
            },
            new MoviesModel
            {
                Header = "Gangsta Walk",
                Content = "/WallPoster;component/Resources/Album/8.jpg",
                Footer = "Illusionize"
            },
            new MoviesModel
            {
                Header = "Won't Back Down",
                Content = "/WallPoster;component/Resources/Album/9.jpg",
                Footer = "Boehm"
            },
            new MoviesModel
            {
                Header = "Katchi",
                Content = "/WallPoster;component/Resources/Album/10.jpg",
                Footer = "Ofenbach"
            }
            };
        }*/

        public MovieViewModel()
        {
            
            ShowInfo();
            /*GetMovies();*/
            ClickCover = new DelegateCommand<object>(ShowClick);
            ClickInfo = new DelegateCommand<object>(Info_click);
        }

        private void ShowClick(object parm)
        {
            MessageBox.Show($"执行命令{parm}");
        }
        

        public void GetMovies()
        {
            
            var helper = FilesHelper.GetInstance();
            List<FilesModel> filesModels = helper.Files
                .Where(m => m.Category == "0").Take(100).ToList();
            
            foreach (var path in filesModels)
            {
                BitmapImage image = new BitmapImage();
                image.BeginInit();
                image.CacheOption = BitmapCacheOption.OnLoad;
                string url = path.StoreSite + @"\" + path.FileName + ".jpg";
                string loca = File.Exists(url) ? url : path.StoreSite + @"\poster.jpg";
                image.UriSource = new Uri(loca);
                if (!File.Exists(loca))
                {
                    continue;
                }
                MoviesModel movies = new MoviesModel
                {
                    Content = image,
                    Header = path.Caption,
                    Footer = path.FileName
                };
                image.EndInit();
                models.Add(movies);
            }
            
            DataList = models;
            /*Task.Run(() =>
            {
                lock (lockobj)
                {
                    
                }

            });*/
        }


        private async void ShowInfo()
        {
            FilesHelper files = new FilesHelper();
            List<string> paths = Settings.MovieLocation;
            await files.GetMediaFiles(paths, "0");
        }

        private void Info_click(object p)
        {
            MainWindow.Instance.NavigateTo(typeof(MovieInfo), p);
            /*MessageBox.Show($"info{p}");*/
        }

    }

}
