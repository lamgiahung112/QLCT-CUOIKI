using CuoiKi.Controllers;
using CuoiKi.HelperClasses;
using CuoiKi.Models;
using CuoiKi.States;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace CuoiKi.ViewModels.Forms
{
    public class StageFormViewModel : ViewModelBase
    {
        private readonly KpiController _controller;
        public StageFormViewModel()
        {
            _controller = new KpiController();
            if (TaskAssignmentState.SelectedStage == null)
            {
                Title = "Add New Stage";
                StageID = "";
                _description = "";
                showID = Visibility.Collapsed;
            }
            else
            {
                Title = "Edit Stage";
                StageID = TaskAssignmentState.SelectedStage!.ID;
                _description = TaskAssignmentState.SelectedStage!.Description;
                showID = Visibility.Visible;
            }
            ProjectID = TaskAssignmentState.SelectedProject!.ID;
        }

        public Visibility showID { get; set; }
        public string StageID { get; set; }
        public string Title { get; set; }
        public string ProjectID { get; set; }

        private string _description;
        public string Description
        {
            get { return _description; }
            set 
            { 
                _description = value;
                OnPropertyChanged(nameof(Description));
                validateInput();
            }
        }

        private bool canSave = false;
        private ICommand? _saveCommand;
        public ICommand SaveCommand
        {
            get 
            {
                _saveCommand ??= new RelayCommand(
                        obj => this.canSave,
                        obj => SaveStageToDB()
                    );
                return _saveCommand; 
            }
        }

        private ICommand? _closeCommand;
        public ICommand CloseCommand
        {
            get
            {
                _closeCommand ??= new RelayCommand(
                        obj => true,
                        obj => close()
                    );
                return _closeCommand;
            }
        }
        private void SaveStageToDB()
        {
            if (Description.Length == 0) return;
            Stage sToSave;
            if (StageID.Length == 0)
            {
                sToSave = new Stage(ProjectID, Description);
            }
            else sToSave = new Stage(StageID, ProjectID, Description);
            _controller.Save(sToSave);
            close();
        }

        private void close()
        {
            var w = Application.Current.Windows.OfType<Window>().SingleOrDefault(w => w.IsActive);
            if (w != null)
            {
                w.Close();
            }
        }

        private void validateInput()
        {
            canSave = !string.IsNullOrEmpty( Description ); 
        }
    }
}
