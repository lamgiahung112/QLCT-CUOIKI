﻿using CuoiKi.Models;
using System.Collections.Generic;

namespace CuoiKi.DAOs
{
    public class TaskDAO : IDAO<Task>
    {
        private readonly DBConnection dbc;

        public TaskDAO()
        {
            dbc = new DBConnection();
        }
        public void Add(Task entry)
        {
            string cmd = SqlConverter.GetAddCommandForTask(entry);
            dbc.Execute(cmd);
        }

        public void Delete(string id)
        {
            string cmd = SqlConverter.GetDeleteCommandForTask(id);
            dbc.Execute(cmd);
        }

        public List<Task>? GetAll()
        {
            string cmd = SqlConverter.GetAllTaskCommand();
            return dbc.ExecuteWithResults<Task>(cmd);
        }

        public Task? GetOne(string id)
        {
            string cmd = SqlConverter.GetOneByIdCommandForTask(id);
            return dbc.ExecuteQuery<Task>(cmd);
        }

        public void Modify(Task entry)
        {
            string cmd = SqlConverter.GetModifyCommandForTask(entry);
            dbc.Execute(cmd);
        }

        public List<Task>? GetAllTasksOfAnEmployee(string id)
        {
            return GetAll()?.FindAll(x => x.Assigner == id);
        }

        public List<Task>? GetTasksOfATeamMember(TeamMember member)
        {
            string cmd = SqlConverter.GetTasksOfATeamMemberCommand(member);
            return dbc.ExecuteWithResults<Task>(cmd);
        }
        public List<Task>? GetAssignedJobsByManagerToEmployee(string assignerID, string assigneeID)
        {
            string cmd = SqlConverter.GetAssignedJobsByManagerToEmployeeCommand(assignerID, assigneeID);
            return dbc.ExecuteWithResults<Task>(cmd);
        }
        public List<Task>? GetAllTaskOfProject(string projectID)
        {
            string cmd = SqlConverter.GetAllTaskOfProjectCommand(projectID);
            return dbc.ExecuteWithResults<Task>(cmd);
        }
        public List<Task>? GetAllTaskOfStage(string stageID)
        {
            string cmd = SqlConverter.GetAllTaskOfStageCommand(stageID);
            return dbc.ExecuteWithResults<Task>(cmd);
        }
        public List<Task>? GetAllTaskOfTeam(string teamID)
        {
            string cmd = SqlConverter.GetAllTaskOfTeamCommand(teamID);
            return dbc.ExecuteWithResults<Task>(cmd);
        }
    }
}
