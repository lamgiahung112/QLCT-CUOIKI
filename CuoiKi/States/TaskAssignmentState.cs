using CuoiKi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CuoiKi.States
{
    public class TaskAssignmentState
    {
        private static TaskAssignmentState? _instance;
        
        public static TaskAssignmentState getInstance()
        {
            _instance ??= new TaskAssignmentState();
            return _instance;
        }

        private TaskAssignmentState() 
        {
            SelectedProject = null;
            SelectedStage = null;
        }

        public Project? SelectedProject;
        public Stage? SelectedStage;
    }
}
