using CuoiKi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CuoiKi.States
{
    public static class TaskAssignmentState
    {
        public static Project? SelectedProject { get; set; }
        public static Stage? SelectedStage { get; set; }
        public static Team? SelectedTeam { get; set; }
    }
}
