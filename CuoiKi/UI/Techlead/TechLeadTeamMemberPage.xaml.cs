using CuoiKi.ViewModels;
using System.Windows;
using System.Windows.Controls;
namespace CuoiKi.UI.Techlead
{
    /// <summary>
    /// Interaction logic for TechLeadTeamMemberPage.xaml
    /// </summary>
    public partial class TechLeadTeamMemberPage : Page
    {
        public TechLeadTeamMemberPage()
        {
            InitializeComponent();
            this.DataContext = new TechLeadTeamMemberViewModel();
        }
        private void BackClick(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }
        private void ViewTaskClick(object sender, RoutedEventArgs e)
        {
            var btn = sender as Button;
            btn!.Command.Execute(btn.CommandParameter);
            NavigationService.Navigate(new TechLeadViewTask());
        }
    }
}
