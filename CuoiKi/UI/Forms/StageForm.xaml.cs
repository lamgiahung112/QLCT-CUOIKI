using CuoiKi.Models;
using CuoiKi.ViewModels.Forms;
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
    /// Interaction logic for StageForm.xaml
    /// </summary>
    public partial class StageForm : Window
    {
        public StageForm()
        {
            DataContext = new StageFormViewModel();
            InitializeComponent();
        }
    }
}
