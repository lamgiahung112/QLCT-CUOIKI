using CuoiKi.ViewModels;
using System.Windows;
using System.Windows.Controls;

namespace CuoiKi.UI.Staff
{
    /// <summary>
    /// Interaction logic for InformationForm.xaml
    /// </summary>
    public partial class InformationForm : Page
    {
        private bool isEditMode = true;
        public InformationForm()
        {
            InitializeComponent();
            this.DataContext = new UserInformationViewModel();
        }
    }
}
