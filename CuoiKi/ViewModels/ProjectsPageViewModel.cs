using CuoiKi.Controllers;
using CuoiKi.Models;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace CuoiKi.ViewModels
{
    class ProjectsPageViewModel : ViewModelBase
    {
        private readonly KpiController kpiController;
        private ObservableCollection<Project> _projectList;
        public ProjectsPageViewModel()
        {
            kpiController = new KpiController();
            _projectList = new ObservableCollection<Project>();
            fetchProjectListToObservableCollection();
        }
        private void fetchProjectListToObservableCollection()
        {
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
    }
}
