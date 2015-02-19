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

    public class EmployeeViewModel : DRMViewModelConfig
    {
        public EmployeeViewModel() { }
        private EmployeeViewModel(  int employeeid,
                                    string firstname,
                                    string lastname,
                                    string address,
                                    string city,
                                    string province,
                                    string role,
                                    string phone,
                                    string username,
                                    string password,
                                    string email,
                                    string repeatpassword,
                                    string message
        )
        {
            EmployeeID = employeeid;
            Firstname = firstname;
            Lastname = lastname;
            Address = address;
            City = city;
            Province = province;
            Role = role;
            Phone = phone;
            Username = username;
            Password = password;
            Email = email;
            RepeatPassword = repeatpassword;
            Message = message;	
        }


        // Data Members
        public int EmployeeID { get; set; }
        
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

        public string Role { get; set; }

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
            Dictionary<string, Object> dictionaryEmployee = new Dictionary<string, Object>();
            try
            {
                EmployeeModel myData = new EmployeeModel();
                dictionaryEmployee["firstname"] = Firstname;
                dictionaryEmployee["lastname"] = Lastname;
                dictionaryEmployee["address"] = Address;
                dictionaryEmployee["city"] = City;
                dictionaryEmployee["province"] = Province;
                dictionaryEmployee["phone"] = Phone;
                dictionaryEmployee["role"] = Role;
                dictionaryEmployee["username"] = Username;
                dictionaryEmployee["password"] = Password;
                dictionaryEmployee["email"] = Email;

                EmployeeID = myData.Register(Serializer(dictionaryEmployee));
                Message = "Customer " + EmployeeID + " registered!";
            }
            catch(Exception ex)
            {
                Message = "Employee not registered, problem was " + ex.Message;
                ErrorRoutine(ex, "EmployeeViewModel", "Register");
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
                myData.Delete(EmployeeID);
            }
            catch(Exception e)
            {
                throw new Exception(e.Message);
            }

            return EmployeeID;
        }

        /// <summary>
        /// Gets the profile once a user is successfully logged in
        /// </summary>
        public void GetCurrentProfile()
        {
            Employee emp = new Employee();
            try
            {
                EmployeeModel myData = new EmployeeModel();
                emp = myData.GetCurrentProfile(Username);

                EmployeeID = emp.EmployeeId;
                Firstname = emp.EFirstName;
                Lastname = emp.ELastName;
                Address = emp.EAddress;
                City = emp.ECity;
                Province = emp.EProvince;
                Phone = emp.EPhone;
                Role = emp.ERole;
                Username = emp.userName;
                Password = emp.Password;
                Email = emp.Email;
            }
            catch(Exception e)
            {
                Message = "Employee could not log in, problem was " + e.Message;
                ErrorRoutine(e, "EmployeeViewModel", "GetCurrentProfile");
                throw new Exception("Error occured");
            }
        }

        public List<EmployeeViewModel> GetEmployeeList()
        {
            List<EmployeeViewModel> empies = new List<EmployeeViewModel>();
            try
            {
                EmployeeModel pm = new EmployeeModel();
                List<Employee> pList = pm.GetAll();

                //We return ProductViewModel instances as the ASP layer has no knowledge of EF
                foreach (Employee e in pList)
                {
                    EmployeeViewModel evm = new EmployeeViewModel();
                    evm.Firstname = e.EFirstName;
                    evm.Lastname = e.ELastName;
                    evm.EmployeeID = (int)e.EmployeeId;
                    empies.Add(evm);//add to the list
                }

            }
            catch (Exception ex)
            {

                ErrorRoutine(ex, "ProductViewModel", "GetAll");
            }
            return empies;




        }
    }
}
