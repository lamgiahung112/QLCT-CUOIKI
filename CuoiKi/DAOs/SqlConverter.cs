using CuoiKi.Models;

namespace CuoiKi.DAOs
{
    public class SqlConverter
    {
        public SqlConverter() { }
        public string GetAddCommandForEmployee(Employee employee)
        {
            return string.Format(
                "INSERT INTO Employees (ID, [Name], [Address], Birth, EmployeeStatus, [Password], Gender) " +
                "VALUES (" +
                "'{0}', " +
                "N'{1}', " +
                "N'{2}', " +
                "'1999-2-3', " +
                "'Active', " +
                "'victorydak', " +
                "'Male');",
                employee.Id,
                employee.Name,
                employee.Address,
                employee.Birth.ToString("s"),
                employee.);
        }
    }
}
