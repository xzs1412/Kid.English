using System.Web.Mvc;

namespace Kid.English.Web.Controllers
{
    public class AboutController : EnglishControllerBase
    {
        public ActionResult Index()
        {
            return View();
        }
	}
}