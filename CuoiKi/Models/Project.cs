using CuoiKi.States;
using System;

namespace CuoiKi.Models
{
    public class Project : ModelBase
    {
        public override string ID { get; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string ManagerID { get; set; }
        public DateTime CreatedAt { get; set; }

        public Project(string ID, string Name, string Description, string ManagerID, DateTime CreatedAt)
        {
            this.ID = ID;
            this.Name = Name;
            this.Description = Description;
            this.ManagerID = ManagerID;
            this.CreatedAt = CreatedAt;
        }

        public static Project CreateNewProject(string Name, string Description)
        {
            string ID = "Prj" + DateTime.Now.ToShortDateString() + new Random().Next(1000, 9999);
            return new(ID, Name, Description, LoginInfoState.getInstance().Id, DateTime.Now);
        }
    }
}
