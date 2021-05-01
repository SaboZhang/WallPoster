using log4net;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using WallPoster.Models;
using WallPoster.Helper.Video;
using WallPoster.Helper.Common;

namespace WallPoster.Helper
{
    /// <summary>
    /// 文件读取帮助类
    /// </summary>
    public class FilesHelper
    {
        private static ILog log = LogManager.GetLogger("FilesHelper");
        private readonly NamingOptions _namingOptions = new NamingOptions();
        private readonly VideoResolver _videoResolver = new VideoResolver(new NamingOptions());

        private static readonly object LockObj = new();
        private static SQLiteHelper helper = null;

        public static SQLiteHelper GetInstance()
        {
            if (helper == null)
            {
                lock (LockObj)
                {
                    if (helper == null)
                    {
                        helper = new SQLiteHelper();
                    }
                }
            }
            return helper;
        }

        public FilesHelper()
        {
            GetInstance();
        }

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
                    DirectoryInfo dir = new(path);
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
                var resolver = GetResolver();
                resolver.ResolveFiles(fileList);
                await SaveMediaFiles(fileList, category);
            });
        }

        private Task SaveMediaFiles(List<string> fileList, string category)
        {
            return Task.Factory.StartNew(() =>
            {
                foreach (string path in fileList)
                {
                    FileInfo fileInfo = new(path);
                    StringHelper stringHelper = new();
                    var file = helper.Files.Where(p => p.FilePath.Contains(path)).ToList();
                    if (file.Count > 0)
                    {
                        log.Info($"跳过路径[{path}]因为数据库中已经存在");
                        continue;
                    }
                    VideoFileInfo video = _videoResolver.Resolve(path, false);
                    var model = new FilesModel()
                    {
                        FilePath = path,
                        FileName = Path.GetFileNameWithoutExtension(path),
                        Caption = video.Name,
                        FileSize = fileInfo.Length / 1024,
                        AddTime = DateTime.Now.ToLocalTime(),
                        FileModifyTime = fileInfo.LastWriteTime,
                        Ext = fileInfo.Extension,
                        Duration = video.Year,
                        PrivatePwd = "",
                        Category = category,
                        StoreSite = fileInfo.DirectoryName,
                        Container = video.Container
                    };
                    helper.Files.Add(model);
                    helper.SaveChanges();
                    log.Info($"{(category == "0" ? "影视数据保存成功" : "剧集数据保存成功")}");
                }
            });
        }

        private StackResolver GetResolver()
        {
            return new StackResolver(_namingOptions);
        }
    }
}
