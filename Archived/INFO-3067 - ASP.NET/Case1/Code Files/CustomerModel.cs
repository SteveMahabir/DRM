using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eStoreModels
{
    public class CustomerModel : eStoreModelConfig
    {
        /// <summary>
        /// Registers a customer to the database
        /// </summary>
        /// <param name="bytCustomer"></param>
        /// <returns>Customer number once completed</returns>
        public int Register(byte[] bytCustomer)
        {
            int custId = -1;
            Customer cust = new Customer();
            eStoreDBEntities dbContext = new eStoreDBEntities();

            try
            {
                Dictionary<string, Object> dictionaryCustomer = (Dictionary<string, Object>)Deserializer(bytCustomer);
                dbContext = new eStoreDBEntities();
                cust.Username = Convert.ToString(dictionaryCustomer["username"]);
                cust.FirstName = Convert.ToString(dictionaryCustomer["firstname"]);
                cust.LastName = Convert.ToString(dictionaryCustomer["lastname"]);
                cust.Email = Convert.ToString(dictionaryCustomer["email"]);
                cust.Age = Convert.ToInt32(dictionaryCustomer["age"]);
                cust.Address1 = Convert.ToString(dictionaryCustomer["address1"]);
                cust.City = Convert.ToString(dictionaryCustomer["city"]);
                cust.Mailcode = Convert.ToString(dictionaryCustomer["mailcode"]);
                cust.Region = Convert.ToString(dictionaryCustomer["region"]);
                cust.Country = Convert.ToString(dictionaryCustomer["country"]);
                cust.Creditcardtype = Convert.ToString(dictionaryCustomer["creditcardtype"]);
                dbContext.Customers.Add(cust);
                dbContext.SaveChanges();
                custId = cust.CustomerID;
            }
            catch (Exception ex)
            {
                ErrorRoutine(ex, "CustomerModel", "Register");
            }
            return custId;
        }

        /// <summary>
        /// Gets the current profile from the database
        /// </summary>
        /// <param name="username"></param>
        /// <returns></returns>
        public Customer GetCurrentProfile(string username)
        {
            Customer cust = new Customer();
            try
            {
                eStoreDBEntities dbContext = new eStoreDBEntities();
                cust = dbContext.Customers.FirstOrDefault(c => c.Username == username);
            }
            catch(Exception e)
            {
                throw new Exception(e.Message);
            }
            return cust;
        }

        /// <summary>
        /// Deletes a customer when something goes wrong
        /// </summary>
        /// <param name="customerId">Customer Id of customer to be deleted</param>
        public void Delete(int customerId)
        {
            Customer cust = new Customer();
            try
            {
                eStoreDBEntities dbContext = new eStoreDBEntities();
                cust = dbContext.Customers.FirstOrDefault(c => c.CustomerID == customerId);
                dbContext.Customers.Remove(cust);
                dbContext.SaveChanges();
            }
            catch(Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}
