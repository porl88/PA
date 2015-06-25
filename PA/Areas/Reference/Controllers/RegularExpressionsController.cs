using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PA.Areas.Reference.Controllers
{

	public class RegularExpressionsController : Controller
    {

		public ViewResult Index() {
			return View();
		}

		[ActionName("common-expressions")]
		public ViewResult CommonExpressions() {
			return View();
		}

    }
}
