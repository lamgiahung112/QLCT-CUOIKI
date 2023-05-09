using CuoiKi.Controllers;
using CuoiKi.HelperClasses;
using CuoiKi.Models;
using CuoiKi.States;
using CuoiKi.UI.Forms;
using System;
using System.Collections.Generic;

namespace CuoiKi.ViewModels
{
    public class LeaveOfAbsencePageViewModel : ViewModelBase
    {
        private DbController _controller;
        private List<WorkLeave>? _workLeaves;
        public List<WorkLeave>? WorkLeaves
        {
            get => _workLeaves;
            set
            {
                _workLeaves = value;
                OnPropertyChanged(nameof(WorkLeaves));
            }
        }
        public LeaveOfAbsencePageViewModel()
        {
            _controller = new();
            _workLeaves = new List<WorkLeave>();
            fetchWorkLeaves();
            WorkLeaves = _workLeaves;
        }

        private void fetchWorkLeaves()
        {
            _workLeaves!.Clear();
            _workLeaves = _controller.GetAllWorkLeaveOfEmployeeInMonth(_employeeID);
        }

        #region UI Binding
        private string _employeeID = LoginInfoState.Id!;
        public string EmployeeID
        {
            get { return _employeeID; }
            set { }
        }

        private DateTime _fromDate = DateTime.Now;
        public DateTime FromDate
        {
            get
            {
                return _fromDate;
            }
            set
            {
                _fromDate = value;
                OnPropertyChanged(nameof(FromDate));
            }
        }

        private DateTime _toDate = DateTime.Now;
        public DateTime ToDate
        {
            get
            {
                return _toDate;
            }
            set
            {
                _toDate = value;
                OnPropertyChanged(nameof(ToDate));
            }
        }

        private string _reason = string.Empty;
        public string Reason
        {
            get
            {
                return _reason;
            }
            set
            {
                _reason = value;
                OnPropertyChanged(nameof(Reason));
            }
        }

        private RelayCommand? _CmdSaveWorkLeave;
        public RelayCommand CmdSaveWorkLeave
        {
            get
            {
                _CmdSaveWorkLeave ??= new(
                        p => true,
                        p => SaveWorkLeave()
                    );
                return _CmdSaveWorkLeave;
            }
        }
        #endregion

        private RelayCommand? _CmdAddWorkLeave;
        public RelayCommand CmdAddWorkLeave
        {
            get
            {
                _CmdAddWorkLeave ??= new(
                        p => true,
                        p => OpenAddWorkLeaveForm()
                    );
                return _CmdAddWorkLeave;
            }
        }

        private void OpenAddWorkLeaveForm()
        {
            var form = new WorkLeaveForm(this);
            form.Show();
        }

        private void SaveWorkLeave()
        {
            if (!(FromDate >= DateTime.Today && ToDate > FromDate && Reason.Length > 0))
            {
                return;
            }
            _controller.Save(WorkLeave.GetNewWorkLeave(EmployeeID, FromDate, ToDate, Reason));
            fetchWorkLeaves();
            WorkLeaves = _workLeaves;
        }
    }
}
