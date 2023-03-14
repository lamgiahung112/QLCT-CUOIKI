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
                "ID, [Name], [Address], Birth, EmployeeStatus, [Password], Gender,, Role) " +
                "VALUES (" +
                "N'{0}', " +
                "N'{1}', " +
                "N'{2}', " +
                "'{3}', " +
                "'{4}', " +
                "'{5}', " +
                "'{6}', " +
                "'{8}');",
                employee.ID,
                employee.Name,
                employee.Address,
                employee.Birth.ToString("yyyy-MM-dd"),
                EnumMapper.mapToString(employee.Status),
                employee.Password,
                EnumMapper.mapToString(employee.Gender),
                EnumMapper.mapToString(employee.Role));
        }
        public static string GetDeleteCommandForEmployee(string id)
        {
            return string.Format("DELETE FROM Employees WHERE ID = N'{0}'", id);
        }
        public static string GetUpdateCommandForEmployee(Employee employee)
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
                "WHERE ID = N'{8}';",
                employee.Name,
                employee.Address,
                employee.Birth.ToString("yyyy-MM-dd"),
                nameof(employee.Status),
                employee.Password,
                nameof(employee.Gender),
                nameof(employee.Role),
                employee.ID);
        }
        #endregion

        #region Work Session
        public static string GetAddCommandForWorkSession(WorkSession workSession)
        {
            string endingTimeParam = "NULL";
            if (workSession.EndingTime is not null)
            {
                endingTimeParam = workSession.EndingTime?.ToString("s");
            }
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
                workSession.StartingTime.ToString("s"),
                endingTimeParam);
        }
        public static string GetDeleteCommandForWorkSession(string id)
        {
            return string.Format("DELETE FROM WorkSessions WHERE ID = N'{0}'", id);
        }
        public static string GetUpdateCommandForWorkSession(WorkSession workSession)
        {
            return string.Format("UPDATE WorkSessions SET EndingTime = '{0}' WHERE ID = N'{1}'", workSession.EndingTime?.ToString("s"), workSession.Id);
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
        public static string GetCommandToGetAllWorkSession()
        {
            return string.Format("SELECT * FROM WorkSessions");
        }
        #endregion

        #region Task
        public static string GetAddCommandForTask(Task task)
        {
            return string.Format(
                "INSERT INTO Tasks "+
                "(ID, Assignee, Assigner, [Description], Title, StartingTime, EndingTime, [Status], CreatedAt, UpdatedAt) " +
                "VALUES ('{0}', '{1}', '{2}', '{3}', '{4}', '{5}', '{6}', '{7}','{8}','{9}')",
                task.ID,
                task.Assignee,
                task.Assigner,
                task.Description,
                task.Title,
                task.StartingTime,
                task.EndingTime,
                task.Status,
                task.CreatedAt,
                task.UpdatedAt);
        }
        public static string GetDeleteCommandForTask(string id)
        {
            return string.Format("DELETE FROM Tasks WHERE ID = '{0}'", id);
        }

        public static string GetTasksOfATeamMemberCommand(TeamMember member)
        {
            return string.Format(
                "SELECT " +
                "Tasks.ID, Tasks.Assignee, Task.Assigner, Task.[Description], " +
                "Task.Title, Task.StartingTime, Task.EndingTime, Task.[Status], " +
                "Task.CreatedAt, Task.UpdatedAt " +
                "FROM Tasks INNER JOIN TeamMembers on Tasks.Assignee = '{0}'"
                , member.ID);
        }

        public static string GetModifyCommandForTask(Task task)
        {
            return string.Format(
                "UPDATE Tasks " +
                "SET " +
                "[Description] = '{0}', " +
                "Title = '{1}', " +
                "StartingTime = '{2}', " +
                "EndingTime = '{3}', " +
                "[Status] = '{4}', " +
                "UpdatedAt = '{5}' " +
                "WHERE ID = '{6}'",
                task.Description,
                task.Title,
                task.StartingTime,
                task.EndingTime,
                task.Status,
                task.UpdatedAt,
                task.ID
                );
        }

        public static string GetOneByIdCommandForTask(string id)
        {
            return string.Format("SELECT * FROM Tasks WHERE ID = '{0}'", id);
        }
        #endregion
    }
}
