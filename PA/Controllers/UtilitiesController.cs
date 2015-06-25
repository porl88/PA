namespace PA.Controllers
{
	using PA.Models;
	using System;
	using System.Collections.Generic;
	using System.Diagnostics;
	using System.Linq;
	using System.Net.Http;
	using System.Text.RegularExpressions;
	using System.Threading.Tasks;
	using System.Web;
	using System.Web.Mvc;
	using System.Web.Security;
	using PA.Services.Crawler.Transfer;
	using PA.Services.Crawler;
	
	public class UtilitiesController : Controller
    {
        public ViewResult Index()
        {
            return View("regular-expressions");
        }

        [ActionName("strip-out-html")]
        public ViewResult RemoveHtml()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ActionName("strip-out-html")]
        public ViewResult RemoveHtml(RemoveHtmlModel model, string removeHtml)
        {
            if (ModelState.IsValid)
            {
				//if (removeHtml == "all")
				//{
				//	model.Html = model.Html.StripOutHtml();
				//}
				//else if (removeHtml == "selected")
				//{
				//	var separator = new char[] { ',' };
				//	model.Html = model.Html.StripOutHtml(model.HtmlElements, model.HtmlAttributes, null);
				//}

                Response.Clear();
                Response.ContentType = "text/plain";
                Response.AddHeader("Content-Disposition", "attachment;filename=strip-out-html.txt");
                Response.Write(model.Html);
                Response.End();
            }

            return View();
        }

        [ActionName("regular-expressions")]
        public ViewResult RegEx()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ActionName("regular-expressions")]
        public ViewResult RegEx(RegExModel model, string type)
        {
            try
            {
                var optionList = new RegexOptions();

                if (model.IgnoreCase)
                {
                    optionList |= RegexOptions.IgnoreCase;
                }

                if (model.Singleline)
                {
                    optionList |= RegexOptions.Singleline;
                }

                if (model.Multiline)
                {
                    optionList |= RegexOptions.Multiline;
                }

                if (model.CultureInvariant)
                {
                    optionList |= RegexOptions.CultureInvariant;
                }

                if (model.ECMAScript)
                {
                    optionList |= RegexOptions.ECMAScript;
                }

                if (model.ExplicitCapture)
                {
                    optionList |= RegexOptions.ExplicitCapture;
                }

                if (model.IgnorePatternWhitespace)
                {
                    optionList |= RegexOptions.IgnorePatternWhitespace;
                }

                if (model.RightToLeft)
                {
                    optionList |= RegexOptions.RightToLeft;
                }

                switch (type)
                {
                    case "replace":
                        string replacement = model.Replacement ?? string.Empty;
                        ViewBag.Replace = Regex.Replace(model.Input, model.Pattern, replacement, optionList);
                        break;
                    case "matches":
                        MatchCollection matches = Regex.Matches(model.Input, model.Pattern, optionList);
                        ViewBag.Matches = string.Format("{0} matches: {1}", matches.Count, string.Join(", ", matches.Cast<Match>().Select(x => x.Value).ToArray()));
                        break;
                }
            }
            catch (ArgumentException)
            {
                ModelState.AddModelError(string.Empty, "Your regular expression is invalid.");
            }

            return View();
        }

        [ActionName("encode-html")]
        public ViewResult EncodeHtml()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ActionName("encode-html")]
        public ViewResult EncodeHtml(EncodeHtmlModel model, string action)
        {
            if (ModelState.IsValid)
            {
                string text = model.Html;
                switch (action)
                {
                    case "encode":
                        DownloadFile("encoded-html.txt", "text/plain", HttpUtility.HtmlEncode(text));
                        break;
                    case "decode":
                        DownloadFile("encoded-html.txt", "text/plain", HttpUtility.HtmlDecode(text));
                        break;
                }
            }

            return View();
        }

		//[ActionName("user-agent")]
		//public ViewResult UserAgent()
		//{
		//	var client = new ClientInfo(Request.UserAgent);
		//	return View(client);
		//}

		//[HttpPost]
		//[ValidateAntiForgeryToken]
		//[ActionName("user-agent")]
		//public ViewResult UserAgent(string userAgent)
		//{
		//	var client = new ClientInfo(userAgent);
		//	return View(client);
		//}

        [ActionName("random-number-generator")]
        public ViewResult RandomNumberGenerator()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ActionName("random-number-generator")]
        public ViewResult RandomNumberGenerator(string action)
        {
            switch (action)
            {
                case "guid":
                    ViewBag.Output = "GUID: " + Guid.NewGuid();
                    break;
                case "password":
                    ViewBag.Output = "PASSWORD: " + Membership.GeneratePassword(8, 2);
                    break;
            }

            return View();
        }

        private void DownloadFile(string fileName, string contentType, string text)
        {
            Response.Clear();
            Response.AddHeader("Content-Disposition", "attachment; filename=" + fileName);
            Response.ContentType = contentType;
            Response.Write(text);
            Response.End();
        }

        [ActionName("site-checker")]
        public ViewResult SiteChecker()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ActionName("site-checker")]
        public async Task<ViewResult> SiteChecker(string url)
        {
			if (!string.IsNullOrWhiteSpace(url))
			{
				var stopwatch = new Stopwatch();
				stopwatch.Start();


				var siteUri = new Uri("http://girlwithaduckjumper.com/");
				var crawler = new CrawlerService(siteUri);
				var links = await crawler.CheckPageLinks();

				//string domain = url;
				//if (!domain.StartsWith("http"))
				//{
				//	domain = "http://" + domain;
				//}

				stopwatch.Stop();
				var time = stopwatch.Elapsed;
				ViewBag.StopWatch = string.Format("{0}m {1}s", time.Minutes, time.Seconds);
				ViewBag.PageCount = links.Select(x => x.PageUrl.AbsolutePath).Distinct().Count();
				ViewBag.LinkCount = links.Count;

				links = links.Where(x => x.StatusCode >= 400 && x.StatusCode < 500).ToList();

				return View(links);
			}
			else
			{
				return View();
			}
        }
    }
}
