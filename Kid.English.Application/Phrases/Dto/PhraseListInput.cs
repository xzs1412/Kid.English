using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kid.English.Phrases.Dto
{
    /// <summary>
    /// Hirence from 
    /// </summary>
    public class PhraseListInput : PagedResultRequestDto
    {
        public string EnglishKeyWords { get; set; }
        public string ChineseKeyWords { get; set; }

        public int PageIndex { get; set; }

        /// <summary>
        /// skipCount=0;maxResultCount=10;
        /// </summary>
        public PhraseListInput()
            : this(0, 10)
        {

        }

        public PhraseListInput(int skipCount, int maxResultCount)
        {
            this.SkipCount = skipCount;
            this.MaxResultCount = MaxResultCount;
        }


    }
}
