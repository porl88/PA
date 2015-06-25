namespace PA.Areas.Reference.Controllers
{
	using System.Web.Mvc;

	public class AspxController : Controller
    {
		[ActionName("formatting-numeric")]
		public ViewResult FormattingNumeric() {
			return View();
		}

		[ActionName("formatting-datetime")]
		public ViewResult FormattingDateTime()
		{
			return View();
		}

		public ViewResult Arrays()
		{
			return View();
		}

		public ViewResult Security() {
			return View();
		}
    }
}
