using CuoiKi.Models;
using System;
using System.Collections.Generic;

namespace CuoiKi.DAOs
{
    public class TeamMemberDAO : IDAO<TeamMember>
    {
        private readonly DBConnection dbc;
        public TeamMemberDAO()
        {
            dbc = new DBConnection();
        }
        public void Add(TeamMember entry)
        {
            string cmd = SqlConverter.GetAddCommandForTeamMember(entry);
            dbc.Execute(cmd);
        }

        public void Delete(string id)
        {
            string cmd = SqlConverter.GetDeleteCommandForTeamMember(id);
            dbc.Execute(cmd);
        }

        public List<TeamMember>? GetAll()
        {
            throw new NotImplementedException();
        }

        public TeamMember? GetOne(string id)
        {
            // What is this?...
            string cmd = SqlConverter.GetDeleteCommandForTeamMember(id);
            return dbc.ExecuteQuery<TeamMember>(cmd);
        }

        public void Modify(TeamMember entry)
        {
            throw new NotImplementedException();
        }

        public List<TeamMember>? GetAllMembersOfTeam(Team team)
        {
            string cmd = SqlConverter.GetAllMembersOfTeamCommand(team);
            return dbc.ExecuteWithResults<TeamMember>(cmd);
        }
    }
}
