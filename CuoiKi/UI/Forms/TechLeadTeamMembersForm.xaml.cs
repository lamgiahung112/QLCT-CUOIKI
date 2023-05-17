using CuoiKi.ViewModels;
using System.Windows;

namespace CuoiKi.UI.Forms
{
    /// <summary>
    /// Interaction logic for TechLeadTeamMembersForm.xaml
    /// </summary>
    public partial class TechLeadTeamMembersForm : Window
    {
        public TechLeadTeamMembersForm(TechLeadTeamMemberViewModel tltmvm)
        {
            InitializeComponent();
            this.DataContext = tltmvm;
        }
    }
}
