using CuoiKi.Controllers;
using CuoiKi.Models;
using CuoiKi.States;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;

namespace CuoiKi.ViewModels
{
    public class TeamMembersPageViewModel : ViewModelBase
    {
        private KpiController _controller;

        public TeamMembersPageViewModel()
        {
            _controller = new KpiController();
            InitializeVariables();
        }

        private void InitializeVariables()
        {
            TeamMemberListInit();
        }
        void TeamMemberListInit()
        {
            MessageBox.Show(TaskAssignmentState.SelectedTeam.Name);
            List<TeamMember>? teamMembers = _controller.GetAllMembersOfTeam(TaskAssignmentState.SelectedTeam!);
            foreach (TeamMember member in teamMembers)
            {
                TeamMemberList.Add(_controller.GetTeamMemberDetails(member));
            }
        }
        private ObservableCollection<Employee> _TeamMemberList;
        public ObservableCollection<Employee> TeamMemberList
        {
            get { return _TeamMemberList; }
            set
            {
                _TeamMemberList = value;
                OnPropertyChanged(nameof(TeamMemberList));
            }
        }
    }
}