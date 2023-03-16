using System;

namespace CuoiKi.Models
{
    public class WorkSession : ModelBase
    {
        public String EmployeeId { get; set; }
        public DateTime StartingTime { get; set; }
        public DateTime? EndingTime { get; set; }

        /// <summary>
        /// Used when creating a new Session for an Employee 
        /// </summary>
        public WorkSession(String employeeId) : base(employeeId + DateTime.Now.ToShortDateString() + new Random().Next(1000, 9999))
        {
            EmployeeId = employeeId;
            StartingTime = DateTime.Now;
            EndingTime = null;
        }

        /// <summary>
        /// Used when getting data from db
        /// </summary>
        public WorkSession(String ID, String employeeId, DateTime startingTime, DateTime endingTime) : base(ID)
        {
            EmployeeId = employeeId;
            StartingTime = startingTime;
            EndingTime = endingTime;
        }
    }
}
