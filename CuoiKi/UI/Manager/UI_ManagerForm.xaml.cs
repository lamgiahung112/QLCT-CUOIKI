using CuoiKi.UI.Manager.AssignTaskPages;
using System.Windows;
using System.Windows.Input;
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
            frameContent.Navigate(new ProjectsPage());
        }

        private void btn_KPI_click(object sender, RoutedEventArgs e)
        {
            frameContent.Navigate(new KPIForm());
        }
        public void Border_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
            {
                this.DragMove();
            }
        }

        public bool IsMaximized = false;
        public void Border_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ClickCount == 2)
            {
                if (IsMaximized)
                {
                    this.WindowState = WindowState.Normal;
                    this.Width = 1080;
                    this.Height = 720;

                    IsMaximized = false;
                }
                else
                {
                    this.WindowState = WindowState.Maximized;

                    IsMaximized = true;
                }
            }
        }

    }
}
