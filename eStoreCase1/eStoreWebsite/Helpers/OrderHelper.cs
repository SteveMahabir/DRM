using eStoreViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace eStoreWebsite.Helpers
{
    public static class OrdersHelper
    {

        public static HtmlString Orders(this HtmlHelper helper, string id)
        {
            //Create tag builder
            var builder = new TagBuilder("table id='Orders' class='row text-center'style='margin-left: 1px; border-spacing: 15px; width: 100%; text-align: center;'");
            StringBuilder innerHtml = new StringBuilder();

            //Create valid id
            builder.GenerateId(id);

            //Get the list of Orders
            List<OrderDetailsViewModel> allOrders = new List<OrderDetailsViewModel>();

            #region Get-Order
            try
            {
                OrderViewModel data = new OrderViewModel();
                data.CustomerID = (int)HttpContext.Current.Session["CustomerID"];
                allOrders = data.GetAllDetailsForAllOrders();
            }
            catch (Exception e)
            {
                innerHtml.Append("<h3 style='text-align: center;'>Error, could not get order information: " + e.Message + "</h3>");
                builder.InnerHtml = innerHtml.ToString();
                return new HtmlString(builder.ToString());
            }
            #endregion

            if (allOrders.Count() != 0)
            {
                #region Header
                innerHtml.Append("<tr style='width: 20%; padding-left:15px; text-align: center; padding-top: 10px;'>");
                innerHtml.Append("<td><b>ID</b></td>");
                innerHtml.Append("<td><b>Date</b></td>");
                innerHtml.Append("</tr>");
                #endregion

                int tempOrderId = 0;

                foreach (OrderDetailsViewModel order in allOrders)
                {
                    if (order.OrderID != tempOrderId)
                    {
                        tempOrderId = order.OrderID;
                        #region Body
                        innerHtml.Append("<tr style='padding:5px; text-align: center;'>");
                        innerHtml.Append("<td id='OrderNo'>" + order.OrderID + "</td>");
                        innerHtml.Append("<td>" + order.OrderDate + "</td>");
                        innerHtml.Append("</tr>");
                        #endregion
                    }
                }
            }
            else
            {
                innerHtml.Append("<h1 style='text-align: center;'>Sorry, No Orders Found!</h1>");
            }

            builder.InnerHtml = innerHtml.ToString();
            return new HtmlString(builder.ToString());
        }

        //Used for rounding purposes
        #region Mathimatical-Helpers

        private static string Round(decimal itemTotal)
        {
            double roundedValue = (Convert.ToDouble(itemTotal) + 0.005) * 100;
            roundedValue = (int)roundedValue;
            return Convert.ToString(roundedValue / 100.00);
        }

        private static string Round(double itemTotal)
        {
            double roundedValue = (itemTotal + 0.005) * 100;
            roundedValue = (int)roundedValue;
            return Convert.ToString(roundedValue / 100.00);
        }

        #endregion
    }
}