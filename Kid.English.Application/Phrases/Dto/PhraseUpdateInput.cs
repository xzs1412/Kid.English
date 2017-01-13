using Abp.AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kid.English.Phrases.Dto
{
    /// <summary>
    /// Contains four properties
    /// </summary>
    [AutoMap(typeof(Phrase))]
    public class PhraseUpdateInput:PhraseDto
    {
        public string Sentence { get; set; }
    }
}