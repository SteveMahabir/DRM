using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace eStoreModels
{
    public class OrderModel : eStoreModelConfig
    {

        /// <summary>
        /// Completes the order process by adding order items to the database
        /// </summary>
        /// <param name="bytOrder">Dictionary holding order information</param>
        /// <returns>populated dictionary with new order #, bo flag or error</returns>
        public byte[] AddOrder(byte[] bytOrder)
        {
            Dictionary<string, Object> dictionaryOrder = (Dictionary<string, Object>)Deserializer(bytOrder);
            Dictionary<string, Object> dictionaryReturnValues = new Dictionary<string, Object>();
            eStoreDBEntities dbContext;
            Order myOrder;
            bool boFlg = false;

            //Deserialize dictionary Contexts into local variables
            int[] qty = (int[])dictionaryOrder["qty"];
            string[] prodcd = (string[])dictionaryOrder["prodcd"];
            Decimal[] sellPrice = (Decimal[])dictionaryOrder["msrp"];
            int cid = Convert.ToInt32(dictionaryOrder["cid"]);

            // Define a transactin scope for the operations.
            using (TransactionScope transaction = new TransactionScope())
            {
                try
                {

                    dbContext = new eStoreDBEntities();
                    myOrder = new Order();


                    #region Order
                    // back to the db to get the right customer based on session customer id
                    var selectedCusts = dbContext.Customers.Where(c => c.CustomerID == cid);
                    myOrder.Customer = selectedCusts.First();
                    myOrder.OrderDate = DateTime.Now;

                    // build the order
                    myOrder.OrderAmount = Convert.ToDecimal(dictionaryOrder["amt"]);

                    // Add the order
                    dbContext.Orders.Add(myOrder); //Add the order and get the orderid for LineItem

                    #endregion

                    #region OrderListItem
                    for (int idx = 0; idx < qty.Length; idx++)
                    {
                        OrderLineitem item = new OrderLineitem();
                        string pcd = prodcd[idx];
                        var selectedProd = dbContext.Products.Where(p => p.ProductID == pcd);
                        item.Product = selectedProd.First(); //god product for item

                        //Only worry about ordered items
                        if (qty[idx] != 0)
                        {

                            if (item.Product.QtyOnHand > qty[idx]) // enough stock
                            {
                                item.Product.QtyOnHand = item.Product.QtyOnHand - qty[idx];
                                item.QtySold = qty[idx];
                                item.QtyOrdered = qty[idx];
                                item.QtyBackOrdered = 0;
                                item.SellingPrice = sellPrice[idx];
                            }
                            else // not enough stock
                            {
                                item.QtyBackOrdered = qty[idx] - item.Product.QtyOnHand;
                                item.Product.QtyOnBackOrder = item.Product.QtyOnBackOrder + (qty[idx] - item.Product.QtyOnHand);
                                item.Product.QtyOnHand = 0;
                                item.QtyOrdered = qty[idx];
                                item.QtySold = item.QtyOrdered - item.QtyBackOrdered;
                                item.SellingPrice = sellPrice[idx];
                                boFlg = true; //Something has been backordered
                            }

                            myOrder.OrderLineitems.Add(item);
                        }
                    }

                    #endregion

                    dbContext.SaveChanges(); //made it this far, persist changes
                    //throw new Exception ("Rollback"); //test trans by uncommenting out this line

                    //Mark the transaction as complete
                    transaction.Complete();

                    dictionaryReturnValues.Add("orderid", myOrder.OrderID);
                    dictionaryReturnValues.Add("boflag", boFlg);
                    dictionaryReturnValues.Add("message", "");
                }
                catch (Exception e)
                {
                    ErrorRoutine(e, "OrderModel", "AddOrder");
                    dictionaryReturnValues.Add("message", "Problem with Order");
                }


                return Serializer(dictionaryReturnValues);
            }
        }

    }
}
