using System.Collections.ObjectModel;
using WallPoster.Models;

namespace WallPoster.ViewModels
{
    public class MovieViewModel : ViewModelBase
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
            DataList = GetMovieDataList();
        }
    
    }

}
