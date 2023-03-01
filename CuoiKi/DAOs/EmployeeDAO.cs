using CuoiKi.Models;

namespace CuoiKi.DAOs
{
    class EmployeeDAO : IDAO<Employee>
    {
        private DBConnection dbc;
        private SqlConverter sqlConverter;
        public EmployeeDAO()
        {
            dbc = new DBConnection();
            sqlConverter = new SqlConverter();
        }
        public void Add(Employee entry)
        {
            string command = "";
            dbc.Execute(command);
        }
        public void Delete(Employee entry)
        {
            string command = "";
            dbc.Execute(command);
        }

        public void Modify(Employee entry)
        {
            string command = "";
            dbc.Execute(command);
        }
    }
}

