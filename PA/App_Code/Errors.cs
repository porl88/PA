namespace PA {
	using System;
	using System.Configuration;
	using System.Net.Mail;
	using System.Text;
	using System.Web;

	/// <summary>
	/// Error Logging.
	/// </summary>
	public class Errors {


		//***********************************************************************************************************
		//FUNCTION:	emails error details to Technical Support - includes user details and browser details
		//***********************************************************************************************************
		public static void LogError(Exception ex) {
			LogError(ex, null, null);
		}

		public static void LogError(string subject, string message) {
			LogError(null, subject, message);
		}


		public static void LogError(Exception ex, string subject, string message) {

			var request = HttpContext.Current.Request;

			// validate
			if (request.Browser.Crawler) return;

			////filter out specific error types
			//if (ex != null) {
			//	string errorName = ex.GetType().Name;
			//	switch (errorName) {
			//		case "HttpException": // when an ASP.NET MVC page is not found
			//			if (request.UrlReferrer == null) return; // ignore missing page errors that have not come from a hyperlink
			//			if (!request.UrlReferrer.AbsoluteUri.Contains(request.Url.Host)) return; // ignore missing page errors that come from other sites
			//			break;
			//		case "HttpRequestValidationException": // when users submit HTML in form fields
			//			return;
			//	}
			//}

			StringBuilder errorMessage = new StringBuilder();

			//add user details
			string userDetails = GetUserDetails();
			if (!String.IsNullOrWhiteSpace(userDetails)) {
				errorMessage.Append(userDetails).AppendLine();
				errorMessage.AppendLine().AppendLine().AppendLine();
			}

			//add Exception details
			if (ex != null) {

				//create subject
				subject = String.IsNullOrWhiteSpace(subject) ? ex.Message : "ERROR" + subject;

				//add user-defined error message
				if (!String.IsNullOrWhiteSpace(message)) {
					errorMessage.AppendLine("USER-DEFINED ERROR MESSAGE:").AppendLine();
					errorMessage.AppendLine(message);
					errorMessage.AppendLine().AppendLine();
				}

				// error name + message
				errorMessage.AppendLine("ERROR:").AppendLine();
				errorMessage.AppendLine(ex.GetType().Name).AppendLine();
				errorMessage.AppendLine(ex.Message);
				errorMessage.AppendLine().AppendLine();

				// error inner exception
				if (ex.InnerException != null) {
					errorMessage.AppendLine("DETAILS:").AppendLine();
					errorMessage.AppendLine(ex.InnerException.ToString());
					errorMessage.AppendLine().AppendLine();
				}

				// error location
				errorMessage.AppendLine("LOCATION:").AppendLine();
				errorMessage.AppendLine(ex.StackTrace);
				errorMessage.AppendLine().AppendLine().AppendLine();

			}

			//email error message to technical support
			string fromAddress = "errors@" + request.Url.Host;
			string toAddress = ConfigurationManager.AppSettings["techEmail"];
			var msg = new MailMessage(fromAddress, toAddress, subject, errorMessage.ToString());

			var smtp = new SmtpClient();
			if (!request.IsLocal) smtp.Send(msg);

		}


		private static string GetUserDetails() {
			StringBuilder userDetails = new StringBuilder();
			//if (HttpContext.Current.User.Identity.IsAuthenticated) {
			//	var user = Membership.GetUser();
			//	if (user != null) {
			//		userDetails.AppendLine(FormatEntry("User Name", user.UserName));
			//		userDetails.AppendLine(FormatEntry("Email", user.Email));
			//	}
			//}
			var request = HttpContext.Current.Request;
			//var client = new ClientInfo(request.UserAgent);
			userDetails.AppendLine(FormatEntry("URL", request.Url.AbsoluteUri));
			if (request.UrlReferrer != null) userDetails.AppendLine(FormatEntry("Previous URL", request.UrlReferrer.AbsoluteUri));
			//userDetails.AppendLine(FormatEntry("Browser", client.Browser + " " + client.BrowserMajorVersion));
			//userDetails.AppendLine(FormatEntry("OS", client.OS));
			//userDetails.AppendLine(FormatEntry("Rendering Engine", client.RenderingEngine + " " + client.RenderingEngineVersion));
			userDetails.AppendLine(FormatEntry("IP Address", request.UserHostAddress));
			userDetails.AppendLine(FormatEntry("User Agent", request.UserAgent));
			return userDetails.ToString();
		}

		private static string FormatEntry(string key, string value) {
			key += ':';
			return key.ToUpper().PadRight(40) + value;
		}
	}
}