using CuoiKi.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

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
