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

namespace CuoiKi.UI.Manager.AssignTaskPages
{
    /// <summary>
    /// Interaction logic for ProjectPage.xaml
    /// </summary>
    public partial class ProjectPage : Page
    {
        public ProjectPage()
        {
            InitializeComponent();
            this.DataContext = this;
        }
        public class Stage
        {
            private string _stageName;
            private string _leaderName;
            public Stage(string stageName, string leaderName)
            {
                _stageName= stageName;
                _leaderName= leaderName;   
            }
            public string StageName
            {
                get { return _stageName;}
                set { _stageName = value; }
            }
            public string LeaderName
            {
                get { return _leaderName;}  
                set { _leaderName = value; }
            }
        }
        private ObservableCollection<Stage> stagelist = new ObservableCollection<Stage>()
        {
            new Stage("St01","Le Van Chi"),
            new Stage("st02","Vo Van Ngan"),
            new Stage("st03","Dang Van Bi"),
            new Stage("st04","Mai Chi Tho"),
            new Stage("st05","Tran Nao"),
            new Stage("st06","Thao Dien")
        };
        public ObservableCollection<Stage> StageList
        {
            get { return stagelist; }
            set { stagelist = value; }
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
