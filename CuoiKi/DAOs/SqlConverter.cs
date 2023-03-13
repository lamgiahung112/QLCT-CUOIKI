using CuoiKi.Constants;
using CuoiKi.Models;

namespace CuoiKi.DAOs
{
    public class SqlConverter
    {
        public SqlConverter() { }
        #region Employee
        public static string GetAddCommandForEmployee(Employee employee)
        {
            return string.Format(
                "INSERT INTO Employees (" +
                "ID, [Name], [Address], Birth, EmployeeStatus, [Password], Gender, [DepartmentId], Role) " +
                "VALUES (" +
                "N'{0}', " +
                "N'{1}', " +
                "N'{2}', " +
                "'{3}', " +
                "'{4}', " +
                "'{5}', " +
                "'{6}', " +
                "'{7}', " +
                "'{8}');",
                employee.Id,
                employee.Name,
                employee.Address,
                employee.Birth.ToString("yyyy-MM-dd"),
                EnumMapper.mapToString(employee.Status),
                employee.Password,
                EnumMapper.mapToString(employee.Gender),
                employee.DepartmentId,
                EnumMapper.mapToString(employee.Role));
        }
        public static string GetDeleteCommandForEmployee(string id)
        {
            return string.Format("DELETE FROM Employees WHERE ID = N'{0}'", id);
        }
        public static string GetUpdateCommandForEmployee(string id, Employee employee)
        {
            return string.Format(
                "UPDATE Employees " +
                "SET " +
                "[Name] = N'{0}', " +
                "[Address] = N'{1}', " +
                "Birth = '{2}', " +
                "EmployeeStatus = '{3}', " +
                "[Password] = '{4}', " +
                "Gender = '{5}', " +
                "[Role] = '{6}', " +
                "[DepartmentId] = '{7}' " +
                "WHERE ID = N'{8}';",
                employee.Name,
                employee.Address,
                employee.Birth.ToString("yyyy-MM-dd"),
                nameof(employee.Status),
                employee.Password,
                nameof(employee.Gender),
                nameof(employee.Role),
                employee.DepartmentId,
                id);
        }
        #endregion

        #region Work Session
        public static string GetAddCommandForWorkSession(WorkSession workSession)
        {
            return string.Format(
                "INSERT INTO WorkSessions(" +
                "ID, EmployeeID, StartingTime, EndingTime) " +
                "VALUES (" +
                "N'{0}', " +
                "N'{1}', " +
                "'{2}', " +
                "{3});",
                workSession.Id,
                workSession.EmployeeId,
                workSession.StartingTime.ToString(),
                workSession.EndingTime.HasValue ? "'" + workSession.EndingTime.ToString() + "'" : "NULL");
        }
        public static string GetDeleteCommandForWorkSession(string id)
        {
            return string.Format("DELETE FROM WorkSessions WHERE ID = N'{0}'", id);
        }
        public static string GetUpdateCommandForWorkSession(string id, WorkSession workSession)
        {
            return string.Format("UPDATE WorkSessions SET EndingTime = '{0}' WHERE ID = N'{1}'", workSession.EndingTime.ToString(), id);
        }
        public static string GetCommandToGetLastestEmployeeWorkSession(string employeeId)
        {
            return string.Format(
                "WITH EmployeeWorkSessions AS (" +
                "SELECT * FROM WorkSessions WHERE WorkSessions.EmployeeID = N'{0}'" +
                ") " +
                "SELECT TOP 1 * FROM EmployeeWorkSessions " +
                "ORDER BY EmployeeWorkSessions.StartingTime DESC",
                employeeId);
        }

        public static string GetCommandToGetUnfinishedsEmployeeWorkSession(string employeeID)
        {
            return string.Format(
                "WITH EmployeeWorkSessions AS (" +
                "SELECT * FROM WorkSessions WHERE WorkSessions.EmployeeID = N'{0}') " +
                "SELECT * FROM WorkSessions WHERE EndingTime is NULL ",
                employeeID);
        }

        public static string GetCommandToGetOneEmployee(string id)
        {
            return string.Format("SELECT * FROM Employees WHERE ID = N'{0}'", id);
        }

        public static string GetCommandToGetOneWorkSession(string id)
        {
            return string.Format("SELECT * FROM WorkSessions WHERE EmployeeID = N'{0}'", id);
        }

        public static string GetCommandToGetAllWorkSessionOfAnEmployee(string id)
        {
            return string.Format("SELECT * FROM WorkSessions WHERE EmployeeID = N'{0}'", id);
        }
        #endregion

        #region Department
        public static string GetAddDepartmentCommand(Department dep)
        {
            return string.Format("INSERT INTO Departments VALUES ('{0}', '{1}')", dep.ID, dep.Name);
        }
        public static string GetDeleteDepartmentCommand(string id)
        {
            return string.Format("DELETE FROM Departments WHERE ID = N'{0}'", id);
        }
        public static string GetSelectAllDepartmentCommand()
        {
            return "SELECT * FROM Departments";
        }
        public static string GetOneDepartmentByIdCommand(string id) {
            return string.Format("SELECT * FROM Departments where ID = '{0}'", id);
        }
        #endregion
    }
}
