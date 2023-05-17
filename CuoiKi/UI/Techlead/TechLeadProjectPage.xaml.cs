using CuoiKi.ViewModels;
using System.Windows;
using System.Windows.Controls;

namespace CuoiKi.UI.Techlead
{
    /// <summary>
    /// Interaction logic for TechLeadProjectPage.xaml
    /// </summary>
    public partial class TechLeadProjectPage : Page
    {
        public TechLeadProjectPage()
        {
            InitializeComponent();
            this.DataContext = new TechLeadProjectViewModel();
        }
        private void ProjectButtonClick(object sender, RoutedEventArgs e)
        {
            var btn = sender as Button;
            btn!.Command.Execute(btn.CommandParameter);
            NavigationService.Navigate(new TechLeadStagePage());
        }
    }
}
