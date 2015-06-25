using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PA.Areas.Reference.Controllers
{

    public class CssController : Controller
    {
        //
        // GET: /Css/

		//public ActionResult Index()
		//{
		//	return View();
		//}

		public ViewResult Selectors() {
			return View();
		}

		[ActionName("layout-engines")]
		public ViewResult LayoutEngines() {
			return View("layout-engines");
		}

		[ActionName("browser-support")]
		public ViewResult BrowserSupport() {
			return View();
		}

    }
}
