using Prism.Commands;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WallPoster.Helper;
using WallPoster.Models;

namespace WallPoster.ViewModels
{
    public class GeneralViewModel : ViewModelBase<PathModel>
    {
        public DelegateCommand<string> SetStoreLocation { get; private set; }

        public GeneralViewModel()
        {
            SetStoreLocation = new DelegateCommand<string>(TVLocation);
            GetPaths();
        }

        internal async void GetPaths()
        {
            using (var helper = new SQLiteHelper())
            {
                var paths = await helper.Paths.ToListAsync();
                PathModel item = null;
                var pathList = new List<PathModel>();
                foreach (var path in paths)
                {
                    item = path.Category == "2" ? new PathModel { TVPath = path.TVPath } : new PathModel { MoviePath = path.MoviePath };
                    pathList.Add(item);
                }
                DataList = pathList;
            }
        }

        protected virtual void TVLocation(string category)
        {
            using (var dialog = new System.Windows.Forms.FolderBrowserDialog())
            {
                if (dialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    string path = dialog.SelectedPath;
                    using (var helper = new SQLiteHelper())
                    {
                        var model = new PathModel()
                        {
                            MoviePath = "",
                            TVPath = path,
                            Category = category
                        };
                        helper.Paths.Add(model);
                        helper.SaveChanges();
                        GetPaths();
                    }
                }
            }
        }

    }
}
