using DMS_ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
namespace DMS_Website.Helpers
{
    public static class ChildHelper
    {
        public static HtmlString children(this HtmlHelper helper, string id) {
            //Create tag builder
            var builder = new TagBuilder("ul class='group-list'");
            StringBuilder innerHtml = new StringBuilder();
            ChildViewModel cvm = new ChildViewModel();
            List<ChildViewModel> l_cvm = cvm.GetChildList();
            builder.GenerateId(id);
            foreach (var item in l_cvm)
            {
                innerHtml.Append("<li>" + item.Firstname +"</li>");
            }

            //Create valid id
            
            

            builder.InnerHtml = innerHtml.ToString();
            return new HtmlString(builder.ToString());
        }

    }
}