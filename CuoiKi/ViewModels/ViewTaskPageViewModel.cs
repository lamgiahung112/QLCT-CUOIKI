using CuoiKi.Models;
using CuoiKi.States;
using System;
using System.Collections.ObjectModel;

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
    }
}
