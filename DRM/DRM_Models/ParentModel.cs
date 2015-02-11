using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DRM_Models
{
    public class ParentModel : DatabaseConfig
    {

        /// <summary>
        /// Get Closest Branches
        /// </summary>
        /// <param name="dictionaryAddresses">latitude and longitude coordinates determined by google</param>
        /// <returns>List of details of 3 closet branches</returns>
        public List<Parent> GetAllParents()
        {
            List<Parent> parentDetails = new List<Parent>();
            DatabaseEntities dbContext;

            try
            {
            //    dbContext = new DatabaseEntities();
            //    float? latitude = Convert.ToSingle(dictionaryAddresses["lat"]);
            //    float? longitude = Convert.ToSingle(dictionaryAddresses["long"]);
            //    // executes stored proc via EF function
                //branchDetails = dbContext.pGetThreeClosestBranches(latitude, longitude).ToList();


                //parentDetails = dbContext.GetAllparents().ToList();
                parentDetails.Add(new Parent());
            }
            catch (Exception e)
            {
                ErrorRoutine(e, "OrderModel", "GetClosestBranches");
            }
            return parentDetails;
        }
    }
}
