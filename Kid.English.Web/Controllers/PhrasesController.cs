using System;
using System.Threading.Tasks;
using System.Web.Mvc;
using Abp.Authorization;
using Kid.English.BackgroundJobs;
using Kid.English.Phrases;
using Kid.English.Phrases.Dto;

namespace Kid.English.Web.Controllers
{
    public class PhrasesController : EnglishControllerBase
    {
        private readonly IEmailAppService _emailAppService;
        private readonly IPhraseAppService _phraseAppService;

        // GET: Phrases

        public PhrasesController(IPhraseAppService phraseAppService, IEmailAppService emailAppService)
        {
            _phraseAppService = phraseAppService;
            _emailAppService = emailAppService;
        }

        public async Task<ActionResult> Index(int pageIndex = 1, string englishKeyWords = "",
            string chineseKeyWords = "")
        {
            CheckModelState();
            //var fullName = AbpSession.FullName;
            return await Search(englishKeyWords, chineseKeyWords, pageIndex);
            //PhraseListInput input = new Phrases.Dto.PhraseListInput
            //{
            //    ChineseKeyWords = chineseKeyWords,
            //    EnglishKeyWords = englishKeyWords,
            //    MaxResultCount = 10,
            //    PageIndex = pageIndex,
            //    SkipCount = (pageIndex - 1) * 10
            //};

            //ViewBag.pageIndex = pageIndex;
            //ViewBag.englishKeyWords = englishKeyWords;
            //ViewBag.chineseKeyWords = chineseKeyWords;

            //PhraseListOutput phraseListOutput = _phraseAppService.SearchPhrases(input);
            //return View(phraseListOutput);
        }


        [AbpAuthorize]
        public ActionResult Create()
        {
            return View();
        }

        [AbpAuthorize]
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Create(FormCollection fc)
        {
            CheckModelState();

            if ((fc.Get("editorValue") != null) && (fc.Get("ChineseMean") != null))
            {
                //ueditor有时会在最后多出一个br换行,需要去掉.
                var sentenceHtml = fc.Get("editorValue");

                var phrase = new PhraseCreateDto
                {
                    ChineseMean = fc.Get("ChineseMean"),
                    SentenceHtml = sentenceHtml,
                    //1.去掉Html标签 2.把单引号,双引号等被转义的字符转回来.
                    Sentence = Server.HtmlDecode(Common.ReplaceHtmlMark(sentenceHtml))
                };
                _phraseAppService.CreatePhrase(phrase);
            }

            return View("Create");
        }

        /// <summary>
        /// </summary>
        /// <param name="id"></param>
        /// <param name="pageIndex">base on 1</param>
        /// <param name="chineseKeyWords"></param>
        /// <param name="englishKeyWords"></param>
        /// <returns></returns>
        [AbpAuthorize]
        public ActionResult Edit(string id, int pageIndex = 1, string chineseKeyWords = "", string englishKeyWords = "")
        {
            CheckModelState();


            var phrase = _phraseAppService.GetPhraseById(id);

            //把"换成\",为了使Uediotr的setContent方法能把Html正确解析,并显示.
            phrase.SentenceHtml = phrase.SentenceHtml.Replace("\"", "\\\"");

            ViewBag.chineseKeyWords = chineseKeyWords;
            ViewBag.englishKeyWords = englishKeyWords;
            ViewBag.pageIndex = pageIndex;
            return View(phrase);
        }

        [AbpAuthorize]
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Edit(FormCollection fc)
        {
            CheckModelState();


            var sentenceHtml = fc.Get("editorValue");
            var input = new PhraseUpdateInput
            {
                Id = new Guid(fc.Get("ID")),
                SentenceHtml = sentenceHtml,
                ChineseMean = fc.Get("ChineseMean"),
                //1.去掉Html标签 2.把单引号,双引号等被转义的字符转回来.
                Sentence = Server.HtmlDecode(Common.ReplaceHtmlMark(sentenceHtml))
            };

            _phraseAppService.UpdatePhrase(input);

            return RedirectToAction("Index", "Phrases", new
            {
                chineseKeyWords = fc.Get("chineseKeyWords"),
                englishKeyWords = fc.Get("englishKeyWords"),
                pageIndex = fc.Get("pageIndex")
            });
        }


        [AbpAuthorize]
        [HttpPost]
        public JsonResult Delete(string id)
        {
            _phraseAppService.DeletePhrase(id);

            var data = new {message = L("DeleteSuccess")};
            var result = new JsonResult();
            result.Data = data;
            return result;
        }

        public async Task<ActionResult> Search(string englishKeyWords = "", string chineseKeyWords = "",
            int pageIndex = 1, int pageSize = 10)
        {
            CheckModelState();

            var input = new PhraseListInput
            {
                EnglishKeyWords = englishKeyWords,
                ChineseKeyWords = chineseKeyWords,
                PageIndex = pageIndex,
                MaxResultCount = pageSize,
                SkipCount = (pageSize - 1)*pageSize
            };

            //PhraseListOutput phraseListOutput = _phraseAppService.SearchPhrases(input);
            var phraseListOutput = await _phraseAppService.SearchPhrasesAsync(input);
            SetViewBagOfSearch(englishKeyWords, chineseKeyWords, pageIndex, phraseListOutput.TotalCount);

            return View("Index", phraseListOutput);
        } //end action

        private void SetViewBagOfSearch(string englishKeyWords, string chineseKeyWords, int pageIndex, int count)
        {
            ViewBag.englishKeyWords = englishKeyWords;
            ViewBag.chineseKeyWords = chineseKeyWords;
            ViewBag.pageIndex = pageIndex;
            ViewBag.count = count;
            //作为登录之后跳转回来的Url
            ViewBag.returnUrl = Url.Action("Index", "Phrases",
                new
                {
                    englishKeyWords,
                    chineseKeyWords,
                    pageIndex
                });
        }

        [AbpAuthorize]
        public ActionResult AllDeletedPhrases()
        {
            var deletedPhrases = _phraseAppService.GetAllDeletedPhrases();
            _emailAppService.SendEmailAsync(new SendEmailInput
            {
                Body = "body",
                Subject = "subject",
                SenderUserId = 1,
                TargetUserId = 2
            });

            return View(deletedPhrases);
        }
    }
}