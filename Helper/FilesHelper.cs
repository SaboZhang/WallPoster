using log4net;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using WallPoster.Helper.Common;
using WallPoster.Helper.Video;
using WallPoster.Models;
using WallPoster.Models.IO;

namespace WallPoster.Helper
{
    /// <summary>
    /// 文件读取帮助类
    /// </summary>
    public class FilesHelper
    {
        private static ILog log = LogManager.GetLogger("FilesHelper");

        private readonly VideoListResolver _videoListResolver = new VideoListResolver(new NamingOptions());

        public FilesHelper()
        {

        }

        SQLiteHelper<FilesModel> helper = SQLiteHelper<FilesModel>.GetInstance();
        /// <summary>
        /// 读取媒体库文件
        /// </summary>
        /// <param name="mediaPath"></param>
        /// <param name="category"></param>
        /// <returns></returns>
        public Task GetMediaFiles(List<string> mediaPath, string category)
        {
            return Task.Factory.StartNew(async () =>
            {
                List<string> paths = mediaPath;
                if (paths.Count <= 0)
                {
                    return;
                }
                var fileList = new List<FileInfo>();
                var fsm = new List<FileSystemMetadata>();
                foreach (string path in paths)
                {
                    DirectoryInfo dir = new(path);
                    // 获取设置目录下的所有文件
                    var files = dir.GetFiles("*", SearchOption.AllDirectories);
                    foreach (var file in files)
                    {
                        var item = new FileSystemMetadata
                        {
                            IsDirectory = false,
                            FullName = file.FullName,
                            Length = file.Length / 1024,
                            Name = file.Name,
                            DirectoryName = file.DirectoryName,
                            Extension = file.Extension,
                            CreationTimeUtc = file.CreationTime,
                            LastWriteTimeUtc = file.LastWriteTime

                        };
                        fsm.Add(item);
                    }
                }
                try
                {
                    var result = _videoListResolver.Resolve(fsm);
                    await SaveMediaFiles(result, category);
                }
                catch (Exception e)
                {
                    log.Error($"数据处理异常：{e.Message}");
                    return;
                }

            });
        }

        /// <summary>
        /// 存储媒体库文件信息到SQLite
        /// </summary>
        /// <param name="fileList"></param>
        /// <param name="category"></param>
        /// <returns></returns>
        private Task SaveMediaFiles(IEnumerable<VideoInfo> fileList, string category)
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
                        var file = new FileInfo(video.Path);
                        var model = new FilesModel()
                        {
                            FilePath = video.Path,
                            FileName = file.Name,
                            Caption = name,
                            FileSize = file.Length / 1024,
                            AddTime = DateTime.Now.ToLocalTime(),
                            FileModifyTime = file.LastWriteTime,
                            StubType = video.StubType,
                            Year = year,
                            PrivatePwd = "",
                            Category = category,
                            StoreSite = file.DirectoryName,
                            Container = video.Container
                        };
                        helper.Files.Add(model);
                    }
                    try
                    {
                        var count = helper.Files.Where(p => p.Caption.Contains(name)).Count();
                        if (count > 0)
                        {
                            log.Info($"跳过文件[{name}]数据库中已经存在");
                            continue;
                        }
                        helper.SaveChanges();
                        log.Info($"{(category == "0" ? "影视数据保存成功" : "剧集数据保存成功")}");
                    }
                    catch (Exception e)
                    {
                        log.Error($"数据处理异常:{e.Message}");
                        continue;
                    }

                }
            });

        }
    }
}
