using CuoiKi.Enum;
using CuoiKi.HelperClasses;
using CuoiKi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;

namespace CuoiKi.ViewModels
{
    public class StaffTaskManagementViewModel : ViewModelBase
    {
        private List<Task> _taskList;
        public List<Task> TaskList
        {
            get => _taskList;
            set
            {
                _taskList = value;
                OnPropertyChanged(nameof(TaskList));
            }
        }
        private List<Task> _filteredTaskList;
        public List<Task> FilteredTaskList
        {
            get => _filteredTaskList;
            set
            {
                _filteredTaskList = value;
                OnPropertyChanged(nameof(FilteredTaskList));
            }
        }
        public StaffTaskManagementViewModel()
        {
            _taskList = new List<Task>();
            _filteredTaskList = new List<Task>();
            _filterList = new List<FilterName>();
            _taskFilterChain = new FilterChain<Task>();
            _filterOptions = new List<FilterName>() { FilterName.Done, FilterName.WIP, FilterName.NeedReview, FilterName.InThisYear, FilterName.InThisMonth };
            _seletedFilter = (FilterName)0;
            fetchTaskList();
            // At first, there is no filter
            FilteredTaskList = TaskList;
        }
        // Use some services to get tasks from database
        private void fetchTaskList()
        {
            _taskList.Clear();
            // query from database
            // here I add some fake data to test
            for (int i = 0; i < 10; i++)
            {
                Task temp = Task.CreateNewTask("Assignee" + i.ToString(), "Assigner" + i.ToString(), "This is task " + i.ToString(), "Task" + i.ToString(), DateTime.Now, DateTime.Now);
                _taskList.Add(temp);
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
            int index = _taskList.IndexOf(_taskList.FirstOrDefault(t => t.ID == _selectedTask?.ID));
            if (index == -1) return;

            // Create a new list with the updated task
            var updatedList = new List<Task>(_taskList);
            updatedList[index].Status = Constants.TaskStatus.NeedsReview;

            // Assign the new list to FakeTaskList
            FilteredTaskList = updatedList;
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
            int index = _taskList.IndexOf(_taskList.FirstOrDefault(t => t.ID == _selectedTask?.ID));
            if (index == -1) return;

            // Create a new list with the updated task
            var updatedList = new List<Task>(_taskList);
            updatedList[index].Status = Constants.TaskStatus.Done;

            // Assign the new list to FakeTaskList
            FilteredTaskList = updatedList;
        }



        private FilterChain<Task> _taskFilterChain;
        private FilterName? _seletedFilter;
        public FilterName? SelectedFilter
        {
            get { return _seletedFilter; }
            set
            {
                _seletedFilter = value;
                OnPropertyChanged(nameof(SelectedFilter));
            }
        }
        private List<FilterName> _filterList;
        public List<FilterName> FilterList
        {
            get => _filterList;
            set
            {
                _filterList = value;
                OnPropertyChanged(nameof(FilterList));
            }
        }
        private List<FilterName> _filterOptions;
        public List<FilterName> FilterOptions
        {
            get { return _filterOptions; }
            set
            {
                _filterOptions = value;
                OnPropertyChanged(nameof(FilterOptions));
            }
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
            FilterName currName = (FilterName)p;
            FilterList = _filterList.Where(x => x != currName).ToList();
            FilterOptions = _filterOptions.Concat(new[] { currName }).ToList();

            _taskFilterChain.RemovePredicate(currName);

            ApplyFilter();
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
            if (SelectedFilter is null) return;
            FilterName filterName = (FilterName)SelectedFilter;
            switch (filterName)
            {
                case FilterName.WIP:
                    _taskFilterChain.AddPredicate(filterName!, FilterLogicType.Or, p => p.Status == Constants.TaskStatus.WIP);
                    break;
                case FilterName.NeedReview:
                    _taskFilterChain.AddPredicate(filterName, FilterLogicType.Or, p => p.Status == Constants.TaskStatus.NeedsReview);
                    break;
                case FilterName.Done:
                    _taskFilterChain.AddPredicate(filterName, FilterLogicType.Or, p => p.Status == Constants.TaskStatus.Done);
                    break;
                case FilterName.InThisYear:
                    _taskFilterChain.AddPredicate(filterName, FilterLogicType.And, p =>
                        p.StartingTime.Year == DateTime.Now.Year
                        || p.EndingTime.Year == DateTime.Now.Year);
                    break;
                case FilterName.InThisMonth:
                    _taskFilterChain.AddPredicate(filterName, FilterLogicType.And, p =>
                        p.StartingTime.Month == DateTime.Now.Month
                        || p.EndingTime.Month == DateTime.Now.Month);
                    break;
                default:
                    break;
            }
            FilterList = _filterList.Concat(new[] { filterName }).ToList();
            FilterOptions = _filterOptions.Except(_filterList).ToList();
            SelectedFilter = FilterOptions.FirstOrDefault();

            ApplyFilter();
        }

        private void ApplyFilter()
        {
            bool FilterByStatus = FilterList.Any(f => f == FilterName.WIP || f == FilterName.NeedReview || f == FilterName.Done);
            bool FilterByTime = FilterList.Any(f => f == FilterName.InThisYear || f == FilterName.InThisMonth);
            IEnumerable<Task> filteredTasks = _taskList;
            if (_filterList.ToList().Count > 0)
            {
                if (FilterByStatus && FilterByTime)
                {
                    filteredTasks = _taskFilterChain.ApplyOrThenAnd(_taskList);
                }
                else if (FilterByStatus)
                {
                    filteredTasks = _taskFilterChain.ApplyOrLogic(_taskList);
                }
                else
                {
                    filteredTasks = _taskFilterChain.ApplyAndLogic(_taskList);
                }
            }
            FilteredTaskList = filteredTasks.ToList();
        }
    }
}
