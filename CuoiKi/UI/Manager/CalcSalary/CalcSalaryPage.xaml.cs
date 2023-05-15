using System.Windows.Controls;

namespace CuoiKi.UI.Manager.CalcSalary
{
    /// <summary>
    /// Interaction logic for CalcSalaryPage.xaml
    /// </summary>
    public partial class CalcSalaryPage : Page
    {
        public CalcSalaryPage()
        {
            InitializeComponent();
        }
        private void dteSelectedMonth_DisplayModeChanged(object sender, CalendarModeChangedEventArgs e)
        {
            dteSelectedMonth.DisplayMode = CalendarMode.Year;
        }
    }
}
