using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CuoiKi.Models
{
    public class Team
    {
        public String TeamID { get; }
        public String StageID { get; }
        public String TechLeadID { get; set; }

        public Team(String TeamID, String StageID, String TechLeadID) 
        {
            this.TeamID = TeamID;
            this.StageID = StageID;
            this.TechLeadID = TechLeadID;
        }
    }
}
