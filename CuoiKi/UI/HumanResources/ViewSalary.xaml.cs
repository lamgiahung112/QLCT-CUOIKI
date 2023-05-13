using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;

namespace CuoiKi.UI.HumanResources
{
    /// <summary>
    /// Interaction logic for ViewSalary.xaml
    /// </summary>
    public partial class ViewSalary : Page
    {
        public ViewSalary(SalaryOfMember som)
        {
            InitializeComponent();
            this.DataContext = som;
        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }

        private void dteSelectedMonth_DisplayModeChanged(object sender, CalendarModeChangedEventArgs e)
        {
            dteSelectedMonth.DisplayMode = CalendarMode.Year;
        }
    }
}
