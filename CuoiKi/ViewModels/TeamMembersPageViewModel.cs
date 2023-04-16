using CuoiKi.Controllers;
using CuoiKi.DTO;
using CuoiKi.HelperClasses;
using CuoiKi.Models;
using CuoiKi.States;
using CuoiKi.UI.Forms;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
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
        private string? _CurrentTeamName = "";
        public string? CurrentTeamName
        {
            get => _CurrentTeamName;
            set
            {
                _CurrentTeamName = value;
                OnPropertyChanged(nameof(CurrentTeamName));
            }
        }

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
            _CurrentTeamName = _controller.GetTeamName(TaskAssignmentState.SelectedTeam!.ID);
        }
        #region Team member list 
        private void TeamMemberListInit()
        {
            _TeamMemberList = new ObservableCollection<Employee>();
            UpdateTeamMemberList();
        }
        private void UpdateTeamMemberList()
        {
            _TeamMemberList.Clear();
            List<TeamMember>? teamMembers = _controller.GetAllMembersOfTeam(TaskAssignmentState.SelectedTeam!);
            foreach (TeamMember member in teamMembers)
            {
                _TeamMemberList.Add(_controller.GetTeamMemberDetails(member));
            }
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

        #region Assign member's task command
        private ICommand? _CmdAssignMemberTask;
        public ICommand CmdAssignMemberTask
        {
            get
            {
                _CmdAssignMemberTask ??= new RelayCommand(
                    p => true,
                    p => AssignTaskToMember());
                return _CmdAssignMemberTask;
            }
        }
        private void AssignTaskToMember()
        {

        }
        #endregion

        #region View member's task
        private ICommand? _CmdViewMemberTask;
        public ICommand CmdViewMemberTask
        {
            get
            {
                _CmdAssignMemberTask ??= new RelayCommand(
                    p => true,
                    p => ViewMemberTask());
                return _CmdAssignMemberTask;
            }
        }
        private void ViewMemberTask()
        {

        }
        #endregion

        #region View member's information
        private ICommand? _CmdViewMemberInformation;
        public ICommand CmdViewMemberInformation
        {
            get
            {
                _CmdViewMemberInformation ??= new RelayCommand(
                    p => true,
                    p => ViewMemberInformation(p));
                return _CmdViewMemberInformation;
            }
        }
        private void ViewMemberInformation(object p)
        {
            Employee e = (Employee)p;
            MessageBox.Show("View member's information" + e.ID.ToString());
        }
        #endregion

        #region Open team member management form
        private ICommand? _CmdOpenTeamMemberManagementForm;
        public ICommand CmdOpenTeamMemberManagementForm
        {
            get
            {
                _CmdOpenTeamMemberManagementForm ??= new RelayCommand(
                    p => true,
                    p => OpenTeamMemberManagementForm());
                return _CmdOpenTeamMemberManagementForm;
            }
        }
        private void OpenTeamMemberManagementForm()
        {
            UpdateWorkerList();
            var teamMemberManagementForm = new ManageTeamMembersForm(this);
            teamMemberManagementForm.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            teamMemberManagementForm.Show();
        }
        #endregion

        #region Team member management logic
        private List<WorkerDTO>? _workerList;
        public List<WorkerDTO>? WorkerList
        {
            get => _workerList;
            set
            {
                _workerList = value;
                OnPropertyChanged(nameof(WorkerList));
            }
        }

        private void UpdateWorkerList()
        {
            List<TeamMember>? teamMembers = _controller.GetAllMembersOfTeam(TaskAssignmentState.SelectedTeam!);
            List<Employee>? allWorkers = _controller.GetAllWorkers();
            _workerList = allWorkers.Select(x => new WorkerDTO()
            {
                Name = x.Name,
                EmployeeID = x.ID,
                EmployeeRole = x.Role.ToString(),
                IsSelected = teamMembers.Any(tm => tm.EmployeeID == x.ID)
            })
            .ToList();
        }
        #endregion
        #region Save changes command
        private ICommand? _CmdSaveChanges;
        public ICommand CmdSaveChanges
        {
            get
            {
                _CmdSaveChanges ??= new RelayCommand(
                    p => true,
                    p => SaveTeamMemberChanges());
                return _CmdSaveChanges;
            }
        }
        private void SaveTeamMemberChanges()
        {
            List<TeamMember>? currentTeamMembers = _controller.GetAllMembersOfTeam(TaskAssignmentState.SelectedTeam!);
            // Get the IDs of selected workers who are not already team members
            List<string> newMemberIDs = _workerList
                .Where(w => w.IsSelected && !currentTeamMembers.Any(m => m.EmployeeID == w.EmployeeID))
                .Select(w => w.EmployeeID)
                .ToList();
            // Get the IDs of deselected workers who are already team members
            List<string> toBeRemovedTeamMemberIDs = _workerList
                .Where(w => !w.IsSelected && currentTeamMembers.Any(m => m.EmployeeID == w.EmployeeID))
                .Select(w => currentTeamMembers.First(m => m.EmployeeID == w.EmployeeID).ID)
                .ToList();
            _controller.AddTeamMembersToTeam(TaskAssignmentState.SelectedTeam!.ID, newMemberIDs);
            _controller.RemoveTeamMembers(toBeRemovedTeamMemberIDs);
            UpdateWorkerList();
            UpdateTeamMemberList();
            Application.Current.Windows.OfType<Window>().FirstOrDefault(x => x.IsActive == true)!.Close();
        }
        #endregion

        #region Cancel command
        private ICommand? _CmdCancelTeamManagementForm;
        public ICommand CmdCancelTeamMenagementForm
        {
            get
            {
                _CmdCancelTeamManagementForm ??= new RelayCommand(
                    p => true,
                    p => CancelTeamManagementForm());
                return _CmdCancelTeamManagementForm;
            }
        }
        private void CancelTeamManagementForm()
        {
            Application.Current.Windows.OfType<Window>().FirstOrDefault(x => x.IsActive == true)!.Close();

        }
        #endregion
    }
}