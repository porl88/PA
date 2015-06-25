using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PA.Controllers
{
	[AllowAnonymous]
    public class HelpController : Controller
    {

		public ViewResult Cookies()
        {
            return View();
        }

		public ViewResult JavaScript() {
			return View();
		}

		public ViewResult Browser() {
			return View();
		}

    }
}
