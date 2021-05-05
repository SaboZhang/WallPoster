using System.Text.RegularExpressions;

namespace WallPoster.Helper
{
    /// <summary>
    /// 中文字符串操作
    /// </summary>
    public class StringHelper
    {
        public string CleanCaption(string fileName)
        {
            if (string.IsNullOrEmpty(fileName))
            {
                return "";
            }
            bool isCN = Regex.IsMatch(fileName, @"[\u4e00-\u9fa5]");
            if (isCN)
            {
                string newName = Regex.Replace(fileName, @"[a-z].*", "", RegexOptions.IgnoreCase);
                return newName.TrimEnd('.');
            }
            return fileName;
        }
    }
}
