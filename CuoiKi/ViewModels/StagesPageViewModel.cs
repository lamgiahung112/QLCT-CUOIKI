using CuoiKi.Controllers;
using CuoiKi.HelperClasses;
using CuoiKi.Models;
using CuoiKi.States;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Input;

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
            set
            {
                _stageList = value;
                OnPropertyChanged(nameof(StageList));
            }
        }

        #region Save stage logic
        private string _tobeSavedStageDescription = "";
        public string ToBeSavedStageDescription
        {
            get { return _tobeSavedStageDescription; }
            set
            {
                _tobeSavedStageDescription = value;
                OnPropertyChanged(nameof(ToBeSavedStageDescription));
                CheckValidStageInput();
            }
        }

        public void SaveStage()
        {
            // Save logic here
            MessageBox.Show(ToBeSavedStageDescription);
        }

        private bool canSaveStage = false;
        private ICommand? _saveStageCommand;
        public ICommand SaveStageCommand
        {
            get
            {
                _saveStageCommand ??= new RelayCommand(
                    p => this.canSaveStage,
                    p => this.SaveStage());
                return _saveStageCommand;
            }
        }

        private void CheckValidStageInput()
        {
            canSaveStage = !string.IsNullOrEmpty(ToBeSavedStageDescription);
        }

        #endregion
    }
}
