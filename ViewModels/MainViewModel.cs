using ModernWpf.Controls;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Regions;
using WallPoster.Constans;
using static WallPoster.Assets.Helper;

namespace WallPoster.ViewModels
{
    public class MainViewModel : BindableBase
    {
        private readonly IRegionManager _regionManager;
        private IRegionNavigationJournal _journal;

        #region Command
        private DelegateCommand<NavigationViewSelectionChangedEventArgs> _switchCommand;
        public DelegateCommand<NavigationViewSelectionChangedEventArgs> SwitchCommand =>
                _switchCommand ?? (_switchCommand = new DelegateCommand<NavigationViewSelectionChangedEventArgs>(Switch));

        private DelegateCommand _loadingCommand;
        public DelegateCommand LoadingCommand =>
                _loadingCommand ?? (_loadingCommand = new DelegateCommand(ExecuteLoadingCommand));

        #endregion
        private bool _IsFirstRun;

        public bool IsFirstRun
        {
            get => Settings.IsFirstRun;
            set => SetProperty(ref _IsFirstRun, value);
        }

        public MainViewModel(IRegionManager regionManager)
        {
            _regionManager = regionManager;
        }

        /// <summary>
        /// 初始化首页
        /// </summary>
        private void ExecuteLoadingCommand()
        {
            IRegion region = _regionManager.Regions[RegionNames.ContentRegion];
            region.RequestNavigate("Home");
        }

        private void Switch(NavigationViewSelectionChangedEventArgs e)
        {
            if (e.SelectedItem is NavigationViewItem item)
                if (item.Tag != null)
                {
                    Navigate(item.Tag.ToString());
                }
        }

        private void Navigate(string navigatePath)
        {
            if (navigatePath != null)
            {
                _regionManager.RequestNavigate(RegionNames.ContentRegion, navigatePath);
            }
        }

        public void OnNavigatedTo(NavigationContext navigationContext)
        {

            _journal = navigationContext.NavigationService.Journal;
        }
    }
}
