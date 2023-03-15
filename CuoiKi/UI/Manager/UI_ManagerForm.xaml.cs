using CuoiKi.Models;
using System.Windows;
using System.Windows.Input;
using CuoiKi.UI.Manager.AssignTaskPages;

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
        public void Border_MouseDownS(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
            {
                DragMove();
            }
        }
    }
       
}
