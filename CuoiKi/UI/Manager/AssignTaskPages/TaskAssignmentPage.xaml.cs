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
using CuoiKi.UI.Forms;

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
            this.DataContext = this;
        }
        public class OnStage
        {
            private string _idStaff;
            private string _nameStaff;
            public OnStage(string idStaff, string nameStaff)
            {
                _idStaff = idStaff;
                _nameStaff = nameStaff;
            }
            public string StaffID
            {
                get { return _idStaff; }
                set { _idStaff = value; }
            }
            public string StaffName
            {
                get { return _nameStaff; }
                set { _nameStaff = value; }
            }
        }
        private ObservableCollection<OnStage> onStagelist = new ObservableCollection<OnStage>()
        {
            new OnStage("1","Nguyen Van A"),
            new OnStage("2","Nguyen Van B"),
            new OnStage("3","Nguyen Van c"),
            new OnStage("4","Nguyen Van d"),
            new OnStage("5","Nguyen Van e"),
            new OnStage("6","Nguyen Van f"),
            new OnStage("7","Nguyen Van g"),
            new OnStage("8","Nguyen Van h"),
            new OnStage("9","Nguyen Van i"),
            new OnStage("1","Nguyen Van A"),
            new OnStage("2","Nguyen Van B"),
            new OnStage("3","Nguyen Van c"),
            new OnStage("4","Nguyen Van d"),
            new OnStage("5","Nguyen Van e"),
            new OnStage("10","Nguyen Van j")
        };
        public ObservableCollection<OnStage> OnStagelist
        {
            get { return onStagelist; }
            set { onStagelist = value; }
        }

        private void back_click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack(); 
        }

        private void Right_click(object sender, MouseButtonEventArgs e)
        {
            //Point position = e.GetPosition(this);
            //MessageBox.Show("Mouse position: X=" + position.X + ", Y=" + position.Y);
            RightClickForm rightClickForm = new RightClickForm();
            rightClickForm.Left = e.GetPosition(this).X+440;
            rightClickForm.Top = e.GetPosition(this).Y+60;
            rightClickForm.Show();
        }
    }
}
