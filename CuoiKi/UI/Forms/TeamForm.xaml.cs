using CuoiKi.ViewModels;
using System;
using System.Collections.Generic;
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

namespace CuoiKi.UI.Forms
{
    /// <summary>
    /// Interaction logic for TeamForm.xaml
    /// </summary>
    public partial class TeamForm : Window
    {
        public TeamForm(TeamsPageViewModel vm)
        {
            InitializeComponent();
            DataContext = vm;
        }

        private void BtnCancel_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void BtnSave_Click(object sender, RoutedEventArgs e)
        {
            if (sender is Button btn)
            {
                btn.Command.Execute(btn.CommandParameter);
            }
            Close();
        }

    }
}
