using CuoiKi.Models;
using System.Collections.Generic;

namespace CuoiKi.DAOs
{
    public class WorkSessionDAO : IDAO<WorkSession>, IWorkTaskRetriever
    {
        private readonly DBConnection dbc;
        public WorkSessionDAO()
        {
            dbc = new DBConnection();
        }
        public void Add(WorkSession entry)
        {
            string command = SqlConverter.GetAddCommandForWorkSession(entry);
            dbc.Execute(command);
        }

        public void Delete(string id)
        {
            string command = SqlConverter.GetDeleteCommandForWorkSession(id);
            dbc.Execute(command);
        }
        public void Modify(WorkSession entry)
        {
            string command = SqlConverter.GetUpdateCommandForWorkSession(entry);
            dbc.Execute(command);
        }
        public List<WorkSession>? GetAll()
        {
            string command = SqlConverter.GetCommandToGetAllWorkSession();
            List<WorkSession>? list = dbc.ExecuteWithResults<WorkSession>(command);
            if (list == null || list.Count == 0) { return null; }
            return list;
        }

        public WorkSession? GetOne(string id)
        {
            string command = SqlConverter.GetCommandToGetOneWorkSession(id);
            return dbc.ExecuteQuery<WorkSession>(command);
        }

        public WorkSession? GetLastest(string employeeID)
        {
            string command = SqlConverter.GetCommandToGetLastestEmployeeWorkSession(employeeID);
            return dbc.ExecuteQuery<WorkSession>(command);
        }

        public WorkSession? GetUnfinished(string employeeID)
        {
            string command = SqlConverter.GetCommandToGetUnfinishedsEmployeeWorkSession(employeeID);
            return dbc.ExecuteQuery<WorkSession>(command);
        }
    }
}
