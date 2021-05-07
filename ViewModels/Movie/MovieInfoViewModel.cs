using HandyControl.Controls;
using Prism.Regions;
using WallPoster.Models;

namespace WallPoster.ViewModels
{
    public class MovieInfoViewModel : ViewModelBase<MoviesModel>, INavigationAware, IRegionMemberLifetime
    {
        private IRegionNavigationJournal _journal;

        public bool KeepAlive => true;

        public bool IsNavigationTarget(NavigationContext navigationContext)
        {
            return true;
        }

        public void OnNavigatedFrom(NavigationContext navigationContext)
        {

        }
        /// <summary>
        /// 从Movie导航到MovieInfo处理信息
        /// </summary>
        /// <param name="navigationContext"></param>
        public void OnNavigatedTo(NavigationContext navigationContext)
        {
            _journal = navigationContext.NavigationService.Journal;

            var movieInfo = navigationContext.Parameters["MovieInfo"] as string;
            if (movieInfo != null)
            {
                MessageBox.Show(movieInfo);

            }
        }

    }
}
