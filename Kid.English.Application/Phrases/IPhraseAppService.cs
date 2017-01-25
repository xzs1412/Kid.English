using Abp.Application.Services;
using Kid.English.Phrases.Dto;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Kid.English.Phrases
{
    public interface IPhraseAppService: IApplicationService
    {
        PhraseListOutput GetPhrases(PhraseListInput input);

        void CreatePhrase(PhraseCreateDto input);

        PhraseDto GetPhraseById(Guid id);

        PhraseDto GetPhraseById(string id);

        void UpdatePhrase(PhraseUpdateInput input);

        void DeletePhrase(string id);

        PhraseListOutput SearchPhrases(PhraseListInput input);

        Task<PhraseListOutput> SearchPhrasesAsync(PhraseListInput input);

        /// <summary>
        /// 获取包含被软删除的记录
        /// Get the entities including SoftDelete
        /// </summary>
        /// <returns></returns>
        List<PhraseDto> GetAllDeletedPhrases();

         int  GetCount();
    }
}
