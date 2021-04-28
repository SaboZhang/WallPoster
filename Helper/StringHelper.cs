using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

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
