using System.Text.RegularExpressions;

namespace WallPoster.Helper
{
    public class StringHelper
    {
        public string GetCaption(string fileName)
        {
            string tempStr = Regex.Match(fileName, @"^\[([^]]*)\]").Value;
            return tempStr;
        }
    }
}
