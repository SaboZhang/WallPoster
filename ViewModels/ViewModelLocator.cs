using GalaSoft.MvvmLight.Ioc;
using System;
using System.Windows;
using WallPoster.Models.Service;

namespace WallPoster.ViewModels
{
    public class ViewModelLocator
    {
        public ViewModelLocator()
        {
            SimpleIoc.Default.Register<DataService>();

            SimpleIoc.Default.Register<MovieViewModel>();
        }

        public static ViewModelLocator Instance => new Lazy<ViewModelLocator>(() =>
            Application.Current.TryFindResource("Locator") as ViewModelLocator).Value;

        public MovieViewModel MoviesModel => new MovieViewModel(SimpleIoc.Default.GetInstance<DataService>());
    }
}
