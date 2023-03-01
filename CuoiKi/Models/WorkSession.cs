using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CuoiKi.Models
{
    class WorkSession
    {
        public String Id { get; set; }
        public String EmployeeId { get; set; }
        public DateTime StartingTime { get; set; }
        public DateTime? EndingTime { get; set;}

        /// <summary>
        /// Used when creating a new Session for an Employee 
        /// </summary>
        public WorkSession(String employeeId) {
            Id = employeeId + DateTime.Now.ToShortDateString();
            EmployeeId = employeeId;
            StartingTime = DateTime.Now;
            EndingTime = null;
        }

        /// <summary>
        /// Used when getting data from db
        /// </summary>
        public WorkSession(String id, String employeeId, DateTime startingTime, DateTime endingTime) {
            Id = id;
            EmployeeId = employeeId;
            StartingTime = startingTime;
            EndingTime = endingTime;
        }
    }
}
