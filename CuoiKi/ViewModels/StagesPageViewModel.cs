using CuoiKi.Controllers;
using CuoiKi.HelperClasses;
using CuoiKi.Models;
using CuoiKi.States;
using CuoiKi.UI.Forms;
using CuoiKi.Wrappers;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Input;

namespace CuoiKi.ViewModels
{
    public class StagesPageViewModel : ViewModelBase
    {
        private readonly DbController _controller;
        public StagesPageViewModel()
        {
            _controller = new DbController();
            _stageList = _controller.GetStagesOfProject(TaskAssignmentState.SelectedProject!) ?? new List<Stage>();
            _stageWrappers = new List<StageWrapper>();
            StageWrappers = new List<StageWrapper>();
            Title = "Add New Stage";
            StageID = "";
            _tobeSavedStageDescription = "";
            ShowID = Visibility.Collapsed;
            ProjectID = TaskAssignmentState.SelectedProject!.ID;
            FetchStageList();
        }

        public Visibility ShowID { get; set; }
        public string StageID { get; set; }
        public string Title { get; set; }
        public string ProjectID { get; set; }


        #region Stage list binding
        private List<Stage>? _stageList;
        private List<StageWrapper>? _stageWrappers;
        public List<StageWrapper>? StageWrappers
        {
            get => _stageWrappers;
            set
            {
                _stageWrappers = value;
                OnPropertyChanged(nameof(StageWrappers));
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
        private ICommand? _saveStageToState;
        public ICommand CmdSaveStageToState
        {
            get
            {
                _saveStageToState ??= new RelayCommand(
                        obj => true,
                        obj => SaveStageToState(obj)
                    );
                return _saveStageToState;
            }
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

        private ICommand? _AddStageClickCommand;
        public ICommand AddStageClickCommand
        {
            get
            {
                _AddStageClickCommand ??= new RelayCommand(
                        p => true,
                        p => OpenAddStageForm()
                    );
                return _AddStageClickCommand;
            }
        }
        #endregion

        #region Utils
        private void SaveStageToState(object param)
        {
            if (param == null) return;
            string stageID = (param as string)!;
            TaskAssignmentState.SelectedStage = _stageList.Where(x => x.ID == stageID).FirstOrDefault();
            Title = "Edit Stage";
            StageID = TaskAssignmentState.SelectedStage!.ID;
            ToBeSavedStageDescription = TaskAssignmentState.SelectedStage!.Description;
            ShowID = Visibility.Visible;
        }

        private void FetchStageList()
        {
            _stageList = _controller.GetStagesOfProject(TaskAssignmentState.SelectedProject!) ?? new List<Stage>();
            _stageWrappers!.Clear();
            for (int i = 0; i < _stageList.Count; i++)
            {
                StageWrapper stageWrapper = new StageWrapper(_stageList[i]);
                List<Task>? tasks = _controller.GetAllTaskOfStage(stageWrapper.ID);
                int percentDone = 0;
                if (tasks is not null && tasks.Count != 0) percentDone = (tasks!.Where(t => t.Status == Constants.TaskStatus.Done).Count() * 100 / tasks.Count);
                stageWrapper.InitializeUI(percentDone);
                _stageWrappers.Add(stageWrapper);
            }
            StageWrappers = new List<StageWrapper>(_stageWrappers);
        }

        public void SaveStage()
        {
            SaveStageToDB();
            FetchStageList();
        }

        private void OpenAddStageForm()
        {
            TaskAssignmentState.SelectedStage = null;
            Title = "Add New Stage";
            StageID = "";
            _tobeSavedStageDescription = "";
            ShowID = Visibility.Collapsed;
            var addStageForm = new StageForm(this);
            addStageForm.Show();
        }

        private void CheckValidStageInput()
        {
            canSaveStage = !string.IsNullOrEmpty(ToBeSavedStageDescription);
        }

        #endregion

        #region Context menu command functions
        // Can execute variables
        private bool canViewStage = true;
        private bool canEditStage = true;
        private bool canDeleteStage = true;
        private void ViewStage()
        {
            TaskAssignmentState.SelectedStage = _stageList.Where(x => x.ID == StageID).FirstOrDefault();
        }
        private void EditStage()
        {
            var stgForm = new StageForm(this);
            stgForm.Show();
        }
        private void DeleteStage()
        {
            MessageBox.Show("Delete stage");
        }
        #endregion

        #region Context menu commands
        private ICommand? _cmdViewStage;
        public ICommand CmdViewStage
        {
            get
            {
                _cmdViewStage ??= new RelayCommand(
                    p => canViewStage,
                    p => ViewStage()
                );
                return _cmdViewStage;
            }
        }
        private ICommand? _cmdEditStage;
        public ICommand CmdEditStage
        {
            get
            {
                _cmdEditStage ??= new RelayCommand(
                    p => canEditStage,
                    p => EditStage());
                return _cmdEditStage;
            }
        }
        private ICommand? _cmdDeleteStage;
        public ICommand CmdDeleteStage
        {
            get
            {
                _cmdDeleteStage ??= new RelayCommand(
                    p => canDeleteStage,
                    p => DeleteStage());
                return _cmdDeleteStage;
            }
        }
        #endregion

    }
}
