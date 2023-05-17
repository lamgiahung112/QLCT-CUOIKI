using CuoiKi.ViewModels;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;

namespace CuoiKi.UI.Techlead
{
    /// <summary>
    /// Interaction logic for TechLeadViewTask.xaml
    /// </summary>
    public partial class TechLeadViewTask : Page
    {
        public TechLeadViewTask()
        {
            InitializeComponent();
            this.DataContext = new TechLeadViewTaskViewModel();
        }
        private void Back_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }
    }
}
