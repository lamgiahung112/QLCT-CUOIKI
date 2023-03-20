using CuoiKi.States;
using CuoiKi.UI.Forms;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;

namespace CuoiKi.UI.Manager.AssignTaskPages
{
    /// <summary>
    /// Interaction logic for StagesPage.xaml
    /// </summary>
    public partial class StagesPage : Page
    {
        public StagesPage()
        {
            if (TaskAssignmentState.getInstance().SelectedProject!= null)
            {
                MessageBox.Show("OK");
            }
            InitializeComponent();
        }
        private void back_click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }
        private void BtnAddStage_Click(object sender, RoutedEventArgs e)
        {
            var stageEditorWindow = new StageForm();
            stageEditorWindow.Show();
        }
    }
}
