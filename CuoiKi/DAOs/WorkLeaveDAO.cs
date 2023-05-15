using CuoiKi.Models;
using System.Collections.Generic;

namespace CuoiKi.DAOs
{
    public class WorkLeaveDAO : IDAO<WorkLeave>
    {
        private readonly DBConnection dbc;
        public WorkLeaveDAO()
        {
            dbc = new DBConnection();
        }
        public void Add(WorkLeave entry)
        {
            string cmd = SqlConverter.GetAddCommandForWorkLeave(entry);
            dbc.Execute(cmd);
        }

        public void Delete(string id)
        {
            string command = SqlConverter.GetDeleteCommandForWorkLeave(id);
            dbc.Execute(command);
        }

        public List<WorkLeave>? GetAll()
        {
            return null;
        }

        public WorkLeave? GetOne(string id)
        {
            return null;
        }

        public List<WorkLeave>? GetAllOfAnEmployee(string employeeID)
        {
            string cmd = SqlConverter.GetAllLeavesOfAnEmployeeCommand(employeeID);
            return dbc.ExecuteWithResults<WorkLeave>(cmd);
        }

        public void Modify(WorkLeave entry)
        {
            string command = SqlConverter.GetUpdateCommandForWorkLeave(entry);
            dbc.Execute(command);
        }
    }
}
