using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Threading;
using WallPoster.Models;
using static WallPoster.Assets.Helper;

namespace WallPoster.ViewModels
{
    public class MovieViewModel : ViewModelBase<MoviesModel>
    {

        internal ObservableCollection<MoviesModel> GetMovieDataList()
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
        }

        public MovieViewModel()
        {
            ThreadStart movieref = new ThreadStart(GetMoviesFile);
            Thread MoviesThread = new Thread(movieref);
            MoviesThread.Start();
        }

        /*internal MoviesModel GetMovieData()
        {
            return new MoviesModel
            {
                Content = $"/WallPoster;component/Resources/Album/{DateTime.Now.Second % 10 + 1}.jpg"
            };
        }*/

        /*internal async void GetPaths()
        {
            using (var helper = new SQLiteHelper())
            {
                var paths = await helper.Paths.ToListAsync();
                var pathList = new List<PathModel>();
                foreach (var path in paths)
                {
                    PathModel item = new PathModel 
                    { 
                        MoviePath = path.MoviePath 
                    };
                    pathList.Add(item);
                }
                DataList = pathList;
        Directory.GetFiles
            }
        }*/
        internal void GetMoviesFile()
        {
            List<string> paths = Settings.MovieLocation;
            if (paths.Count <= 0)
            {
                return;
            }
            var pathList = new List<MoviesModel>();
            var fileList = new List<string>();
            foreach (string path in paths)
            {
                /*Directory.GetFiles(path, "*.*", SearchOption.AllDirectories).Where(s => s.EndsWith(".mp3") || s.EndsWith(".jpg"));*/
                string[] file = Directory.GetFiles(@path, "*.png", SearchOption.AllDirectories);
                fileList.AddRange(file);
            }
            foreach (string poster in fileList)
            {
                MoviesModel item = new MoviesModel()
                {
                    Content = poster,
                    Header = Path.GetFileNameWithoutExtension(poster),
                    Footer = "测试内容"
                };
                pathList.Add(item);
            }
            DataList = pathList;
        }

    }

}
