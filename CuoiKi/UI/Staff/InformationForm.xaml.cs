using CuoiKi.Constants;
using CuoiKi.ViewModels;
using MaterialDesignThemes.Wpf;
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
using System.Windows.Shapes;

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
        
        private void Edit_Click(object sender, RoutedEventArgs e)
        {
            if (isEditMode)
            {
                txt_Address.IsReadOnly = false;
                //txt_Birth.IsReadOnly = false;
                cbb_Gender.IsEnabled = true;
                txt_Name.IsReadOnly = false;
                cbb_Role.IsEnabled = true;
                isEditMode = false;
                btn_Edit_Or_Save.Content = "Save";
            }
            else
            {
                txt_Address.IsReadOnly = true;
                //txt_Birth.IsReadOnly = true;
                cbb_Gender.IsReadOnly = false;
                txt_Name.IsReadOnly = true;
                cbb_Role.IsEnabled = false;
/*                string address = txt_Address.Text;
                string birth = txt_Birth.Text;
                string name = txt_Name.Text;
                string role = cbb_Role.Text;
                string status = txt_Status.Text;
                string gender = txt_Gender.Text;*/
                btn_Edit_Or_Save.Content = "Edit";
                isEditMode = true;
            }
            
    }
    }
}
