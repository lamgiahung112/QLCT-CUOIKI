using CuoiKi.ViewModels;
using System.Windows;
using System.Windows.Input;

namespace CuoiKi.UI.Staff
{

    public partial class UI_StaffForm : Window
    {
        public UI_StaffForm()
        {
            InitializeComponent();
            this.DataContext = new StaffFormViewModel();
        }

        public void Border_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
            {
                this.DragMove();
            }
        }

        //public bool IsMaximized = false;

        //public void Border_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        //{
        //    if (e.ClickCount == 2)
        //    {
        //        if (IsMaximized)
        //        {
        //            this.WindowState = WindowState.Normal;
        //            this.Width = 1080;
        //            this.Height = 720;

        //            IsMaximized = false;
        //        }
        //        else
        //        {
        //            this.WindowState = WindowState.Maximized;

        //            IsMaximized = true;
        //        }
        //    }
        //}

        private void btn_Information_click(object sender, RoutedEventArgs e)
        {
            frameContent.Navigate(new InformationForm());
        }

        private void btn_WorkSession_Click(object sender, RoutedEventArgs e)
        {
            frameContent.Navigate(new WorkSessionForm());
        }

        private void btn_LeaveOfAbsence_Click(object sender, RoutedEventArgs e)
        {
            frameContent.Navigate(new LeaveOfAbsenceForm());
        }

        private void LogOut_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void btn_ViewTask_Click(object sender, RoutedEventArgs e)
        {
            frameContent.Navigate(new StaffTaskManagementForm());
        }
    }
}
