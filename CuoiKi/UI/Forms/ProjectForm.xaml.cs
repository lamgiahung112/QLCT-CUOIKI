using CuoiKi.ViewModels;
using System.Windows;

namespace CuoiKi.UI.Forms
{
    /// <summary>
    /// Interaction logic for ProjectForm.xaml
    /// </summary>
    public partial class ProjectForm : Window
    {
        public ProjectForm(ProjectsPageViewModel pgvm)
        {
            InitializeComponent();
            this.DataContext = pgvm;
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
