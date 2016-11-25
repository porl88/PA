namespace PA.Controllers
{
    using System.Web.Mvc;
    
    public class HomeController : Controller
    {
        public RedirectToRouteResult Index()
        {
            return this.RedirectToAction("index", "reference", new { area = "" });
            //return this.View("~/areas/reference/views/reference/request-information.cshtml");
        }

        public ViewResult Sitemap() {
			return this.View();
		}
    }
}
