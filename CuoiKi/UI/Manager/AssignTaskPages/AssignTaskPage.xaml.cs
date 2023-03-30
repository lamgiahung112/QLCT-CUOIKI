using CuoiKi.UI.Forms;
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
using CuoiKi.States;

namespace CuoiKi.UI.Manager.AssignTaskPages
{
    /// <summary>
    /// Interaction logic for AssignTaskPage.xaml
    /// </summary>
    public partial class AssignTaskPage : Page
    {
        public AssignTaskPage()
        {
            InitializeComponent();
            this.DataContext = this;
        }
        public class OnStage
        {
            private string _staffid;
            private string _staffname;
            public OnStage(string staffid, string staffname)
            {
                _staffid = staffid;
                _staffname = staffname; 
            }
            public string StaffID
            {
                get { return _staffid; }
                set { _staffid = value; }
            }
            public string StaffName
            {
                get { return _staffname; }
                set { _staffname = value; }
            }
        }
        private ObservableCollection<OnStage> onstagelist = new ObservableCollection<OnStage>()
        {
            new OnStage("1","Nguyen van a"),
            new OnStage("2","Nguyen van b"),
            new OnStage("3","Nguyen van c"),
            new OnStage("4","Nguyen van d"),
            new OnStage("5","Nguyen van e"),
            new OnStage("6","Nguyen van f"),
            new OnStage("7","Nguyen van g"),
            new OnStage("8","Nguyen van h"),
            new OnStage("9","Nguyen van i"),
            new OnStage("10","Nguyen van j"),
            new OnStage("11","Nguyen van k"),
            new OnStage("12","Nguyen van l"),
            new OnStage("13","Nguyen van m")
        };
        public ObservableCollection<OnStage> OnStageList
        {
            get { return onstagelist; }
            set { onstagelist = value; }
        }
        private void Back_Click(object sender, RoutedEventArgs e)
        {
            TaskAssignmentState.SelectedTeam = null;
            NavigationService.GoBack(); 
        }

        private void MouseRight_Click(object sender, MouseButtonEventArgs e)
        {
                    RightClickForm rightClickForm = new RightClickForm();
                    rightClickForm.Left = e.GetPosition(this).X + 440;
                    rightClickForm.Top = e.GetPosition(this).Y + 60;
                    rightClickForm.Show();
            
        }
    }
}
