namespace PA.Areas.Reference.Controllers
{
	using System.Web.Mvc;
	using ClassLibrary;

	public class ReferenceController : Controller
	{
		public ViewResult Index()
		{
			var client = new ClientInfo(Request.UserAgent);
			return View("request-information", client);
		}

		[ActionName("request-information")]
		public ViewResult RequestInformation()
		{
			var client = new ClientInfo(Request.UserAgent);
			return View(client);
		}

		[ActionName("character-sets")]
		public ViewResult CharacterSets()
		{
			return View();
		}

		public ViewResult Performance()
		{
			return View();
		}

		[ActionName("website-checklist")]
		public ViewResult CheckList()
		{
			return View();
		}
	}
}
