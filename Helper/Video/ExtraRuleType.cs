namespace WallPoster.Helper.Video
{
    public enum ExtraRuleType
    {
        /// <summary>
        ///  <see cref="ExtraRule.Token"/> 对应文件名中的后缀
        /// </summary>
        Suffix = 0,

        /// <summary>
        /// <see cref="ExtraRule.Token"/> 标记针对文件名，不包括文件扩展名
        /// </summary>
        Filename = 1,

        /// <summary>
        /// <see cref="ExtraRule.Token"/> 针对文件名，包括文件扩展名
        /// </summary>
        Regex = 2,

        /// <summary>
        /// <see cref="ExtraRule.Token"/> 针对包含文件的目录名
        /// </summary>
        DirectoryName = 3
    }
}
