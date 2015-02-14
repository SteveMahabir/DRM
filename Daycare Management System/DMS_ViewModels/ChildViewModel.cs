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

    public class ChildViewModel : DRMViewModelConfig
    {
        public ChildViewModel() { }
        private ChildViewModel( int childid,
                                string firstname,
                                string lastname,
                                DateTime dob,
                                string address,
                                string city,
                                string province,
                                int healthcard,
                                string docname,
                                string docphone,
                                string comments,
                                int parentid,
                                string message)		
        {
            ChildID = childid;
            Firstname = firstname;
            Lastname = lastname;
            DoB = dob;
            Address = address;
            City = city;
            Province = province;
            HealthCard = healthcard;
            DocName = docname;
            DocPhone = docphone;
            Comments = comments;
            ParentId = parentid;
            Message = message;
        }

        // Data Members
        public int ChildID { get; set; }
        
        [Required(ErrorMessage = "Firstname is required.")]
        public string Firstname { get; set; }

        [Required(ErrorMessage = "Lastname is required.")]
        public string Lastname { get; set; }


        [Required(ErrorMessage = "Date of Birth is required.")]
        public DateTime DoB { get; set; }

        [Required(ErrorMessage = "Street address is required.")]
        public string Address { get; set; }

        [Required(ErrorMessage = "City is required.")]
        public string City { get; set; }

        [Required(ErrorMessage = "Province is required.")]
        public string Province { get; set; }

        [Required(ErrorMessage = "Health Card is required.")]
        public int HealthCard { get; set; }

        [Required(ErrorMessage = "Doctors Name number is required.")]
        public string DocName { get; set; }

        [Required(ErrorMessage = "Doctors Phone number is required.")]
        public string DocPhone { get; set; }

        [Required(ErrorMessage = "Comments are required.")]
        public string Comments { get; set; }

        public int ParentId { get; set; }
        
        public string Message { get; set; }

        /// <summary>
        /// Registers a Member for the site
        /// </summary>
        public void Register()
        {
            Dictionary<string, Object> dictionaryChild = new Dictionary<string, Object>();
            try
            {
                ChildModel myData = new ChildModel();
                dictionaryChild["firstname"] = Firstname;
                dictionaryChild["lastname"] = Lastname;
                dictionaryChild["dob"] = DoB;
                dictionaryChild["address"] = Address;
                dictionaryChild["city"] = City;
                dictionaryChild["province"] = Province;
                dictionaryChild["HealthCard"] = HealthCard;
                dictionaryChild["docname"] = DocName;
                dictionaryChild["docphone"] = DocPhone;
                dictionaryChild["comments"] = Comments;
                dictionaryChild["parentid"] = ParentId;

                ChildID = myData.Register(Serializer(dictionaryChild));
                Message = "Child " + ChildID + " registered!";
            }
            catch(Exception ex)
            {
                Message = "Child not registered, problem was " + ex.Message;
                ErrorRoutine(ex, "ChildViewModel", "Register");
            }

        }

        /// <summary>
        /// Deletes a child if something goes wrong
        /// </summary>
        /// <returns>Child Number deleted</returns>
        public int Delete()
        {           
            try
            {
                ChildModel myData = new ChildModel();
                myData.Delete(ChildID);
            }
            catch(Exception e)
            {
                throw new Exception(e.Message);
            }

            return ChildID;
        }

        /// <summary>
        /// Gets the profile once a user is successfully logged in
        /// </summary>
        public void GetCurrentProfile()
        {
            Child chld = new Child();
            try
            {
                ChildModel myData = new ChildModel();
                chld = myData.GetCurrentProfile(ParentId);

                ChildID = chld.Id;
                Firstname = chld.FirstName;
                Lastname = chld.LastName;
                DoB = chld.DoB;
                Address = chld.Address;
                City = chld.City;
                Province = chld.Province;
                HealthCard = (int)chld.HealthCard;
                DocName = chld.DocName;
                DocPhone = chld.DocPhone;
                Comments = chld.Comments; 
            }
            catch(Exception e)
            {
                Message = "Child could not log in, problem was " + e.Message;
                ErrorRoutine(e, "ChildViewModel", "GetCurrentProfile");
                throw new Exception("Error occured");
            }
        }
        
        public List<ChildViewModel> GetChildList() { 
            List<ChildViewModel> l_cvm = new List<ChildViewModel>();
            for (int i = 0; i < 5; i++)
            {
                l_cvm.Add(new ChildViewModel(
                       1,
                       "ChildFirst" + i,
                       "ChildLast" + i,
                       DateTime.Now,
                       i + "82 delatre St",
                       "London",
                       "ON",
                       123456789,
                       "Dr. Baby",
                       "519-285-2502",
                       "Comment " + i,
                       1,
                       "Kid smells"));
            }
            return l_cvm;
        }
    }
}
