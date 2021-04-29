using WallPoster.Helper.Common;

namespace WallPoster.Helper.Video
{
    /// <summary>
    /// 一个用于匹配文件路径的规则
    /// </summary>
    public class ExtraRule
    {
        public ExtraRule(ExtraType extraType, ExtraRuleType ruleType, string token, MediaType mediaType)
        {
            Token = token;
            ExtraType = extraType;
            RuleType = ruleType;
            MediaType = mediaType;
        }

        /// <summary>
        /// 获取或设置用于与文件路径匹配的令牌
        /// </summary>
        public string Token { get; set; }

        /// <summary>
        /// 获取或设置匹配时要返回的额外值的类型
        /// </summary>
        public ExtraType ExtraType { get; set; }

        /// <summary>
        /// 获取或设置规则的类型
        /// </summary>
        public ExtraRuleType RuleType { get; set; }

        /// <summary>
        /// 获取或设置匹配时要返回的媒体类型
        /// </summary>
        public MediaType MediaType { get; set; }
    }
}
