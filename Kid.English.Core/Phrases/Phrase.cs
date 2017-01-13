using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using Kid.English.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kid.English.Phrases
{
    public class Phrase :EnglishEntityBase
    {
        public virtual string Sentence { get; set; }

        public virtual string SentenceHtml { get; set; }

        public virtual  string ChineseMean { get; set; }

    }
}
