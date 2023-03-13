using CuoiKi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CuoiKi.DAOs
{
    public class DepartmentDAO : IDAO<Department>
    {
        private readonly DBConnection<Department> dbc;
        public DepartmentDAO() 
        {
            dbc = new DBConnection<Department>();    
        }
        public void Add(Department entry)
        {
            string cmd = SqlConverter.GetAddDepartmentCommand(entry);
            dbc.Execute(cmd);
        }

        public void Delete(string id)
        {
            string cmd = SqlConverter.GetDeleteDepartmentCommand(id);
            dbc.Execute(cmd);
        }

        public List<Department>? GetAll()
        {
            string cmd = SqlConverter.GetSelectAllDepartmentCommand();
            return dbc.ExecuteWithResults(cmd);
        }

        public Department? GetOne(string id)
        {
            string cmd = SqlConverter.GetOneDepartmentByIdCommand(id);
            return dbc.ExecuteQuery(cmd);
        }

        public void Modify(string id, Department entry)
        {
            throw new NotImplementedException();
        }
    }
}
