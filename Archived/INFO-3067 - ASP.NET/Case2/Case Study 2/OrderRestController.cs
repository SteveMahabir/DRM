using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using eStoreViewModels;

namespace eStoreWebsite.Controllers
{
    public class OrderRestController : ApiController
    {
        // GET api/orderrest
        public IHttpActionResult Get(int id)
        {
            OrderViewModel myOrder = new OrderViewModel();
            myOrder.CustomerID = id;
            List<OrderDetailsViewModel> orders = myOrder.GetAllDetailsForAllOrders();
            return Ok(orders); //http 200
        }
    }
}
