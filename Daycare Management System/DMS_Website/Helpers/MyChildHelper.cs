using DMS_ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
namespace DMS_Website.Helpers
{
    public static class MyChildHelper
    {
        public static HtmlString myChildren(this HtmlHelper helper, string id) {
            //Create tag builder
            var builder = new TagBuilder("ul class='group-list'");
            StringBuilder innerHtml = new StringBuilder();
            ChildViewModel cvm = new ChildViewModel();
            List<ChildViewModel> l_cvm = cvm.GetChildList();
            builder.GenerateId(id);
            foreach (var item in l_cvm)
            {
                innerHtml.Append("<li>" + item.Firstname + "<a href=\"#detail_popup\" data-toggle=\"modal\" class=\"btn btn-primary\" data-prodcd=\"" + item.ChildID + "\">Details</a></li>");

                //innerHtml.Append("<li>");
                //innerHtml.Append("<div class='img' style='padding-left: 13%;'><img alt='Surfboard Shop' src='Content/eStoreImages/ .jpg' id='Graphic '/> <div>");
                //innerHtml.Append("<div class='info'><h3 id='Name '> </h3>");
                //innerHtml.Append("<p id='Descr ' data-description=' '>");
                //innerHtml.Append("...</p>");
                //innerHtml.Append("<div class='price'><span class='st'>Our price:</span>");
                //innerHtml.Append("<strong id='Price '> {0:C}");
                //innerHtml.Append("</strong></div><div class='actions' style='padding-bottom: 50px'>");
                //innerHtml.Append("<a href=\"#detail_popup\" data-toggle=\"modal\" class=\"btn btn-primary\" data-prodcd=\"");
                //innerHtml.Append( "\">Details</a></div></div></li>");
            }

            //Create valid id
            
            

            builder.InnerHtml = innerHtml.ToString();
            return new HtmlString(builder.ToString());
        }

    }
}