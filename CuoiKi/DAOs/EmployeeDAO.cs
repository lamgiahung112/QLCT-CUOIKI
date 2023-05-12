using CuoiKi.Constants;
using CuoiKi.Models;
using System.Collections.Generic;

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
        public void Modify(Employee entry)
        {
            string command = SqlConverter.GetUpdateCommandForEmployee(entry);
            dbc.Execute(command);
        }
        public List<Employee>? GetAll()
        {
            string command = SqlConverter.GetAllEmployeesCommand();
            return dbc.ExecuteWithResults<Employee>(command);
        }
        public List<Employee>? GetAllManagers()
        {
            string cmd = SqlConverter.GetAllEmployeeOfARoleCommand(Role.Manager);
            return dbc.ExecuteWithResults<Employee>(cmd);
        }
        public List<Employee>? GetAllTechLeads()
        {
            string cmd = SqlConverter.GetAllEmployeeOfARoleCommand(Role.TechLead);
            return dbc.ExecuteWithResults<Employee>(cmd);
        }
        public List<Employee>? GetAllMemberOfARole(Role role)
        {
            string cmd = SqlConverter.GetAllEmployeeOfARoleCommand(role);
            return dbc.ExecuteWithResults<Employee>(cmd);
        }
        public Employee? GetOne(string id)
        {
            string command = SqlConverter.GetCommandToGetOneEmployee(id);
            return dbc.ExecuteQuery<Employee>(command);
        }
    }
}

