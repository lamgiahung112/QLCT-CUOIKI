using CuoiKi.States;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace CuoiKi.ViewModels.Forms
{
    public class StageFormViewModel : ViewModelBase
    {
        public StageFormViewModel()
        {
            if (TaskAssignmentState.getInstance().SelectedStage == null)
            {
                Title = "Add New Stage";
                StageID = "";
                _description = "";
                showID = Visibility.Collapsed;
            }
            else
            {
                Title = "Edit Stage";
                StageID = TaskAssignmentState.getInstance().SelectedStage!.ID;
                _description = TaskAssignmentState.getInstance().SelectedStage!.Description;
                showID = Visibility.Visible;
            }
            ProjectID = TaskAssignmentState.getInstance().SelectedProject!.ID;
        }
        public Visibility showID;
        public string StageID;
        public string Title;
        public string ProjectID;

        private string _description;
        public string Description
        {
            get { return _description; }
            set { _description = value;  OnPropertyChanged(nameof(Description)); }
        }
    }
}
