using CuoiKi.Models;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;

namespace CuoiKi.DAOs
{
    public class WorkSessionDAO : IDAO<WorkSession>
    {
        DBConnection dbc;
        public WorkSessionDAO()
        {
            dbc = new DBConnection();
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
        public List<WorkSession> GetAll()
        {
            string command = "";
            SqlDataReader? reader = dbc.ExecuteReader(command);
            if (reader == null)
            {
                return new List<WorkSession>();
            }
            return reader.Cast<WorkSession>().ToList();
        }

        public WorkSession? GetOne(string id)
        {
            string command = "";
            return (WorkSession?)dbc.ExecuteScalar(command);
        }

    }
}
