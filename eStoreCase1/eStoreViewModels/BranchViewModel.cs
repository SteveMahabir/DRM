using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using eStoreModels;

namespace eStoreViewModels
{
    /// BranchViewModel
    ///     Author:     Evan Lauersen
    ///     Date:       Created: Feb 27, 2014
    ///     Inherits:   Standard config data
    ///     Purpose:    View Model Class to interface with DAL, will serve the 
    ///                 Branch controller
    ///     Revisions:  none
    public class BranchViewModel : eStoreViewModelConfig
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
        /// Get 3 closet branches details for a single order
        /// </summary>
        /// <returns>List of OrderDetailWeb instances to Presentation layer</returns>
        public List<BranchViewModel> GetThreeClosetBranches()
        {
            List<BranchViewModel> webDetails = new List<BranchViewModel>();
            try
            {
                BranchModel data = new BranchModel();
                Dictionary<string, Object> dictionaryAddresses = new Dictionary<string, Object>();
                dictionaryAddresses["lat"] = Latitude;
                dictionaryAddresses["long"] = Longitude;
                List<CloseBranchDetails> dataDetails = data.GetClosetBranches(dictionaryAddresses);

                // We return BranchViewModel instances as the Web layer should have 
                // no knowledge of EF objects
                foreach (CloseBranchDetails d in dataDetails)
                {
                    BranchViewModel brn = new BranchViewModel();
                    brn.BranchID = d.BranchID;
                    brn.City = d.City;
                    brn.Street = d.Street;
                    brn.Region = d.Region;
                    brn.Latitude = Convert.ToDouble(d.Latitude);
                    brn.Longitude = Convert.ToDouble(d.Longitude);
                    brn.Distance = Convert.ToDouble(d.DistanceFromAddress);
                    webDetails.Add(brn);
                }
            }
            catch (Exception e)
            {
                ErrorRoutine(e, "BranchViewModel", "GetThreeClosestBranches");
            }
            return webDetails;
        }

    }
}
