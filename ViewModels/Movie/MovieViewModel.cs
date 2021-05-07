using HandyControl.Data;
using HandyControl.Tools.Command;
using log4net;
using Prism.Commands;
using Prism.Regions;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media.Imaging;
using System.Windows.Threading;
using WallPoster.Constans;
using WallPoster.Helper;
using WallPoster.Models;

namespace WallPoster.ViewModels
{
    public class MovieViewModel : ViewModelBase<MoviesModel>, INavigationAware, IRegionMemberLifetime
    {
        private static ILog log = LogManager.GetLogger("MovieViewModel");

        private IRegionNavigationJournal _journal;
        private readonly IRegionManager _regionManager;

        #region Command
        private DelegateCommand<object> _movieInfoCommand;
        public DelegateCommand<object> MovieInfoCommand =>
                _movieInfoCommand ?? (_movieInfoCommand = new DelegateCommand<object>(ExecuteMovieInfoCommand));

        private DelegateCommand<object> _playMovieCommand;
        public DelegateCommand<object> PlayMovieCommand =>
                _playMovieCommand ?? (_playMovieCommand = new DelegateCommand<object>(ExecutePlayMovieCommand));

        #endregion



        SQLiteHelper<FilesModel> helper = SQLiteHelper<FilesModel>.GetInstance();

        public MovieViewModel()
        {
            Status = "Visible";
            _ = GetMoviesAsync();
        }

        public MovieViewModel(IRegionManager regionManager) : this()
        {
            _regionManager = regionManager;
        }

        private void ExecutePlayMovieCommand(object parm)
        {
            Path.GetFileNameWithoutExtension(parm.ToString());
            var file = new FileInfo(parm.ToString());
            string info = file.DirectoryName;
            string baseUrl = AppDomain.CurrentDomain.BaseDirectory.ToString() + @"file:\";
            string location = info.Replace(@baseUrl, "");
            var videos = helper.GetAllQuery<FilesModel>(p => p.StoreSite == location);
            var list = new List<string>();
            foreach (var video in videos)
            {
                string p = video.FilePath;
                list.Add(p);
            }
            Process s = new Process();
            if (list.Count > 0)
            {
                s.StartInfo = new ProcessStartInfo(@list[0]);
                s.StartInfo.UseShellExecute = true;
                s.Start();
            }
        }

        /// <summary>
        /// 异步获取影片信息列表
        /// </summary>
        /// <returns></returns>
        public async Task GetMoviesAsync()
        {
            MaxPageCount = 2;
            Status = "Visible";
            await Task.Run(() =>
            {
                List<FilesModel> filesModels = null;
                try
                {
                    int total = helper.GetCount<FilesModel>(i => i.Category == "0");
                    MaxPageCount = total / 50;
                    filesModels = helper.LoadPageItems<FilesModel, DateTime>(50, PageIndex, out total, c => c.Category == "0", u => u.AddTime, false).ToList();
                }
                catch (Exception e)
                {
                    log.Debug($"数据处理错误{e.Message}");
                    var helper = SQLiteHelper<FilesModel>.GetInstance();
                    int total = helper.GetCount<FilesModel>(i => i.Category == "0");
                    MaxPageCount = total / 50;
                    filesModels = helper.LoadPageItems<FilesModel, DateTime>(50, PageIndex, out total, c => c.Category == "0", u => u.AddTime, false).ToList();
                }
                return filesModels;
            }).ContinueWith(movieModel =>
            {
                Application.Current.Dispatcher.BeginInvoke(new Action(() =>
                {
                    var movies = new ObservableCollection<MoviesModel>();
                    foreach (var path in movieModel.Result)
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
                        movies.Add(new MoviesModel()
                        {
                            Content = image,
                            Header = path.Caption,
                            Footer = path.AddTime
                        });
                        image.EndInit();
                        DoEvents();
                    }
                    DataList = movies;
                }));
                Status = "Hidden";
            });
        }

        /// <summary>
        ///     页码改变命令
        /// </summary>
        public RelayCommand<FunctionEventArgs<int>> PageUpdatedCmd =>
            new Lazy<RelayCommand<FunctionEventArgs<int>>>(() =>
                new RelayCommand<FunctionEventArgs<int>>(PageUpdated)).Value;

        /// <summary>
        ///     页码改变
        ///     因SQLite线程池问题暂时将页面更新与初始化分别处理
        /// </summary>
        private void PageUpdated(FunctionEventArgs<int> info)
        {
            Status = "Visible";
            #region 页码改变
            Task.Run(() =>
            {
                List<FilesModel> filesModels = null;
                try
                {
                    int total = helper.GetCount<FilesModel>(i => i.Category == "0");
                    MaxPageCount = total / 50;
                    filesModels = helper.LoadPageItems<FilesModel, DateTime>(50, info.Info, out total, c => c.Category == "0", u => u.AddTime, false).ToList();
                }
                catch (Exception e)
                {
                    log.Debug($"数据处理错误{e.Message}");
                    var helper = SQLiteHelper<FilesModel>.GetInstance();
                    int total = helper.GetCount<FilesModel>(i => i.Category == "0");
                    MaxPageCount = total / 50;
                    filesModels = helper.LoadPageItems<FilesModel, DateTime>(50, info.Info, out total, c => c.Category == "0", u => u.AddTime, false).ToList();
                }
                return filesModels;
            }).ContinueWith(movieModel =>
            {
                Application.Current.Dispatcher.BeginInvoke(new Action(() =>
                {
                    var movies = new ObservableCollection<MoviesModel>();
                    foreach (var path in movieModel.Result)
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
                        movies.Add(new MoviesModel()
                        {
                            Content = image,
                            Header = path.Caption,
                            Footer = path.AddTime
                        });
                        image.EndInit();
                        DoEvents();
                    }
                    DataList = movies;
                }));
            });
            #endregion
            Status = "Hidden";
        }
        /// <summary>
        /// 防止UI线程阻塞
        /// </summary>
        public static void DoEvents()
        {
            DispatcherFrame frame = new DispatcherFrame();
            Dispatcher.CurrentDispatcher.BeginInvoke(DispatcherPriority.Background, new DispatcherOperationCallback(ExitFrame), frame);
            Dispatcher.PushFrame(frame);
        }

        private static Object ExitFrame(Object state)
        {
            ((DispatcherFrame)state).Continue = false;
            return null;
        }

        private string _movieInfo;
        public string MovieInfo
        {
            get { return _movieInfo; }
            set { SetProperty(ref _movieInfo, value); }
        }

        public bool KeepAlive => true;

        private void ExecuteMovieInfoCommand(object parameter)
        {
            //在Movie区域导航到MovieInfoRegion
            MovieInfo = parameter as string;
            IRegion region = _regionManager.Regions[RegionNames.ContentRegion];
            region.RequestNavigate("MovieInfo");
        }

        public void OnNavigatedTo(NavigationContext navigationContext)
        {
            _journal = navigationContext.NavigationService.Journal;
        }

        public bool IsNavigationTarget(NavigationContext navigationContext)
        {
            return true;
        }

        public void OnNavigatedFrom(NavigationContext navigationContext)
        {
            navigationContext.Parameters.Add("MovieInfo", MovieInfo);
        }
    }

}
