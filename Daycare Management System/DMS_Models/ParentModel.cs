using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DMS_Model
{
    public class ParentModel : DMS_ModelConfig
    {
        /// <summary>
        /// Registers a parent to the database
        /// </summary>
        /// <param name="bytParent"></param>
        /// <returns>Parent number once completed</returns>
        public int Register(byte[] bytParent)
        {
            int ParentId = -1;
            Parent parent = new Parent();
            NeighbourhoodEntities dbContext = new NeighbourhoodEntities();

            try
            {
                Dictionary<string, Object> dictionaryParent = (Dictionary<string, Object>)Deserializer(bytParent);
                dbContext = new NeighbourhoodEntities();
                parent.userName = Convert.ToString(dictionaryParent["username"]);
                parent.Password = Convert.ToString(dictionaryParent["password"]);
                parent.PFirstName = Convert.ToString(dictionaryParent["firstname"]);
                parent.PLastName = Convert.ToString(dictionaryParent["lastname"]);
                parent.Email = Convert.ToString(dictionaryParent["email"]);
                parent.PAddress = Convert.ToString(dictionaryParent["address1"]);
                parent.PCity = Convert.ToString(dictionaryParent["city"]);
                //parent.PPostal = Convert.ToString(dictionaryParent["postal"]);
                parent.PProvince = Convert.ToString(dictionaryParent["province"]);
                parent.PPhone = Convert.ToString(dictionaryParent["phone"]);
                dbContext.Parents.Add(parent);
                dbContext.SaveChanges();
                ParentId = parent.ParentId;
            }
            catch (Exception ex)
            {
                ErrorRoutine(ex, "ParentModel", "Register");
            }
            return ParentId;
        }

        /// <summary>
        /// Gets the current profile from the database
        /// </summary>
        /// <param name="ParentId"></param>
        /// <returns></returns>
        public Parent GetCurrentProfile(string username)
        {
            Parent parent = new Parent();
            try
            {
                NeighbourhoodEntities dbContext = new NeighbourhoodEntities();
                parent = dbContext.Parents.FirstOrDefault(c => c.userName == username);
            }
            catch(Exception e)
            {
                throw new Exception(e.Message);
            }
            return parent;
        }

        /// <summary>
        /// Deletes a parent when something goes wrong
        /// </summary>
        /// <param name="ParentId">Parent Id of parent to be deleted</param>
        public void Delete(int ParentId)
        {
            Parent parent = new Parent();
            try
            {
                NeighbourhoodEntities dbContext = new NeighbourhoodEntities();
                parent = dbContext.Parents.FirstOrDefault(c => c.ParentId == ParentId);
                dbContext.Parents.Remove(parent);
                dbContext.SaveChanges();
            }
            catch(Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}
