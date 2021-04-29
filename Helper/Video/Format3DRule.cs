namespace WallPoster.Helper.Video
{
    /// <summary>
    /// 3D视频规则
    /// </summary>
    public class Format3DRule
    {
        public Format3DRule(string token, string? precedingToken = null)
        {
            Token = token;
            PrecedingToken = precedingToken;
        }

        public string Token { get; set; }

        public string? PrecedingToken { get; set; }
    }
}
