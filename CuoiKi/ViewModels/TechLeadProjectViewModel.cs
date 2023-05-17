using CuoiKi.Controllers;
using CuoiKi.HelperClasses;
using CuoiKi.Models;
using CuoiKi.States;
using CuoiKi.Wrappers;

using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;

namespace CuoiKi.ViewModels
{
    public class TechLeadProjectViewModel : ViewModelBase
    {
        private readonly DbController _dbController;
        private List<Project>? _projects;
        private List<ProjectWrapper>? _projectWrappers;
        public List<ProjectWrapper>? ProjectWrappers
        {
            get => _projectWrappers;
            set
            {
                _projectWrappers = value;
                OnPropertyChanged(nameof(ProjectWrappers));
            }
        }

        public TechLeadProjectViewModel()
        {
            _dbController = new DbController();
            _projects = new List<Project>();
            _projectWrappers = new List<ProjectWrapper>();
            UpdateProjects();
        }
        private void UpdateProjects()
        {
            _projects!.Clear();
            _projectWrappers!.Clear();
            _projects = _dbController.GetAllProjectsOfTechLead(LoginInfoState.Id!);
            if (_projects is null) return;
            for (int i = 0; i < _projects!.Count; i++)
            {
                ProjectWrapper projectWrapper = new ProjectWrapper(_projects[i]);
                List<Task>? tasks = _dbController.GetAllTaskOfProject(projectWrapper.ID);
                int percentDone = 0;
                if (tasks is not null && tasks.Count != 0) percentDone = (tasks!.Where(t => t.Status == Constants.TaskStatus.Done).Count() * 100 / tasks.Count);
                projectWrapper.InitializeUI(percentDone);
                _projectWrappers.Add(projectWrapper);
            }
            ProjectWrappers = new List<ProjectWrapper>(_projectWrappers);
        }
        private ICommand? _cmdProjectItem;
        public ICommand ProjectItemClickCommand
        {
            get
            {
                _cmdProjectItem ??= new RelayCommand(
                p => true,
                p => ProjectItemClick(p));
                return _cmdProjectItem;
            }
        }
        private void ProjectItemClick(object p)
        {
            if (p is null) return;
            string projectID = p as string;
            TaskAssignmentState.SelectedProject = _projects.Where(x => x.ID == projectID).ElementAt(0);
        }

    }
}
