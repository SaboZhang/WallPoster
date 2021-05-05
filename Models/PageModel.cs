namespace WallPoster.Models
{
    /// <summary>
    /// 分页数据
    /// </summary>
    public class PageModel
    {
        public int PageIndex { get; set; }

        public int PageCount { get; set; }

        public int PageSize { get; set; }

        public int MaxPageCount { get; set; }

        public string Status { get; set; }
    }
}
