using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DMS_Model;
using System.ComponentModel.DataAnnotations;
using DMS_ViewModel;

namespace DMS_ViewModel
{

    public class CustomerViewModel : DRMViewModelConfig
    {
        // Data Members
        public int CustomerID { get; set; }

        [Required(ErrorMessage = "Username is required.")]
        public string Username { get; set; }

        [Required(ErrorMessage = "Firstname is required.")]
        public string Firstname { get; set; }

        [Required(ErrorMessage = "Lastname is required.")]
        public string Lastname { get; set; }

        [Required(ErrorMessage = "Password is required.")]
        public string Password { get; set; }

        [Required(ErrorMessage = "This Password is required also.")]
        [CompareAttribute("Password", ErrorMessage = "Passwords don't match.")]
        public string RepeatPassword { get; set; }

        [Required(ErrorMessage = "Email is required.")]
        [RegularExpression(@"^([0-9a-zA-Z]([-.\w]*[0-9a-zA-Z])*@([0-9a-zA-Z][-\w]*[0-9a-zA-Z]\.)+[a-zA-Z]{2,9})$", ErrorMessage = "Email format invalid")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Street address is required.")]
        public string Address1 { get; set; }

        [Required(ErrorMessage = "City is required.")]
        public string City{ get; set; }

        [Required(ErrorMessage = "Postal Code is required.")]
        [RegularExpression(@"[a-zA-z]\d[a-zA-Z]\d[a-zA-Z]\d", ErrorMessage="Enter a proper Postal Code")]
        public string Mailcode { get; set; }

        public string Country { get; set; }
        
        public string CreditcardType { get; set; }

        [Required(ErrorMessage = "Region is required.")]
        public string Region { get; set; }
        
        public string Message { get; set; }

        [Required(ErrorMessage = "Age is required.")]
        [Range(18, 99,ErrorMessage="Age must be between 18 - 99")]
        public int Age { get; set; }

        /// <summary>
        /// Registers a Member for the site
        /// </summary>
        public void Register()
        {
            Dictionary<string, Object> dictionaryCustomer = new Dictionary<string, Object>();
            try
            {
                CustomerModel myData = new CustomerModel();
                dictionaryCustomer["username"] = Username;
                dictionaryCustomer["firstname"] = Firstname;
                dictionaryCustomer["lastname"] = Lastname;
                dictionaryCustomer["age"] = Age;
                dictionaryCustomer["address1"] = Address1;
                dictionaryCustomer["city"] = City;
                dictionaryCustomer["mailcode"] = Mailcode;
                dictionaryCustomer["region"] = Region;
                dictionaryCustomer["email"] = Email;
                dictionaryCustomer["country"] = Country;
                dictionaryCustomer["creditcardtype"] = CreditcardType;
                CustomerID = myData.Register(Serializer(dictionaryCustomer));
                Message = "Customer " + CustomerID + " registered!";
            }
            catch(Exception ex)
            {
                Message = "Customer not registered, problem was " + ex.Message;
                ErrorRoutine(ex, "CustomerViewModel", "Register");
            }

        }

        /// <summary>
        /// Deletes a customer if something goes wrong
        /// </summary>
        /// <returns>Customer Number deleted</returns>
        public int Delete()
        {           
            try
            {
                CustomerModel myData = new CustomerModel();
                myData.Delete(CustomerID);
            }
            catch(Exception e)
            {
                throw new Exception(e.Message);
            }

            return CustomerID;
        }

        /// <summary>
        /// Gets the profile once a user is successfully logged in
        /// </summary>
        public void GetCurrentProfile()
        {
            Customer cust = new Customer();
            try
            {
                CustomerModel myData = new CustomerModel();
                cust = myData.GetCurrentProfile(Username);
                CustomerID = cust.CustomerID;
                Firstname = cust.FirstName;
                Lastname = cust.LastName;
                Email = cust.Email;
                Age = (int)cust.Age;
                Address1 = cust.Address1;
                City = cust.City;
                Mailcode = cust.Mailcode;
                Region = cust.Region;
                Country = cust.Country;
                CreditcardType = cust.Creditcardtype;
            }
            catch(Exception e)
            {
                Message = "Customer could not log in, problem was " + e.Message;
                ErrorRoutine(e, "CustomerViewModel", "GetCurrentProfile");
                throw new Exception("Error occured");
            }
        }
    }
}
