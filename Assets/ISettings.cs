using HandyControl.Themes;
using ModernWpf.Controls;
using nucs.JsonSettings;
using System;
using System.Windows.Media;

namespace WallPoster.Assets
{
    public class ISettings : JsonSettings
    {
        public override string FileName { get; set; } = Consts.ConfigPath;

        public virtual int Version { get; set; } = 0;


        public virtual string InterfaceLanguage { get; set; } = "zh-CN";
        public virtual string StoreLocation { get; set; } = Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + @"\";
        public virtual bool IsFirstRun { get; set; } = true;
        public virtual bool IsBackEnabled { get; set; } = true;
        public virtual NavigationViewPaneDisplayMode PaneDisplayMode { get; set; } = NavigationViewPaneDisplayMode.LeftCompact;
        public virtual ApplicationTheme Theme { get; set; } = ApplicationTheme.Light;
        public virtual Brush Accent { get; set; }
        public virtual string AppSecret { get; set; } = Consts.WeatherKey;
        public virtual string Location { get; set; } = "101030100";

        public ISettings()
        {
            Version = 0;
        }

        public ISettings(string fileName) : base(fileName)
        {
        }
    }
}
