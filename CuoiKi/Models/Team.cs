using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CuoiKi.Models
{
    public class Team
    {
        public String ID { get; set; }
        public String StageID { get; set; }
        public String TechLeadID { get; set; }

        public Team(String ID, String StageID, String TechLeadID) 
        {
            this.ID = ID;
            this.StageID = StageID;
            this.TechLeadID = TechLeadID;
        }
    }
}
