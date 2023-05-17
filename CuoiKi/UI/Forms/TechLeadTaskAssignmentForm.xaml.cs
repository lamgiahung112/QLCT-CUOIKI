using CuoiKi.ViewModels;
using System.Windows;

namespace CuoiKi.UI.Forms
{
    /// <summary>
    /// Interaction logic for TechLeadTaskAssignmentForm.xaml
    /// </summary>
    public partial class TechLeadTaskAssignmentForm : Window
    {
        public TechLeadTaskAssignmentForm(TechLeadTeamMemberViewModel tltmvm)
        {
            InitializeComponent();
            this.DataContext = tltmvm;
        }
        private void CancelClick(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
