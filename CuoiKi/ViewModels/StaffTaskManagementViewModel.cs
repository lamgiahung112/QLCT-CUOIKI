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
        private List<Task> _taskList;
        private FilterChain<Task> _taskFilterChain;
        private string _seletedFilter;
        public string SelectedFilter
        {
            get { return _seletedFilter; }
            set
            {
                _seletedFilter = value;
                OnPropertyChanged(nameof(SelectedFilter));
            }
        }
        private List<string> _filterOptions;
        public List<string> FilterOptions
        {
            get { return _filterOptions; }
            set
            {
                _filterOptions = value;
                OnPropertyChanged(nameof(FilterOptions));
            }
        }
        private List<string> _filterList;
        public List<string> FilterList
        {
            get => _filterList;
            set
            {
                _filterList = value;
                OnPropertyChanged(nameof(FilterList));
            }
        }
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
            _realTaskList = new List<Task>();
            _taskFilterChain = new FilterChain<Task>();
            _seletedFilter = "";
            _filterOptions = new List<string>() { "All", "Done", "WIP", "Need review", "In this year", "In this month", "In this week" };
            _filterList = new List<string>();
            FilterOptions.RemoveAll(x => FilterList.Contains(x));
            _fakeTaskList = new ObservableCollection<Task>();
            UpdateTaskList();
        }
        private void FilterTaskList()
        {
            List<Task> filteredTasks = _taskFilterChain.ApplyAllOrLogic(_realTaskList.ToList());
            FakeTaskList = new ObservableCollection<Task>(filteredTasks as List<Task>);
        }
        private void UpdateTaskList()
        {
            _realTaskList.Clear();
            // query from database
            // here I add some fake data to test
            for (int i = 0; i < 10; i++)
            {
                Task temp = Task.CreateNewTask("Assignee" + i.ToString(), "Assigner" + i.ToString(), "This is task " + i.ToString(), "Task" + i.ToString(), DateTime.Now, DateTime.Now);
                _realTaskList.Add(temp);
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
            int index = _realTaskList.IndexOf(_realTaskList.FirstOrDefault(t => t.ID == _selectedTask?.ID));
            if (index == -1) return;

            // Create a new list with the updated task
            var updatedList = new List<Task>(_realTaskList);
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
            int index = _realTaskList.IndexOf(_realTaskList.FirstOrDefault(t => t.ID == _selectedTask?.ID));
            if (index == -1) return;

            // Create a new list with the updated task
            var updatedList = new List<Task>(_realTaskList);
            updatedList[index].Status = Constants.TaskStatus.Done;

            // Assign the new list to FakeTaskList
            FakeTaskList = new ObservableCollection<Task>(updatedList);
        }
        private ICommand? _CmdRemoveFilterItem { get; set; }
        public ICommand CmdRemoveFilterItem
        {
            get
            {
                _CmdRemoveFilterItem ??= new RelayCommand(
                    p => true,
                    p => RemoveFilterItem(p));
                return _CmdRemoveFilterItem;
            }
        }
        private void RemoveFilterItem(object p)
        {
            string currStr = (string)p;
            FilterList = _filterList.Where(x => x != currStr).ToList();
            FilterOptions = _filterOptions.Concat(new[] { currStr }).ToList();
            _taskFilterChain.RemovePredicate(currStr);
            FilterTaskList();
        }
        private ICommand? _CmdAddFilter;
        public ICommand CmdAddFilter
        {
            get
            {
                _CmdAddFilter ??= new RelayCommand(
                    p => true,
                    p => addFilter());
                return _CmdAddFilter;
            }
        }
        private void addFilter()
        {
            FilterList = _filterList.Concat(new[] { SelectedFilter.ToString() }).ToList();
            if (SelectedFilter.ToString() != "All")
            {
                FilterList = _filterList.Where(x => x != "All").ToList();
                FilterOptions = _filterOptions.Concat(new[] { "All" }).ToList();
                _taskFilterChain.RemovePredicate("All");
            }
            else
            {
                for (int i = _filterList.Count - 1; i >= 0; i--)
                {
                    _taskFilterChain.RemovePredicate(_filterList[i]);
                    _filterOptions.Add(_filterList[i]);
                }
                _filterList = new List<string>() { "All" };
                FilterList = new List<string>() { "All" };
            }
            switch (SelectedFilter.ToString())
            {
                case "All":
                    _taskFilterChain.AddPredicate(SelectedFilter.ToString(), p => true);
                    break;
                case "WIP":
                    _taskFilterChain.AddPredicate(SelectedFilter.ToString(), p => p.Status == Constants.TaskStatus.WIP);
                    break;
                case "Need review":
                    _taskFilterChain.AddPredicate(SelectedFilter.ToString(), p => p.Status == Constants.TaskStatus.NeedsReview);
                    break;
                case "Done":
                    _taskFilterChain.AddPredicate(SelectedFilter.ToString(), p => p.Status == Constants.TaskStatus.Done);
                    break;
                default:
                    break;
            }
            FilterOptions = _filterOptions.Except(_filterList).ToList();
            SelectedFilter = FilterOptions.FirstOrDefault();
            FilterTaskList();
        }
    }
}
