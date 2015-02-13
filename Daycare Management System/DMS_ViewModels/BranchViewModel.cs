using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DMS_Model;
using DMS_ViewModel;

namespace DMS_ViewModel
{
    public class BranchViewModel : DRMViewModelConfig
    {
        // Auto implemented properties
        public int BranchID { get; set; }
        public string Street { get; set; }
        public string City { get; set; }
        public string Region { get; set; }
        public double Longitude { get; set; }
        public double Latitude { get; set; }
        public double Distance { get; set; }

        /// <summary>
        /// Get 3 closet branches details for a single location
        /// </summary>
        /// <returns>List of BranchViewModel instances to Presentation layer</returns>
        //public List<BranchViewModel> GetThreeClosetBranches()
        //{
        //    List<BranchViewModel> webDetails = new List<BranchViewModel>();
        //    try
        //    {
        //        BranchModel data = new BranchModel();
        //        Dictionary<string, Object> dictionaryAddresses = new Dictionary<string, Object>();
        //        dictionaryAddresses["lat"] = Latitude;
        //        dictionaryAddresses["long"] = Longitude;
        //        List<CloseBranchDetails> dataDetails = data.GetClosetBranches(dictionaryAddresses);

        //        // We return BranchViewModel instances as the Web layer should have 
        //        // no knowledge of EF objects
        //        foreach (CloseBranchDetails d in dataDetails)
        //        {
        //            BranchViewModel brn = new BranchViewModel();
        //            brn.BranchID = d.BranchID;
        //            brn.City = d.City;
        //            brn.Street = d.Street;
        //            brn.Region = d.Region;
        //            brn.Latitude = Convert.ToDouble(d.Latitude);
        //            brn.Longitude = Convert.ToDouble(d.Longitude);
        //            brn.Distance = Convert.ToDouble(d.DistanceFromAddress);
        //            webDetails.Add(brn);
        //        }
        //    }
        //    catch (Exception e)
        //    {
        //        ErrorRoutine(e, "BranchViewModel", "GetThreeClosestBranches");
        //    }
        //    return webDetails;
        //}

    }
}
