using HandyControl.Themes;
using ModernWpf.Controls;
using nucs.JsonSettings;
using System.Collections;
using System.Collections.Generic;
using System.Windows.Media;

namespace WallPoster.Assets
{
    public class ISettings : JsonSettings
    {
        public override string FileName { get; set; } = Consts.ConfigPath;

        public virtual int Version { get; set; } = 0;
        public static ArrayList SourcePath = new ArrayList();


        public virtual string InterfaceLanguage { get; set; } = "zh-CN";
        public virtual bool IsFirstRun { get; set; } = true;
        public virtual bool IsBackEnabled { get; set; } = true;
        public virtual List<string> MovieLocation { get; set; } = new List<string>();
        public virtual List<string> TVLocation { get; set; } = new List<string>();
        public virtual NavigationViewPaneDisplayMode PaneDisplayMode { get; set; } = NavigationViewPaneDisplayMode.LeftCompact;
        public virtual ApplicationTheme Theme { get; set; } = ApplicationTheme.Light;
        public virtual Brush Accent { get; set; }
        public virtual string AppSecret { get; set; } = Consts.WeatherKey;
        public virtual string Location { get; set; } = "101030500";

        public ISettings()
        {
            Version = 0;
        }

        public ISettings(string fileName) : base(fileName)
        {
        }
    }
}
