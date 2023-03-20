using CuoiKi.ViewModels;
using System.Windows;

namespace CuoiKi.UI.Forms
{
    /// <summary>
    /// Interaction logic for ProjectForm.xaml
    /// </summary>
    public partial class ProjectForm : Window
    {
        public ProjectForm(ProjectsPageViewModel ppvm)
        {
            InitializeComponent();
            this.DataContext = ppvm;
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
