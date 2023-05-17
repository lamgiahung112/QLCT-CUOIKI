using CuoiKi.Constants;
using CuoiKi.Models;

namespace CuoiKi.Wrappers
{
    public class EmployeeWrapper : Wrapper
    {
        private readonly Employee _employee;
        public EmployeeWrapper(Employee employee)
        {
            this._employee = employee;
        }
        public string ID => _employee.ID;
        public string Name => _employee.Name;
        public Role Role => _employee.Role;
        public Employee Employee => _employee;
    }
}
