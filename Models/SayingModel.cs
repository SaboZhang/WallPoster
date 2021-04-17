namespace WallPoster.Models
{
    public class SayingModel
    {
        /// <summary>
        /// 状态码
        /// </summary>
        public string code { get; set; }
        /// <summary>
        /// 数据
        /// </summary>
        public Data data { get; set; }
    }

    public class Data
    {
        /// <summary>
        /// id
        /// </summary>
        public string id { get; set; }
        /// <summary>
        /// 分类
        /// </summary>
        public string tag { get; set; }
        /// <summary>
        /// 源
        /// </summary>
        public string origin { get; set; }
        /// <summary>
        /// 内容
        /// </summary>
        public string content { get; set; }
        /// <summary>
        /// 时间
        /// </summary>
        public string datetime { get; set; }
    }
}
