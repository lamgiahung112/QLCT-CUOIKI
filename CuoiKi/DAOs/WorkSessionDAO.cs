using CuoiKi.Models;
using System.Collections.Generic;

namespace CuoiKi.DAOs
{
    public class WorkSessionDAO : IDAO<WorkSession>
    {
        private readonly DBConnection<WorkSession> dbc;
        public WorkSessionDAO()
        {
            dbc = new DBConnection<WorkSession>();
        }
        // Not implement yet
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
        public List<WorkSession>? GetAll(string id)
        {
            string command = string.Format("SELECT * FROM WorkSessions WHERE EmployeeID = N'{0}'", id);
            List<WorkSession>? list = dbc.ExecuteWithResults(command);
            if (list == null || list.Count == 0) { return null; }
            return list;
        }

        public WorkSession? GetOne(string id)
        {
            string command = string.Format("SELECT * FROM WorkSessions WHERE EmployeeID = N'{0}'", id);
            List<WorkSession>? list = dbc.ExecuteWithResults(command);
            if (list == null || list.Count == 0) { return null; }
            return list[0];
        }
    }
}
