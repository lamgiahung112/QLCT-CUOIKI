using CuoiKi.States;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
        public ManagerViewModel() { 
        }   
    }
}
