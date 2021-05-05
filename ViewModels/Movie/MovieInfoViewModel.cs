using HandyControl.Controls;
using ModernWpf.Navigation;
using Prism.Commands;
using Prism.Regions;
using System.Windows.Navigation;
using WallPoster.Constans;
using WallPoster.Models;

namespace WallPoster.ViewModels
{
    public class MovieInfoViewModel : ViewModelBase<MoviesModel>
    {
        public DelegateCommand<object> ClickInfo { get; private set; }

        public MovieInfoViewModel()
        {
            ClickInfo = new DelegateCommand<object>(Info_click);
        }

        private void Info_click(object p)
        {
            MessageBox.Show($"新页面{p}");
        }

        private readonly IRegionManager _regionManager;

        private DelegateCommand _createAccountCommand;
        public DelegateCommand CreateAccountCommand =>
                _createAccountCommand ?? (_createAccountCommand = new DelegateCommand(ExecuteCreateAccountCommand));

        //导航到CreateAccount
        void ExecuteCreateAccountCommand()
        {
            Navigate("CreateAccount");
        }

        private void Navigate(string navigatePath)
        {
            if (navigatePath != null)
                _regionManager.RequestNavigate(RegionNames.MovieInfoRegion, navigatePath);

        }

    }
}
