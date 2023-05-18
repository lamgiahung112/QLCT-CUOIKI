using CuoiKi.Controllers;
using CuoiKi.Models;
using System;
using System.Collections.Generic;
using System.Windows;

namespace CuoiKi.ViewModels
{
    public class EmployeeSalaryPageViewModel : ViewModelBase
    {
        private readonly DbController db;
        public EmployeeSalaryPageViewModel(Employee e)
        {
            db = new DbController();
            FetchSalary(e);
            EmployeeID = e.ID;
            EmployeeName = e.Name;

        }
        private string _employeeID = string.Empty;
        private string _employeeName = string.Empty;
        public string EmployeeID
        {
            get => _employeeID;
            set
            {
                _employeeID = value;
                OnPropertyChanged(nameof(EmployeeID));
            }
        }

        public string EmployeeName
        {
            get => _employeeName;
            set
            {
                _employeeName = value;
                OnPropertyChanged(nameof(EmployeeName));
            }
        }

        private DateTime _PickedDate = DateTime.Now;
        private string _PickedDateMessage
        {
            get => string.Format("Viewing Salary of {0}/{1}", PickedDate.Month + 1, PickedDate.Year);
            set { }
        }
        public DateTime PickedDate
        {
            get => _PickedDate;
            set
            {
                _PickedDate = value;
                OnPropertyChanged(nameof(PickedDate));
                MessageBox.Show(_PickedDateMessage);
            }
        }

        private int _BasicPay;
        public int BasicPay
        {
            get => _BasicPay;
            set
            {
                _BasicPay = value;
                OnPropertyChanged(nameof(BasicPay));
            }
        }

        private int leaveCost;
        private int kpiCost;

        private int _DelayedTasksCnt;
        public int DelayedTasksCnt
        {
            get => _DelayedTasksCnt;
            set
            {
                _DelayedTasksCnt = value;
                OnPropertyChanged(nameof(DelayedTasksCnt));
            }
        }

        private int _WorkLeaveCnt;
        public int WorkLeaveCnt
        {
            get => _WorkLeaveCnt;
            set
            {
                _WorkLeaveCnt = value;
                OnPropertyChanged(nameof(WorkLeaveCnt));
            }
        }


        public int TotalPay
        {
            get => BasicPay - DelayedTasksCnt * kpiCost - (WorkLeaveCnt > 2 ? WorkLeaveCnt - 2 : 0) * leaveCost;
            set { }
        }


        private void FetchSalary(Employee e)
        {
            DateTime firstDayOfMonth = new(PickedDate.Year, PickedDate.Month, 1);
            DateTime lastDayOfMonth = firstDayOfMonth.AddMonths(1).AddDays(-1);
            WorkLeaveCnt = db.GetAllWorkLeaveOfAnEmployee(e.ID, firstDayOfMonth, lastDayOfMonth)?.Count ?? 0;
            Salary? s = db.GetSalaryOfUser(e);
            List<Task>? delayedTasks = db.GetDelayedTasksOfUser(firstDayOfMonth, lastDayOfMonth, e);
            BasicPay = s?.BasicPay ?? 0;
            kpiCost = s?.KPICost ?? 0;
            leaveCost = s?.LeaveCost ?? 0;
            DelayedTasksCnt = delayedTasks?.Count ?? 0;
        }
    }
}
