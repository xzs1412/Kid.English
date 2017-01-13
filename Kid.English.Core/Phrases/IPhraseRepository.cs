using Abp.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kid.English.Phrases
{
    public interface IPhraseRepository : IRepository<Phrase, Guid>
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="count">The result count</param>
        /// <param name="englishKeyWords"></param>
        /// <param name="chineseKeyWords"></param>
        /// <param name="pageIndex">base on 1,default:1</param>
        /// <param name="pageSize">default:10</param>
        /// <returns></returns>
        List<Phrase> SearchPhrases(out int count, string englishKeyWords, string chineseKeyWords, int pageIndex, int pageSize);

         Task<Tuple<int,List<Phrase>>> SearchPhrasesAsync(string englishKeyWords, string chineseKeyWords, int pageIndex, int pageSize);
    }
}
