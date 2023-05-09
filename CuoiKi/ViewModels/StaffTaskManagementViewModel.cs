using CuoiKi.Controllers;
using CuoiKi.Enum;
using CuoiKi.HelperClasses;
using CuoiKi.Models;
using CuoiKi.States;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;

namespace CuoiKi.ViewModels
{
    public class StaffTaskManagementViewModel : ViewModelBase
    {
        private DbController _dbController;
        private List<Task>? _taskList;
        public List<Task>? TaskList
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
            _dbController = new DbController();
            _taskList = new List<Task>();
            _filteredTaskList = new List<Task>();
            _filterList = new List<FilterName>();
            _taskFilterChain = new FilterChain<Task>();
            _filterOptions = new List<FilterName>() { FilterName.Done, FilterName.WIP, FilterName.NeedReview, FilterName.InThisYear, FilterName.InThisMonth, FilterName.InThisDay };
            _seletedFilter = (FilterName)0;
            fetchTaskList();
            // At first, there is no filter
            FilteredTaskList = TaskList;
        }
        // Use some services to get tasks from database
        private void fetchTaskList()
        {
            _taskList!.Clear();
            _taskList = _dbController.GetAllTaskOfEmployee(LoginInfoState.Id!);
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

            // Get the task to update
            Task taskToUpdate = _taskList[index];

            // Update the task's status
            taskToUpdate.Status = Constants.TaskStatus.NeedsReview;

            // Save the updated task using the _dbController
            _dbController.Save(taskToUpdate);

            fetchTaskList();

            // Create a new list with the updated task
            var updatedList = new List<Task>(_taskList);
            updatedList[index] = taskToUpdate;

            // Assign the new list to FilteredTaskList
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

            // Get the task to update
            Task taskToUpdate = _taskList[index];

            // Update the task's status
            taskToUpdate.Status = Constants.TaskStatus.Done;

            // Save the updated task using the _dbController
            _dbController.Save(taskToUpdate);

            fetchTaskList();

            // Create a new list with the updated task
            var updatedList = new List<Task>(_taskList);
            updatedList[index] = taskToUpdate;

            // Assign the new list to FilteredTaskList
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
                    RemoveTimeFilter();
                    _taskFilterChain.AddPredicate(filterName, FilterLogicType.And, p =>
                        p.StartingTime.Year == DateTime.Now.Year
                        || p.EndingTime.Year == DateTime.Now.Year);
                    break;
                case FilterName.InThisMonth:
                    RemoveTimeFilter();
                    _taskFilterChain.AddPredicate(filterName, FilterLogicType.And, p =>
                        p.StartingTime.Month == DateTime.Now.Month
                        || p.EndingTime.Month == DateTime.Now.Month);
                    break;
                case FilterName.InThisDay:
                    RemoveTimeFilter();
                    _taskFilterChain.AddPredicate(filterName, FilterLogicType.And, p =>
                        p.StartingTime.Date == DateTime.Now.Date
                        || p.EndingTime.Date == DateTime.Now.Date);
                    break;
                default:
                    break;
            }
            FilterList = _filterList.Concat(new[] { filterName }).ToList();
            FilterOptions = _filterOptions.Except(_filterList).ToList();
            SelectedFilter = FilterOptions.FirstOrDefault();

            ApplyFilter();
        }
        private void RemoveTimeFilter()
        {
            _taskFilterChain.RemovePredicate(FilterName.InThisYear);
            _taskFilterChain.RemovePredicate(FilterName.InThisMonth);
            _taskFilterChain.RemovePredicate(FilterName.InThisDay);
            _filterList.RemoveAll(filter => filter == FilterName.InThisYear || filter == FilterName.InThisMonth || filter == FilterName.InThisDay);
            FilterList = _filterList;
            if (!FilterOptions.Contains(FilterName.InThisYear))
            {
                FilterOptions.Add(FilterName.InThisYear);
            }

            if (!FilterOptions.Contains(FilterName.InThisMonth))
            {
                FilterOptions.Add(FilterName.InThisMonth);
            }

            if (!FilterOptions.Contains(FilterName.InThisDay))
            {
                FilterOptions.Add(FilterName.InThisDay);
            }

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
