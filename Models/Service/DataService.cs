using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WallPoster.Models.Service
{
    public class DataService
    {
        internal ObservableCollection<MoviesModel> GetMovieDataList()
        {
            return new ObservableCollection<MoviesModel>
            {
                new MoviesModel
            {
                Header = "Atomic",
                Content = "1.jpg",
                Footer = "Stive Morgan"
            },
            new MoviesModel
            {
                Header = "Zinderlong",
                Content = "2.jpg",
                Footer = "Zonderling"
            },
            new MoviesModel
            {
                Header = "Busy Doin' Nothin'",
                Content = "3.jpg",
                Footer = "Ace Wilder"
            },
            new MoviesModel
            {
                Header = "Wrong",
                Content = "4.jpg",
                Footer = "Blaxy Girls"
            },
            new MoviesModel
            {
                Header = "The Lights",
                Content = "5.jpg",
                Footer = "Panda Eyes"
            },
            new MoviesModel
            {
                Header = "EA7-50-Cent Disco",
                Content = "6.jpg",
                Footer = "еяхат музыка"
            },
            new MoviesModel
            {
                Header = "Monsters",
                Content = "7.jpg",
                Footer = "Different Heaven"
            },
            new MoviesModel
            {
                Header = "Gangsta Walk",
                Content = "8.jpg",
                Footer = "Illusionize"
            },
            new MoviesModel
            {
                Header = "Won't Back Down",
                Content = "9.jpg",
                Footer = "Boehm"
            },
            new MoviesModel
            {
                Header = "Katchi",
                Content = "10.jpg",
                Footer = "Ofenbach"
            }
            };
        }

        internal MoviesModel GetMovieData()
        {
            return new MoviesModel
            {
                Content = $"/WallPoster;component/Resources/Album/{DateTime.Now.Second % 10 + 1}.jpg"
            };
        }
    }
}
