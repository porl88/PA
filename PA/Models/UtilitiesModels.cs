using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PA.Models
{
    public class RemoveHtmlModel
    {
        public string[] HtmlElements { get; set; }

        public string[] HtmlAttributes { get; set; }

        [AllowHtml]
        [Required]
        public string Html { get; set; }
    }

	public class RegExModel
	{
		[AllowHtml]
		[Required]
		public string Input { get; set; }

        [AllowHtml]
		[Required]
		[DisplayName("Regular Expression")]
		public string Pattern { get; set; }

        [AllowHtml]
		public string Replacement { get; set; }

		public string Output { get; set; }

        [DisplayName("Case insensitive")]
        public bool IgnoreCase { get; set; }

        public bool Singleline { get; set; }

        public bool Multiline { get; set; }

        public bool CultureInvariant { get; set; }

        public bool ECMAScript { get; set; }

        public bool ExplicitCapture { get; set; }

        public bool IgnorePatternWhitespace { get; set; }

        public bool RightToLeft { get; set; }
    }

	public class EncodeHtmlModel
	{
		[Required, AllowHtml]
		public string Html { get; set; }
	}
}