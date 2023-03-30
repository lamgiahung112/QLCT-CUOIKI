using CuoiKi.States;
using CuoiKi.ViewModels;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Navigation;

namespace CuoiKi.UI.Manager.AssignTaskPages
{
    /// <summary>
    /// Interaction logic for TaskAssignmentPage.xaml
    /// </summary>
    public partial class TaskAssignmentPage : Page
    {
        public TaskAssignmentPage()
        {
            InitializeComponent();
            this.DataContext = new TeamsPageViewModel();
        }

        private void back_click(object sender, RoutedEventArgs e)
        {
            TaskAssignmentState.SelectedStage = null;
            NavigationService.GoBack();
        }
        private void Team_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new AssignTaskPage());
        }

        private void BtnTeamItem_PreviewMouseRightButtonDown(object sender, MouseButtonEventArgs e)
        {
            var btn = sender as Button;
            btn!.Command.Execute(btn.CommandParameter);
        }

        private void ViewMenuItem_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new AssignTaskPage());
        }
    }
}
