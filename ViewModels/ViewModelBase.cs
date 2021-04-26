using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using Prism.Mvvm;
using WallPoster.Models;

namespace WallPoster.ViewModels
{
    public class ViewModelBase<T> : BindableBase, INotifyDataErrorInfo
    {

        public ErrorsContainer<string> _errorsContainer;

        protected ErrorsContainer<string> ErrorsContainer
        {
            get
            {
                if (_errorsContainer == null)
                    _errorsContainer = new ErrorsContainer<string>(s => OnErrorsChanged(s));

                return _errorsContainer;
            }
        }

        public event EventHandler<DataErrorsChangedEventArgs> ErrorsChanged;

        public void OnErrorsChanged(string propertyName)
        {
            ErrorsChanged?.Invoke(this, new DataErrorsChangedEventArgs(propertyName));
        }

        public IEnumerable GetErrors(string propertyName)
        {
            return ErrorsContainer.GetErrors(propertyName);
        }

        public bool HasErrors
        {
            get { return ErrorsContainer.HasErrors; }
        }

        /// <summary>
        ///     数据列表
        /// </summary>
        private IList<T> _dataList;

        /// <summary>
        ///     数据列表 => SetProperty(ref _dataList, value);
        /// </summary>
        public IList<T> DataList
        {
            get => _dataList;
            set
            {
                SetProperty(ref _dataList, value);
                if (_dataList.Count < 0)
                    ErrorsContainer.SetErrors(() => DataList, new string[] { "value cannot be less than 0" });
                else
                    ErrorsContainer.ClearErrors(() => DataList);
            }
        }

        /// <summary>
        ///     布尔类型数据绑定
        /// </summary>
        private bool _movieWhether;

        /// <summary>
        ///     布尔类型数据绑定
        /// </summary>
        public bool MovieWhether
        {
            get => _movieWhether;
            set => SetProperty(ref _movieWhether, value);
        }

        /// <summary>
        ///     布尔类型数据绑定
        /// </summary>
        private bool _tvWhether;

        /// <summary>
        ///     布尔类型数据绑定
        /// </summary>
        public bool TVWhether
        {
            get => _tvWhether;
            set => SetProperty(ref _tvWhether, value);
        }
    }
}
