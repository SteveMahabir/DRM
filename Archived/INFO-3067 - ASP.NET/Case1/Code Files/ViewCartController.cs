using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using eStoreViewModels;

namespace eStoreWebsite.Controllers
{
    public class ViewCartController : Controller
    {

        /// <summary>
        /// Default method for controller GET: /ViewCart/
        /// </summary>
        /// <returns>Index page for ViewCart</returns>
        public ActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// Needed for business Logic
        /// </summary>
        /// <returns>Default view</returns>
        public ActionResult getOrder()
        {
            return View();
        }

        /// <summary>
        /// Generate Order - Once shopping is complete, hit the database with the order
        /// </summary>
        /// <returns>Populated Modal</returns>
        public ActionResult genOrder()
        {
            OrderViewModel myOrder = new OrderViewModel();
            try
            {
                myOrder.AddOrder((CartItem[])Session["Cart"],
                    Convert.ToInt32(Session["CustomerID"]),
                    (double)Session["OrderAmt"]);
                if (myOrder.OrderID > 0) // Order Added
                {
                    ViewBag.Message = "Order " + myOrder.OrderID + " Created!";
                    if (myOrder.BackOrderFlag > 0)
                        ViewBag.Message += " Some goods were backordered!";
                }
                else // Problem
                {
                    ViewBag.Message = myOrder.Message = ", try again later!";
                }
            }
            catch(Exception e)
            {
                ViewBag.Message = "Order was not created, try again later! - " + e.Message;
            }

            return PartialView("PopupMessage");
        }
	}
}