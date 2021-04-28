using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WallPoster.Models;
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
        /// Gets or sets the token to use for matching against the file path.
        /// </summary>
        public string Token { get; set; }

        /// <summary>
        /// Gets or sets the type of the extra to return when matched.
        /// </summary>
        public ExtraType ExtraType { get; set; }

        /// <summary>
        /// Gets or sets the type of the rule.
        /// </summary>
        public ExtraRuleType RuleType { get; set; }

        /// <summary>
        /// Gets or sets the type of the media to return when matched.
        /// </summary>
        public MediaType MediaType { get; set; }
    }
}
