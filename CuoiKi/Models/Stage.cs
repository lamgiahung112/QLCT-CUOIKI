using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CuoiKi.Models
{
    public class Stage : ModelBase
    {
        public string ProjectID { get; }
        public string Description { get; set; }

        public Stage(string ID, string ProjectID, string Description) : base(ID)
        {
            this.ProjectID = ProjectID;
            this.Description = Description;
        }

        public Stage(string ProjectID, string Description) : base(ProjectID + "-" + new Random().Next(9999).ToString())
        {
            this.Description = Description;
            this.ProjectID = ProjectID;
        }
    }
}
