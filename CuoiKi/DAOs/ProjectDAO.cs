using CuoiKi.Models;
using System;
using System.Collections.Generic;

namespace CuoiKi.DAOs
{
    public class ProjectDAO : IDAO<Project>
    {
        private readonly DBConnection dbc;
        public ProjectDAO()
        {
            dbc = new DBConnection();
        }
        public void Add(Project entry)
        {
            string cmd = SqlConverter.GetAddCommandForProject(entry);
            dbc.Execute(cmd);
        }

        public void Delete(string id)
        {
            string cmd = SqlConverter.GetDeleteCommandForProject(id);
            dbc.Execute(cmd);
        }

        public List<Project>? GetAll()
        {
            throw new NotImplementedException();
        }

        public List<Project>? GetAllProjectsOfAManager(string managerID)
        {
            string cmd = SqlConverter.GetAllProjectsOfAManagerCommand(managerID);
            return dbc.ExecuteWithResults<Project>(cmd);
        }

        public List<Project>? GetAllProjectsOfTechLead(string techleadID)
        {
            return null;
        }

        public List<Project>? GetAllProjectsOfEmployee(string employeeID)
        {
            string cmd = SqlConverter.GetAllProjectsOfEmployeeCommand(employeeID);
            return dbc.ExecuteWithResults<Project>(cmd);
        }

        public Project? GetOne(string id)
        {
            string cmd = SqlConverter.GetOneByIdCommandForProject(id);
            return dbc.ExecuteQuery<Project>(cmd);
        }

        public void Modify(Project entry)
        {
            string cmd = SqlConverter.GetUpdateCommandForProject(entry);
            dbc.Execute(cmd);
        }
    }
}
