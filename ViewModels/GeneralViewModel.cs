using log4net;
using Prism.Commands;
using System.Collections.Generic;
using WallPoster.Assets;
using WallPoster.Models;
using static WallPoster.Assets.Helper;

namespace WallPoster.ViewModels
{
    public class GeneralViewModel : ViewModelBase<PathModel>
    {
        private static ILog log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.Name);
        /// <summary>
        ///     影视路径
        /// </summary>
        private IList<PathModel> _moviePathList;

        /// <summary>
        ///     影视路径
        /// </summary>
        public IList<PathModel> MoviePathDataList
        {
            get => _moviePathList;
            set
            {
                SetProperty(ref _moviePathList, value);
                if (_moviePathList.Count < 0)
                    ErrorsContainer.SetErrors(() => MoviePathDataList, new string[] { "value cannot be less than 0" });
                else
                    ErrorsContainer.ClearErrors(() => MoviePathDataList);
            }
        }

        /// <summary>
        ///     剧集路径
        /// </summary>
        private IList<PathModel> _tvPathList;

        /// <summary>
        ///     剧集路径
        /// </summary>
        public IList<PathModel> TVPathDataList
        {
            get => _tvPathList;
            set
            {
                SetProperty(ref _tvPathList, value);
                if (_tvPathList.Count < 0)
                    ErrorsContainer.SetErrors(() => TVPathDataList, new string[] { "value cannot be less than 0" });
                else
                    ErrorsContainer.ClearErrors(() => TVPathDataList);
            }
        }

        public DelegateCommand<string> SetStoreLocation { get; private set; }
        public DelegateCommand<object> DelMoviePath { get; private set; }
        public DelegateCommand<object> DelTVPath { get; private set; }

        public GeneralViewModel()
        {
            SetStoreLocation = new DelegateCommand<string>(SetLocation);
            DelMoviePath = new DelegateCommand<object>(DeleteMoviePath);
            DelTVPath = new DelegateCommand<object>(DeleteTVPath);
            GetLocations(Consts.MovieCategory);
            GetLocations(Consts.TVCategory);
        }

        internal void GetLocations(string category)
        {
            var MovieLocations = Settings.MovieLocation;
            var pathList = new List<PathModel>();
            var TVLocation = Settings.TVLocation;
            PathModel item;
            switch (category)
            {
                case Consts.MovieCategory:
                    MovieWhether = MovieLocations.Count > 0;
                    foreach (var movie in MovieLocations)
                    {
                        item = new PathModel
                        {
                            MoviePath = movie
                        };
                        pathList.Add(item);
                    }
                    MoviePathDataList = pathList;
                    log.Info(pathList);
                    break;
                case Consts.TVCategory:
                    TVWhether = TVLocation.Count > 0;
                    foreach (var tv in TVLocation)
                    {
                        item = new PathModel
                        {
                            TVPath = tv
                        };
                        pathList.Add(item);
                    }
                    TVPathDataList = pathList;
                    break;
            }
        }

        protected virtual void SetLocation(string category)
        {
            using var dialog = new System.Windows.Forms.FolderBrowserDialog();
            if (dialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                string path = dialog.SelectedPath;
                switch (category)
                {
                    case Consts.MovieCategory:
                        Settings.MovieLocation.Add(path);
                        Settings.MovieLocation = new List<string>(Settings.MovieLocation);
                        GetLocations(category);
                        MovieWhether = true;
                        break;
                    case Consts.TVCategory:
                        Settings.TVLocation.Add(path);
                        Settings.TVLocation = new List<string>(Settings.TVLocation);
                        GetLocations(category);
                        TVWhether = true;
                        break;
                }
            }
        }

        protected virtual void DeleteMoviePath(object index)
        {
            List<string> path = Settings.MovieLocation;
            path.RemoveAt((int)index);
            GetLocations(Consts.MovieCategory);
        }

        protected virtual void DeleteTVPath(object index)
        {
            List<string> path = Settings.TVLocation;
            path.RemoveAt((int)index);
            GetLocations(Consts.TVCategory);
        }

    }
}
