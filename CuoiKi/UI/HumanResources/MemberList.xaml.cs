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

        private void btn_ViewSalary_Click(object sender, RoutedEventArgs e)
        {
            //NavigationService.Navigate(new ViewSalary());
        }
    }
}
