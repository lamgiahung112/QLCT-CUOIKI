﻿using CuoiKi.Controllers;
using CuoiKi.HelperClasses;
using CuoiKi.Models;
using CuoiKi.States;
using CuoiKi.UI.Manager.AssignTaskPages;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using System.Windows;
using System.Windows.Navigation;

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
            _currentManagerID = LoginInfoState.getInstance().Id;
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
        public void SaveNewProject()
        {
            Project project = Project.CreateNewProject(ToBeSavedProjectName, ToBeSavedProjectDescription);
            kpiController.Save(project);
            fetchProjectListToObservableCollection();
        }

        private bool canSaveNewProject = false;
        private ICommand? _saveNewProjectCommand;
        public ICommand SaveNewProjectCommand
        {
            get
            {
                _saveNewProjectCommand ??= new RelayCommand(
                    p => this.canSaveNewProject,
                    p => this.SaveNewProject());
                return _saveNewProjectCommand;
            }
        }
        public void CheckValidProjectInput()
        {
            canSaveNewProject = !string.IsNullOrEmpty(ToBeSavedProjectName);
            canSaveNewProject = !string.IsNullOrEmpty(ToBeSavedProjectDescription);
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
            //TaskAssignmentState.getInstance().SelectedProject = _projectList.Where(x => x.ID == projectId).ElementAt(0);
            MessageBox.Show(projectId);
        }
        #endregion

    }
}
