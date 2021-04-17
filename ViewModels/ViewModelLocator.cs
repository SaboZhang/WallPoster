using System;
using System.Windows;
using WallPoster.Models.Service;

namespace WallPoster.ViewModels
{
    public class ViewModelLocator
    {
        public ViewModelLocator()
        {
            
        }

        public static ViewModelLocator Instance => new Lazy<ViewModelLocator>(() =>
            Application.Current.TryFindResource("Locator") as ViewModelLocator).Value;

       
    }
}
