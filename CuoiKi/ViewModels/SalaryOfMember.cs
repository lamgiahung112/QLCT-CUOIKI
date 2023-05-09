using CuoiKi.Controllers;
using CuoiKi.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

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
