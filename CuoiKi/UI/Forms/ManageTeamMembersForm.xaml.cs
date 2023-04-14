using CuoiKi.ViewModels;
using System.Windows;

namespace CuoiKi.UI.Forms
{
    /// <summary>
    /// Interaction logic for ManageTeamMembersForm.xaml
    /// </summary>
    public partial class ManageTeamMembersForm : Window
    {
        public ManageTeamMembersForm(TeamMembersPageViewModel tmpvm)
        {
            InitializeComponent();
            this.DataContext = tmpvm;
        }
    }
}
