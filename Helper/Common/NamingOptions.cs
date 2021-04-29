using System;
using System.Linq;
using System.Text.RegularExpressions;
using WallPoster.Helper.Video;

namespace WallPoster.Helper.Common
{
    /// <summary>
    /// 又大又丑的类包含许多不同的命名选项，这些选项应该被分割和注入，而不是到处传递。
    /// </summary>
    public class NamingOptions
    {
        public NamingOptions()
        {
            VideoFileExtensions = new[]
            {
                ".m4v",
                ".3gp",
                ".nsv",
                ".ts",
                ".ty",
                ".strm",
                ".rm",
                ".rmvb",
                ".ifo",
                ".mov",
                ".qt",
                ".divx",
                ".xvid",
                ".bivx",
                ".vob",
                ".nrg",
                ".img",
                ".iso",
                ".pva",
                ".wmv",
                ".asf",
                ".asx",
                ".ogm",
                ".m2v",
                ".avi",
                ".bin",
                ".dvr-ms",
                ".mpg",
                ".mpeg",
                ".mp4",
                ".mkv",
                ".avc",
                ".vp3",
                ".svq3",
                ".nuv",
                ".viv",
                ".dv",
                ".fli",
                ".flv",
                ".001",
                ".tp"
            };

            VideoFlagDelimiters = new[]
            {
                '(',
                ')',
                '-',
                '.',
                '_',
                '[',
                ']'
            };

            StubFileExtensions = new[]
            {
                ".disc"
            };

            StubTypes = new[]
            {
                new StubTypeRule(
                    stubType: "dvd",
                    token: "dvd"),

                new StubTypeRule(
                    stubType: "hddvd",
                    token: "hddvd"),

                new StubTypeRule(
                    stubType: "bluray",
                    token: "bluray"),

                new StubTypeRule(
                    stubType: "bluray",
                    token: "brrip"),

                new StubTypeRule(
                    stubType: "bluray",
                    token: "bd25"),

                new StubTypeRule(
                    stubType: "bluray",
                    token: "bd50"),

                new StubTypeRule(
                    stubType: "vhs",
                    token: "vhs"),

                new StubTypeRule(
                    stubType: "tv",
                    token: "HDTV"),

                new StubTypeRule(
                    stubType: "tv",
                    token: "PDTV"),

                new StubTypeRule(
                    stubType: "tv",
                    token: "DSR")
            };

            VideoFileStackingExpressions = new[]
            {
                "(?<title>.*?)(?<volume>[ _.-]*(?:cd|dvd|p(?:ar)?t|dis[ck])[ _.-]*[0-9]+)(?<ignore>.*?)(?<extension>\\.[^.]+)$",
                "(?<title>.*?)(?<volume>[ _.-]*(?:cd|dvd|p(?:ar)?t|dis[ck])[ _.-]*[a-d])(?<ignore>.*?)(?<extension>\\.[^.]+)$",
                "(?<title>.*?)(?<volume>[ ._-]*[a-d])(?<ignore>.*?)(?<extension>\\.[^.]+)$"
            };

            CleanDateTimes = new[]
            {
                @"(.+[^_\,\.\(\)\[\]\-])[_\.\(\)\[\]\-](19[0-9]{2}|20[0-9]{2})(?![0-9]+|\W[0-9]{2}\W[0-9]{2})([ _\,\.\(\)\[\]\-][^0-9]|).*(19[0-9]{2}|20[0-9]{2})*",
                @"(.+[^_\,\.\(\)\[\]\-])[ _\.\(\)\[\]\-]+(19[0-9]{2}|20[0-9]{2})(?![0-9]+|\W[0-9]{2}\W[0-9]{2})([ _\,\.\(\)\[\]\-][^0-9]|).*(19[0-9]{2}|20[0-9]{2})*"
            };

            CleanStrings = new[]
            {
                @"[ _\,\.\(\)\[\]\-](3d|sbs|tab|hsbs|htab|mvc|HDR|HDC|UHD|UltraHD|4k|ac3|dts|custom|dc|divx|divx5|dsr|dsrip|dutch|dvd|dvdrip|dvdscr|dvdscreener|screener|dvdivx|cam|fragment|fs|hdtv|hdrip|hdtvrip|internal|limited|multisubs|ntsc|ogg|ogm|pal|pdtv|proper|repack|rerip|retail|cd[1-9]|r3|r5|bd5|bd|se|svcd|swedish|german|read.nfo|nfofix|unrated|ws|telesync|ts|telecine|tc|brrip|bdrip|480p|480i|576p|576i|720p|720i|1080p|1080i|2160p|hrhd|hrhdtv|hddvd|bluray|blu-ray|x264|x265|h264|xvid|xvidvd|xxx|www.www|AAC|DTS|\[.*\])([ _\,\.\(\)\[\]\-]|$)",
                @"(\[.*\])"
            };

            SubtitleFileExtensions = new[]
            {
                ".srt",
                ".ssa",
                ".ass",
                ".sub"
            };

            SubtitleFlagDelimiters = new[]
            {
                '.'
            };

            SubtitleForcedFlags = new[]
            {
                "foreign",
                "forced"
            };

            SubtitleDefaultFlags = new[]
            {
                "default"
            };

            AudioFileExtensions = new[]
            {
                ".nsv",
                ".m4a",
                ".flac",
                ".aac",
                ".strm",
                ".pls",
                ".rm",
                ".mpa",
                ".wav",
                ".wma",
                ".ogg",
                ".opus",
                ".mp3",
                ".mp2",
                ".mod",
                ".amf",
                ".669",
                ".dmf",
                ".dsm",
                ".far",
                ".gdm",
                ".imf",
                ".it",
                ".m15",
                ".med",
                ".okt",
                ".s3m",
                ".stm",
                ".sfx",
                ".ult",
                ".uni",
                ".xm",
                ".sid",
                ".ac3",
                ".dts",
                ".cue",
                ".aif",
                ".aiff",
                ".ape",
                ".mac",
                ".mpc",
                ".mp+",
                ".mpp",
                ".shn",
                ".wv",
                ".nsf",
                ".spc",
                ".gym",
                ".adplug",
                ".adx",
                ".dsp",
                ".adp",
                ".ymf",
                ".ast",
                ".afc",
                ".hps",
                ".xsp",
                ".acc",
                ".m4b",
                ".oga",
                ".dsf",
                ".mka"
            };

            EpisodeExpressions = new[]
            {
                #region kodi 标准命名规则开始
                //<!-- foo.s01.e01, foo.s01_e01, S01E02 foo, S01 - E02 -->
                new EpisodeExpression(@".*(\\|\/)(?<seriesname>((?![Ss]([0-9]+)[][ ._-]*[Ee]([0-9]+))[^\\\/])*)?[Ss](?<seasonnumber>[0-9]+)[][ ._-]*[Ee](?<epnumber>[0-9]+)([^\\/]*)$")
                {
                    IsNamed = true
                },
                // <!-- foo.ep01, foo.EP_01 -->
                new EpisodeExpression(@"[\._ -]()[Ee][Pp]_?([0-9]+)([^\\/]*)$"),
                new EpisodeExpression("(?<year>[0-9]{4})[\\.-](?<month>[0-9]{2})[\\.-](?<day>[0-9]{2})", true)
                {
                    DateTimeFormats = new[]
                    {
                        "yyyy.MM.dd",
                        "yyyy-MM-dd",
                        "yyyy_MM_dd"
                    }
                },
                new EpisodeExpression(@"(?<day>[0-9]{2})[.-](?<month>[0-9]{2})[.-](?<year>[0-9]{4})", true)
                {
                    DateTimeFormats = new[]
                    {
                        "dd.MM.yyyy",
                        "dd-MM-yyyy",
                        "dd_MM_yyyy"
                    }
                },
                //这不是一个Kodi命名规则，但是下面的表达式会导致误报
                //要确保这个先测试
                //"Foo Bar 889"
                new EpisodeExpression(@".*[\\\/](?![Ee]pisode)(?<seriesname>[\w\s]+?)\s(?<epnumber>[0-9]{1,3})(-(?<endingepnumber>[0-9]{2,3}))*[^\\\/x]*$")
                {
                    IsNamed = true
                },

                new EpisodeExpression("[\\\\/\\._ \\[\\(-]([0-9]+)x([0-9]+(?:(?:[a-i]|\\.[1-9])(?![0-9]))?)([^\\\\/]*)$")
                {
                    SupportsAbsoluteEpisodeNumbers = true
                },
                //这个也不是Kodi规则，但低于规则也会导致三位数的剧集名出现误报
                //[bar] Foo - 1 [baz] 下面表达式的特殊情况，以防止序列名中数字出现误报
                new EpisodeExpression(@".*?(\[.*?\])+.*?(?<seriesname>[\w\s]+?)[\s_]*-[\s_]*(?<epnumber>[0-9]+).*$")
                {
                    IsNamed = true
                },
                // /server/anything_102.mp4
                // /server/james.corden.2017.04.20.anne.hathaway.720p.hdtv.x264-crooks.mkv
                // /server/anything_1996.11.14.mp4
                new EpisodeExpression(@"[\\/._ -](?<seriesname>(?![0-9]+[0-9][0-9])([^\\\/_])*)[\\\/._ -](?<seasonnumber>[0-9]+)(?<epnumber>[0-9][0-9](?:(?:[a-i]|\.[1-9])(?![0-9]))?)([._ -][^\\\/]*)$")
                {
                    IsOptimistic = true,
                    IsNamed = true,
                    SupportsAbsoluteEpisodeNumbers = false
                },
                new EpisodeExpression("[\\/._ -]p(?:ar)?t[_. -]()([ivx]+|[0-9]+)([._ -][^\\/]*)$")
                {
                    SupportsAbsoluteEpisodeNumbers = true
                },
                #endregion kodi标准命名规则结束
                new EpisodeExpression(@".*(\\|\/)[sS]?(?<seasonnumber>[0-9]+)[xX](?<epnumber>[0-9]+)[^\\\/]*$")
                {
                    IsNamed = true
                },

                new EpisodeExpression(@".*(\\|\/)[sS](?<seasonnumber>[0-9]+)[x,X]?[eE](?<epnumber>[0-9]+)[^\\\/]*$")
                {
                    IsNamed = true
                },

                new EpisodeExpression(@".*(\\|\/)(?<seriesname>((?![sS]?[0-9]{1,4}[xX][0-9]{1,3})[^\\\/])*)?([sS]?(?<seasonnumber>[0-9]{1,4})[xX](?<epnumber>[0-9]+))[^\\\/]*$")
                {
                    IsNamed = true
                },

                new EpisodeExpression(@".*(\\|\/)(?<seriesname>[^\\\/]*)[sS](?<seasonnumber>[0-9]{1,4})[xX\.]?[eE](?<epnumber>[0-9]+)[^\\\/]*$")
                {
                    IsNamed = true
                },
                // "01.avi"
                new EpisodeExpression(@".*[\\\/](?<epnumber>[0-9]+)(-(?<endingepnumber>[0-9]+))*\.\w+$")
                {
                    IsOptimistic = true,
                    IsNamed = true
                },

                // "1-12 episode title"
                new EpisodeExpression(@"([0-9]+)-([0-9]+)"),

                // "01 - blah.avi", "01-blah.avi"
                new EpisodeExpression(@".*(\\|\/)(?<epnumber>[0-9]{1,3})(-(?<endingepnumber>[0-9]{2,3}))*\s?-\s?[^\\\/]*$")
                {
                    IsOptimistic = true,
                    IsNamed = true
                },

                // "01.blah.avi"
                new EpisodeExpression(@".*(\\|\/)(?<epnumber>[0-9]{1,3})(-(?<endingepnumber>[0-9]{2,3}))*\.[^\\\/]+$")
                {
                    IsOptimistic = true,
                    IsNamed = true
                },
                // "blah - 01.avi", "blah 2 - 01.avi", "blah - 01 blah.avi", "blah 2 - 01 blah", "blah - 01 - blah.avi", "blah 2 - 01 - blah"
                new EpisodeExpression(@".*[\\\/][^\\\/]* - (?<epnumber>[0-9]{1,3})(-(?<endingepnumber>[0-9]{2,3}))*[^\\\/]*$")
                {
                    IsOptimistic = true,
                    IsNamed = true
                },

                // "01 episode title.avi"
                new EpisodeExpression(@"[Ss]eason[\._ ](?<seasonnumber>[0-9]+)[\\\/](?<epnumber>[0-9]{1,3})([^\\\/]*)$")
                {
                    IsOptimistic = true,
                    IsNamed = true
                },
                // "Episode 16", "Episode 16 - Title"
                new EpisodeExpression(@".*[\\\/][^\\\/]* (?<epnumber>[0-9]{1,3})(-(?<endingepnumber>[0-9]{2,3}))*[^\\\/]*$")
                {
                    IsOptimistic = true,
                    IsNamed = true
                }
            };

            EpisodeWithoutSeasonExpressions = new[]
            {
                @"[/\._ \-]()([0-9]+)(-[0-9]+)?"
            };

            EpisodeMultiPartExpressions = new[]
            {
                @"^[-_ex]+([0-9]+(?:(?:[a-i]|\\.[1-9])(?![0-9]))?)"
            };

            VideoExtraRules = new[]
            {
                new ExtraRule(
                    ExtraType.Trailer,
                    ExtraRuleType.Filename,
                    "trailer",
                    MediaType.Video),

                new ExtraRule(
                    ExtraType.Trailer,
                    ExtraRuleType.Suffix,
                    "-trailer",
                    MediaType.Video),

                new ExtraRule(
                    ExtraType.Trailer,
                    ExtraRuleType.Suffix,
                    ".trailer",
                    MediaType.Video),

                new ExtraRule(
                    ExtraType.Trailer,
                    ExtraRuleType.Suffix,
                    "_trailer",
                    MediaType.Video),

                new ExtraRule(
                    ExtraType.Trailer,
                    ExtraRuleType.Suffix,
                    " trailer",
                    MediaType.Video),

                new ExtraRule(
                    ExtraType.Sample,
                    ExtraRuleType.Filename,
                    "sample",
                    MediaType.Video),

                new ExtraRule(
                    ExtraType.Sample,
                    ExtraRuleType.Suffix,
                    "-sample",
                    MediaType.Video),

                new ExtraRule(
                    ExtraType.Sample,
                    ExtraRuleType.Suffix,
                    ".sample",
                    MediaType.Video),

                new ExtraRule(
                    ExtraType.Sample,
                    ExtraRuleType.Suffix,
                    "_sample",
                    MediaType.Video),

                new ExtraRule(
                    ExtraType.Sample,
                    ExtraRuleType.Suffix,
                    " sample",
                    MediaType.Video),

                new ExtraRule(
                    ExtraType.ThemeSong,
                    ExtraRuleType.Filename,
                    "theme",
                    MediaType.Audio),

                new ExtraRule(
                    ExtraType.Scene,
                    ExtraRuleType.Suffix,
                    "-scene",
                    MediaType.Video),

                new ExtraRule(
                    ExtraType.Clip,
                    ExtraRuleType.Suffix,
                    "-clip",
                    MediaType.Video),

                new ExtraRule(
                    ExtraType.Interview,
                    ExtraRuleType.Suffix,
                    "-interview",
                    MediaType.Video),

                new ExtraRule(
                    ExtraType.BehindTheScenes,
                    ExtraRuleType.Suffix,
                    "-behindthescenes",
                    MediaType.Video),

                new ExtraRule(
                    ExtraType.DeletedScene,
                    ExtraRuleType.Suffix,
                    "-deleted",
                    MediaType.Video),

                new ExtraRule(
                    ExtraType.Clip,
                    ExtraRuleType.Suffix,
                    "-featurette",
                    MediaType.Video),

                new ExtraRule(
                    ExtraType.Clip,
                    ExtraRuleType.Suffix,
                    "-short",
                    MediaType.Video),

                new ExtraRule(
                    ExtraType.BehindTheScenes,
                    ExtraRuleType.DirectoryName,
                    "behind the scenes",
                    MediaType.Video),

                new ExtraRule(
                    ExtraType.DeletedScene,
                    ExtraRuleType.DirectoryName,
                    "deleted scenes",
                    MediaType.Video),

                new ExtraRule(
                    ExtraType.Interview,
                    ExtraRuleType.DirectoryName,
                    "interviews",
                    MediaType.Video),

                new ExtraRule(
                    ExtraType.Scene,
                    ExtraRuleType.DirectoryName,
                    "scenes",
                    MediaType.Video),

                new ExtraRule(
                    ExtraType.Sample,
                    ExtraRuleType.DirectoryName,
                    "samples",
                    MediaType.Video),

                new ExtraRule(
                    ExtraType.Clip,
                    ExtraRuleType.DirectoryName,
                    "shorts",
                    MediaType.Video),

                new ExtraRule(
                    ExtraType.Clip,
                    ExtraRuleType.DirectoryName,
                    "featurettes",
                    MediaType.Video),

                new ExtraRule(
                    ExtraType.Unknown,
                    ExtraRuleType.DirectoryName,
                    "extras",
                    MediaType.Video),
            };

            Format3DRules = new[]
            {
                // Kodi rules:
                new Format3DRule(
                    precedingToken: "3d",
                    token: "hsbs"),

                new Format3DRule(
                    precedingToken: "3d",
                    token: "sbs"),

                new Format3DRule(
                    precedingToken: "3d",
                    token: "htab"),

                new Format3DRule(
                    precedingToken: "3d",
                    token: "tab"),

                 // Media Browser rules:
                new Format3DRule("fsbs"),
                new Format3DRule("hsbs"),
                new Format3DRule("sbs"),
                new Format3DRule("ftab"),
                new Format3DRule("htab"),
                new Format3DRule("tab"),
                new Format3DRule("sbs3d"),
                new Format3DRule("mvc")
            };

            var extensions = VideoFileExtensions.ToList();

            extensions.AddRange(new[]
            {
                ".mkv",
                ".m2t",
                ".m2ts",
                ".img",
                ".iso",
                ".mk3d",
                ".ts",
                ".rmvb",
                ".mov",
                ".avi",
                ".mpg",
                ".mpeg",
                ".wmv",
                ".mp4",
                ".divx",
                ".dvr-ms",
                ".wtv",
                ".ogm",
                ".ogv",
                ".asf",
                ".m4v",
                ".flv",
                ".f4v",
                ".3gp",
                ".webm",
                ".mts",
                ".m2v",
                ".rec",
                ".mxf"
            });

            MultipleEpisodeExpressions = new[]
            {
                @".*(\\|\/)[sS]?(?<seasonnumber>[0-9]{1,4})[xX](?<epnumber>[0-9]{1,3})((-| - )[0-9]{1,4}[eExX](?<endingepnumber>[0-9]{1,3}))+[^\\\/]*$",
                @".*(\\|\/)[sS]?(?<seasonnumber>[0-9]{1,4})[xX](?<epnumber>[0-9]{1,3})((-| - )[0-9]{1,4}[xX][eE](?<endingepnumber>[0-9]{1,3}))+[^\\\/]*$",
                @".*(\\|\/)[sS]?(?<seasonnumber>[0-9]{1,4})[xX](?<epnumber>[0-9]{1,3})((-| - )?[xXeE](?<endingepnumber>[0-9]{1,3}))+[^\\\/]*$",
                @".*(\\|\/)[sS]?(?<seasonnumber>[0-9]{1,4})[xX](?<epnumber>[0-9]{1,3})(-[xE]?[eE]?(?<endingepnumber>[0-9]{1,3}))+[^\\\/]*$",
                @".*(\\|\/)(?<seriesname>((?![sS]?[0-9]{1,4}[xX][0-9]{1,3})[^\\\/])*)?([sS]?(?<seasonnumber>[0-9]{1,4})[xX](?<epnumber>[0-9]{1,3}))((-| - )[0-9]{1,4}[xXeE](?<endingepnumber>[0-9]{1,3}))+[^\\\/]*$",
                @".*(\\|\/)(?<seriesname>((?![sS]?[0-9]{1,4}[xX][0-9]{1,3})[^\\\/])*)?([sS]?(?<seasonnumber>[0-9]{1,4})[xX](?<epnumber>[0-9]{1,3}))((-| - )[0-9]{1,4}[xX][eE](?<endingepnumber>[0-9]{1,3}))+[^\\\/]*$",
                @".*(\\|\/)(?<seriesname>((?![sS]?[0-9]{1,4}[xX][0-9]{1,3})[^\\\/])*)?([sS]?(?<seasonnumber>[0-9]{1,4})[xX](?<epnumber>[0-9]{1,3}))((-| - )?[xXeE](?<endingepnumber>[0-9]{1,3}))+[^\\\/]*$",
                @".*(\\|\/)(?<seriesname>((?![sS]?[0-9]{1,4}[xX][0-9]{1,3})[^\\\/])*)?([sS]?(?<seasonnumber>[0-9]{1,4})[xX](?<epnumber>[0-9]{1,3}))(-[xX]?[eE]?(?<endingepnumber>[0-9]{1,3}))+[^\\\/]*$",
                @".*(\\|\/)(?<seriesname>[^\\\/]*)[sS](?<seasonnumber>[0-9]{1,4})[xX\.]?[eE](?<epnumber>[0-9]{1,3})((-| - )?[xXeE](?<endingepnumber>[0-9]{1,3}))+[^\\\/]*$",
                @".*(\\|\/)(?<seriesname>[^\\\/]*)[sS](?<seasonnumber>[0-9]{1,4})[xX\.]?[eE](?<epnumber>[0-9]{1,3})(-[xX]?[eE]?(?<endingepnumber>[0-9]{1,3}))+[^\\\/]*$"
            }.Select(i => new EpisodeExpression(i)
            {
                IsNamed = true
            }).ToArray();

            VideoFileExtensions = extensions
                .Distinct(StringComparer.OrdinalIgnoreCase)
                .ToArray();

            Compile();
        }

        /// <summary>
        /// 设置或获取音频文件扩展名列表
        /// </summary>
        public string[] AudioFileExtensions { get; set; }

        /// <summary>
        /// 获取或设置字幕文件扩展名列表
        /// </summary>
        public string[] SubtitleFileExtensions { get; set; }

        /// <summary>
        /// 获取或设置字幕标志分隔符的列表
        /// </summary>
        public char[] SubtitleFlagDelimiters { get; set; }

        /// <summary>
        /// 获取或设置字幕强制标志列表
        /// </summary>
        public string[] SubtitleForcedFlags { get; set; }

        /// <summary>
        /// 获取或设置字幕默认标志的列表
        /// </summary>
        public string[] SubtitleDefaultFlags { get; set; }

        /// <summary>
        /// 获取或设置"集"正则表达式的列表
        /// </summary>
        public EpisodeExpression[] EpisodeExpressions { get; set; }

        /// <summary>
        /// 获取或设置不包含"季"正则表达式字符串的原始"集"列表
        /// </summary>
        public string[] EpisodeWithoutSeasonExpressions { get; set; }

        /// <summary>
        /// 获取或设置多部的原始"集"正则表达式字符串列表
        /// </summary>
        public string[] EpisodeMultiPartExpressions { get; set; }

        /// <summary>
        /// 获取或设置视频文件扩展名列表
        /// </summary>
        public string[] VideoFileExtensions { get; set; }

        /// <summary>
        /// 获取或设置视频存根文件扩展名列表
        /// </summary>
        public string[] StubFileExtensions { get; set; }

        /// <summary>
        /// 获取或设置视频标志分隔符列表
        /// </summary>
        public char[] VideoFlagDelimiters { get; set; }

        /// <summary>
        /// 获取或设置3D格式规则列表
        /// </summary>
        public Format3DRule[] Format3DRules { get; set; }

        /// <summary>
        /// 获取或设置原始视频文件堆叠表达式字符串的列表
        /// </summary>
        public string[] VideoFileStackingExpressions { get; set; }

        /// <summary>
        /// 获取或设置原始干净的DateTimes正则表达式字符串列表
        /// </summary>
        public string[] CleanDateTimes { get; set; }

        /// <summary>
        /// 获取或设置原始干净字符串正则表达式的列表
        /// </summary>
        public string[] CleanStrings { get; set; }

        /// <summary>
        /// 获取或设置"多集"正则表达式的列表
        /// </summary>
        public EpisodeExpression[] MultipleEpisodeExpressions { get; set; }

        /// <summary>
        /// 获取或设置视频的额外规则列表
        /// </summary>
        public ExtraRule[] VideoExtraRules { get; set; }

        public StubTypeRule[] StubTypes { get; set; }

        /// <summary>
        /// 获取视频文件堆栈正则表达式列表
        /// </summary>
        public Regex[] VideoFileStackingRegexes { get; private set; } = Array.Empty<Regex>();

        /// <summary>
        /// 获取干净日期时间正则表达式的列表
        /// </summary>
        public Regex[] CleanDateTimeRegexes { get; private set; } = Array.Empty<Regex>();

        /// <summary>
        /// 获取干净字符串正则表达式的列表
        /// </summary>
        public Regex[] CleanStringRegexes { get; private set; } = Array.Empty<Regex>();

        /// <summary>
        /// 获取没有"季"正则表达式的"集"列表
        /// </summary>
        public Regex[] EpisodeWithoutSeasonRegexes { get; private set; } = Array.Empty<Regex>();

        /// <summary>
        /// 获取多部分章节正则表达式的列表
        /// </summary>
        public Regex[] EpisodeMultiPartRegexes { get; private set; } = Array.Empty<Regex>();

        /// <summary>
        /// 将原始正则表达式字符串编译为正则表达式
        /// </summary>
        public void Compile()
        {
            VideoFileStackingRegexes = VideoFileStackingExpressions.Select(Compile).ToArray();
            CleanDateTimeRegexes = CleanDateTimes.Select(Compile).ToArray();
            CleanStringRegexes = CleanStrings.Select(Compile).ToArray();
            EpisodeWithoutSeasonRegexes = EpisodeWithoutSeasonExpressions.Select(Compile).ToArray();
            EpisodeMultiPartRegexes = EpisodeMultiPartExpressions.Select(Compile).ToArray();
        }

        private Regex Compile(string exp)
        {
            return new Regex(exp, RegexOptions.IgnoreCase | RegexOptions.Compiled);
        }
    }
}
