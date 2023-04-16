using CuoiKi.Constants;
using CuoiKi.DAOs;
using CuoiKi.Models;
using CuoiKi.States;
using System.Collections.Generic;

namespace CuoiKi.Controllers
{
    public class KpiController
    {
        private readonly ProjectDAO projectDAO;
        private readonly StageDAO stageDAO;
        private readonly TaskDAO taskDAO;
        private readonly TeamDAO teamDAO;
        private readonly TeamMemberDAO teamMemberDAO;
        private readonly EmployeeDAO employeeDAO;

        public KpiController()
        {
            projectDAO = new ProjectDAO();
            stageDAO = new StageDAO();
            taskDAO = new TaskDAO();
            teamDAO = new TeamDAO();
            employeeDAO = new EmployeeDAO();
            teamMemberDAO = new TeamMemberDAO();
        }

        /// <summary>
        /// If Record exists in DB, modify the record else add a new record
        /// </summary>
        /// <typeparam name="T">A class inheriting ModelBase</typeparam>
        /// <param name="entry">The entry to save</param>
        public void Save<T>(T entry) where T : ModelBase
        {
            dynamic? existingRecord;
            IDAO<T> dao;
            if (entry is Project)
            {
                existingRecord = projectDAO.GetOne(entry.ID);
                dao = (IDAO<T>)projectDAO;
            }
            else if (entry is Stage)
            {
                existingRecord = stageDAO.GetOne(entry.ID);
                dao = (IDAO<T>)stageDAO;
            }
            else if (entry is Task)
            {
                existingRecord = taskDAO.GetOne(entry.ID);
                dao = (IDAO<T>)taskDAO;
            }
            else
            {
                existingRecord = teamDAO.GetOne(entry.ID);
                dao = (IDAO<T>)teamDAO;
            }

            if (existingRecord != null)
            {
                dao.Modify(entry);
            }
            else dao.Add(entry);
        }

        /// <summary>
        /// Delete a record
        /// </summary>
        /// <param name="entry">The entry to Delete</param>
        public void Delete<T>(T entry) where T : ModelBase
        {
            IDAO<T> dao;
            if (entry is Project)
            {
                dao = (IDAO<T>)projectDAO;
            }
            else if (entry is Stage)
            {
                dao = (IDAO<T>)stageDAO;
            }
            else if (entry is Task)
            {
                dao = (IDAO<T>)taskDAO;
            }
            else if (entry is Team)
            {
                dao = (IDAO<T>)teamDAO;
            }
            else
            {
                dao = (IDAO<T>)teamMemberDAO;
            }
            dao.Delete(entry.ID);
        }

        /// <summary>
        /// Get all projects where the logged-in account is a part of as a List
        /// </summary>
        public List<Project>? GetProjectsOfCurrentAccount()
        {
            return LoginInfoState.Role switch
            {
                Role.Manager => projectDAO.GetAllProjectsOfAManager(LoginInfoState.Id!),
                Role.Dev or Role.Designer or Role.Tester => projectDAO.GetAllProjectsOfEmployee(LoginInfoState.Id!),
                Role.TechLead => projectDAO.GetAllProjectsOfTechLead(LoginInfoState.Id!),
                Role.Hr => null,
                _ => null,
            };
        }

        /// <summary>
        /// Get all stages in a project
        /// </summary>
        /// <param name="prj">The project</param>
        /// <returns></returns>
        public List<Stage>? GetStagesOfProject(Project prj)
        {
            return stageDAO.GetStagesOfAProject(prj);
        }

        /// <summary>
        /// Get all teams in a project stage
        /// </summary>
        /// <param name="stg">the stage</param>
        /// <returns></returns>
        public List<Team>? GetTeamsOfStage(Stage stg)
        {
            return teamDAO.GetTeamOfAStage(stg);
        }

        /// <summary>
        /// Get all members of a team
        /// </summary>
        /// <param name="team">the team</param>
        public List<TeamMember>? GetAllMembersOfTeam(Team team)
        {
            return teamMemberDAO.GetAllMembersOfTeam(team);
        }

        public List<Employee>? GetAllWorkers()
        {
            List<Employee>? workers = new List<Employee>();
            workers.AddRange(employeeDAO.GetAllMemberOfARole(Role.TechLead));
            workers.AddRange(employeeDAO.GetAllMemberOfARole(Role.Dev));
            workers.AddRange(employeeDAO.GetAllMemberOfARole(Role.Designer));
            workers.AddRange(employeeDAO.GetAllMemberOfARole(Role.Tester));
            workers.AddRange(employeeDAO.GetAllMemberOfARole(Role.Staff));
            return workers;
        }

        /// <summary>
        /// Get Detail information of a team member
        /// </summary>
        /// <param name="teamMember">the member</param>
        public Employee? GetTeamMemberDetails(TeamMember teamMember)
        {
            return employeeDAO.GetOne(teamMember.EmployeeID);
        }

        public Employee? GetTechLeadOfTeam(Team team)
        {
            return employeeDAO.GetOne(team.TechLeadID);
        }

        public void AddTeamMembersToTeam(string teamID, List<string> employeeIDs)
        {
            foreach (string eID in employeeIDs)
            {
                var toBeSavedTeamMember = TeamMember.CreateNewTeamMember(teamID, eID);
                teamMemberDAO.Add(toBeSavedTeamMember);
            }
        }
        public void RemoveTeamMembers(List<string> toBeRemovedTeamMemberIDs)
        {
            foreach (string teamMemberID in toBeRemovedTeamMemberIDs)
            {
                teamMemberDAO.Delete(teamMemberID);
            }
        }
        public string? GetTeamName(string teamID)
        {
            Team currentTeam = teamDAO.GetOne(teamID);
            return currentTeam?.Name;
        }
    }
}
