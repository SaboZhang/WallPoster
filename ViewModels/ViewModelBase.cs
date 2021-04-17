using System.Collections.Generic;
using Prism.Mvvm;
using WallPoster.Models;

namespace WallPoster.ViewModels
{
    public class ViewModelBase : BindableBase
    {
        /// <summary>
        ///     数据列表
        /// </summary>
        private IList<MoviesModel> _dataList;

        /// <summary>
        ///     数据列表
        /// </summary>
        public IList<MoviesModel> DataList
        {
            get => _dataList;
            set => SetProperty(ref _dataList, value);
        }
    }
}
