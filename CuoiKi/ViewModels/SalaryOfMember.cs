using System.Collections.Generic;

namespace CuoiKi.ViewModels
{
    public class SalaryOfMember : ViewModelBase
    {
        DbController _controller;
        public List<Employee> StaffList { get; set; }
        public SalaryOfMember()
        {
            _controller = new();
            StaffList = _controller.GetAllEmployees() ?? new();
        }
    }
}
