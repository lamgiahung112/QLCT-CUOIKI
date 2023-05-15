using CuoiKi.ViewModels;
using System.Windows.Controls;

namespace CuoiKi.UI.Staff
{
    /// <summary>
    /// Interaction logic for LeaveOfAbsenceForm.xaml
    /// </summary>
    public partial class LeaveOfAbsenceForm : Page
    {
        public LeaveOfAbsenceForm()
        {
            InitializeComponent();
            this.DataContext = new LeaveOfAbsencePageViewModel();
        }
    }
}
