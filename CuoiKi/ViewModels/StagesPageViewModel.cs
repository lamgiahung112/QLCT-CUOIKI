using CuoiKi.Controllers;
using CuoiKi.Models;
using CuoiKi.States;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace CuoiKi.ViewModels
{
    public class StagesPageViewModel : ViewModelBase
    {
        private readonly KpiController _controller;
        public StagesPageViewModel() 
        { 
            _controller = new KpiController();
            _stageList = _controller.GetStagesOfProject(TaskAssignmentState.SelectedProject!) ?? new List<Stage>();
        }
        private List<Stage> _stageList;
        public List<Stage> StageList
        {
            get { return _stageList; }
            set { 
                _stageList = value; 
                OnPropertyChanged(nameof(StageList)); 
            }
        }
    }
}
