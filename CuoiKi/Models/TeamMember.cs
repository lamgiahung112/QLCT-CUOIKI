using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CuoiKi.Models
{
    public class TeamMember
    {
        public string ID { get; set; }
        public string TeamID { get; }
        public string EmployeeID { get; }

        public TeamMember(string ID, string TeamID, string EmployeeID)
        {
            this.ID = ID;
            this.TeamID = TeamID;
            this.EmployeeID = EmployeeID;
        }
    }
}
