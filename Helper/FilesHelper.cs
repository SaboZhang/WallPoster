using log4net;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using WallPoster.Models;
using WallPoster.Helper.Video;
using WallPoster.Helper.Common;
using WallPoster.Models.IO;

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
        private readonly VideoListResolver _videoListResolver = new VideoListResolver(new NamingOptions());

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

        public Task GetMediaFiles(List<string> mediaPath, string category)
        {
            return Task.Factory.StartNew(async () =>
            {
                List<string> paths = mediaPath;
                if (paths.Count <= 0)
                {
                    return;
                }
                /*var result = _videoListResolver.Resolve(paths.Select(i => new FileSystemMetadata
                {
                    IsDirectory = true,
                    FullName = i
                }).ToList()).ToList();*/
                var fileList = new List<string>();
                foreach (string path in paths)
                {
                    // 获取设置目录下的所有文件夹
                    DirectoryInfo dir = new(path);
                    // 获取设置目录下的所有文件
                    System.Collections.IList list = paths;
                    for (int i = 0; i < list.Count; i++)
                    {
                        FileInfo file = (FileInfo)list[i];
                        fileList.Add(file.ToString());
                    }
                    fileList.Add(dir.ToString());
                    log.Info(fileList);
                    /*DirectoryInfo dir = new(path);
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
                    }*/
                }
                bool isDir = true;
                long size = 0;
                var fsm = new List<FileSystemMetadata>();
                foreach (var location in fileList)
                {
                    
                    if (File.Exists(location))
                    {
                        isDir = false;
                        var info = new FileInfo(location);
                        size = info.Length / 1024;
                    }
                    else if (Directory.Exists(location))
                    {
                        isDir = true;
                    }
                    var item = new FileSystemMetadata 
                    {
                        IsDirectory = isDir,
                        FullName = Path.GetFileName(location),
                        Length = size,
                    };
                    fsm.Add(item);
                }
                List<VideoInfo> result = (List<VideoInfo>)_videoListResolver.Resolve(fsm);
                log.Info(result);
                /*var resolver = GetResolver();
                resolver.ResolveFiles(fileList);*/
                await SaveMediaFiles(result, category);
            });
        }

        private Task SaveMediaFiles(List<VideoInfo> fileList, string category)
        {
            return Task.Factory.StartNew(() =>
            {
                foreach (var info in fileList)
                {
                    // 获取Name与Year以及VideoFileInfo信息列表
                    var name = info.Name;
                    var year = info.Year;
                    foreach (var video in info.Files)
                    {
                        var model = new FilesModel()
                        {
                            FilePath = video.Path,
                            FileName = name,
                            Caption = video.Name,
                            FileSize = video.Format3D,
                            AddTime = DateTime.Now.ToLocalTime(),
                            FileModifyTime = DateTime.Now.ToLocalTime(),
                            Ext = video.ExtraType.ToString(),
                            Year = year,
                            PrivatePwd = "",
                            Category = category,
                            StoreSite = video.Path,
                            Container = video.Container
                        };
                        helper.Files.Add(model);
                    }
                    /*FileInfo fileInfo = new(path);
                    StringHelper stringHelper = new();
                    var file = helper.Files.Where(p => p.FilePath.Contains(path)).ToList();
                    if (file.Count > 0)
                    {
                        log.Info($"跳过路径[{path}]因为数据库中已经存在");
                        continue;
                    }
                    VideoFileInfo video = _videoResolver.Resolve(path, false);*/
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
