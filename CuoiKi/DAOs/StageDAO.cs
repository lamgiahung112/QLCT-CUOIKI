using CuoiKi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CuoiKi.DAOs
{
    public class StageDAO : IDAO<Stage>
    {
        private readonly DBConnection dbc;
        public StageDAO()
        {
            dbc = new DBConnection();
        }
        public void Add(Stage entry)
        {
            string cmd = SqlConverter.GetAddCommandForStage(entry);
            dbc.Execute(cmd);
        }

        public void Delete(string id)
        {
            string cmd = SqlConverter.GetDeleteCommandForStage(id);
            dbc.Execute(cmd);
        }

        public List<Stage>? GetAll()
        {
            throw new NotImplementedException();
        }

        public Stage? GetOne(string id)
        {
            string cmd = SqlConverter.GetOneByIdCommandForStage(id);
            return dbc.ExecuteQuery<Stage>(cmd);
        }

        public void Modify(Stage entry)
        {
            string cmd = SqlConverter.GetUpdateCommandForStage(entry);
            dbc.Execute(cmd);
        }

        public List<Stage>? GetStagesOfAProject(Project project)
        {
            string cmd = SqlConverter.GetStagesOfAProjectCommand(project);
            return dbc.ExecuteWithResults<Stage>(cmd);
        }
    }
}
