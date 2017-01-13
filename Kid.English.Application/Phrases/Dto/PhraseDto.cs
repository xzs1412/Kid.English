using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kid.English.Phrases.Dto
{
    /// <summary>
    /// Contains three properties:Id,SentenceHtml,ChineseMean
    /// </summary>
    [AutoMapFrom(typeof(Phrase))]
    public class PhraseDto : EntityDto<Guid>
    {
        public string SentenceHtml { get; set; }
        public string ChineseMean { get; set; }
    }
}
