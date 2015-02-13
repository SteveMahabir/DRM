using eStoreModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eStoreViewModels
{

    public class OrderViewModel : eStoreViewModelConfig
    {
        //Data Members
        public string Message;
        public int BackOrderFlag;
        public int OrderID;
        public int CustomerID;
        /// <summary>
        /// AddOrder - Completes an order for checking out
        /// </summary>
        /// <param name="items">Item to be ordered</param>
        /// <param name="cid">Customer Id Number</param>
        /// <param name="amt">Total amount for Orders Table</param>
        public void AddOrder(CartItem[] items, int cid, double amt)
        {
            Dictionary<string, Object> dictionaryReturnValues = new Dictionary<string, Object>();
            Dictionary<string, Object> dictionaryOrder = new Dictionary<string, Object>();

            try
            {
                Message = "";
                BackOrderFlag = 0;
                OrderID = -1;
                OrderModel myData = new OrderModel();
                int idx = 0;
                string[] prodcds = new string[items.Length];
                int[] qty = new int[items.Length];
                Decimal[] sellPrice = new Decimal[items.Length];

                foreach(CartItem item in items)
                {
                    prodcds[idx] = item.ProdCd;
                    sellPrice[idx] = item.Msrp;
                    qty[idx++] = item.Qty;
                }

                dictionaryOrder["prodcd"] = prodcds;
                dictionaryOrder["qty"] = qty;
                dictionaryOrder["msrp"] = sellPrice;
                dictionaryOrder["cid"] = cid;
                dictionaryOrder["amt"] = amt;
                dictionaryReturnValues = (Dictionary<string, Object>)Deserializer(myData.AddOrder(Serializer(dictionaryOrder)));
                OrderID = Convert.ToInt32(dictionaryReturnValues["orderid"]);
                BackOrderFlag = Convert.ToInt32(dictionaryReturnValues["boflag"]);
                Message = Convert.ToString(dictionaryReturnValues["message"]);

            }
            catch(Exception e)
            {
                ErrorRoutine(e, "OrderViewModel", "AddOrder");
            }
        }

        /// <summary>
        /// Hits the database and returns a list of all orders one customer has made.
        /// </summary>
        /// <returns>A list of orders contiaining only important information</returns>
        public List<OrderDetailsViewModel> GetAllDetailsForAllOrders()
        {
            List<OrderDetailsViewModel> viewModelDetails = new List<OrderDetailsViewModel>();

            try
            {
                OrderModel myData = new OrderModel();
                List<OrderDetailsModel> modelDetails = myData.GetAllDetailsForAllOrders(CustomerID); //TODO: Change 1 to CustomerID

                // this could be done with a foreach loop as well - see above
                viewModelDetails = modelDetails.ConvertAll(new Converter<OrderDetailsModel, OrderDetailsViewModel>(ModelToViewModel));
            }
            catch(Exception e)
            {
                ErrorRoutine(e, "OrderViewModel", "GetAllDetailsForAllOrders");
            }
            return viewModelDetails;
        }

        /// <summary>
        /// Conversion method used for transfering a database object to a viewmodel object
        /// </summary>
        /// <param name="o">OrderDetailsModel</param>
        /// <returns>OrderDetailsViewModel</returns>
        private OrderDetailsViewModel ModelToViewModel(OrderDetailsModel o)
        {
            OrderDetailsViewModel v = new OrderDetailsViewModel();
            v.OrderID = o.OrderID;
            v.ProductName = o.ProductName;
            v.QtyBackOrdered = o.QtyBackOrdered;
            v.QtyOrdered = o.QtyOrdered;
            v.QtySold = o.QtySold;
            v.SellingPrice = o.SellingPrice;
            v.OrderDate = o.OrderDate;
            return v;
        }
    }


    /// <summary>
    /// A detailed list of one item in an order
    /// </summary>
    public class OrderDetailsViewModel
    {
        public int OrderID { get; set; }
        public string ProductName { get; set; }
        public decimal SellingPrice { get; set; }
        public int QtySold { get; set; }
        public int QtyOrdered { get; set; }
        public int QtyBackOrdered { get; set; }
        public DateTime OrderDate { get; set; }
    }
}
