using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
// Added for Register
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin.Security;
using eStoreViewModels;
using eStoreWebsite.Models;

namespace eStoreWebsite.Controllers
{
    public class HomeController : Controller
    {

        /// <summary>
        /// Defaut Method, sets Status's to NULL
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            if (Session["Message"] == null)
            {
                Session["Message"] = "Please Login First";
            }
            
            if (Session["LoginStatus"] == null)
            {
                Session["loggedIn"] = false;
                Session["LoginStatus"] = "Please Log in to continue. You are not logged in.";
            }

            return View();
        }


        /// <summary>
        /// Login - Checks the database for user and populates session
        /// </summary>
        /// <param name="model">Customer Model for hitting the database</param>
        /// <param name="returnUrl">Returns the next pages url</param>
        /// <returns></returns>
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Login(CustomerViewModel model, string returnUrl)
        {
            var user = await UserManager.FindAsync(model.Username, model.Password);
            if (user != null)
            {
                await SignInAsync(user, false);
                model.GetCurrentProfile();
                Session["CustomerID"] = model.CustomerID;
                Session["Message"] = "Welcome " + model.Username;
                Session["loggedIn"] = true;
                Session["LoginStatus"] = "Thank you for logging in, You are currently logged in as : " + model.Username;
                Session["customer"] = model;
                return Json(new {url=Url.Action("")}, JsonRequestBehavior.AllowGet);
            }
            else
            {
                ViewBag.Message = "login failed - try again";
                return PartialView("PopupMessage");
            }
        }

        /// <summary>
        ///  POST : /Home/LogOff
        /// </summary>
        /// <returns>Home Page Redirect</returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult LogOff()
        {
            AuthenticationManager.SignOut();
            Session.Abandon();
            return RedirectToAction("Index", "Home");
        }
        
        /// <summary>
        /// Register method : hits the database and popualtes User / Customer tables
        /// </summary>
        /// <param name="model">Customer Model for storing</param>
        /// <param name="returnUrl">Next page to load once completed</param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Register(CustomerViewModel model, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    model.Register();
                    if(model.CustomerID > 0)
                    {
                        var user = new ApplicationUser() { UserName = model.Username };
                        var result = await UserManager.CreateAsync(user, model.Password);
                        if (result.Succeeded)
                        {
                            ViewBag.Message = model.Message + ". Please proceed to login";
                        }
                        else
                        {
                            int rowsDelete = model.Delete(); // something went wrong with ASPNet.Identity get rid of our customer
                            ViewBag.Message = "Problem Registering, " + result.Errors.ElementAt(0) + " try again!";
                        }
                    }
                    else
                    {
                        ViewBag.Message = "Problem Registering, try again later";
                    }
                }
                catch(Exception e)
                {
                    ViewBag.Message = "Problem Registering, " + e.Message + " try again later";
                }
            }
            return PartialView("PopupMessage");
        }


        //This region is for usefull methods used in the website.
        #region Helpers

        // Microsoft Methods from Original Account Controller
        public HomeController()
            : this(new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new ApplicationDbContext())))
        {
        }

        public HomeController(UserManager<ApplicationUser> userManager)
        {
            UserManager = userManager;
        }

        private async Task SignInAsync(ApplicationUser user, bool isPersistent)
        {
            AuthenticationManager.SignOut(DefaultAuthenticationTypes.ExternalCookie);
            var identity = await UserManager.CreateIdentityAsync(user, DefaultAuthenticationTypes.ApplicationCookie);
            AuthenticationManager.SignIn(new AuthenticationProperties() { IsPersistent = isPersistent }, identity);
        }

        private IAuthenticationManager AuthenticationManager
        {
            get
            {
                return HttpContext.GetOwinContext().Authentication;
            }
        }


        private ActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }

        public UserManager<ApplicationUser> UserManager { get; private set; }


        private class ChallengeResult : HttpUnauthorizedResult
        {
            public ChallengeResult(string provider, string redirectUri)
                : this(provider, redirectUri, null)
            {
            }

            public ChallengeResult(string provider, string redirectUri, string userId)
            {
                LoginProvider = provider;
                RedirectUri = redirectUri;
                UserId = userId;
            }

            private const string XsrfKey = "XsrfId";

            public string LoginProvider { get; set; }
            public string RedirectUri { get; set; }
            public string UserId { get; set; }

            public override void ExecuteResult(ControllerContext context)
            {
                var properties = new AuthenticationProperties() { RedirectUri = RedirectUri };
                if (UserId != null)
                {
                    properties.Dictionary[XsrfKey] = UserId;
                }
                context.HttpContext.GetOwinContext().Authentication.Challenge(properties, LoginProvider);
            }

        }
        #endregion

    }
}

