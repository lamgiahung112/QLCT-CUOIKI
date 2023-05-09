using CuoiKi.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CuoiKi.ViewModels
{
    public class SalaryOfMember : ViewModelBase
    {
        public class Staff
        {
            public string ID { get; set; }
            public string Name { get; set; }
            public string Role { get; set; }
            public string Status { get; set; }
        }
        public List<Staff> StaffList { get; set; }

        public SalaryOfMember()
        {
            StaffList = new List<Staff>()
        {
            new Staff { ID = "1", Name = "Nguyen Van A", Role = "Dev", Status = "Active" },
            new Staff { ID = "2", Name = "Nguyen Van B", Role = "Tester", Status = "Active" },
            new Staff { ID = "3", Name = "Nguyen Van C", Role = "Designer", Status = "Active" }
        };
        }
    }
}
