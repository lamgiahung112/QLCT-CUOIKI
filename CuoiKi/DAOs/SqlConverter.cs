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

        public static string GetAllEmployeeOfARoleCommand(Role role)
        {
            return string.Format("SELECT * FROM Employees WHERE Role='{0}'", EnumMapper.mapToString(role));
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
                workSession.ID,
                workSession.EmployeeID,
                workSession.StartingTime.ToString("s"),
                endingTimeParam);
        }
        public static string GetDeleteCommandForWorkSession(string id)
        {
            return string.Format("DELETE FROM WorkSessions WHERE ID = N'{0}'", id);
        }
        public static string GetUpdateCommandForWorkSession(WorkSession workSession)
        {
            return string.Format("UPDATE WorkSessions SET EndingTime = '{0}' WHERE ID = N'{1}'", workSession.EndingTime?.ToString("s"), workSession.ID);
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
                task.StartingTime.ToShortDateString(),
                task.EndingTime.ToShortDateString(),
                EnumMapper.mapToString(task.Status),
                task.CreatedAt.ToShortDateString(),
                task.UpdatedAt.ToShortDateString());
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
                task.StartingTime.ToShortDateString(),
                task.EndingTime.ToShortDateString(),
                EnumMapper.mapToString(task.Status),
                task.UpdatedAt.ToShortDateString(),
                task.ID
                );
        }

        public static string GetOneByIdCommandForTask(string id)
        {
            return string.Format("SELECT * FROM Tasks WHERE ID = '{0}'", id);
        }
        #endregion

        #region Project
        public static string GetAddCommandForProject(Project project)
        {
            return string.Format(
                "INSERT INTO Projects " +
                "(ID, [Name], [Description], ManagerID, CreatedAt) " +
                "VALUES ('{0}', '{1}', '{2}', '{3}', '{4}')",
                project.ID,
                project.Name,
                project.Description,
                project.ManagerID,
                project.CreatedAt.ToShortDateString()
            );
        }

        public static string GetDeleteCommandForProject(string id)
        {
            return string.Format("DELETE FROM Projects WHERE ID = '{0}'", id);
        }

        public static string GetOneByIdCommandForProject(string id)
        {
            return string.Format("SELECT * FROM Projects WHERE ID = '{0}'", id);
        }

        public static string GetUpdateCommandForProject(Project project)
        {
            return string.Format(
                "UPDATE Projects SET " +
                "[Name] = '{0}', " +
                "[Description] = '{1}', " +
                "ManagerID = '{2}' " +
                "WHERE ID = '{3}'",
                project.Name,
                project.Description,
                project.ManagerID,
                project.ID
            );
        }

        public static string GetAllProjectsOfAManagerCommand(string managerID)
        {
            return string.Format(
                "SELECT * FROM Projects WHERE ManagerID = '{0}'",
                managerID
            );
        }

        public static string GetAllProjectsOfEmployeeCommand(string employeeID)
        {
            return string.Format(
                "SELECT Projects.ID, Projects.[Name], Projects.[Description], Project.ManagerID " +
                "FROM TeamMembers " +
                "INNER JOIN Teams " +
                "ON TeamMembers.TeamID = Teams.ID and TeamMembers.EmployeeID = '{0}' " +
                "INNER JOIN Stages " +
                "ON Teams.StageID = Stages.ID " +
                "INNER JOIN Projects " +
                "ON Stages.ProjectID = Projects.ID",
                employeeID
            );
        }

        public static string GetAllProjectsOfTechleadCommand(string techleadID)
        {
            return string.Format(
                "SELECT Projects.ID, Projects.[Name], Projects.[Description], Project.ManagerID " +
                "FROM TeamMembers " +
                "INNER JOIN Teams " +
                "ON TeamMembers.TeamID = Teams.ID and Teams.TechLeadID = '{0}' " +
                "INNER JOIN Stages " +
                "ON Teams.StageID = Stages.ID " +
                "INNER JOIN Projects " +
                "ON Stages.ProjectID = Projects.ID",
                techleadID
            );
        }
        #endregion

        #region Team
        public static string GetAddCommandForTeam(Team team)
        {
            return string.Format(
                "INSERT INTO Teams " +
                "(ID, StageID, TechLeadID, [Name]) " +
                "VALUES ('{0}', '{1}', '{2}', '{3}')",
                team.ID, team.StageID, team.TechLeadID, team.Name
            );
        }

        public static string GetDeleteCommandForTeam(string id)
        {
            return string.Format("DELETE From Teams WHERE ID = '{0}'", id);
        }

        public static string GetOneByIdCommandForTeam(string id)
        {
            return string.Format("SELECT * FROM Teams WHERE ID = '{0}'", id);
        }

        public static string GetUpdateCommandForTeam(Team team)
        {
            return string.Format(
                "UPDATE Teams SET " +
                "TechLeadID = '{0}', " +
                "[Name] = '{1}' " +
                "WHERE ID = '{2}'",
                team.TechLeadID, team.Name, team.ID
            );
        }

        public static string GetTeamsOfAStageCommand(Stage stage)
        {
            return string.Format("SELECT * FROM Teams WHERE StageID = '{0}'", stage.ID);
        }
        #endregion

        #region Stage
        public static string GetAddCommandForStage(Stage stage)
        {
            return string.Format(
                "INSERT INTO Stages " +
                "(ID, ProjectID, [Description]) " +
                "VALUES ('{0}', '{1}', '{2}') ",
                stage.ID, stage.ProjectID, stage.Description
            );
        }

        public static string GetDeleteCommandForStage(string id)
        {
            return string.Format(
                "DELETE FROM Stages WHERE ID = '{0}'", id    
            );
        }

        public static string GetOneByIdCommandForStage(string id)
        {
            return string.Format("SELECT * FROM Stages WHERE ID = '{0}'", id);
        }

        public static string GetUpdateCommandForStage(Stage stage)
        {
            return string.Format(
                "UPDATE Stages SET " +
                "([Description]) " +
                "VALUES ('{0}') " +
                "WHERE ID = '{1}'",
                stage.Description, stage.ID
            );
        }
        
        public static string GetStagesOfAProjectCommand(Project project)
        {
            return string.Format("SELECT * FROM Stages WHERE ProjectID = '{0}'", project.ID);
        }
        #endregion

        #region TeamMember
        public static string GetAddCommandForTeamMember(TeamMember tm)
        {
            return string.Format(
                "INSERT INTO TeamMembers " +
                "(ID, TeamID, EmployeeID) " +
                "VALUES ('{0}', '{1}', '{2}')",
                tm.ID, tm.TeamID, tm.EmployeeID
            );
        }

        public static string GetDeleteCommandForTeamMember(string id)
        {
            return string.Format("DELETE FROM TeamMembers WHERE ID = '{0}'", id);
        }

        public static string GetOneByIdCommandForTeamMember(string id)
        {
            return string.Format("SELECT * FROM TeamMembers WHERE ID = '{0}'", id);
        }

        public static string GetAllMembersOfTeamCommand(Team team)
        {
            return string.Format("SELECT * FROM TeamMembers WHERE TeamID = '{0}'", team.ID);
        }
        #endregion

    }
}
