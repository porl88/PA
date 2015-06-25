using System.Web;
using System.Web.Optimization;

namespace PA {
	public class BundleConfig {
		// For more information on Bundling, visit http://go.microsoft.com/fwlink/?LinkId=254725
		public static void RegisterBundles(BundleCollection bundles) {

			bundles.Add(new StyleBundle("~/css").Include(
				"~/content/css/default.css",
				"~/content/css/validation.css",
				"~/content/css/css3info-fonts.css",
				"~/content/css/css3info.css"
			));

			bundles.Add(new ScriptBundle("~/script").Include(
				"~/scripts/core.js",
				"~/scripts/css3info.js"
			));

			bundles.Add(new ScriptBundle("~/script-m").Include(
				"~/scripts/m-css3info.js"
			));
		}
	}
}