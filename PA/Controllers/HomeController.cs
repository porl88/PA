namespace PA.Controllers
{
    using System.Web.Mvc;
    
    public class HomeController : Controller
    {
        public ViewResult Index()
        {
            return View();
        }

		public ViewResult Sitemap() {
			return View();
		}
    }
}
