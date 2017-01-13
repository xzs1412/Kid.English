using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;

namespace Kid.English.Web
{
    public class Common
    {
        public static string ReplaceHtmlMark(string html)
        {
            return Regex.Replace(html, @"<[^>]*>", String.Empty);
        }
    }
}