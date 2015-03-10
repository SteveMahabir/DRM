using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DMS_Model
{
    public class EmployeeModel : DMS_ModelConfig
    {
        /// <summary>
        /// Registers a employee to the database
        /// </summary>
        /// <param name="bytEmployee"></param>
        /// <returns>Employee number once completed</returns>
        public int Register(byte[] bytEmployee)
        {
            int empId = -1;
            Employee emp = new Employee();
            NeighbourhoodDataEntities dbContext = new NeighbourhoodDataEntities();

            try
            {
                Dictionary<string, Object> dictionaryEmployee = (Dictionary<string, Object>)Deserializer(bytEmployee);
                dbContext = new NeighbourhoodDataEntities();
                emp.userName = Convert.ToString(dictionaryEmployee["username"]);
                emp.Password = Convert.ToString(dictionaryEmployee["password"]);
                emp.EFirstName = Convert.ToString(dictionaryEmployee["firstname"]);
                emp.ELastName = Convert.ToString(dictionaryEmployee["lastname"]);
                emp.Email = Convert.ToString(dictionaryEmployee["email"]);
                emp.EAddress = Convert.ToString(dictionaryEmployee["address"]);
                emp.ECity = Convert.ToString(dictionaryEmployee["city"]);
               // emp.EPostal = Convert.ToString(dictionaryEmployee["postal"]);
                emp.EProvince = Convert.ToString(dictionaryEmployee["province"]);
                emp.EPhone = Convert.ToString(dictionaryEmployee["phone"]);
                emp.ERole = Convert.ToString(dictionaryEmployee["role"]);
                dbContext.Employees.Add(emp);
                dbContext.SaveChanges();
                empId = emp.EmployeeId;
            }
            catch (Exception ex)
            {
                ErrorRoutine(ex, "EmployeeModel", "Register");
            }
            return empId;
        }

        /// <summary>
        /// Gets the current profile from the database
        /// </summary>
        /// <param name="username"></param>
        /// <returns></returns>
        public Employee GetCurrentProfile(string username)
        {
            Employee emp = new Employee();
            try
            {
                NeighbourhoodDataEntities dbContext = new NeighbourhoodDataEntities();
                emp = dbContext.Employees.FirstOrDefault(c => c.userName == username);
            }
            catch(Exception e)
            {
                throw new Exception(e.Message);
            }
            return emp;
        }

        /// <summary>
        /// Deletes a employee when something goes wrong
        /// </summary>
        /// <param name="customerId">Employee Id of employee to be deleted</param>
        public void Delete(int employeeId)
        {
            Employee emp = new Employee();
            try
            {
                NeighbourhoodDataEntities dbContext = new NeighbourhoodDataEntities();
                emp = dbContext.Employees.FirstOrDefault(c => c.EmployeeId == employeeId);
                dbContext.Employees.Remove(emp);
                dbContext.SaveChanges();
            }
            catch(Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public List<Employee> GetAll()
        {
            NeighbourhoodDataEntities dbContext;
            List<Employee> empies = null;
            try
            {
                dbContext = new NeighbourhoodDataEntities();
                empies = dbContext.Employees.ToList();
            }
            catch (Exception ex)
            {
                ErrorRoutine(ex, "ProductModel", "GetAll");
            }
            return empies;
        }
    }
}
