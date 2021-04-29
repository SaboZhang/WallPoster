namespace WallPoster.Helper.Video
{
    /// <summary>
    /// 用于从ExtraResolver传递结果的Holder对象
    /// </summary>
    public class ExtraResult
    {
        public ExtraType? ExtraType { get; set; }

        public ExtraRule? Rule { get; set; }
    }
}
