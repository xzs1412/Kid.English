using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
            // as IReadOnlyList<Phrase>;
            var phraseListOutput = new PhraseListOutput();
            phraseListOutput.Items = Mapper.Map<IReadOnlyList<PhraseDto>>(phrases);
            phraseListOutput.TotalCount = _phraseRepository.Count();

            return phraseListOutput;
        }

        public void CreatePhrase(PhraseCreateDto input)
        {
            var phrase = Mapper.Map<Phrase>(input);
            phrase.Id = Guid.NewGuid();
            _phraseRepository.Insert(phrase);
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
            //这句Map之后,update的话异常,提示Id已经存在
            //phrase = Mapper.Map<Phrase>(input);
            _phraseRepository.Update(phrase);
        }

        public void DeletePhrase(string id)
        {
            _phraseRepository.Delete(new Guid(id));
        }

        public PhraseListOutput SearchPhrases(PhraseListInput input)
        {
            var count = 0;
            var phrases = _phraseRepository.SearchPhrases(out count, input.EnglishKeyWords,
                input.ChineseKeyWords, input.PageIndex, input.MaxResultCount);
            var phraseListOutput = new PhraseListOutput
            {
                TotalCount = count,
                Items = Mapper.Map<List<PhraseDto>>(phrases)
            };
            return phraseListOutput;
        }

        public async Task<PhraseListOutput> SearchPhrasesAsync(PhraseListInput input)
        {
            var phrasesAndCount =
                await
                    _phraseRepository.SearchPhrasesAsync(input.EnglishKeyWords, input.ChineseKeyWords, input.PageIndex,
                        input.MaxResultCount);

            var phraseListOutput = new PhraseListOutput
            {
                TotalCount = phrasesAndCount.Item1,
                Items = Mapper.Map<List<PhraseDto>>(phrasesAndCount.Item2)
            };
            return phraseListOutput;
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
    }
}