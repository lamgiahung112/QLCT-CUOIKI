using CuoiKi.ViewModels;
using System.Windows;
using System.Windows.Controls;

namespace CuoiKi.UI.Techlead
{
    /// <summary>
    /// Interaction logic for TechLeadTeamPage.xaml
    /// </summary>
    public partial class TechLeadTeamPage : Page
    {
        public TechLeadTeamPage()
        {
            InitializeComponent();
            this.DataContext = new TechLeadTeamViewModel();
        }
        private void BackClick(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }
        private void TeamItemClick(object sender, RoutedEventArgs e)
        {
            var btn = sender as Button;
            btn.Command.Execute(btn.CommandParameter);
            NavigationService.Navigate(new TechLeadTeamMemberPage());
        }
    }
}
