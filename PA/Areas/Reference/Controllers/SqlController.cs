using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PA.Areas.Reference.Controllers
{
    public class SqlController : Controller
    {
        //
        // GET: /Reference/Sql/

		[ActionName("data-types")]
		public ViewResult DataTypes() {
			return View();
		}

        public ViewResult Performance()
        {
            return View();
        }

    }
}
