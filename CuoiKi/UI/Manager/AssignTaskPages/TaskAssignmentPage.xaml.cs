using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using CuoiKi.States;
using CuoiKi.UI.Forms;
using CuoiKi.ViewModels;

namespace CuoiKi.UI.Manager.AssignTaskPages
{
    /// <summary>
    /// Interaction logic for TaskAssignmentPage.xaml
    /// </summary>
    public partial class TaskAssignmentPage : Page
    {
        public TaskAssignmentPage()
        {
            InitializeComponent();
            this.DataContext = new TeamsPageViewModel();
        }

        private void back_click(object sender, RoutedEventArgs e)
        {
            TaskAssignmentState.SelectedStage = null;
            NavigationService.GoBack(); 
        }
    }
}
