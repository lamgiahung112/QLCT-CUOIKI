using CuoiKi.HelperClasses;
using CuoiKi.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;

namespace CuoiKi.ViewModels
{
    public class StaffTaskManagementViewModel : ViewModelBase
    {
        private ObservableCollection<Task> _fakeTaskList;
        public ObservableCollection<Task> FakeTaskList
        {
            get => _fakeTaskList;
            set
            {
                _fakeTaskList = value;
                OnPropertyChanged(nameof(FakeTaskList));
            }
        }
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

        public StaffTaskManagementViewModel()
        {
            _fakeTaskList = new ObservableCollection<Task>();
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
            // Find the index of the task to update
            int index = _fakeTaskList.IndexOf(_fakeTaskList.FirstOrDefault(t => t.ID == _selectedTask?.ID));
            if (index == -1) return;

            // Create a new list with the updated task
            var updatedList = new List<Task>(_fakeTaskList);
            updatedList[index].Status = Constants.TaskStatus.NeedsReview;

            // Assign the new list to FakeTaskList
            FakeTaskList = new ObservableCollection<Task>(updatedList);
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
            // Find the index of the task to update
            int index = _fakeTaskList.IndexOf(_fakeTaskList.FirstOrDefault(t => t.ID == _selectedTask?.ID));
            if (index == -1) return;

            // Create a new list with the updated task
            var updatedList = new List<Task>(_fakeTaskList);
            updatedList[index].Status = Constants.TaskStatus.Done;

            // Assign the new list to FakeTaskList
            FakeTaskList = new ObservableCollection<Task>(updatedList);
        }

    }
}
