using CuoiKi.States;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CuoiKi.Models
{
    public class Project : ModelBase
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string ManagerID { get; set; }
        public DateTime CreatedAt { get; set; }

        public Project(string ID, string Name, string Description, string ManagerID, DateTime CreatedAt) : base(ID)
        {
            this.Name = Name;
            this.Description = Description;
            this.ManagerID = ManagerID;
            this.CreatedAt = CreatedAt;
        }

        public static Project CreateNewProject(string ID, string Name, string Description)
        {
            return new(ID, Name, Description, LoginInfoState.getInstance().Id, DateTime.Now);
        }
    }
}
