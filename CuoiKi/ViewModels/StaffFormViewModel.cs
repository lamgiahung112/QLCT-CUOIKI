using CuoiKi.Controllers;
using CuoiKi.HelperClasses;
using CuoiKi.States;
using System;
using System.Windows.Input;

namespace CuoiKi.ViewModels
{
    public class StaffFormViewModel : ViewModelBase
    {
        private WorkSessionController workSessionController;
        private string currentUserId = "";
        public StaffFormViewModel()
        {
            workSessionController = new WorkSessionController();
            InitializeInfo();
        }

        private void InitializeInfo()
        {
            currentUserId = LoginInfoState.getInstance().Id;
        }

        #region Binding CurrentDate
        private string _currentDate = "Today is: " + DateTime.Now.ToString();
        public string CurrentDate
        {
            get { return _currentDate; }
            set
            {
                _currentDate = "Today is: " + value.ToString();
                OnPropertyChanged(nameof(CurrentDate));
            }
        }
        #endregion

        #region Binding CurrentStatus
        private string _currentStatus = "Your current status: ";
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
        }
        private readonly bool canCheckIn = true;
        public ICommand CheckInCommand
        {
            get
            {
                if (_checkInCommand == null)
                {
                    _checkInCommand = new RelayCommand(
                        p => this.canCheckIn,
                        p => this.CheckIn());
                }
                return _checkInCommand;
            }
        }
        #endregion

        #region Handle check out command
        private ICommand? _checkOutCommand;
        private void CheckOut()
        {
            workSessionController.CheckOutAndReturnSuccessOrNot(LoginInfoState.getInstance().Id);
        }
        private readonly bool canCheckOut = true;
        public ICommand CheckOutCommand
        {
            get
            {
                if (_checkOutCommand == null)
                {
                    _checkOutCommand = new RelayCommand(
                        p => this.canCheckOut,
                        p => this.CheckOut());
                }
                return _checkOutCommand;
            }
        }
        #endregion
    }
}
