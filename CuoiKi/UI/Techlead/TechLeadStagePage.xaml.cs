using CuoiKi.ViewModels;
using System.Windows;
using System.Windows.Controls;

namespace CuoiKi.UI.Techlead
{
    /// <summary>
    /// Interaction logic for TechLeadStagePage.xaml
    /// </summary>
    public partial class TechLeadStagePage : Page
    {
        public TechLeadStagePage()
        {
            InitializeComponent();
            this.DataContext = new TechLeadStageViewModel();
        }
        private void StageButtonClick(object sender, RoutedEventArgs e)
        {
            var btn = sender as Button;
            btn.Command.Execute(btn.CommandParameter);
            NavigationService.Navigate(new TechLeadTeamPage());
        }
        private void BackClick(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }
    }
}
