using Prism.Mvvm;
using Prism.Regions;

namespace WallPoster.ViewModels
{
    public class HomeViewModel : BindableBase, IRegionMemberLifetime
    {
        private readonly IRegionManager _regionManager;

        public HomeViewModel(IRegionManager regionManager)
        {
            Test = "这个是测试内容";
            _regionManager = regionManager;
        }

        /// <summary>
        ///    状态
        /// </summary>
        private string _test;

        /// <summary>
        ///  状态
        /// </summary>
        public string Test
        {
            get => _test;
            set => SetProperty(ref _test, value);
        }

        public bool KeepAlive => true;
    }
}
