using CuoiKi.Controllers;
using CuoiKi.HelperClasses;
using CuoiKi.Models;
using CuoiKi.States;
using CuoiKi.UI.Forms;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Input;

namespace CuoiKi.ViewModels
{
    public class TeamMembersPageViewModel : ViewModelBase
    {
        private KpiController _controller;
        private string _CurrentLeaderID = "";
        private string _CurrentLeaderName = "";
        private string _CurrentManagerName = "";
        private string _CurrentManagerID = "";

        public TeamMembersPageViewModel()
        {
            _controller = new KpiController();
            InitializeVariables();
        }

        private void InitializeVariables()
        {
            TeamMemberListInit();
            Employee? currentLeader = _controller.GetTechLeadOfTeam(TaskAssignmentState.SelectedTeam!);
            if (currentLeader is not null)
            {
                CurrentLeaderID = currentLeader.ID;
                CurrentLeaderName = currentLeader.Name;
            }
            _CurrentManagerName = LoginInfoState.Name!;
            _CurrentManagerID = LoginInfoState.Id!;
        }
        #region Team member list 
        void TeamMemberListInit()
        {
            _TeamMemberList = new ObservableCollection<Employee>();
            _TeamMemberList.Clear();
            MessageBox.Show(TaskAssignmentState.SelectedTeam.Name);
            List<TeamMember>? teamMembers = _controller.GetAllMembersOfTeam(TaskAssignmentState.SelectedTeam!);
            foreach (TeamMember member in teamMembers)
            {
                _TeamMemberList.Add(_controller.GetTeamMemberDetails(member));
            }
            // Temporary add some fake data to view some team members
            // Just curious, the app freeze a little when add a bunch of employee
            // I wonder is it because Bcrypt hash the password...
            _TeamMemberList.Add(new Employee("Nguyen Van A", "Ho Chi Minh", DateTime.Now, Constants.EmployeeStatus.Active, "123", Constants.Gender.Male, Constants.Role.Dev));
            /*
            _TeamMemberList.Add(new Employee("Nguyen Van B", "Ho Chi Minh", DateTime.Now, Constants.EmployeeStatus.Active, "123", Constants.Gender.Male, Constants.Role.Dev));
            _TeamMemberList.Add(new Employee("Nguyen Van C", "Ho Chi Minh", DateTime.Now, Constants.EmployeeStatus.Active, "123", Constants.Gender.Male, Constants.Role.Dev));
            _TeamMemberList.Add(new Employee("Nguyen Van D", "Ho Chi Minh", DateTime.Now, Constants.EmployeeStatus.Active, "123", Constants.Gender.Male, Constants.Role.Dev));
            _TeamMemberList.Add(new Employee("Nguyen Van E", "Ho Chi Minh", DateTime.Now, Constants.EmployeeStatus.Active, "123", Constants.Gender.Male, Constants.Role.Dev));
            _TeamMemberList.Add(new Employee("Nguyen Van F", "Ho Chi Minh", DateTime.Now, Constants.EmployeeStatus.Active, "123", Constants.Gender.Male, Constants.Role.Dev));
            _TeamMemberList.Add(new Employee("Nguyen Van G", "Ho Chi Minh", DateTime.Now, Constants.EmployeeStatus.Active, "123", Constants.Gender.Male, Constants.Role.Dev));
            _TeamMemberList.Add(new Employee("Nguyen Van H", "Ho Chi Minh", DateTime.Now, Constants.EmployeeStatus.Active, "123", Constants.Gender.Male, Constants.Role.Dev));
            _TeamMemberList.Add(new Employee("Nguyen Van I", "Ho Chi Minh", DateTime.Now, Constants.EmployeeStatus.Active, "123", Constants.Gender.Male, Constants.Role.Dev));
            _TeamMemberList.Add(new Employee("Nguyen Van J", "Ho Chi Minh", DateTime.Now, Constants.EmployeeStatus.Active, "123", Constants.Gender.Male, Constants.Role.Dev));
            _TeamMemberList.Add(new Employee("Nguyen Van K", "Ho Chi Minh", DateTime.Now, Constants.EmployeeStatus.Active, "123", Constants.Gender.Male, Constants.Role.Dev));
            _TeamMemberList.Add(new Employee("Nguyen Van L", "Ho Chi Minh", DateTime.Now, Constants.EmployeeStatus.Active, "123", Constants.Gender.Male, Constants.Role.Dev));
            */
        }
        private ObservableCollection<Employee> _TeamMemberList;
        public ObservableCollection<Employee> TeamMemberList
        {
            get { return _TeamMemberList; }
            set
            {
                _TeamMemberList = value;
                OnPropertyChanged(nameof(TeamMemberList));
            }
        }
        #endregion
        #region Assignee and assigner information property
        public string CurrentLeaderID
        {
            get { return _CurrentLeaderID; }
            set
            {
                _CurrentLeaderID = value;
                OnPropertyChanged(nameof(CurrentLeaderID));
            }
        }
        public string CurrentLeaderName
        {
            get { return _CurrentLeaderName; }
            set
            {
                _CurrentLeaderName = value;
                OnPropertyChanged(nameof(CurrentLeaderName));
            }
        }
        public string CurrentManagerName
        {
            get { return _CurrentManagerName; }
            set
            {
                _CurrentManagerName = value;
                OnPropertyChanged(nameof(CurrentManagerName));
            }
        }
        #endregion

        #region Open task assigment form command
        private ICommand? _CmdOpenTaskAssignmentForm;
        public ICommand CmdOpenTaskAssignmentForm
        {
            get
            {
                _CmdOpenTaskAssignmentForm ??= new RelayCommand(
                    p => true,
                    p => OpenTaskAssignmentForm());
                return _CmdOpenTaskAssignmentForm;
            }
        }
        private void OpenTaskAssignmentForm()
        {
            var taskAssignmentForm = new ManagerTaskAssignmentForm(this);
            taskAssignmentForm.Show();
        }
        #endregion
        #region Form input
        private string _ToBeSavedTaskTitle = "";
        private string _ToBeSavedTaskDescription = "";
        private DateTime? _ToBeSavedTaskStartingTime = DateTime.Now;
        private DateTime? _ToBeSavedTaskEndingTime = DateTime.Now;
        public string ToBeSavedTaskTitle
        {
            get { return _ToBeSavedTaskTitle; }
            set
            {
                _ToBeSavedTaskTitle = value;
                OnPropertyChanged(nameof(ToBeSavedTaskTitle));
                CheckValidTaskInput();
            }
        }
        public string ToBeSavedTaskDescription
        {
            get { return _ToBeSavedTaskDescription; }
            set
            {
                _ToBeSavedTaskDescription = value;
                OnPropertyChanged(nameof(ToBeSavedTaskDescription));
                CheckValidTaskInput();
            }
        }
        public DateTime ToBeSavedTaskStartingTime
        {
            get { return _ToBeSavedTaskStartingTime ?? DateTime.Now; }
            set
            {
                _ToBeSavedTaskStartingTime = value;
                OnPropertyChanged(nameof(ToBeSavedTaskStartingTime));
            }
        }
        public DateTime ToBeSavedTaskEndingTime
        {
            get { return _ToBeSavedTaskEndingTime ?? DateTime.Now; }
            set
            {
                _ToBeSavedTaskEndingTime = value;
                OnPropertyChanged(nameof(ToBeSavedTaskEndingTime));
            }
        }
        #endregion
        #region Save task command
        private ICommand? _CmdSaveTask;
        public ICommand CmdSaveTask
        {
            get
            {
                _CmdSaveTask ??= new RelayCommand(
                    p => _CanSaveTask,
                    p => SaveTask());
                return _CmdSaveTask;
            }
        }
        private bool _CanSaveTask = false;
        private void SaveTask()
        {
            /*
            MessageBox.Show("Save task");
            MessageBox.Show(ToBeSavedTaskTitle);
            MessageBox.Show(ToBeSavedTaskDescription);
            MessageBox.Show(ToBeSavedTaskStartingTime.ToString());
            MessageBox.Show(ToBeSavedTaskEndingTime.ToString());
            */
            Task task = Task.CreateNewTask(CurrentLeaderID, _CurrentManagerID, ToBeSavedTaskDescription, ToBeSavedTaskTitle, ToBeSavedTaskStartingTime, ToBeSavedTaskEndingTime); ;
            _controller.Save(task);
            Application.Current.Windows.OfType<Window>().FirstOrDefault(x => x.IsActive == true)!.Close();
        }
        private void CheckValidTaskInput()
        {
            _CanSaveTask = !string.IsNullOrEmpty(ToBeSavedTaskTitle);
            _CanSaveTask = !string.IsNullOrEmpty(ToBeSavedTaskDescription);
        }
        #endregion
    }
}