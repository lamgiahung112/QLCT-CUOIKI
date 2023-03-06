using CuoiKi.Models;
using System.Collections.Generic;

namespace CuoiKi.DAOs
{
    public class EmployeeDAO : IDAO<Employee>
    {
        private readonly DBConnection<Employee> dbc;
        public EmployeeDAO()
        {
            dbc = new DBConnection<Employee>();
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
        public List<Employee>? GetAll(string id)
        {
            string command = "";
            return null;
        }
        public Employee? GetOne(string id)
        {
            string command = string.Format("SELECT * FROM Employees WHERE ID = N'{0}'", id);
            List<Employee>? list = dbc.ExecuteWithResults(command);
            if (list == null || list.Count == 0) { return null; }
            return list[0];
        }
    }
}

