using CuoiKi.Constants;
using CuoiKi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        public List<WorkLeave>? GetAllOfAnEmployeeInMonth(string employeeID)
        {
            string cmd = SqlConverter.GetAllLeavesOfAnEmployeeCommand(employeeID);
            List<WorkLeave>? allLeaves = dbc.ExecuteWithResults<WorkLeave>(cmd);
            return allLeaves?.FindAll(x => x.FromDate.Month == DateTime.Now.Month && x.FromDate.Year == DateTime.Now.Year);
        }

        public void Modify(WorkLeave entry)
        {
            string command = SqlConverter.GetUpdateCommandForWorkLeave(entry);
            dbc.Execute(command);
        }
    }
}
