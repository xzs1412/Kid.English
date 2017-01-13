using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kid.English.Phrases.Dto
{
   
    public class PhraseListOutput : PagedResultDto<PhraseDto>
    {
        //public Guid Id { get; set; }
        //public string SentenceHtml { get; set; }
        //public string ChineseMean { get; set; }

    }


}
