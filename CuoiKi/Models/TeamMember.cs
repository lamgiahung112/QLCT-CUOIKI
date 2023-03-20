using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CuoiKi.Models
{
    public class TeamMember : ModelBase
    {
        public override string ID { get; }
        public string TeamID { get; }
        public string EmployeeID { get; }

        public TeamMember(string ID, string TeamID, string EmployeeID)
        {
            this.ID = ID;
            this.TeamID = TeamID;
            this.EmployeeID = EmployeeID;
        }

        public static TeamMember CreateNewTeamMember(string TeamID, string EmployeeID)
        {
            string ID = "TeamMember" + TeamID + new Random().Next(1000, 9999).ToString();
            return new(ID, TeamID, EmployeeID);
        }
    }
}
