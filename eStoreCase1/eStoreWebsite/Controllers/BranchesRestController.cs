using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using eStoreViewModels;

namespace eStoreWebsite.Controllers
{
    public class BranchesRestController : ApiController
    {
        // GET api/ordersrest
        [Route("api/closebranches/{lat:double}/{lng:double}")]
        public IHttpActionResult Get(double lat, double lng)
        {
            BranchViewModel branch = new BranchViewModel();
            branch.Latitude = lat;
            branch.Longitude = lng;
            List<BranchViewModel> closeBranches = branch.GetThreeClosetBranches();

            return Ok(closeBranches); //http 200
        }
    }
}
