using CuoiKi.DAOs;
using CuoiKi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace CuoiKi.Controllers
{
    public class AuthenticationController
    {
        private readonly EmployeeDAO employeeDAO;

        public AuthenticationController()
        {
            employeeDAO = new EmployeeDAO();
        }
        /// <summary>
        /// Login a user
        /// </summary>
        /// <returns>Whether the operation was successful</returns>
        public Employee? Login(string id, string password)
        {
            Employee? foundEmployee = employeeDAO.GetOne(id);
            return foundEmployee != null && BCrypt.Net.BCrypt.Verify(password, foundEmployee.Password) ? foundEmployee : null;
        }
    }
}
