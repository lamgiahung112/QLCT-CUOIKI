using CuoiKi.ViewModels;
using System.Windows;

namespace CuoiKi.UI.Forms
{
    /// <summary>
    /// Interaction logic for StageForm.xaml
    /// </summary>
    public partial class StageForm : Window
    {
        public StageForm(StagesPageViewModel spvm)
        {
            InitializeComponent();
            this.DataContext = spvm;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
