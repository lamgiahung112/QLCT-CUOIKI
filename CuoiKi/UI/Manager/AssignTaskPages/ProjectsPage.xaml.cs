using CuoiKi.UI.Forms;
using CuoiKi.ViewModels;
using System.Windows;
using System.Windows.Controls;

namespace CuoiKi.UI.Manager.AssignTaskPages
{
    /// <summary>
    /// Interaction logic for ProjectsForm.xaml
    /// </summary>
    public partial class ProjectsPage : Page
    {
        public ProjectsPage()
        {
            InitializeComponent();
            this.DataContext = new ProjectsPageViewModel();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            var projectEditorWindow = new ProjectForm(this.DataContext as ProjectsPageViewModel);
            projectEditorWindow.Show();
        }
    }
}
