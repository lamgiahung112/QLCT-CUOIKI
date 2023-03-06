using CuoiKi.HelperClasses;
using System.Windows;
using System.Windows.Input;

namespace CuoiKi.ViewModels
{
    public class StaffFormViewModel : ViewModelBase
    {
        private bool _showCheckInButton = false;
        public bool ShowCheckInButton
        {
            get { return _showCheckInButton; }
            set
            {
                _showCheckInButton = value;
                OnPropertyChanged(nameof(ShowCheckInButton));
            }
        }
        private bool _showCheckOutButton = false;
        public bool ShowCheckOutButton
        {
            get { return _showCheckOutButton; }
            set
            {
                _showCheckOutButton = value;
                OnPropertyChanged(nameof(ShowCheckOutButton));
            }
        }

        private ICommand? _checkInCommand;
        private void CheckIn()
        {
            MessageBox.Show("Hi");
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
    }
}
