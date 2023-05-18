using CuoiKi.Controllers;
using CuoiKi.Models;
using CuoiKi.States;
using System;
using System.Collections.Generic;
using System.Windows;

namespace CuoiKi.ViewModels
{
    public class SalaryPageViewModel : ViewModelBase
    {
        private readonly DbController db;
        public SalaryPageViewModel()
        {
            db = new DbController();
            FetchSalary();
        }
        public string EmployeeID
        {
            get => LoginInfoState.Id!;
            set { }
        }

        public string EmployeeName
        {
            get => LoginInfoState.Name!;
            set { }
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


        private void FetchSalary()
        {
            DateTime firstDayOfMonth = new(PickedDate.Year, PickedDate.Month, 1);
            DateTime lastDayOfMonth = firstDayOfMonth.AddMonths(1).AddDays(-1);
            WorkLeaveCnt = db.GetAllWorkLeaveOfAnEmployee(LoginInfoState.Id!, firstDayOfMonth, lastDayOfMonth)?.Count ?? 0;
            Salary? s = db.GetSalaryOfCurrentUser();
            List<Task>? delayedTasks = db.GetDelayedTasksOfCurrentUser(firstDayOfMonth, lastDayOfMonth);
            BasicPay = s?.BasicPay ?? 0;
            kpiCost = s?.KPICost ?? 0;
            leaveCost = s?.LeaveCost ?? 0;
            DelayedTasksCnt = delayedTasks?.Count ?? 0;
        }
    }
}
