using System;
using System.Windows.Media.Imaging;

namespace WallPoster.Models
{
    public class MoviesModel
    {
        public string Header { get; set; }

        public BitmapImage Content { get; set; }

        public DateTime Footer { get; set; }

    }
}
