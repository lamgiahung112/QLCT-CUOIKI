using System.Windows;

namespace CuoiKi.ViewModels
{
    public class ManagerViewModel : ViewModelBase
    {
        private Visibility _tab = Visibility.Visible;
        public Visibility Tab
        {
            get { return _tab; }
            set
            {
                _tab = value;
                OnPropertyChanged(nameof(Tab));
            }
        }
        public ManagerViewModel()
        {
        }
    }
}
