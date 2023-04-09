using CuoiKi.ViewModels;
using System.Windows;

namespace CuoiKi.UI.Forms
{
    /// <summary>
    /// Interaction logic for TeamManagementWindow.xaml
    /// </summary>
    public partial class TeamManagementWindow : Window
    {
        public TeamManagementWindow(TeamMembersPageViewModel tmpvm)
        {
            InitializeComponent();
            this.DataContext = tmpvm;
        }
    }
}
