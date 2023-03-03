using CuoiKi.Models;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;

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
        public List<WorkSession>? GetAll()
        {
            return null;
        }

        public WorkSession? GetOne(string id)
        {
            string command = "";
            return null;
        }

    }
}
