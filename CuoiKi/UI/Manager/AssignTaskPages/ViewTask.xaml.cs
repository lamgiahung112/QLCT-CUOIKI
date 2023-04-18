using CuoiKi.ViewModels;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;

namespace CuoiKi.UI.Manager.AssignTaskPages
{
    /// <summary>
    /// Interaction logic for ViewTask.xaml
    /// </summary>
    public partial class ViewTask : Page
    {
        public ViewTask()
        {
            InitializeComponent();
            this.DataContext = new ViewTaskPageViewModel();
        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }
    }
}
