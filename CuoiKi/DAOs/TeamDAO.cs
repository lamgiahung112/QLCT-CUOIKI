using CuoiKi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CuoiKi.DAOs
{
    public class TeamDAO : IDAO<Team>
    {
        private readonly DBConnection dbc;
        public TeamDAO() 
        {
            dbc = new DBConnection();
        }
        public void Add(Team entry)
        {
            string cmd = SqlConverter.GetAddCommandForTeam(entry);
            dbc.Execute(cmd);
        }

        public void Delete(string id)
        {
            string cmd = SqlConverter.GetDeleteCommandForTeam(id);
            dbc.Execute(cmd);
        }

        public List<Team>? GetAll()
        {
            throw new NotImplementedException();
        }

        public Team? GetOne(string id)
        {
            string cmd = SqlConverter.GetOneByIdCommandForTeam(id);
            return dbc.ExecuteQuery<Team>(cmd);
        }

        public void Modify(Team entry)
        {
            string cmd = SqlConverter.GetUpdateCommandForTeam(entry);
            dbc.Execute(cmd);
        }

        public List<Team>? GetTeamOfAStage(Stage entry)
        {
            string cmd = SqlConverter.GetTeamsOfAStageCommand(entry);
            return dbc.ExecuteWithResults<Team>(cmd);
        }
    }
}
