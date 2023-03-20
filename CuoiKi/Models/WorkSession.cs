using System;

namespace CuoiKi.Models
{
    public class WorkSession : ModelBase
    {
        public override string ID { get; }
        public string EmployeeID { get; set; }
        public DateTime StartingTime { get; set; }
        public DateTime? EndingTime { get; set; }

        /// <summary>
        /// Used when creating a new Session for an Employee 
        /// </summary>
        public WorkSession(String EmployeeID)
        {
            this.ID = EmployeeID + DateTime.Now.ToShortDateString().Replace("/", "") + new Random().Next(1000, 9999);
            this.EmployeeID = EmployeeID;
            StartingTime = DateTime.Now;
            EndingTime = null;
        }

        /// <summary>
        /// Used when getting data from db
        /// </summary>
        public WorkSession(String ID, String employeeID, DateTime StartingTime, DateTime EndingTime)
        {
            this.ID = ID;
            this.EmployeeID = employeeID;
            this.StartingTime = StartingTime;
            this.EndingTime = EndingTime;
        }
    }
}
