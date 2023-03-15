using CuoiKi.States;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CuoiKi.Models
{
    public class Project
    {
        public String ID { get; }
        public String Name { get; set; }
        public String Description { get; set; }
        public String ManagerID { get; }

        public Project(String ID, String Name, String Description, String ManagerID)
        {
            this.ID = ID;
            this.Name = Name;
            this.Description = Description;
            this.ManagerID = ManagerID;
        }

        public static Project CreateNewProject(String ID, String Name, String Description)
        {
            return new(ID, Name, Description, LoginInfoState.getInstance().Id);
        }
    }
}
