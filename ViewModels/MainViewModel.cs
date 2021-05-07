using HandyControl.Controls;
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
        internal static MainViewModel Instance;

        private readonly IRegionManager _regionManager;
        private IRegionNavigationJournal _journal;

        #region Command
        private DelegateCommand<NavigationViewSelectionChangedEventArgs> _switchCommand;
        public DelegateCommand<NavigationViewSelectionChangedEventArgs> SwitchCommand =>
                _switchCommand ?? (_switchCommand = new DelegateCommand<NavigationViewSelectionChangedEventArgs>(Switch));

        private DelegateCommand _loadingCommand;
        public DelegateCommand LoadingCommand =>
                _loadingCommand ?? (_loadingCommand = new DelegateCommand(ExecuteLoadingCommand));

        private DelegateCommand _backCommand;
        public DelegateCommand BackCommand =>
                _backCommand ?? (_backCommand = new DelegateCommand(ExecuteBakcCommand));

        #endregion
        private bool _IsFirstRun;

        public bool IsFirstRun
        {
            get => Settings.IsFirstRun;
            set => SetProperty(ref _IsFirstRun, value);
        }

        private bool _isBackEnabled;

        public bool IsBackEnabled
        {
            get => _isBackEnabled;
            set => SetProperty(ref _isBackEnabled, value);
        }

        public MainViewModel(IRegionManager regionManager)
        {
            Instance = this;
            _regionManager = regionManager;
        }

        private void ExecuteBakcCommand()
        {
            if (_journal.CanGoBack)
            {
                _journal.GoBack();
            }
        }

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
                _journal.GoBack
                IsBackEnabled = true;
            }   
        }
    }
}
