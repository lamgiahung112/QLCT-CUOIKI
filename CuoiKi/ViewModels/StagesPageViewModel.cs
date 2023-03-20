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

            // 
            if (TaskAssignmentState.SelectedStage == null)
            {
                Title = "Add New Stage";
                StageID = "";
                _tobeSavedStageDescription = "";
                ShowID = Visibility.Collapsed;
            }
            else
            {
                Title = "Edit Stage";
                StageID = TaskAssignmentState.SelectedStage!.ID;
                _tobeSavedStageDescription = TaskAssignmentState.SelectedStage!.Description;
                ShowID = Visibility.Visible;
            }
            ProjectID = TaskAssignmentState.SelectedProject!.ID;
        }

        public Visibility ShowID { get; set; }
        public string StageID { get; set; }
        public string Title { get; set; }
        public string ProjectID { get; set; }


        #region Stage list binding
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
        #endregion

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

        private void SaveStageToDB()
        {
            if (ToBeSavedStageDescription.Length == 0) return;
            Stage sToSave;
            if (StageID.Length == 0)
            {
                sToSave = new Stage(ProjectID, ToBeSavedStageDescription);
            }
            else sToSave = new Stage(StageID, ProjectID, ToBeSavedStageDescription);
            _controller.Save(sToSave);
        }

        public void SaveStage()
        {
            // Save logic here
            //MessageBox.Show(ToBeSavedStageDescription);
            SaveStageToDB();
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
