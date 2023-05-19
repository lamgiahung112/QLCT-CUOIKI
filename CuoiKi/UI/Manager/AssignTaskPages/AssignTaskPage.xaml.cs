using CuoiKi.States;
using CuoiKi.ViewModels;
using System.Windows;
using System.Windows.Controls;
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


        private void Open_TaskMng(object sender, RoutedEventArgs e)
        {
            Forms.TaskMngToTechlead taskMngToTechlead = new Forms.TaskMngToTechlead();
            taskMngToTechlead.Show();
        }

        private void btn_ViewTask_Click(object sender, RoutedEventArgs e)
        {
            var btn = sender as Button;
            btn.Command.Execute(btn.CommandParameter);
            NavigationService.Navigate(new ViewTask());
        }
        private void ViewInformationClick(object sender, RoutedEventArgs e)
        {
            var btn = sender as Button;
            btn.Command.Execute(btn.CommandParameter);
            NavigationService.Navigate(new ManagerViewInfomationPage());
        }
    }
}
