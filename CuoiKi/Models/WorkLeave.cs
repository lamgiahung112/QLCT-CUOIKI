using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CuoiKi.Models
{
    public class WorkLeave : ModelBase
    {
        public override string ID { get; }
        public string EmployeeID { get; set; }
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }
        public string ReasonOfLeave { get; set; }
        public WorkLeave(string ID, string EmployeeID, DateTime FromDate, DateTime ToDate, string ReasonOfLeave)
        {
            this.ID = ID;
            this.EmployeeID = EmployeeID;
            this.FromDate = FromDate;
            this.ToDate = ToDate;
            this.ReasonOfLeave = ReasonOfLeave;
        }

    }
}
