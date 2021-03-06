﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
namespace PA.Controllers
{
    using System.Web.Mvc;

    /// <summary>
    /// For error pages generated by the web.config file.
    /// </summary>
    [AllowAnonymous]
	public class ErrorController : Controller {

		public ViewResult Index() {
			return View("error"); // in shared view
		}

		[ActionName("not-found")]
		public ViewResult NotFound() {
			return View();
		}
	}
}
