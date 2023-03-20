using CuoiKi.Controllers;
using CuoiKi.HelperClasses;
using CuoiKi.Models;
using CuoiKi.States;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;

namespace CuoiKi.ViewModels
{
    public class ProjectsPageViewModel : ViewModelBase
    {
        private readonly KpiController kpiController;
        private ObservableCollection<Project> _projectList;
        private string _currentManagerID = "";
        public ProjectsPageViewModel()
        {
            kpiController = new KpiController();
            _currentManagerID = LoginInfoState.Id!;
            _projectList = new ObservableCollection<Project>();
            fetchProjectListToObservableCollection();
        }
        private void fetchProjectListToObservableCollection()
        {
            _projectList.Clear();
            List<Project>? projects = kpiController.GetProjectsOfCurrentAccount();
            if (projects is null) return;
            foreach (var project in projects)
            {
                _projectList.Add(project);
            }
        }
        public ObservableCollection<Project> ProjectList
        {
            get { return _projectList; }
            set
            {
                _projectList = value;
                OnPropertyChanged(nameof(ProjectList));
            }
        }
        public string CurrentManagerID
        {
            get => _currentManagerID;
            set
            {
                _currentManagerID = value;
                OnPropertyChanged(nameof(CurrentManagerID));
            }
        }

        #region Save project logic
        private string _toBeSavedProjectName = "";
        public string ToBeSavedProjectName
        {
            get { return _toBeSavedProjectName; }
            set
            {
                _toBeSavedProjectName = value;
                OnPropertyChanged(nameof(ToBeSavedProjectName));
                CheckValidProjectInput();
            }
        }
        private string _toBeSavedProjectDescription = "";
        public string ToBeSavedProjectDescription
        {
            get { return _toBeSavedProjectDescription; }
            set
            {
                _toBeSavedProjectDescription = value;
                OnPropertyChanged(nameof(ToBeSavedProjectDescription));
                CheckValidProjectInput();
            }
        }
        public void SaveProject()
        {
            Project project = Project.CreateNewProject(ToBeSavedProjectName, ToBeSavedProjectDescription);
            kpiController.Save(project);
            fetchProjectListToObservableCollection();
        }

        private bool canSaveProject = false;
        private ICommand? _saveProjectCommand;
        public ICommand SaveProjectCommand
        {
            get
            {
                _saveProjectCommand ??= new RelayCommand(
                    p => this.canSaveProject,
                    p => this.SaveProject());
                return _saveProjectCommand;
            }
        }
        public void CheckValidProjectInput()
        {
            canSaveProject = !string.IsNullOrEmpty(ToBeSavedProjectName);
            canSaveProject = !string.IsNullOrEmpty(ToBeSavedProjectDescription);
            // TODO: check if project name existed...
        }
        #endregion

        #region Project click logic
        private bool canProjectItemClick = true;
        private ICommand? _projectItemClickCommand;
        public ICommand ProjectItemClickCommand
        {
            get
            {
                _projectItemClickCommand ??= new RelayCommand(
                        obj => canProjectItemClick,
                        obj => SaveProjectIdToState(obj)
                    );
                return _projectItemClickCommand;
            }
        }

        private void SaveProjectIdToState(object parameter)
        {
            if (parameter == null) { return; }
            var projectId = parameter as string;
            TaskAssignmentState.SelectedProject = _projectList.Where(x => x.ID == projectId).ElementAt(0);
        }
        #endregion

    }
}
