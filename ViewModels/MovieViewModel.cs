using System;
using WallPoster.Models.Service;
using GalaSoft.MvvmLight.Command;
using WallPoster.Models;

namespace WallPoster.ViewModels
{
    public class MovieViewModel : ViewModelBase<MoviesModel>
    {
        private readonly DataService _dataService;

        public MovieViewModel(DataService dataService)
        {
            _dataService = dataService;
            DataList = dataService.GetMovieDataList();
        }

        public RelayCommand AddItemCmd => new Lazy<RelayCommand>(() =>
            new RelayCommand(() => DataList.Insert(0, _dataService.GetMovieData()))).Value;

        public RelayCommand RemoveItemCmd => new Lazy<RelayCommand>(() =>
            new RelayCommand(() =>
            {
                if (DataList.Count > 0)
                {
                    DataList.RemoveAt(0);
                }
            })).Value;
    }
}
