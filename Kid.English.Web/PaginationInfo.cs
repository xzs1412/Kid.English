using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Kid.English.Web
{
    public class PaginationInfo
    {
        public int PageIndex { get; set; }
        public int PageSize { get; set; }
        public int TotalCount { get; set; }
        public string EnglishKeyWords { get; set; }
        public string ChineseKeyWords { get; set; }

    }
}