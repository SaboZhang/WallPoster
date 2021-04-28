using System;
using System.Text.RegularExpressions;

namespace WallPoster.Helper.Common
{
    /// <summary>
    /// 解析电视剧的正则表达式
    /// </summary>
    public class EpisodeExpression
    {
        private string _expression;
        private Regex? _regex;

        public EpisodeExpression(string expression, bool byDate = false)
        {
            _expression = expression;
            IsByDate = byDate;
            DateTimeFormats = Array.Empty<string>();
            SupportsAbsoluteEpisodeNumbers = true;
        }

        /// <summary>
        /// 获取或设置原始表达式字符串
        /// </summary>
        public string Expression
        {
            get => _expression;
            set
            {
                _expression = value;
                _regex = null;
            }
        }

        /// <summary>
        /// 获取或设置一个值，该值表示是否获取或设置指示是否可以在表达式中找到日期的属性
        /// </summary>
        public bool IsByDate { get; set; }

        /// <summary>
        /// 获取或设置一个值，该值指示是否获取或设置指示表达式是否乐观的属性
        /// </summary>
        public bool IsOptimistic { get; set; }

        /// <summary>
        /// 获取或设置一个值，该值指示是否获取或设置指示是否命名表达式的属性
        /// </summary>
        public bool IsNamed { get; set; }

        /// <summary>
        /// 获取或设置一个值，该值指示是否获取或设置属性，该属性指示表达式是否支持带有绝对数字的"集"
        /// </summary>
        public bool SupportsAbsoluteEpisodeNumbers { get; set; }

        /// <summary>
        /// 获取或设置用于日期解析的可选日期格式列表
        /// </summary>
        public string[] DateTimeFormats { get; set; }

        /// <summary>
        /// 获取一个<see cref="Regex"/>表达式对象(如果为null则创建它)
        /// </summary>
        public Regex Regex => _regex ??= new Regex(Expression, RegexOptions.IgnoreCase | RegexOptions.Compiled);
    }
}