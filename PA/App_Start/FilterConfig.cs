using System.Web;
using System.Web.Mvc;

namespace PA {

	public class FilterConfig {
		public static void RegisterGlobalFilters(GlobalFilterCollection filters) {

			filters.Add(new AuthorizeAttribute());
			
			filters.Add(new HandleErrorAttribute {
				ExceptionType = typeof(HttpRequestValidationException),
				View = "error-httprequest"
			});

			filters.Add(new HandleErrorAttribute {
				ExceptionType = typeof(HttpAntiForgeryException),
				View = "error-httpanti"
			});

			filters.Add(new CustomHandleErrorAttribute());
		}
	}

	public class CustomHandleErrorAttribute : HandleErrorAttribute {
		public override void OnException(ExceptionContext filterContext) {
			if (!filterContext.HttpContext.IsCustomErrorEnabled) return;
			PA.Errors.LogError(filterContext.Exception);
		}
	}
}