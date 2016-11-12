namespace PA.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Linq;
    using System.Text;
    using System.Text.RegularExpressions;
    using System.Threading.Tasks;
    using System.Web;
    using System.Web.Helpers;
    using System.Web.Mvc;
    using System.Web.Security;
    using Info;
    using Models;
    using Text;
    using Http;
    using Languages;
    using Text.Markup;

    public class UtilitiesController : Controller
    {
        private static List<PageLink> siteLinks;

        public ViewResult Index()
        {
            return this.View("regular-expressions");
        }

        [ActionName("strip-out-html")]
        public ViewResult RemoveHtml()
        {
			return this.View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ActionName("strip-out-html")]
        public ActionResult RemoveHtml(RemoveHtmlModel model, string removeHtml)
        {
            if (ModelState.IsValid)
            {
                return this.File(Encoding.UTF8.GetBytes(HttpUtility.HtmlEncode(model.Html)), "text/plain", "strip-out-html.txt");
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
        public ActionResult RegEx(RegExModel model, string type)
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
                    case "replace-download":
                    case "replace":
                        string replacement = model.Replacement ?? string.Empty;
                        ViewBag.Replace = Regex.Replace(model.Input, model.Pattern, replacement, optionList);

                        if (type == "replace-download")
                        {
                            return this.File(Encoding.UTF8.GetBytes(ViewBag.Replace), "text/plain", "regex-replace.txt");
                        }

                        break;
                    case "matches":
                        var matches = Regex.Matches(model.Input, model.Pattern, optionList);
                        ViewBag.Matches = string.Format("{0} matches: {1}", matches.Count, string.Join(", ", matches.Cast<Match>().Select(x => x.Value).ToArray()));
                        break;
                    case "groups":
                        var match = Regex.Match(model.Input, model.Pattern, optionList);
                        if (match.Success)
                        {
                            ViewBag.Match = match;
                        }
                        break;
                }
            }
            catch (ArgumentException)
            {
                ModelState.AddModelError(string.Empty, "Your regular expression is invalid.");
            }

            return this.View();
        }

        [ActionName("encode-html")]
        public ViewResult EncodeHtml()
        {
            return this.View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ActionName("encode-html")]
        public ActionResult EncodeHtml(EncodeHtmlModel model, string action)
        {
            if (ModelState.IsValid)
            {
                string text = model.Html;
                switch (action)
                {
                    case "encode":
                        return this.File(Encoding.UTF8.GetBytes(HttpUtility.HtmlEncode(text)), "text/plain", "encoded-html.txt");
                    case "decode":
                        return this.File(Encoding.UTF8.GetBytes(HttpUtility.HtmlDecode(text)), "text/plain", "decoded-html.txt");
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


				var siteUri = new Uri(url);
				var crawler = new Crawler(siteUri);
				var links = await crawler.CheckPageLinks();
                siteLinks = links;

				stopwatch.Stop();
				var time = stopwatch.Elapsed;
				ViewBag.StopWatch = string.Format("{0}m {1}s", time.Minutes, time.Seconds);
				ViewBag.PageCount = links.Select(x => x.PageUrl.AbsolutePath).Distinct().Count();
				ViewBag.LinkCount = links.Count;

				//links = links.Where(x => x.StatusCode >= 400 && x.StatusCode < 500).ToList();

				return this.View(links);
			}
			else
			{
				return this.View();
			}
        }

        public FileResult DownloadSiteLinks()
        {
            if (siteLinks != null && siteLinks.Count > 0)
            {
                var csv = new StringBuilder(siteLinks.Count + 20);

                csv.Append("Page URL").Append(',')
                   .Append("Link URL").Append(',')
                   .Append("Status Code").Append(',')
                   .Append("Status Message").Append(',')
                   .Append("Mime Type")
                   .AppendLine();

                foreach (var link in siteLinks)
                {
                    csv.Append('"').Append(link.PageUrl.AbsoluteUri).Append('"').Append(',')
                       .Append('"').Append(link.Link.AbsoluteUri).Append('"').Append(',')
                       .Append(link.StatusCode).Append(',')
                       .Append('"').Append(link.StatusMessage).Append('"').Append(',')
                       .Append(link.ContentType)
                       .AppendLine();
                }

                return this.File(Encoding.UTF8.GetBytes(csv.ToString()), "text/csv", "site-links.csv");
            }

            return null;
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
            var pattern = @"(?<=[ :(]-?)(\d+|\d+\.\d+|\.\d+)px(?=[ ;)])";
            var convertedCss = Regex.Replace(css, "(?<=[ :(])0px(?=[ ;)])", "0", RegexOptions.IgnoreCase); // replace 0px values with 0
            convertedCss = Regex.Replace(convertedCss, pattern, this.ReplacePxWithRem, RegexOptions.IgnoreCase);
            return this.File(Encoding.Unicode.GetBytes(convertedCss), "text/plain", "convert-px-to-rem.txt");
        }

        [ActionName("convert-entities")]
        public ViewResult ConvertEntities()
        {
            return this.View();
        }

        [HttpPost]
        [ActionName("convert-entities")]
        public ActionResult ConvertEntities(EncodeHtmlModel model)
        {
            if (this.ModelState.IsValid)
            {
                var result = model.Html.EncodeCharacterEntityReferences();
                return this.File(Encoding.UTF8.GetBytes(result), "text/plain", "convert-entities.txt");
            }
            else
            {
                return this.View();
            }
        }

        [ActionName("convert-to-html")]
        public ViewResult ConvertTextToHtml()
        {
            return this.View();
        }

        [HttpPost]
        [ActionName("convert-to-html")]
        public ActionResult ConvertTextToHtml(EncodeHtmlModel model)
        {
            if (this.ModelState.IsValid)
            {
                var result = model.Html.ConvertPlainTextToHtml();
                return this.File(Encoding.UTF8.GetBytes(result), "text/plain", "convert-text-to-html.txt");
            }
            else
            {
                return this.View();
            }
        }

        [ActionName("convert-english")]
        public ViewResult ConvertEnglish()
        {
            return this.View();
        }

        [HttpPost]
        [ActionName("convert-english")]
        public FileResult ConvertEnglish(string text)
        {
            var converter = new LanguageConverter();
            var result = converter.ToAmericanEnglish(text);
            return this.File(Encoding.Unicode.GetBytes(result), "text/plain", "convert-english.txt");
        }

        private string ReplacePxWithRem(Match match)
        {
            var text = match.Value.ToLower();
            var pxValue = Convert.ToDouble(match.Groups[1].Value);

            if (pxValue > 0)
            {
                var remValue = pxValue / 16;
                text = text.Replace(match.Groups[1].Value, remValue.ToString()).Replace("px", "rem");
            }

            return text;
        }
    }
}






	               