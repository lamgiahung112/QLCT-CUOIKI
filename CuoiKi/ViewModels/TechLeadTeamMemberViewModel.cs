using CuoiKi.Controllers;
using CuoiKi.DTO;
using CuoiKi.HelperClasses;
using CuoiKi.Models;
using CuoiKi.States;
using CuoiKi.UI.Forms;
using CuoiKi.Wrappers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Input;

namespace CuoiKi.ViewModels
{
    public class TechLeadTeamMemberViewModel : ViewModelBase
    {
        private readonly DbController _dbController;
        private List<Employee>? _employees;
        private List<EmployeeWrapper>? _employeeWrappers;
        private string? _currentTeamName;
        public string? CurrentTeamName
        {
            get => _currentTeamName;
            set
            {
                _currentTeamName = value;
                OnPropertyChanged(nameof(_currentTeamName));
            }
        }
        public List<EmployeeWrapper>? EmployeeWrappers
        {
            get => _employeeWrappers;
            set
            {
                _employeeWrappers = value;
                OnPropertyChanged(nameof(EmployeeWrappers));
            }
        }
        public TechLeadTeamMemberViewModel()
        {
            _dbController = new DbController();
            _employees = new List<Employee>();
            _employeeWrappers = new List<EmployeeWrapper>();
            _currentTeamName = _dbController.GetTeamName(TaskAssignmentState.SelectedTeam!.ID);
            _CurrentTechLeadName = LoginInfoState.Name;
            UpdateEmployees();
        }
        private void UpdateEmployees()
        {
            _employees!.Clear();
            _employeeWrappers!.Clear();
            List<TeamMember>? teamMembers = _dbController.GetAllMembersOfTeam(TaskAssignmentState.SelectedTeam!);
            foreach (TeamMember member in teamMembers)
            {
                _employees.Add(_dbController.GetTeamMemberDetails(member));
            }
            for (int i = 0; i < _employees!.Count; i++)
            {
                EmployeeWrapper employeeWrapper = new EmployeeWrapper(_employees[i]);
                List<Task>? teamTasks = _dbController.GetAllTaskOfTeam(TaskAssignmentState.SelectedTeam!.ID);
                List<Task>? employeeTasks = teamTasks.Where(task => task.Assignee == employeeWrapper.ID).ToList();
                int percentDone = 0;
                if (employeeTasks is not null && employeeTasks.Count != 0) percentDone = (employeeTasks!.Where(t => t.Status == Constants.TaskStatus.Done).Count() * 100 / employeeTasks.Count);
                employeeWrapper.InitializeUI(percentDone);
                _employeeWrappers.Add(employeeWrapper);
            }
            EmployeeWrappers = new List<EmployeeWrapper>(_employeeWrappers);
        }

        #region Assign member's task command
        private ICommand? _CmdAssignMemberTask;
        public ICommand CmdAssignMemberTask
        {
            get
            {
                _CmdAssignMemberTask ??= new RelayCommand(
                    p => true,
                    p => AssignTaskToMember(p));
                return _CmdAssignMemberTask;
            }
        }
        private void AssignTaskToMember(object p)
        {
            EmployeeWrapper ew = (EmployeeWrapper)p;
            Employee e = ew.Employee;
            TaskAssignmentState.SelectedEmployee = e;
            CurrentEmployeeName = e.Name;
            var taskForm = new TechLeadTaskAssignmentForm(this);
            taskForm.Show();
        }
        #endregion

        #region View member's task
        private ICommand? _CmdViewMemberTask;
        public ICommand CmdViewMemberTask
        {
            get
            {
                _CmdViewMemberTask ??= new RelayCommand(
                    p => true,
                    p => ViewMemberTask());
                return _CmdViewMemberTask;
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
            EmployeeWrapper ew = (EmployeeWrapper)p;
            Employee e = ew.Employee;
            TaskAssignmentState.SelectedEmployee = e;
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

        #region Assignee and assigner information property
        private string _CurrentEmployeeName;
        private string _CurrentTechLeadName;
        public string CurrentEmployeeName
        {
            get
            {
                return _CurrentEmployeeName;
            }
            set
            {
                _CurrentEmployeeName = value;
                OnPropertyChanged(nameof(CurrentEmployeeName));
            }
        }
        public string CurrentTechLeadName
        {
            get { return _CurrentTechLeadName; }
            set
            {
                _CurrentTechLeadName = value;
                OnPropertyChanged(nameof(CurrentTechLeadName));
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
            Task task = Task.CreateNewTask(TaskAssignmentState.SelectedEmployee!.ID, LoginInfoState.Id!, TaskAssignmentState.SelectedTeam!.ID, ToBeSavedTaskDescription, ToBeSavedTaskTitle, ToBeSavedTaskStartingTime, ToBeSavedTaskEndingTime); ;
            _dbController.Save(task);
            Application.Current.Windows.OfType<Window>().FirstOrDefault(x => x.IsActive == true)!.Close();
        }
        private void CheckValidTaskInput()
        {
            _CanSaveTask = !string.IsNullOrEmpty(ToBeSavedTaskTitle);
            _CanSaveTask = !string.IsNullOrEmpty(ToBeSavedTaskDescription);
        }
        #endregion

        #region Save current employee to state
        private ICommand? _CmdSaveEmployeeToCurrentState;
        public ICommand CmdSaveEmployeeToCurrentState
        {
            get
            {
                _CmdSaveEmployeeToCurrentState ??= new RelayCommand(
                    p => true,
                    p => SaveEmployeeToCurrentState(p));
                return _CmdSaveEmployeeToCurrentState;
            }
        }
        private void SaveEmployeeToCurrentState(object parameter)
        {
            EmployeeWrapper ew = (EmployeeWrapper)parameter;
            Employee e = ew.Employee;
            TaskAssignmentState.SelectedEmployee = e;
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
            var teamMemberManagementForm = new TechLeadTeamMembersForm(this);
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
            List<TeamMember>? teamMembers = _dbController.GetAllMembersOfTeam(TaskAssignmentState.SelectedTeam!);
            List<Employee>? allWorkers = _dbController.GetAllWorkers();
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
            List<TeamMember>? currentTeamMembers = _dbController.GetAllMembersOfTeam(TaskAssignmentState.SelectedTeam!);
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
            _dbController.AddTeamMembersToTeam(TaskAssignmentState.SelectedTeam!.ID, newMemberIDs);
            _dbController.RemoveTeamMembers(toBeRemovedTeamMemberIDs);
            UpdateWorkerList();
            UpdateEmployees();
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

        private ICommand? _reloadCommand;
        public ICommand ReloadCommand
        {
            get
            {
                _reloadCommand ??= new RelayCommand(
                    p => true,
                    p => Reload());
                return _reloadCommand;
            }
        }

        private void Reload()
        {
            UpdateEmployees();
        }
    }
}
