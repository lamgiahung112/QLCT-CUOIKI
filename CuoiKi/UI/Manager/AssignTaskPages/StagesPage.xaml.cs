using CuoiKi.UI.Forms;
using CuoiKi.ViewModels;
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
            InitializeComponent();
            DataContext = new StagesPageViewModel();
        }
        private void back_click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }
        private void BtnAddStage_Click(object sender, RoutedEventArgs e)
        {
            var stageEditorWindow = new StageForm(this.DataContext as StagesPageViewModel);
            stageEditorWindow.Show();
        }
    }
}
