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


        public virtual string InterfaceLanguage { get; set; } = "zh-CN";
        public virtual string StoreLocation { get; set; } = Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + @"\";
        public virtual bool IsFirstRun { get; set; } = true;
        public virtual bool IsBackEnabled { get; set; } = true;
        public virtual NavigationViewPaneDisplayMode PaneDisplayMode { get; set; } = NavigationViewPaneDisplayMode.Left;
        public virtual ApplicationTheme Theme { get; set; } = ApplicationTheme.Light;
        public virtual Brush Accent { get; set; }

        public ISettings()
        {

        }

        public ISettings(string fileName) : base(fileName)
        {
        }
    }
}
