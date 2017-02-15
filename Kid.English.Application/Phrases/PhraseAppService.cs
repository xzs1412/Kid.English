using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Transactions;
using Abp.Domain.Uow;
using AutoMapper;
using Kid.English.Phrases.Dto;

namespace Kid.English.Phrases
{
    public class PhraseAppService : EnglishAppServiceBase, IPhraseAppService
    {
        private readonly IPhraseRepository _phraseRepository;

        public PhraseAppService(IPhraseRepository phraseRepository)
        {
            _phraseRepository = phraseRepository;
        }


        /// <summary>
        ///     implement IPhraseAppService
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public PhraseListOutput GetPhrases(PhraseListInput input)
        {
            var phrases =
                _phraseRepository.GetAll().OrderByDescending(p => p.CreationTime).Take(input.MaxResultCount).ToList();

            return new PhraseListOutput()
            {
                Items = Mapper.Map<IReadOnlyList<PhraseDto>>(phrases),
                TotalCount = _phraseRepository.Count()
            };
        }

 
        public void CreatePhrase(PhraseCreateDto input)
        {  
            using (var uow=UnitOfWorkManager.Begin(TransactionScopeOption.Suppress))
            {
                var phrase = Mapper.Map<Phrase>(input);
                phrase.Id = Guid.NewGuid();
                _phraseRepository.Insert(phrase);          
                uow.Complete();
            }
            throw new Exception($"the exception inner {nameof(CreatePhrase)}");
        }


        public PhraseDto GetPhraseById(Guid id)
        {
            var phrase = _phraseRepository.Get(id);
            return Mapper.Map<PhraseDto>(phrase);
        }

        public PhraseDto GetPhraseById(string id)
        {
            return GetPhraseById(new Guid(id));
        }


        public void UpdatePhrase(PhraseUpdateInput input)
        {
            var phrase = _phraseRepository.Get(input.Id);
            phrase.Id = input.Id;
            phrase.SentenceHtml = input.SentenceHtml;
            phrase.ChineseMean = input.ChineseMean;
            phrase.Sentence = input.Sentence;
            //phrase = Mapper.Map<Phrase>(input);//这句Map之后,update的话异常,提示Id已经存在
            _phraseRepository.Update(phrase);
        }

        public void DeletePhrase(string id)
        {
            _phraseRepository.Delete(new Guid(id));
        }

        public PhraseListOutput SearchPhrases(PhraseListInput input)
        {
            int count;
            var phrases = _phraseRepository.SearchPhrases(out count, input.EnglishKeyWords,
                input.ChineseKeyWords, input.PageIndex, input.MaxResultCount);
            return new PhraseListOutput
            {
                TotalCount = count,
                Items = Mapper.Map<List<PhraseDto>>(phrases)
            };
        }

        public async Task<PhraseListOutput> SearchPhrasesAsync(PhraseListInput input)
        {
            var phrasesAndCount =
                await
                    _phraseRepository.SearchPhrasesAsync(input.EnglishKeyWords, input.ChineseKeyWords, input.PageIndex,
                        input.MaxResultCount);

            return new PhraseListOutput
            {
                TotalCount = phrasesAndCount.Item1,
                Items = Mapper.Map<List<PhraseDto>>(phrasesAndCount.Item2)
            };
        }

        /// <summary>
        ///     获取包含被软删除的记录
        ///     Get the entities including SoftDelete
        /// </summary>
        /// <returns></returns>
        public List<PhraseDto> GetAllDeletedPhrases()
        {
            using (UnitOfWorkManager.Current.DisableFilter(AbpDataFilters.SoftDelete))
            {
                var phrases = _phraseRepository.GetAll().Where(p => p.IsDeleted).ToList();
                return Mapper.Map<List<PhraseDto>>(phrases);
            }
        }

        public int GetCount()
        {
            return _phraseRepository.GetAll().Count();
        }
    }
}