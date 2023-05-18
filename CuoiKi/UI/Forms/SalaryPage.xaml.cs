using CuoiKi.ViewModels;
using System;
using System.Windows.Controls;

namespace CuoiKi.UI.Forms
{
    /// <summary>
    /// Interaction logic for SalaryPage.xaml
    /// </summary>
    public partial class SalaryPage : Page
    {
        public SalaryPage()
        {
            InitializeComponent();
            DataContext = new SalaryPageViewModel();
        }
        private void dteSelectedMonth_DisplayModeChanged(object sender, CalendarModeChangedEventArgs e)
        {
            dteSelectedMonth.DisplayMode = CalendarMode.Year;
        }

        private void dteSelectedMonth_SelectedDatesChanged(object sender, SelectionChangedEventArgs e)
        {
            DateTime selectedDate = (DateTime)e.AddedItems[0];
            (DataContext as SalaryPageViewModel).PickedDate = selectedDate;
        }
    }
}
