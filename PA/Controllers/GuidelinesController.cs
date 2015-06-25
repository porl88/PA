using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PA.Controllers
{

	public class GuidelinesController : Controller
    {

        public ViewResult Index()
        {
            return View("website-design");
        }

		public ViewResult Fonts() {
			return View();
		}

		[ActionName("website-checklist")]
		public ViewResult Checklist() {
			return View();
		}

		[ActionName("website-design")]
		public ViewResult Design() {
			return View();
		}

		[ActionName("website-design-tips")]
		public ViewResult DesignTips() {
			return View();
		}

		public PartialViewResult Navigation() {
			return PartialView("_navigation");
		}

    }
}
