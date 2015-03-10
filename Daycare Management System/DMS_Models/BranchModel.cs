using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DMS_Model
{
    public class BranchModel : DMS_ModelConfig
    {
        /// <summary>
        /// Get Closest Branches
        /// </summary>
        /// <param name="dictionaryAddresses">latitude and longitude coordinates determined by google</param>
        /// <returns>List of details of 3 closet branches</returns>
        public List<CloseBranchDetails> GetClosetBranches(Dictionary<string, object> dictionaryAddresses)
        {
            NeighbourhoodDataEntities dbContext;
            List<CloseBranchDetails> branchDetails = new List<CloseBranchDetails>();

            try
            {
                dbContext = new NeighbourhoodDataEntities();
                float? latitude = Convert.ToSingle(dictionaryAddresses["lat"]);
                float? longitude = Convert.ToSingle(dictionaryAddresses["long"]);

                //branchDetails = dbContext.pGetThreeClosestBranches(latitude, longitude).ToList();
            }
            catch (Exception e)
            {
                ErrorRoutine(e, "OrderModel", "GetClosestBranches");
            }
            return branchDetails;
        }
    }
}
