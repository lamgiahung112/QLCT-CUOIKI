using CuoiKi.Controllers;
using CuoiKi.HelperClasses;
using CuoiKi.States;
using System.Windows.Input;

namespace CuoiKi.ViewModels
{
    public class StaffFormViewModel : ViewModelBase
    {
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
            WorkSessionController workSessionController = new WorkSessionController();
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
            WorkSessionController workSessionController = new WorkSessionController();
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
