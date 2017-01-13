using System.Web.Mvc;
using Abp.Web.Mvc.Authorization;

namespace Kid.English.Web.Controllers
{
   // [AbpMvcAuthorize]
    public class HomeController : EnglishControllerBase
    {
        [AllowAnonymous]
        public ActionResult Index()
        {
            return RedirectToAction("Index", "Phrases");
           // return View();
        }
	}
}