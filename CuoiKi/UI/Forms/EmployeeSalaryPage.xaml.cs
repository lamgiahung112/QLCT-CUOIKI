using CuoiKi.Models;
using CuoiKi.ViewModels;
using System;
using System.Windows.Controls;

namespace CuoiKi.UI.Forms
{
    /// <summary>
    /// Interaction logic for EmployeeSalaryPage.xaml
    /// </summary>
    public partial class EmployeeSalaryPage : Page
    {
        public EmployeeSalaryPage(Employee e)
        {
            InitializeComponent();
            this.DataContext = new EmployeeSalaryPageViewModel(e);
        }

        private void dteSelectedMonth_DisplayModeChanged(object sender, CalendarModeChangedEventArgs e)
        {
            dteSelectedMonth.DisplayMode = CalendarMode.Year;
        }

        private void dteSelectedMonth_SelectedDatesChanged(object sender, SelectionChangedEventArgs e)
        {
            DateTime selectedDate = (DateTime)e.AddedItems[0];
            (DataContext as EmployeeSalaryPageViewModel).PickedDate = selectedDate;
        }
    }
}
