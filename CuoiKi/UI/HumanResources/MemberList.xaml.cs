using CuoiKi.Models;
using CuoiKi.UI.Forms;
using CuoiKi.ViewModels;
using System.Windows;
using System.Windows.Controls;

namespace CuoiKi.UI.HumanResources
{
    /// <summary>
    /// Interaction logic for MemberList.xaml
    /// </summary>
    public partial class MemberList : Page
    {
        public MemberList()
        {
            InitializeComponent();
            this.DataContext = new SalaryOfMember();
        }

        private void ViewSalaryClick(object sender, RoutedEventArgs e)
        {
            var btn = sender as Button;
            NavigationService.Navigate(new EmployeeSalaryPage(btn.CommandParameter as Employee));
        }
    }
}
