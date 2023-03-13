using CuoiKi.Constants;
using CuoiKi.Controllers;
using CuoiKi.HelperClasses;
using CuoiKi.Models;
using CuoiKi.States;
using System;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;

namespace CuoiKi.ViewModels
{
    public class WorkSessionPageViewModel : ViewModelBase
    {
        private readonly WorkSessionController workSessionController;
        private string _currentUserId = "";
        private ObservableCollection<WorkSession> workSessionsInSelectedMonth;
        public WorkSessionPageViewModel()
        {
            workSessionController = new WorkSessionController();
            InitializeInfo();
            workSessionsInSelectedMonth = new ObservableCollection<WorkSession>();
            workSessionsInSelectedMonth.Add(new WorkSession("abc"));
            workSessionsInSelectedMonth.Add(new WorkSession("def"));
        }
        private void InitializeInfo()
        {
            _currentUserId = LoginInfoState.getInstance().Id;
            CurrentStatus = EnumMapper.mapToString(workSessionController.GetWorkSessionStatus(_currentUserId));
            UpdateLastestWorkSessionPanelVariables();
        }

        #region Binding work session list in month

        public ObservableCollection<WorkSession> WorkSessionsInSelectedMonth
        {
            get { return workSessionsInSelectedMonth; }
            set
            {
                workSessionsInSelectedMonth = value;
                OnPropertyChanged(nameof(WorkSessionsInSelectedMonth));
            }
        }
        #endregion

        #region Binding calendar current selected date
        private DateTime _calendarSelectedDate = DateTime.Today;
        public DateTime CalendarSelectedDate
        {
            get { return _calendarSelectedDate; }
            set
            {
                _calendarSelectedDate = value;
                OnPropertyChanged(nameof(CalendarSelectedDate));
                MessageBox.Show(value.ToString());
            }
        }
        #endregion

        #region Binding CurrentDate
        private string _currentDate = DateTime.Now.Date.ToString();
        public string CurrentDate
        {
            get { return _currentDate; }
            set
            {
                string[] tokens = value.Split(new Char[] { '/', ' ', '.' });
                _currentDate = tokens[1] + "/" + tokens[0] + "/" + tokens[2];
                OnPropertyChanged(nameof(CurrentDate));
            }
        }
        #endregion

        #region Binding CurrentStatus
        private string _currentStatus = "";
        public string CurrentStatus
        {
            get { return _currentStatus; }
            set
            {
                _currentStatus = value;
                OnPropertyChanged(nameof(CurrentStatus));
            }
        }
        #endregion

        #region Binding and update work session visible variables
        private void UpdateLastestWorkSessionPanelVariables()
        {
            CurrentDate = DateTime.Now.ToString();

            WorkSession? ws = workSessionController.GetLastestWorkSession(_currentUserId);
            if (ws is not null)
            {
                ShowEmptyWorkSession = false;
                ShowCurrentWorkSession = true;
                WorkSessionID = ws.Id;
                WorkSessionStartingTime = ws.StartingTime.ToString("F");
                if (ws.EndingTime == DateTime.MinValue)
                {
                    WorkSessionEndingTime = "You haven't checked out yet";
                }
                else
                {
                    WorkSessionEndingTime = ws.EndingTime?.ToString("F");
                }
            }
            else
            {
                ShowEmptyWorkSession = true;
                ShowCurrentWorkSession = false;
            }
        }
        private string _workSessionID = "";
        public string WorkSessionID
        {
            get { return _workSessionID; }
            set
            {
                _workSessionID = value;
                OnPropertyChanged(nameof(WorkSessionID));
            }
        }
        private string _workSessionStartingTime = "";
        public string WorkSessionStartingTime
        {
            get { return _workSessionStartingTime; }
            set
            {
                _workSessionStartingTime = value;
                OnPropertyChanged(nameof(WorkSessionStartingTime));
            }
        }
        private string _workSessionEndingTime = "";
        public string WorkSessionEndingTime
        {
            get { return _workSessionEndingTime; }
            set
            {
                _workSessionEndingTime = value;
                OnPropertyChanged(nameof(WorkSessionEndingTime));
            }
        }
        private bool _showEmptyWorkSession = true;
        public bool ShowEmptyWorkSession
        {
            get { return _showEmptyWorkSession; }
            set
            {
                _showEmptyWorkSession = value;
                OnPropertyChanged(nameof(ShowEmptyWorkSession));
            }
        }
        private bool _showCurrentWorkSession = false;
        public bool ShowCurrentWorkSession
        {
            get { return _showCurrentWorkSession; }
            set
            {
                _showCurrentWorkSession = value;
                OnPropertyChanged(nameof(ShowCurrentWorkSession));
            }
        }
        #endregion

        #region Show check in/out button properties
        private bool _showCheckInButton = true;
        public bool ShowCheckInButton
        {
            get { return _showCheckInButton; }
            set
            {
                _showCheckInButton = value;
                OnPropertyChanged(nameof(ShowCheckInButton));
            }
        }
        private bool _showCheckOutButton = true;
        public bool ShowCheckOutButton
        {
            get { return _showCheckOutButton; }
            set
            {
                _showCheckOutButton = value;
                OnPropertyChanged(nameof(ShowCheckOutButton));
            }
        }
        #endregion

        #region Handle check in command
        private ICommand? _checkInCommand;
        private void CheckIn()
        {
            workSessionController.CheckInAndReturnSuccessOrNot(LoginInfoState.getInstance().Id);
            CurrentStatus = EnumMapper.mapToString(workSessionController.GetWorkSessionStatus(_currentUserId));
            UpdateLastestWorkSessionPanelVariables();
        }
        private readonly bool canCheckIn = true;
        public ICommand CheckInCommand
        {
            get
            {
                _checkInCommand ??= new RelayCommand(
                        p => this.canCheckIn,
                        p => this.CheckIn());
                return _checkInCommand;
            }
        }
        #endregion

        #region Handle check out command
        private ICommand? _checkOutCommand;
        private void CheckOut()
        {
            workSessionController.CheckOutAndReturnSuccessOrNot(LoginInfoState.getInstance().Id);
            CurrentStatus = EnumMapper.mapToString(workSessionController.GetWorkSessionStatus(_currentUserId));
            UpdateLastestWorkSessionPanelVariables();
        }
        private readonly bool canCheckOut = true;
        public ICommand CheckOutCommand
        {
            get
            {
                _checkOutCommand ??= new RelayCommand(
                        p => this.canCheckOut,
                        p => this.CheckOut());
                return _checkOutCommand;
            }
        }
        #endregion
    }
}
