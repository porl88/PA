using System.Web.Mvc;

namespace PA.Areas.Reference {
	public class ReferenceAreaRegistration : AreaRegistration {

		public override string AreaName {
			get {
				return "Reference";
			}
		}

		public override void RegisterArea(AreaRegistrationContext context) {

			// remove alternative paths for SEO purposes
			context.Routes.IgnoreRoute("{area}/index");
			context.Routes.IgnoreRoute("{area}/{controller}/index");
			context.Routes.IgnoreRoute("{area}/{controller}/{action}/index");

			context.MapRoute(
				name: "Reference_RegExp",
				url: "reference/regular-expressions/{action}",
				defaults: new { controller = "regularexpressions", action = "index" }
			);

			context.MapRoute(
				name: "Reference_Home",
				url: "reference/{action}",
				defaults: new { controller = "reference", action = "index" }
			);

			context.MapRoute(
				"Reference_Default",
				"reference/{controller}/{action}/{id}",
				defaults: new { action = "Index", id = UrlParameter.Optional }
			);
		}
	}
}
