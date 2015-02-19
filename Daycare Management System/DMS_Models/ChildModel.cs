using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DMS_Model
{
    public class ChildModel : DMS_ModelConfig
    {
        /// <summary>
        /// Registers a child to the database
        /// </summary>
        /// <param name="bytChild"></param>
        /// <returns>Child number once completed</returns>
        public int Register(byte[] bytChild)
        {
            int Id = -1;
            Child child = new Child();
            NeighbourhoodEntities dbContext = new NeighbourhoodEntities();

            try
            {
                Dictionary<string, Object> dictionaryChild = (Dictionary<string, Object>)Deserializer(bytChild);
                dbContext = new NeighbourhoodEntities();
                child.FirstName = Convert.ToString(dictionaryChild["firstname"]);
                child.LastName = Convert.ToString(dictionaryChild["lastname"]);
                //child.DoB = Convert.ToDateTime(dictionaryChild["dob"]);
                child.DoB = Convert.ToDateTime(DateTime.Today);
                child.Address = Convert.ToString(dictionaryChild["address"]);
                child.City = Convert.ToString(dictionaryChild["city"]);
                child.Province = Convert.ToString(dictionaryChild["province"]);
                child.HealthCard = Convert.ToInt32(dictionaryChild["healthcard"]);
                child.DocName = Convert.ToString(dictionaryChild["docname"]);
                child.DocPhone = Convert.ToString(dictionaryChild["docphone"]);
                //child.Comments = Convert.ToString(dictionaryChild["comments"]);
                child.ParentId = Convert.ToInt32(dictionaryChild["parentid"]);
                dbContext.Children.Add(child);
                dbContext.SaveChanges();
                Id = child.Id;
            }
            catch (Exception ex)
            {
                ErrorRoutine(ex, "ChildModel", "Register");
            }
            return Id;
        }

        /// <summary>
        /// Gets the current profile from the database
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public Child GetCurrentProfile(int Id)
        {
            Child child = new Child();
            try
            {
                NeighbourhoodEntities dbContext = new NeighbourhoodEntities();
                child = dbContext.Children.FirstOrDefault(c => c.Id == Id);
            }
            catch(Exception e)
            {
                throw new Exception(e.Message);
            }
            return child;
        }

        /// <summary>
        /// Deletes a child when something goes wrong
        /// </summary>
        /// <param name="childId">Child Id of child to be deleted</param>
        public void Delete(int childId)
        {
            Child child = new Child();
            try
            {
                NeighbourhoodEntities dbContext = new NeighbourhoodEntities();
                child = dbContext.Children.FirstOrDefault(c => c.Id == childId);
                dbContext.Children.Remove(child);
                dbContext.SaveChanges();
            }
            catch(Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public List<Child> GetAll()
        {            
            NeighbourhoodEntities dbContext;
            List<Child> chillins = null;
            try
            {
                dbContext = new NeighbourhoodEntities();
                chillins = dbContext.Children.ToList();
            }
            catch (Exception ex)
            {
                ErrorRoutine(ex, "ProductModel", "GetAll");                
            }
            return chillins;
        }

        public List<Child> GetAll(int PID)
        {
            NeighbourhoodEntities dbContext;
            List<Child> chillins = null;
            try
            {
                dbContext = new NeighbourhoodEntities();
                chillins = dbContext.Children.Where( c => c.ParentId == PID ).ToList();
            }
            catch (Exception ex)
            {
                ErrorRoutine(ex, "ProductModel", "GetAll");
            }
            return chillins;
        }

        }
    }

