﻿using CuoiKi.Controllers;
using CuoiKi.HelperClasses;
using CuoiKi.Models;
using CuoiKi.States;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;

namespace CuoiKi.ViewModels
{
    public class TechLeadViewTaskViewModel : ViewModelBase
    {
        private readonly DbController _dbController;
        private string? _employeeID;
        private string? _employeeName;
        private Task? _selectedTask;
        public Task? SelectedTask
        {
            get => _selectedTask;
            set
            {
                _selectedTask = value;
                OnPropertyChanged(nameof(SelectedTask));
            }
        }
        private List<Task>? _assignedTasks;
        public List<Task>? AssignedTasks
        {
            get => _assignedTasks;
            set
            {
                _assignedTasks = value;
                OnPropertyChanged(nameof(AssignedTasks));
            }
        }
        public string? EmployeeID
        {
            get => _employeeID;
            set
            {
                _employeeID = value;
                OnPropertyChanged(nameof(EmployeeID));
            }
        }
        public string? EmployeeName
        {
            get => _employeeName;
            set
            {
                _employeeName = value;
                OnPropertyChanged(nameof(EmployeeName));
            }
        }

        public TechLeadViewTaskViewModel()
        {
            _dbController = new DbController();
            EmployeeName = TaskAssignmentState.SelectedEmployee!.Name;
            EmployeeID = TaskAssignmentState.SelectedEmployee!.ID;
            _assignedTasks = new List<Task>();
            UpdateAssignedTasks();
        }
        private void UpdateAssignedTasks()
        {
            List<Task>? currentTeamTasks = _dbController.GetAllTaskOfTeam(TaskAssignmentState.SelectedTeam!.ID);
            _assignedTasks = currentTeamTasks.Where(task => task.Assignee == EmployeeID).ToList();
            AssignedTasks = new List<Task>(_assignedTasks);
        }
        private ICommand? _CmdEdit { get; set; }
        public ICommand CmdEdit
        {
            get
            {
                _CmdEdit ??= new RelayCommand(
                    p => true,
                    p => EditTask());
                return _CmdEdit;
            }
        }
        private void EditTask()
        {

        }

        private ICommand? _CmdDone { get; set; }
        public ICommand CmdDone
        {
            get
            {
                _CmdDone ??= new RelayCommand(
                    p => true,
                    p => MarkAsDone());
                return _CmdDone;
            }
        }
        private void MarkAsDone()
        {
            SelectedTask!.Status = Constants.TaskStatus.Done;
            _dbController.Save(SelectedTask);
            UpdateAssignedTasks();
        }
    }
}
