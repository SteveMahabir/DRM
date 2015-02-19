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
using DMS_ViewModel;
using DMS_Website.Models;
// Added for Mail Client
using System.Net.Mail;

namespace DMS_Website.Controllers
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
        public async Task<ActionResult> Login(ParentViewModel model, string returnUrl)
        {
            
            var user = await UserManager.FindAsync(model.Username, model.Password);
            if (user != null)
            {
                await SignInAsync(user, false);
                model.GetCurrentProfile();
                Session["ParentID"] = model.ParentID;
                Session["Message"] = "Welcome " + model.Username;
                Session["loggedIn"] = true;
                Session["LoginStatus"] = "Thank you for logging in, You are currently logged in as : " + model.Username;
                Session["parent"] = model;
                Session["Email"] = model.Email;
                Session["FullName"] = model.Firstname + " " + model.Lastname;
                Session["userType"] = "parent";
                ViewBag.custID = model.ParentID;
                return Json(new {url=Url.Action("")}, JsonRequestBehavior.AllowGet);
            }
            else
            {
                ViewBag.Message = "login failed - try again";
                return PartialView("PopupMessage");
            }
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
        public async Task<ActionResult> Login_emp(EmployeeViewModel model, string returnUrl)
        {
            var user = await UserManager.FindAsync(model.Username, model.Password);
            if (user != null)
            {
                await SignInAsync(user, false);
                model.GetCurrentProfile();
                Session["EmployeeID"] = model.EmployeeID;
                Session["Message"] = "Welcome " + model.Username;
                Session["loggedIn"] = true;
                Session["LoginStatus"] = "Thank you for logging in, You are currently logged in as : " + model.Username;
                Session["parent"] = model;
                Session["Email"] = model.Email;
                Session["FullName"] = model.Firstname + " " + model.Lastname;
                Session["userType"] = model.Role;
                ViewBag.custID = model.EmployeeID;
                return Json(new { url = Url.Action("") }, JsonRequestBehavior.AllowGet);
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
        public async Task<ActionResult> Register(ParentViewModel model, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    model.Register();
                    if(model.ParentID > 0)
                    {
                        var user = new ApplicationUser() { UserName = model.Username };
                        var result = await UserManager.CreateAsync(user, model.Password);

                        if (result.Succeeded)
                            ViewBag.Message = model.Message + ". Please proceed to login";
                        else
                            ViewBag.Message = "Problem Registering, " + result.Errors.ElementAt(0) + " try again!";


                        //TO DO: This will eventually send a confirmation email
                        if (result.Succeeded)
                        {
                            ViewBag.Message = model.Message + ". Please proceed to login";
                        //    #region Send-Mail
                        //    MailMessage msg = new MailMessage();
                        //    msg.Subject = "New Registration";
                        //    msg.From = new MailAddress("registrar@surfshop.com", "Surf Shop Registrar");
                        //    msg.To.Add(new MailAddress(model.Email));
                        //    msg.Body = "Congratulations " + model.Firstname +
                        //                ", you have been registered at the Surf Shop!!" +
                        //                " your new username is " + model.Username;
                        //    using (SmtpClient mailClient = new SmtpClient())
                        //    {
                        //        mailClient.Send(msg);
                        //    }
                        //    #endregion
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

