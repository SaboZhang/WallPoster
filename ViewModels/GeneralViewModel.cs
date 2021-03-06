using HandyControl.Controls;
using log4net;
using Prism.Commands;
using System.Collections.Generic;
using WallPoster.Assets;
using WallPoster.Helper;
using WallPoster.Models;
using static WallPoster.Assets.Helper;

namespace WallPoster.ViewModels
{
    public class GeneralViewModel : ViewModelBase<PathModel>
    {
        private static ILog log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.Name);

        #region Command
        private DelegateCommand<string> _setStoreLocationCommand;
        public DelegateCommand<string> SetStoreLocationCommand =>
            _setStoreLocationCommand ?? (_setStoreLocationCommand = new DelegateCommand<string>(SetStoreLocation));

        private DelegateCommand<object> _deleteMoviePathCommand;
        public DelegateCommand<object> DeleteMovieCommand =>
            _deleteMoviePathCommand ?? (_deleteMoviePathCommand = new DelegateCommand<object>(DeleteMoviePath));

        private DelegateCommand<object> _deleteTVPathCommand;
        public DelegateCommand<object> DeleteTVPathCommand =>
            _deleteTVPathCommand ?? (_deleteTVPathCommand = new DelegateCommand<object>(DeleteTVPath));

        #endregion

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

        public GeneralViewModel()
        {
            GetLocations(Consts.MovieCategory);
            GetLocations(Consts.TVCategory);
        }

        private void GetLocations(string category)
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

        private void SetStoreLocation(string category)
        {
            using var dialog = new System.Windows.Forms.FolderBrowserDialog();
            if (dialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                string path = dialog.SelectedPath;
                if (Settings.MovieLocation.Contains(path) || Settings.TVLocation.Contains(path))
                {
                    Growl.WarningGlobal($"路径{path}已存在");
                    return;
                }
                switch (category)
                {
                    case Consts.MovieCategory:
                        Settings.MovieLocation.Add(path);
                        Settings.MovieLocation = new List<string>(Settings.MovieLocation);
                        GetLocations(category);
                        Retrieval(category);
                        MovieWhether = true;
                        break;
                    case Consts.TVCategory:
                        Settings.TVLocation.Add(path);
                        Settings.TVLocation = new List<string>(Settings.TVLocation);
                        GetLocations(category);
                        Retrieval(category);
                        TVWhether = true;
                        break;
                }
            }
        }

        private void DeleteMoviePath(object index)
        {
            if (index.Equals(-1))
            {
                Growl.WarningGlobal("请选择需要删除的路径!");
                return;
            }
            List<string> path = Settings.MovieLocation;
            path.RemoveAt((int)index);
            Settings.MovieLocation = new List<string>(path);
            GetLocations(Consts.MovieCategory);
        }

        protected virtual void DeleteTVPath(object index)
        {
            if (index.Equals(-1))
            {
                Growl.WarningGlobal("请选择需要删除的路径!");
                return;
            }
            List<string> path = Settings.TVLocation;
            path.RemoveAt((int)index);
            Settings.MovieLocation = new List<string>(path);
            GetLocations(Consts.TVCategory);
        }

        private async void Retrieval(string category)
        {
            var files = new FilesHelper();
            List<string> paths = category == "0" ? Settings.MovieLocation : Settings.TVLocation;
            await files.GetMediaFiles(paths, category);
        }

    }
}
