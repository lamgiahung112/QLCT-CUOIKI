using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CuoiKi.Models
{
    public class TeamMember : ModelBase
    {
        public string TeamID { get; }
        public string EmployeeID { get; }

        public TeamMember(string ID, string TeamID, string EmployeeID) : base(ID)
        {
            this.TeamID = TeamID;
            this.EmployeeID = EmployeeID;
        }
    }
}
