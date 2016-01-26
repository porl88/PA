namespace PA.Controllers
{
    using System.Web.Mvc;

    [AllowAnonymous]
	public class PortfolioController : Controller
    {
        public ViewResult Index()
        {
            return View();
        }

        public ViewResult InterContinental()
        {
            return View();
        }

        [ActionName("intercontinental-fortune")]
        public ViewResult InterContinentalFortune()
        {
            return View();
        }

        [ActionName("hep-c-quiz")]
		public ViewResult HepCQuiz()
		{
			return View();
		}

		public ViewResult Northamber()
		{
			return View();
		}

		[ActionName("northamber-login")]
		public ViewResult Northamber2() {
			return View();
		}

		[ActionName("northamber-contact")]
		public ViewResult Northamber3() {
			return View();
		}

		[ActionName("northamber-product")]
		public ViewResult Northamber4() {
			return View();
		}

		[ActionName("fse")]
		public ViewResult FSE() {
			return View();
		}

		[ActionName("cabinet-maker")]
		public ViewResult CabinetMaker() {
			return View();
		}

		[ActionName("furniture-show")]
		public ViewResult FurnitureShow() {
			return View();
		}

		public ViewResult Because() {
			return View();
		}
    }
}
