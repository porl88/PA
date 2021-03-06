﻿using PA.Filters;
using PA.Models;
using System;
using System.Transactions;
using System.Web.Mvc;
using System.Web.Security;
using WebMatrix.WebData;

namespace PA.Controllers {

	[InitializeSimpleMembership]
	//[HandleError(ExceptionType = typeof(HttpAntiForgeryException), View = "NoCookies")]
	public class AccountController : Controller {

		// GET: /Account/Login

		[AllowAnonymous]
		public ActionResult Login(string returnUrl) {
			ViewBag.ReturnUrl = returnUrl;
			return View();
		}

		//
		// POST: /Account/Login

		[HttpPost]
		[AllowAnonymous]
		[ValidateAntiForgeryToken]
		public ActionResult Login(LoginModel model, string returnUrl) {

            //if (ModelState.IsValid && WebSecurity.Login(model.UserName, model.Password, persistCookie: model.RememberMe)) {
            //	return RedirectToLocal(returnUrl);
            //}

#pragma warning disable CS0618
            if (FormsAuthentication.Authenticate(model.UserName, model.Password)) {

				FormsAuthentication.RedirectFromLoginPage(model.UserName, false);

				////log user in and redirect to original page
				//if (model.RememberMe != null) {
				//	FormsAuthentication.RedirectFromLoginPage(model.UserName, model.RememberMe);
				//}
				//else {
				//	FormsAuthentication.RedirectFromLoginPage(model.UserName, false);
				//}

			}
#pragma warning restore CS0618

            // If we got this far, something failed, redisplay form
            ModelState.AddModelError("", "The user name or password provided is incorrect.");
			return View(model);
		}

		//
		// POST: /Account/LogOff

		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult LogOff() {
			WebSecurity.Logout();

			return RedirectToAction("Index", "Home");
		}

		#region Helpers
		private ActionResult RedirectToLocal(string returnUrl) {
			if (Url.IsLocalUrl(returnUrl)) {
				return Redirect(returnUrl);
			}
			else {
				return RedirectToAction("Index", "Home");
			}
		}

		public enum ManageMessageId {
			ChangePasswordSuccess,
			SetPasswordSuccess,
			RemoveLoginSuccess,
		}


		private static string ErrorCodeToString(MembershipCreateStatus createStatus) {
			// See http://go.microsoft.com/fwlink/?LinkID=177550 for
			// a full list of status codes.
			switch (createStatus) {
				case MembershipCreateStatus.DuplicateUserName:
					return "User name already exists. Please enter a different user name.";

				case MembershipCreateStatus.DuplicateEmail:
					return "A user name for that e-mail address already exists. Please enter a different e-mail address.";

				case MembershipCreateStatus.InvalidPassword:
					return "The password provided is invalid. Please enter a valid password value.";

				case MembershipCreateStatus.InvalidEmail:
					return "The e-mail address provided is invalid. Please check the value and try again.";

				case MembershipCreateStatus.InvalidAnswer:
					return "The password retrieval answer provided is invalid. Please check the value and try again.";

				case MembershipCreateStatus.InvalidQuestion:
					return "The password retrieval question provided is invalid. Please check the value and try again.";

				case MembershipCreateStatus.InvalidUserName:
					return "The user name provided is invalid. Please check the value and try again.";

				case MembershipCreateStatus.ProviderError:
					return "The authentication provider returned an error. Please verify your entry and try again. If the problem persists, please contact your system administrator.";

				case MembershipCreateStatus.UserRejected:
					return "The user creation request has been canceled. Please verify your entry and try again. If the problem persists, please contact your system administrator.";

				default:
					return "An unknown error occurred. Please verify your entry and try again. If the problem persists, please contact your system administrator.";
			}
		}
		#endregion
	}
}
