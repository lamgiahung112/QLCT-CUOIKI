using System.Windows;
using System.Windows.Input;

namespace CuoiKi.UI.HumanResources
{
    /// <summary>
    /// Interaction logic for UI_HrForm.xaml
    /// </summary>
    public partial class UI_HrForm : Window
    {
        public UI_HrForm()
        {
            InitializeComponent();
        }
        public void Border_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
            {
                this.DragMove();
            }
        }

        private void LogOut_click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void btn_CalcSalary_Click(object sender, RoutedEventArgs e)
        {
            frameContent.Navigate(new MemberList());
        }
    }
}
