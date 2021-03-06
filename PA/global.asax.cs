﻿namespace PA {
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
	}
}