using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eStoreModels
{
    public class ProductModel : eStoreModelConfig 
    {
        /// <summary>
        /// Gets the entire list of products from the eStore Database
        /// </summary>
        /// <returns></returns>
        public List<Product> GetAll()
        {
            eStoreDBEntities dbContext;
            List<Product> allProducts = null;
            try
            {
                dbContext = new eStoreDBEntities();
                allProducts = dbContext.Products.ToList();
            }
            catch (Exception e)
            {
                ErrorRoutine(e, "ProductData", "GetAll");
            }
            return allProducts;
        }       
    }
}
