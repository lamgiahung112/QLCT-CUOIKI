using CuoiKi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CuoiKi.DAOs
{
    public class SalaryDAO : IDAO<Salary>
    {
        private readonly DBConnection dbc;

        public SalaryDAO()
        {
            dbc = new DBConnection();
        }

        public void Add(Salary entry)
        {
            throw new NotImplementedException();
        }

        public void Delete(string id)
        {
            throw new NotImplementedException();
        }

        public List<Salary>? GetAll()
        {
            String cmd = SqlConverter.GetAllSalaryCommand();
            return dbc.ExecuteWithResults<Salary>(cmd);
        }

        public Salary? GetOne(string id)
        {
            throw new NotImplementedException();
        }

        public void Modify(Salary entry)
        {
            throw new NotImplementedException();
        }
    }
}
