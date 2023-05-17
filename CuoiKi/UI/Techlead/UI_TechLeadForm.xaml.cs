using System.Windows;
using System.Windows.Input;

namespace CuoiKi.UI.Techlead
{
    /// <summary>
    /// Interaction logic for UI_TechLeadForm.xaml
    /// </summary>
    public partial class UI_TechLeadForm : Window
    {
        public UI_TechLeadForm()
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
        private void MyProjectsClick(object sender, RoutedEventArgs e)
        {
            frameContent.Navigate(new TechLeadProjectPage());
        }
        private void LogOut_click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }
    }
}
