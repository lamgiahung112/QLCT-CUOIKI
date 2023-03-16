using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;

namespace CuoiKi.UI.Manager
{
    /// <summary>
    /// Interaction logic for AssignTaskForm.xaml
    /// </summary>
    public partial class AssignTaskForm : Page
    {
        // This code for testing while waiting for view model
        public class Project
        {
            private string _id;
            private string _name;
            public Project(string id, string name)
            {
                _id = id;
                _name = name;
            }
            public string ID
            {
                get { return _id; }
                set { _id = value; }
            }
            public string Name
            {
                get { return _name; }
                set { _name = value; }
            }
        }
        private ObservableCollection<Project> testList = new ObservableCollection<Project>()
        {
            new Project("GD01", "Gamedev Project"),
            new Project("DA02", "Desktop App Project"),
            new Project("AA03", "Android App Project"),
            new Project("W04", "Web Project"),
            new Project("AI05", "AI Project"),
            new Project("E06", "Embedded Project"),
            new Project("S07", "Security Project"),
            new Project("WPF08", "WPF Project"),
            new Project("IOS09", "IOS App Project")
        };
        public ObservableCollection<Project> TestList
        {
            get { return testList; }
            set
            {
                testList = value;

            }
        }

        public AssignTaskForm()
        {
            InitializeComponent();
            this.DataContext = this;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
