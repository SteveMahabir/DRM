using eStoreViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace eStoreWebsite.Helpers
{
    public static class CatalogueHelper
    {

        public static HtmlString Catalogue(this HtmlHelper helper, string id)
        {
            //Create tag builder
            var builder = new TagBuilder("ul class='group-list'");
            StringBuilder innerHtml = new StringBuilder();
            
            //Create valid id
            builder.GenerateId(id);

            //Render tag
            if (HttpContext.Current.Session["Cart"] != null) // havn't been to the db yet
            {
                CartItem[] cart = (CartItem[])HttpContext.Current.Session["Cart"];
                foreach (CartItem item in cart)
                {
                    innerHtml.Append("<li>");
                    innerHtml.Append("<div class='img' style='padding-left: 13%;'><img alt='Surfboard Shop' src='Content/eStoreImages/" + item.Graphic + ".jpg' id='Graphic" + item.ProdCd + "'/> <div>");
                    innerHtml.Append("<div class='info'><h3 id='Name" + item.ProdCd + "'>" + item.ProdName + "</h3>");
                    innerHtml.Append("<p id='Descr" + item.ProdCd + "' data-description='" + item.Description + "'>");
                    innerHtml.Append(item.Description.Substring(0, 20) + "...</p>");
                    innerHtml.Append("<div class='price'><span class='st'>Our price:</span>");
                    innerHtml.Append("<strong id='Price" + item.ProdCd + "'>" + String.Format("{0:C}", Convert.ToDecimal(item.Msrp)));
                    innerHtml.Append("</strong></div><div class='actions' style='padding-bottom: 50px'>");
                    innerHtml.Append("<a href=\"#detail_popup\" data-toggle=\"modal\" class=\"btn btn-primary\" data-prodcd=\"");
                    innerHtml.Append(item.ProdCd + "\">Details</a></div></div></li>");
                }

            }
            builder.InnerHtml = innerHtml.ToString();
            return new HtmlString(builder.ToString());
        }

    }
}