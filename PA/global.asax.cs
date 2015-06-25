namespace PA {
	using System;
	using System.Web.Http;
	using System.Web.Mvc;
	using System.Web.Optimization;
	using System.Web.Routing;

	public class MvcApplication : System.Web.HttpApplication
	{

		protected void Application_Start() {
			AreaRegistration.RegisterAllAreas();
			WebApiConfig.Register(GlobalConfiguration.Configuration);
			FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
			RouteConfig.RegisterRoutes(RouteTable.Routes);
			BundleConfig.RegisterBundles(BundleTable.Bundles);
		}


		protected void Application_BeginRequest() {

			//// display "not supported" message if the user's browser is not supported
			//if (!CheckBrowserIsSupported()) {
			//	// redirect to browser download page
			//	string redirectUrl = Request.Url.AbsolutePath.ToLower();
			//	if (redirectUrl != "/help/browser") {
			//		Response.Redirect("~/help/browser");
			//	}
			//}

			// optimise URLs for SEO purposes
			//ClassLibrary.Seo.SeoRedirect("paularnold.me.uk");
			
		}


		// used to handle all unhandled exceptions in the application
		protected void Application_Error(object sender, EventArgs e) {
			
			// log all unhandled errors
			PA.Errors.LogError(Server.GetLastError());
		}




		///// <summary>
		///// Checks to see if the user is using an unsupported browser - if so, it redirects to the browser download page
		///// </summary>
		///// <returns></returns>
		//private bool CheckBrowserIsSupported()
		//{
		//	var client = new ClientInfo(Request.UserAgent);
		//	var browser = client.Browser;
		//	int version = client.BrowserMajorVersion;
		//	switch (browser) {
		//		case Browser.Firefox:
		//			if (version < 4) return false;
		//			break;
		//		case Browser.Chrome:
		//			if (version < 4) return false;
		//			break;
		//		case Browser.IE:
		//			if (version < 9) return false;
		//			break;
		//		case Browser.Safari:
		//			if (client.DeviceType == DeviceType.Desktop && version < 4) return false;
		//			break;
		//		case Browser.Opera:
		//			if (version < 11) return false;
		//			break;
		//	}

		//	return true;
		//}
	}
}