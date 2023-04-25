using CuoiKi.ViewModels;
using System.Windows.Controls;

namespace CuoiKi.UI.Staff
{
    /// <summary>
    /// Interaction logic for StaffTaskManagementForm.xaml
    /// </summary>
    public partial class StaffTaskManagementForm : Page
    {
        public StaffTaskManagementForm()
        {
            InitializeComponent();
            this.DataContext = new StaffTaskManagementViewModel();
        }
    }
}
