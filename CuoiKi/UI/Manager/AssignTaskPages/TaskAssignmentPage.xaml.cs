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
        public class Team
        {
            private string _idTeam;
            private string _nameTeam;
            public Team(string idTeam, string nameTeam)
            {
                _idTeam = idTeam;
                _nameTeam = nameTeam;
            }
            public string TeamID
            {
                get { return _idTeam; }
                set { _idTeam = value; }
            }
            public string TeamName
            {
                get { return _nameTeam; }
                set { _nameTeam = value; }
            }
        }
        private ObservableCollection<Team> teamlist = new ObservableCollection<Team>()
        {
            new Team("1", "Pham Nguyen Van Dao"),
            new Team("2", "BB"),
            new Team("3", "CC"),
            new Team("4", "DD"),
            new Team("5", "EE"),
            new Team("6", "FF"),
            new Team("7", "GG")
        };
        public ObservableCollection<Team> TeamList
        {
            get { return teamlist; }
            set { teamlist = value; }
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

        private void Team_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new AssignTaskPage());
        }
    }
}
