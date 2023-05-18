using CuoiKi.Controllers;
using CuoiKi.HelperClasses;
using CuoiKi.Models;
using System.Collections.Generic;
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
        private ICommand? _cmdViewSalary;
        public ICommand CmdViewSalary
        {
            get
            {
                _cmdViewSalary ??= new RelayCommand(
                    p => true,
                    p => ViewSalary(p));
                return _cmdViewSalary;
            }
        }
        private void ViewSalary(object p)
        {

        }
    }
}
