using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using DRM_Models;

namespace DRM_ViewModels
{
    public class ParentViewModel : DatabaseViewModelConfig
    {
        #region Parent Info

        //Data Members
        public int ParentId { get; set; }

        [Required(ErrorMessage = "First Name is required.")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Last Name is required.")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Address is required.")]
        public string Address { get; set; }

        [Required(ErrorMessage = "City is required.")]
        public string City { get; set; }

        [Required(ErrorMessage = "Province is required.")]
        public string Province { get; set; }

        [Required(ErrorMessage = "Phone Number is required.")]
        public string Phone { get; set; }

        [Required(ErrorMessage = "Username is required.")]
        public string Username { get; set; }

        [Required(ErrorMessage = "Password is required.")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Email is required.")]
        public string Email { get; set; }



        public string Message { get; set; }

        #endregion

        public void GetParent()
        {
            try
            {
                ParentModel data = new ParentModel();
                List<Parent> temp = data.GetAllParents();
                Parent currentParent = temp[0];

                ParentId = currentParent.ParentId;
                FirstName = currentParent.PFirstName;
                LastName = currentParent.PLastName;
                Address = currentParent.PAddress;
                City = currentParent.PCity;
                Province = currentParent.PProvince;
                Phone = currentParent.PPhone;
                Username = currentParent.userName;
                Password = currentParent.Password;
                Email = currentParent.Email;
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
