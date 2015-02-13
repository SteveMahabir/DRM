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
                
            }
        }

    }
}
