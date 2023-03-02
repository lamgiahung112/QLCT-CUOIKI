using CuoiKi.Models;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;

namespace CuoiKi.DAOs
{
    public class EmployeeDAO : IDAO<Employee>
    {
        private readonly DBConnection dbc;
        public EmployeeDAO()
        {
            dbc = new DBConnection();
        }
        public void Add(Employee entry)
        {
            string command = SqlConverter.GetAddCommandForEmployee(entry);
            dbc.Execute(command);
        }
        public void Delete(string id)
        {
            string command = SqlConverter.GetDeleteCommandForEmployee(id);
            dbc.Execute(command);
        }
        public void Modify(string id, Employee entry)
        {
            string command = SqlConverter.GetUpdateCommandForEmployee(id, entry);
            dbc.Execute(command);
        }
        public List<Employee> GetAll()
        {
            string command = "";
            SqlDataReader? reader = dbc.ExecuteReader(command);
            if (reader == null)
            {
                return new List<Employee>();
            }
            return reader.Cast<Employee>().ToList();
        }
        public Employee? GetOne(string id)
        {
            string command = "";
            return (Employee?)dbc.ExecuteScalar(command);
        }
    }
}

