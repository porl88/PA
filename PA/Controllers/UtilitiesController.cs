namespace PA.Controllers
{
    using System;
    using System.Diagnostics;
    using System.Linq;
    using System.Text;
    using System.Text.RegularExpressions;
    using System.Threading.Tasks;
    using System.Web;
    using System.Web.Helpers;
    using System.Web.Mvc;
    using System.Web.Security;
    using ClassLibrary;
    using PA.Models;
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
                //return File(Encoding.Unicode.GetBytes(model.Html), "text/plain", "strip-out-html.txt");
            }

            return this.View();
        }

        [ActionName("regular-expressions")]
        public ViewResult RegEx()
        {
            return this.View();
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
            return this.View();
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

            return this.View();
        }

		[ActionName("user-agent")]
		public ViewResult UserAgent()
		{
			var client = new ClientInfo(Request.UserAgent);
			return this.View(client);
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		[ActionName("user-agent")]
		public ViewResult UserAgent(string userAgent)
		{
			var client = new ClientInfo(userAgent);
			return this.View(client);
		}

		public ViewResult Encryption()
		{
			return this.View();
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public ViewResult Encryption(string text, string action)
		{
			switch (action)
			{
				//case "salt":
				//	ViewBag.Output = "Salt: " + Crypto.GenerateSalt(text);
				//	break;
				case "hash":
					ViewBag.Output = "Hash: " + Crypto.Hash(text);
					break;
				case "hash-password":
					ViewBag.Output = "Hash Password: " + Crypto.HashPassword(text);
					break;
				case "sha1":
					ViewBag.Output = "SHA1: " + Crypto.SHA1(text);
					break;
				case "sha256":
					ViewBag.Output = "SHA256: " + Crypto.SHA256(text);
					break;
			}

			return this.View();
		}


        [ActionName("random-number-generator")]
        public ViewResult RandomNumberGenerator()
        {
            return this.View();
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

            return this.View();
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
            return this.View();
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

				return this.View(links);
			}
			else
			{
				return this.View();
			}
        }

        [ActionName("convert-px-to-rem")]
        public ViewResult ConvertPxToRem()
        {
            return this.View();
        }

        [HttpPost]
        [ActionName("convert-px-to-rem")]
        public FileResult ConvertPxToRem(string css)
        {
            var pattern = @"\b-?(\d+(\.\d+)?)px\b";
            var convertedCss = Regex.Replace(css, pattern, this.ReplacePxWithRem, RegexOptions.IgnoreCase);
            return this.File(Encoding.Unicode.GetBytes(convertedCss), "text/plain", "convert-px-to-rem.txt");
        }

        private string ReplacePxWithRem(Match match)
        {
            var text = match.Value.ToLower();
            var pxValue = Convert.ToDouble(match.Groups[1].Value);

            if (pxValue == 0)
            {
                text = text.Replace("px", string.Empty);
            }
            else if (pxValue > 0)
            {
                var remValue = pxValue / 16;
                text = text.Replace(pxValue.ToString(), remValue.ToString()).Replace("px", "rem");
            }

            return text;
        }
    }
}
