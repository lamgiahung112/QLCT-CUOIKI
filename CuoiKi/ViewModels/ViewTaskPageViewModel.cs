using CuoiKi.HelperClasses;
using CuoiKi.Models;
using CuoiKi.States;
using System;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace CuoiKi.ViewModels
{
    public class ViewTaskPageViewModel : ViewModelBase
    {
        private string employeeName = string.Empty;
        private string employeeID = string.Empty;
        private ObservableCollection<Task> _fakeTaskList = new ObservableCollection<Task>();
        public ObservableCollection<Task> FakeTaskList
        {
            get { return _fakeTaskList; }
            set
            {
                _fakeTaskList = value;
                OnPropertyChanged(nameof(FakeTaskList));
            }
        }
        public string EmployeeName
        {
            get => employeeName;
            set
            {
                employeeName = value;
                OnPropertyChanged(nameof(EmployeeName));
            }
        }
        public string EmployeeID
        {
            get => employeeID;
            set
            {
                employeeID = value;
                OnPropertyChanged(nameof(EmployeeID));
            }
        }
        public ViewTaskPageViewModel()
        {
            EmployeeName = TaskAssignmentState.SelectedEmployee.Name;
            EmployeeID = TaskAssignmentState.SelectedEmployee.ID;
            UpdateTaskList();
        }

        private void UpdateTaskList()
        {
            FakeTaskList.Clear();
            // query from database
            // here I add some fake data to test
            for (int i = 0; i < 10; i++)
            {
                Task temp = Task.CreateNewTask("Assignee" + i.ToString(), "Assigner" + i.ToString(), "This is task " + i.ToString(), "Task" + i.ToString(), DateTime.Now, DateTime.Now);
                FakeTaskList.Add(temp);
            }
        }
        private ICommand? _CmdDelete { get; set; }
        public ICommand CmdDelete
        {
            get
            {
                _CmdDelete ??= new RelayCommand(
                    p => true,
                    p => DeleteTask());
                return _CmdDelete;
            }
        }
        private void DeleteTask()
        {

        }
        private ICommand? _CmdReviewRequest { get; set; }
        public ICommand CmdReviewRequest
        {
            get
            {
                _CmdReviewRequest ??= new RelayCommand(
                    p => true,
                    p => ReviewRequest());
                return _CmdReviewRequest;
            }
        }
        private void ReviewRequest()
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

        }

    }
}
