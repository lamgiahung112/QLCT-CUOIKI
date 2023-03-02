using CuoiKi.Models;

namespace CuoiKi.DAOs
{
    public class SqlConverter
    {
        public SqlConverter() { }
        public string GetAddCommandForEmployee(Employee employee)
        {
            return string.Format(
                "INSERT INTO Employees (" +
                "ID, [Name], [Address], Birth, EmployeeStatus, [Password], Gender) " +
                "VALUES (" +
                "N'{0}', " +
                "N'{1}', " +
                "N'{2}', " +
                "'{3}', " +
                "'{4}', " +
                "'{5}', " +
                "'{6}');",
                employee.Id,
                employee.Name,
                employee.Address,
                employee.Birth.ToString("yyyy-MM-dd"),
                employee.Status.ToString(),
                employee.Password,
                employee.Gender.ToString());
        }
        public string GetDeleteCommandForEmployee(Employee employee)
        {
            return "";
        }
    }
}
