using CuoiKi.Controllers;
using CuoiKi.HelperClasses;
using CuoiKi.Models;
using CuoiKi.States;
using CuoiKi.UI.Forms;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Input;

namespace CuoiKi.ViewModels
{
    public class ProjectsPageViewModel : ViewModelBase
    {
        private readonly KpiController kpiController;
        private ObservableCollection<Project> _projectList;
        private string _projectID = "";
        private Visibility _VisID = Visibility.Collapsed;
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
        public string ProjectID
        {
            get { return _projectID; }
            set
            {
                _projectID = value;
                if (value.Length > 0)
                {
                    _VisID = Visibility.Visible;
                }
                else _VisID = Visibility.Collapsed;
                OnPropertyChanged(nameof(ProjectID));
            }
        }
        public Visibility VisID
        {
            get { return _VisID; }
            set { _VisID = value; OnPropertyChanged(nameof(VisID)); }
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
            Project project = TaskAssignmentState.SelectedProject ?? Project.CreateNewProject(ToBeSavedProjectName, ToBeSavedProjectDescription);
            project.Name = ToBeSavedProjectName;
            project.Description = ToBeSavedProjectDescription;
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
            ToBeSavedProjectDescription = TaskAssignmentState.SelectedProject.Description;
            ToBeSavedProjectName = TaskAssignmentState.SelectedProject.Name;
            ProjectID = TaskAssignmentState.SelectedProject.ID;
        }
        #endregion

        #region Add Project Click logic
        private ICommand? _AddProjectClickCommand;

        public ICommand AddProjectClickCommand
        {
            get
            {
                _AddProjectClickCommand ??= new RelayCommand(
                        p => true,
                        p => OpenAddProjectForm()
                    );
                return _AddProjectClickCommand;
            }
        }

        private void OpenAddProjectForm()
        {
            TaskAssignmentState.SelectedProject = null;
            ToBeSavedProjectName = "";
            ToBeSavedProjectDescription = "";
            ProjectID = "";
            var prjForm = new ProjectForm(this);
            prjForm.Show();
        }
        #endregion

        #region Context menu command functions
        // Can execute variables
        private bool canViewProject = true;
        private bool canEditProject = true;
        private bool canDeleteProject = true;
        private void ViewProject()
        {
            TaskAssignmentState.SelectedProject = ProjectList.Where(x => x.ID == ProjectID).ElementAt(0);
        }
        private void EditProject()
        {
            var prjForm = new ProjectForm(this); 
            prjForm.Show();
        }
        private void DeleteProject()
        {
            MessageBox.Show("Delete project");
        }
        #endregion

        #region Context menu commands
        private ICommand? _cmdViewProject;
        public ICommand CmdViewProject
        {
            get
            {
                _cmdViewProject ??= new RelayCommand(
                    p => canViewProject,
                    p => ViewProject()
                );
                return _cmdViewProject;
            }
        }
        private ICommand? _cmdEditProject;
        public ICommand CmdEditProject
        {
            get
            {
                _cmdEditProject ??= new RelayCommand(
                    p => canEditProject,
                    p => EditProject());
                return _cmdEditProject;
            }
        }
        private ICommand? _cmdDeleteProject;
        public ICommand CmdDeleteProject
        {
            get
            {
                _cmdDeleteProject ??= new RelayCommand(
                    p => canDeleteProject,
                    p => DeleteProject());
                return _cmdDeleteProject;
            }
        }
        #endregion
    }
}
