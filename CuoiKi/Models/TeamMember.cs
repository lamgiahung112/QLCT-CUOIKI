using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CuoiKi.Models
{
    public class TeamMember
    {
        public String TeamID { get; }
        public String EmployeeID { get; }

        public TeamMember(string TeamID, string EmployeeID)
        {
            this.TeamID = TeamID;
            this.EmployeeID = EmployeeID;
        }
    }
}
