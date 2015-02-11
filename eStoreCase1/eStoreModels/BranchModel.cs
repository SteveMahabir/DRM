using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eStoreModels
{
    /// <summary>
    /// BranchModel
    ///     Author:     Evan Lauersen
    ///     Date:       Created: Feb 27, 2014
    ///     Inherits:   Standard config data
    ///     Purpose:    Model Layer Class to interface between Database and BranchViewModel class
    ///     Revisions:  none
    /// </summary>
    public class BranchModel : eStoreModelConfig
    {
        /// <summary>
        /// Get Closest Branches
        /// </summary>
        /// <param name="dictionaryAddresses">latitude and longitude coordinates determined by google</param>
        /// <returns>List of details of 3 closet branches</returns>
        public List<CloseBranchDetails> GetClosetBranches(Dictionary<string, object> dictionaryAddresses)
        {
            eStoreDBEntities dbContext;
            List<CloseBranchDetails> branchDetails = new List<CloseBranchDetails>();

            try
            {
                dbContext = new eStoreDBEntities();
                float? latitude = Convert.ToSingle(dictionaryAddresses["lat"]);
                float? longitude = Convert.ToSingle(dictionaryAddresses["long"]);
                // executes stored proc via EF function
                branchDetails = dbContext.pGetThreeClosestBranches(latitude, longitude).ToList();
            }
            catch (Exception e)
            {
                ErrorRoutine(e, "OrderModel", "GetClosestBranches");
            }
            return branchDetails;
        }
    }
}
