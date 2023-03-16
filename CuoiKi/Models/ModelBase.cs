using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CuoiKi.Models
{
    public abstract class ModelBase
    {
        public string ID;

        public ModelBase(string ID) 
        { 
            this.ID = ID;
        }
    }
}
