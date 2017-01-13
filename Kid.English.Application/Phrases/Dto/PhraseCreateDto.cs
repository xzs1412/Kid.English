using Abp.AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kid.English.Phrases.Dto
{
    [AutoMapTo(typeof(Phrase))]
    public class PhraseCreateDto
    {
        public string SentenceHtml { get; set; }
        public string ChineseMean { get; set; }
        public string Sentence { get; set; }
    }
}