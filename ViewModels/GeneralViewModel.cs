﻿using log4net;
using Prism.Commands;
using System.Collections.Generic;
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

        public GeneralViewModel()
        {
            SetStoreLocation = new DelegateCommand<string>(SetLocation);
            GetLocations("1");
            GetLocations("2");
        }

        internal void GetLocations(string category)
        {
            var MovieLocations = Settings.MovieLocation;
            var pathList = new List<PathModel>();
            var TVLocation = Settings.TVlocation;
            PathModel item = new PathModel();
            switch (category)
            {
                case "1":
                    Whether = MovieLocations.Count > 0 ? true : false;
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
                case "2":
                    Whether = TVLocation.Count > 0 ? true : false;
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
            using (var dialog = new System.Windows.Forms.FolderBrowserDialog())
            {
                if (dialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    string path = dialog.SelectedPath;
                    switch (category)
                    {
                        case "1":
                            Settings.MovieLocation.Add(path);
                            Settings.MovieLocation = new List<string>(Settings.MovieLocation);
                            GetLocations(category);
                            Whether = true;
                            break;
                        case "2":
                            Settings.TVlocation.Add(path);
                            Settings.TVlocation = new List<string>(Settings.TVlocation);
                            GetLocations(category);
                            Whether = true;
                            break;
                    }
                }
            }
        }

    }
}