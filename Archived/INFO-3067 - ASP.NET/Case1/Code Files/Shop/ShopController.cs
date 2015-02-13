using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using eStoreViewModels;

namespace eStoreWebsite.Controllers
{
    public class ShopController : Controller
    {
        /// <summary>
        /// Default method for controller : GET: /Shop/
        /// </summary>
        /// <returns>Shop-Index page</returns>
        public ActionResult Index()
        {
            if (Session["Cart"] == null) //havn't been to the db yet
            {
                try
                {
                    ProductViewModel prod = new ProductViewModel();
                    List<ProductViewModel> Prods = prod.GetAll();
                    if (Prods.Count() > 0)
                    {
                        CartItem[] myCart = new CartItem[Prods.Count]; //array
                        int ctr = 0;

                        //built CartItem array from List contents
                        foreach (ProductViewModel p in Prods)
                        {
                            CartItem item = new CartItem();
                            item.ProdCd = p.ProdCode;
                            item.ProdName = p.ProdName;
                            item.Graphic = p.Graphic;
                            item.Msrp = p.Msrp;
                            item.Description = p.Description;
                            item.Qty = 0;
                            myCart[ctr++] = item;
                        }
                        Session["Cart"] = myCart; // load to session
                    }
                }
                catch(Exception e)
                {
                    ViewBag.Message = "Catalogue Problem - " + e.Message;
                }
            }
            return View();
        }

        /// <summary>
        /// AddToCart
        /// </summary>
        /// <param name="qty">Number of products wanting to buy</param>
        /// <param name="detailsProdcd">Id of the products wanting to add to cart</param>
        /// <returns></returns>
        public ActionResult AddToCart(int qty, String detailsProdcd)
        {
            CartItem[] Cart;

            Cart = (CartItem[])Session["Cart"];

            foreach(CartItem item in Cart)
            {
                if (item.ProdCd == detailsProdcd)
                {
                    if (qty >= 0) //update only selected item
                        item.Qty = qty;
                    break;
                }
            }

            Session["Cart"] = Cart; // store updates in session
            ViewBag.Message = qty + " - item(s) Added!";
            return PartialView("PopupMessage");
        }
        
	}
}