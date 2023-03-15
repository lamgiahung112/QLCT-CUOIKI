using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CuoiKi.Models
{
    public class Stage
    {
        public String ID { get; }
        public String ProjectID { get; }
        public String Description { get; set; }

        public Stage(String ID, String ProjectID, String Description)
        {
            this.ID = ID;
            this.ProjectID = ProjectID;
            this.Description = Description;
        }

        public Stage(String ProjectID, String Description)
        {
            this.ID = ProjectID + "-" + Description.Trim();
            this.Description = Description;
            this.ProjectID = ProjectID;
        }
    }
}
