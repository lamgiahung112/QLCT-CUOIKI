using CuoiKi.UI.Staff;
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
using System.Windows.Shapes;

namespace CuoiKi.UI.Manager
{
    /// <summary>
    /// Interaction logic for UI_ManagerForm.xaml
    /// </summary>
    public partial class UI_ManagerForm : Window
    {
        public UI_ManagerForm()
        {
            InitializeComponent();
        }

        private void LogOut_click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void btn_information_click(object sender, RoutedEventArgs e)
        {
            frameContent.Navigate(new InformationManagerForm());
        }

        private void btn_CalcSalary_click(object sender, RoutedEventArgs e)
        {
            frameContent.Navigate(new CalcSalaryForm());
        }

        private void btn_AssignTask_click(object sender, RoutedEventArgs e)
        {
            frameContent.Navigate(new AssignTaskForm());
        }

        private void btn_KPI_click(object sender, RoutedEventArgs e)
        {
            frameContent.Navigate(new KPIForm());
        }
    }
}
