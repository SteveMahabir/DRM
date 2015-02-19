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

    public class ParentViewModel : DRMViewModelConfig
    {

        public ParentViewModel() { }
        private ParentViewModel(int parentid,
                                string firstname,
                                string lastname,
                                string address,
                                string city,
                                string province,
                                string phone,
                                string username,
                                string password,
                                string email,
                                string repeatpassword)
        {
            ParentID = parentid;
            Firstname = firstname;
            Lastname = lastname;
            Address = address;
            City = city;
            Province = province;
            Phone = phone;
            Username = username;
            Password = password;
            Email = email;
            RepeatPassword = repeatpassword;		
        }


        // Data Members
        public int ParentID { get; set; }
        
        [Required(ErrorMessage = "Firstname is required.")]
        public string Firstname { get; set; }

        [Required(ErrorMessage = "Lastname is required.")]
        public string Lastname { get; set; }

        [Required(ErrorMessage = "Street address is required.")]
        public string Address { get; set; }

        [Required(ErrorMessage = "City is required.")]
        public string City { get; set; }

        [Required(ErrorMessage = "Province is required.")]
        public string Province { get; set; }

        [Required(ErrorMessage = "Phone number is required.")]
        public string Phone { get; set; }

        [Required(ErrorMessage = "Username is required.")]
        public string Username { get; set; }

        [Required(ErrorMessage = "Password is required.")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Email is required.")]
        [RegularExpression(@"^([0-9a-zA-Z]([-.\w]*[0-9a-zA-Z])*@([0-9a-zA-Z][-\w]*[0-9a-zA-Z]\.)+[a-zA-Z]{2,9})$", ErrorMessage = "Email format invalid")]
        public string Email { get; set; }

        [Required(ErrorMessage = "This Password is required also.")]
        [CompareAttribute("Password", ErrorMessage = "Passwords don't match.")]
        public string RepeatPassword { get; set; }
        
        public string Message { get; set; }



        /// <summary>
        /// Registers a Member for the site
        /// </summary>
        public void Register()
        {
            Dictionary<string, Object> dictionaryParent = new Dictionary<string, Object>();
            try
            {
                ParentModel myData = new ParentModel();
                dictionaryParent["firstname"] = Firstname;
                dictionaryParent["lastname"] = Lastname;
                dictionaryParent["address"] = Address;
                dictionaryParent["city"] = City;
                dictionaryParent["province"] = Province;
                dictionaryParent["phone"] = Phone;
                dictionaryParent["username"] = Username;
                dictionaryParent["password"] = Password;
                dictionaryParent["email"] = Email;

                ParentID = myData.Register(Serializer(dictionaryParent));
                Message = "Customer " + ParentID + " registered!";
            }
            catch(Exception ex)
            {
                Message = "Parent not registered, problem was " + ex.Message;
                ErrorRoutine(ex, "ParentViewModel", "Register");
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
                myData.Delete(ParentID);
            }
            catch(Exception e)
            {
                throw new Exception(e.Message);
            }

            return ParentID;
        }

        /// <summary>
        /// Gets the profile once a user is successfully logged in
        /// </summary>
        public void GetCurrentProfile()
        {
            Parent par = new Parent();
            try
            {
                ParentModel myData = new ParentModel();
                par = myData.GetCurrentProfile(Username);

                ParentID = par.ParentId;
                Firstname = par.PFirstName;
                Lastname = par.PLastName;
                Address = par.PAddress;
                City = par.PCity;
                Province = par.PProvince;
                Phone = par.PPhone;
                Username = par.userName;
                Password = par.Password;
                Email = par.Email;
            }
            catch(Exception e)
            {
                Message = "Parent could not log in, problem was " + e.Message;
                ErrorRoutine(e, "ParentViewModel", "GetCurrentProfile");
                throw new Exception("Error occured");
            }
        }

        public List<ParentViewModel> GetParentList()
        {
            List<ParentViewModel> parrins = new List<ParentViewModel>();
            try
            {
                ParentModel pm = new ParentModel();
                List<Parent> pList = pm.GetAll();

                //We return ProductViewModel instances as the ASP layer has no knowledge of EF
                foreach (Parent p in pList)
                {
                    ParentViewModel pvm = new ParentViewModel();
                    pvm.Firstname = p.PFirstName;
                    pvm.Lastname = p.PLastName;
                    pvm.ParentID = (int)p.ParentId;
                    parrins.Add(pvm);//add to the list
                }

            }
            catch (Exception ex)
            {

                ErrorRoutine(ex, "ProductViewModel", "GetAll");
            }
            return parrins;


        }


        public override string ToString()
        {
            string res = "First Name: " + Firstname + "Last Name: " + Lastname;
            return res;
        }

    }
}
