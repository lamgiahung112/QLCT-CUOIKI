using CuoiKi.UI.Forms;
using CuoiKi.ViewModels;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Navigation;

namespace CuoiKi.UI.Manager.AssignTaskPages
{
    /// <summary>
    /// Interaction logic for StagesPage.xaml
    /// </summary>
    public partial class StagesPage : Page
    {
        public StagesPage()
        {
            InitializeComponent();
            DataContext = new StagesPageViewModel();
        }
        private void back_click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }

        private void BtnStageItem_Click(object sender, RoutedEventArgs e)
        {
            var btn = sender as Button;
            btn!.Command.Execute(btn.CommandParameter);
            NavigationService.Navigate(new TaskAssignmentPage());
        }

        private void BtnStageItem_PreviewMouseRightButtonDown(object sender, MouseButtonEventArgs e)
        {
            var btn = sender as Button;
            btn!.Command.Execute(btn.CommandParameter);
        }

        private void ViewMenuItem_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new TaskAssignmentPage());
        }
    }
}
