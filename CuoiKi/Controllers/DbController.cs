﻿using CuoiKi.Constants;
using CuoiKi.DAOs;
using CuoiKi.Models;
using CuoiKi.States;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;

namespace CuoiKi.Controllers
{
    public class DbController
    {
        private readonly ProjectDAO projectDAO;
        private readonly StageDAO stageDAO;
        private readonly TaskDAO taskDAO;
        private readonly TeamDAO teamDAO;
        private readonly TeamMemberDAO teamMemberDAO;
        private readonly EmployeeDAO employeeDAO;
        private readonly WorkLeaveDAO workLeaveDAO;
        private readonly SalaryDAO salaryDAO;

        public DbController()
        {
            projectDAO = new();
            stageDAO = new();
            taskDAO = new();
            teamDAO = new();
            employeeDAO = new();
            teamMemberDAO = new();
            workLeaveDAO = new();
            salaryDAO = new();
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
            else if (entry is WorkLeave)
            {
                existingRecord = workLeaveDAO.GetOne(entry.ID);
                dao = (IDAO<T>)workLeaveDAO;
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

        public List<Employee>? GetAllEmployees()
        {
            return employeeDAO.GetAll();
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
        public List<Task>? GetAllTaskOfEmployee(string employeeID)
        {
            List<Task>? tasks = taskDAO.GetAll();
            if (tasks is not null)
            {
                List<Task>? employeeTask = tasks.Where(task => task.Assignee == employeeID).ToList();
                return employeeTask;
            }
            return tasks;
        }
        public List<WorkLeave>? GetAllWorkLeaveOfEmployee(string employeeID)
        {
            return workLeaveDAO.GetAllOfAnEmployee(employeeID);
        }
        public List<Task>? GetAssignedJobsByManagerToEmployee(string assignerID, string assigneeID)
        {
            return taskDAO.GetAssignedJobsByManagerToEmployee(assignerID, assigneeID);
        }
        public List<Task>? GetAllTaskOfProject(string projectID)
        {
            return taskDAO.GetAllTaskOfProject(projectID);
        }
        public List<Task>? GetAllTaskOfStage(string stageID)
        {
            return taskDAO.GetAllTaskOfStage(stageID);
        }
        public List<Task>? GetAllTaskOfTeam(string teamID)
        {
            return taskDAO.GetAllTaskOfTeam(teamID);
        }
        public List<Project>? GetAllProjectsOfTechLead(string techLeadID)
        {
            return projectDAO.GetAllProjectsOfTechLead(techLeadID);
        }

        public Salary? GetSalaryOfCurrentUser()
        {
            return salaryDAO.GetAll()?.Where(x => x.ID == LoginInfoState.Id!).FirstOrDefault();
        }
        public Salary? GetSalaryOfUser(Employee e)
        {
            return salaryDAO.GetAll()?.Where(x => x.ID == e.ID!).FirstOrDefault();
        }
        public List<Task>? GetDelayedTasksOfCurrentUser(DateTime startTime, DateTime endTime)
        {
            return taskDAO.GetAllTasksOfAnEmployee(LoginInfoState.Id!)?.Where(x => x.Status == TaskStatus.WIP && x.EndingTime < endTime && x.StartingTime > startTime).ToList();
        }
        public List<Task>? GetDelayedTasksOfUser(DateTime startTime, DateTime endTime, Employee employee)
        {
            return taskDAO.GetAllTasksOfAnEmployee(employee.ID)?.Where(x => x.Status == TaskStatus.WIP && x.EndingTime < endTime && x.StartingTime > startTime).ToList();
        }
        public List<WorkLeave>? GetAllWorkLeaveOfAnEmployee(string employeeID, DateTime startingTime, DateTime endingTime)
        {
            return workLeaveDAO.GetAllOfAnEmployee(employeeID).Where(x => x.FromDate >= startingTime && x.FromDate <= endingTime).ToList();
        }
    }
}
