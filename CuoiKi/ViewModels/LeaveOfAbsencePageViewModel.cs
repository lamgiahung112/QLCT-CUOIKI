using CuoiKi.Controllers;
using CuoiKi.HelperClasses;
using CuoiKi.Models;
using CuoiKi.States;
using CuoiKi.UI.Forms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CuoiKi.ViewModels
{
    public class LeaveOfAbsencePageViewModel : ViewModelBase
    {
        KpiController _controller;
        public LeaveOfAbsencePageViewModel()
        {
            _controller = new();
        }

        #region UI Binding
        private string _EmployeeID = LoginInfoState.Id!;
        public string EmployeeID 
        {
            get { return _EmployeeID; }
            set { }
        }

        private DateTime _FromDate = DateTime.Now;
        public DateTime FromDate
        {
            get
            {
                return _FromDate;
            }
            set
            {
                _FromDate = value;
                OnPropertyChanged(nameof(FromDate));
            }
        }

        private DateTime _ToDate = DateTime.Now;
        public DateTime ToDate
        {
            get
            {
                return _ToDate;
            }
            set
            {
                _ToDate = value;
                OnPropertyChanged(nameof(ToDate));
            }
        }

        private string _Reason = string.Empty;
        public string Reason
        {
            get
            {
                return _Reason;
            }
            set
            {
                _Reason = value;
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
        }
    }
}
