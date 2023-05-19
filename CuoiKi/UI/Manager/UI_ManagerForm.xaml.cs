﻿using CuoiKi.UI.Forms;
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
            frameContent.Navigate(new UI.Staff.InformationForm());
        }

        private void btn_CalcSalary_click(object sender, RoutedEventArgs e)
        {
            frameContent.Navigate(new SalaryPage());
        }

        private void btn_AssignTask_click(object sender, RoutedEventArgs e)
        {
            frameContent.Navigate(new ProjectsPage());
        }
        public void Border_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
            {
                this.DragMove();
            }
        }
    }
}
