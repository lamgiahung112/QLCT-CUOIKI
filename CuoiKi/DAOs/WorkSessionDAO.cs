using CuoiKi.Models;
using System.Collections.Generic;

namespace CuoiKi.DAOs
{
    public class WorkSessionDAO : IDAO<WorkSession>, IWorkTaskRetriever
    {
        private readonly DBConnection<WorkSession> dbc;
        public WorkSessionDAO()
        {
            dbc = new DBConnection<WorkSession>();
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
        public void Modify(string id, WorkSession entry)
        {
            string command = SqlConverter.GetUpdateCommandForWorkSession(id, entry);
            dbc.Execute(command);
        }
        public List<WorkSession>? GetAll()
        {
            string command = SqlConverter.GetCommandToGetAllWorkSession();
            List<WorkSession>? list = dbc.ExecuteWithResults(command);
            if (list == null || list.Count == 0) { return null; }
            return list;
        }

        public WorkSession? GetOne(string id)
        {
            string command = SqlConverter.GetCommandToGetOneWorkSession(id);
            return dbc.ExecuteQuery(command);
        }

        public WorkSession? GetLastest(string employeeID)
        {
            string command = SqlConverter.GetCommandToGetLastestEmployeeWorkSession(employeeID);
            return dbc.ExecuteQuery(command);
        }

        public WorkSession? GetUnfinished(string employeeID)
        {
            string command = SqlConverter.GetCommandToGetUnfinishedsEmployeeWorkSession(employeeID);
            return dbc.ExecuteQuery(command);
        }
    }
}
