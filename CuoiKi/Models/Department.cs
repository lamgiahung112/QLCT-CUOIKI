using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CuoiKi.Models
{
    public class Department
    {
        public String ID { get; set; }
        public String Name { get; set; }

        public Department(String ID, String Name)
        {
            this.ID = ID;
            this.Name = Name;
        }
    }
}
