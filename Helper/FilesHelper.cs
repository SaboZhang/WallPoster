using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WallPoster.Models;

namespace WallPoster.Helper
{
    public class FilesHelper
    {
        public Task GetMediaFiles(List<string> mediaPath, string[] searchPatterns, string category)
        {
            return Task.Factory.StartNew(async () =>
            {
                List<string> paths = mediaPath;
                if (paths.Count <= 0)
                {
                    return;
                }
                var fileList = new List<string>();
                foreach (string path in paths)
                {
                    DirectoryInfo dir = new DirectoryInfo(path);
                    FileInfo[][] fis = new FileInfo[searchPatterns.Length][];
                    int count = 0;
                    for (int i = 0; i < searchPatterns.Length; i++)
                    {
                        FileInfo[] fileInfos = dir.GetFiles(searchPatterns[i], SearchOption.AllDirectories);
                        fis[i] = fileInfos;
                        count += fileInfos.Length;
                    }
                    string[] files = new string[count];
                    int n = 0;
                    for (int i = 0; i <= fis.GetUpperBound(0); i++)
                    {
                        for (int j = 0; j < fis[i].Length; j++)
                        {
                            string temp = fis[i][j].FullName;
                            files[n] = temp;
                            n++;
                        }
                    }
                    fileList.AddRange(files);
                }
                await SaveMediaFiles(fileList, category);
            });
        }

        private Task SaveMediaFiles(List<string> fileList, string category)
        {
            return Task.Factory.StartNew(() => 
            {
                foreach (string path in fileList)
                {
                    FileInfo fileInfo = new FileInfo(path);
                    StringHelper stringHelper = new StringHelper();
                    using (var helper = new SQLiteHelper())
                    {
                        var model = new FilesModel()
                        {
                            FilePath = path,
                            FileName = Path.GetFileNameWithoutExtension(path),
                            Caption = stringHelper.GetCaption(Path.GetFileNameWithoutExtension(path)),
                            FileSize = (fileInfo.Length / 1024).ToString(),
                            AddTime = DateTime.Now.ToLocalTime(),
                            FileModifyTime = fileInfo.LastWriteTime,
                            Ext = fileInfo.Extension,
                            Duration = "",
                            PrivatePwd = "",
                            Category = category,
                            StoreSite = fileInfo.DirectoryName
                        };
                        helper.Files.Add(model);
                        helper.SaveChanges();
                    }
                }
            });
        }
    }
}
