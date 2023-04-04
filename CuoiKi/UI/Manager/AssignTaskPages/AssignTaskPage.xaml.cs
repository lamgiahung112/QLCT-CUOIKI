using CuoiKi.States;
using CuoiKi.UI.Forms;
using CuoiKi.ViewModels;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Navigation;

namespace CuoiKi.UI.Manager.AssignTaskPages
{
    /// <summary>
    /// Interaction logic for AssignTaskPage.xaml
    /// </summary>
    public partial class AssignTaskPage : Page
    {
        public AssignTaskPage()
        {
            InitializeComponent();
            this.DataContext = new TeamMembersPageViewModel();
        }
        private void Back_Click(object sender, RoutedEventArgs e)
        {
            TaskAssignmentState.SelectedTeam = null;
            NavigationService.GoBack();
        }

        private void MouseRight_Click(object sender, MouseButtonEventArgs e)
        {
            RightClickForm rightClickForm = new RightClickForm();
            rightClickForm.Left = e.GetPosition(this).X + 440;
            rightClickForm.Top = e.GetPosition(this).Y + 60;
            rightClickForm.Show();

        }

        private void Open_TaskMng(object sender, RoutedEventArgs e)
        {
            Forms.TaskMngToTechlead taskMngToTechlead = new Forms.TaskMngToTechlead();
            taskMngToTechlead.Show();
        }
    }
}
