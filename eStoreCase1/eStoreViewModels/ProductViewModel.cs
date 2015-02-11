using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using eStoreModels;

namespace eStoreViewModels
{
    public class ProductViewModel : eStoreViewModelConfig
    {
        // Data Members
        public int Qty { get; set; }
        public string ProdCode { get; set; }
        public string ProdName {get; set;}
        public string Description { get; set; }
        public string Graphic { get; set; }
        public decimal Msrp { get; set; }
        public decimal CostPrice {get; set;}
        public int Qob { get; set; }
        public int Qoh { get; set; }


        /// <summary>
        /// GetAll - Returns entire Products table from eStoreModels
        /// </summary>
        /// <returns>List of all Products</returns>
        public List<ProductViewModel> GetAll()
        {
            List<ProductViewModel> webProducts = new List<ProductViewModel>();
            try
            {
                ProductModel data = new ProductModel();
                List<Product> dataProducts = data.GetAll();

                // We return ProductViewModel instances as the Asp Layer has no knowledge of the EF
                foreach(Product prod in dataProducts)
                {
                    ProductViewModel pvm = new ProductViewModel();
                    pvm.ProdCode = prod.ProductID;
                    pvm.ProdName = prod.ProductName;
                    pvm.Graphic = prod.GraphicName;
                    pvm.CostPrice = prod.CostPrice;
                    pvm.Msrp = prod.MSRP;
                    pvm.Qob = prod.QtyOnBackOrder;
                    pvm.Qoh = prod.QtyOnHand;
                    pvm.Description = prod.Description;
                    webProducts.Add(pvm); // add to list
                }

            }
            catch(Exception e)
            {
                ErrorRoutine(e, "ProductViewModel", "GetAll");
            }

            return webProducts;
        }
    }


}
