using CuoiKi.ViewModels;
using System.Windows;
using System.Windows.Controls;

namespace CuoiKi.UI.Manager.AssignTaskPages
{
    /// <summary>
    /// Interaction logic for ManagerViewInfomationPage.xaml
    /// </summary>
    public partial class ManagerViewInfomationPage : Page
    {
        public ManagerViewInfomationPage()
        {
            InitializeComponent();
            this.DataContext = new ManagerViewInfomationViewModel();
        }
        private void BackClick(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }
    }
}
