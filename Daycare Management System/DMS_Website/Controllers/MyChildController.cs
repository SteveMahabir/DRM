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
    public class MyChildController : Controller
    {
        //
        // GET: /MyChild/
        public ActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// Register method : hits the database and popualtes User / Customer tables
        /// </summary>
        /// <param name="model">Customer Model for storing</param>
        /// <param name="returnUrl">Next page to load once completed</param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public void Register_Child(ChildViewModel model, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    model.Register( Session["ParentID"] );
                    if (model.ChildID > 0)
                    {
                        ViewBag.Message = model.Message + ". Please proceed to login";                       
                    }                       
                    else
                    {
                        ViewBag.Message = "Problem Registering, try again later";
                    }
                }
                catch (Exception e)
                {
                    ViewBag.Message = "Problem Registering, " + e.Message + " try again later";
                }
            }
            //return PartialView("PopupMessage");
        }

	}
}