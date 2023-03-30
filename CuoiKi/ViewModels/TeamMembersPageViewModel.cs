using CuoiKi.Controllers;
using CuoiKi.Models;
using CuoiKi.States;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;

namespace CuoiKi.ViewModels
{
    public class TeamMembersPageViewModel : ViewModelBase
    {
        private KpiController _controller;
        private string _CurrentLeaderID = "";
        private string _CurrentLeaderName = "";
        public TeamMembersPageViewModel()
        {
            _controller = new KpiController();
            InitializeVariables();
        }

        private void InitializeVariables()
        {
            TeamMemberListInit();
            Employee? currentLeader = _controller.GetTechLeadOfTeam(TaskAssignmentState.SelectedTeam!);
            if (currentLeader is not null)
            {
                CurrentLeaderID = currentLeader.ID;
                CurrentLeaderName = currentLeader.Name;
            }

        }
        #region Team member list 
        void TeamMemberListInit()
        {
            _TeamMemberList = new ObservableCollection<Employee>();
            _TeamMemberList.Clear();
            MessageBox.Show(TaskAssignmentState.SelectedTeam.Name);
            List<TeamMember>? teamMembers = _controller.GetAllMembersOfTeam(TaskAssignmentState.SelectedTeam!);
            foreach (TeamMember member in teamMembers)
            {
                _TeamMemberList.Add(_controller.GetTeamMemberDetails(member));
            }
            // Temporary add some fake data to view some team members
            // Just curious, the app freeze a little when add a bunch of employee
            // I wonder is it because Bcrypt hash the password...
            _TeamMemberList.Add(new Employee("Nguyen Van A", "Ho Chi Minh", DateTime.Now, Constants.EmployeeStatus.Active, "123", Constants.Gender.Male, Constants.Role.Dev));
            _TeamMemberList.Add(new Employee("Nguyen Van B", "Ho Chi Minh", DateTime.Now, Constants.EmployeeStatus.Active, "123", Constants.Gender.Male, Constants.Role.Dev));
            _TeamMemberList.Add(new Employee("Nguyen Van C", "Ho Chi Minh", DateTime.Now, Constants.EmployeeStatus.Active, "123", Constants.Gender.Male, Constants.Role.Dev));
            _TeamMemberList.Add(new Employee("Nguyen Van D", "Ho Chi Minh", DateTime.Now, Constants.EmployeeStatus.Active, "123", Constants.Gender.Male, Constants.Role.Dev));
            _TeamMemberList.Add(new Employee("Nguyen Van E", "Ho Chi Minh", DateTime.Now, Constants.EmployeeStatus.Active, "123", Constants.Gender.Male, Constants.Role.Dev));
            _TeamMemberList.Add(new Employee("Nguyen Van F", "Ho Chi Minh", DateTime.Now, Constants.EmployeeStatus.Active, "123", Constants.Gender.Male, Constants.Role.Dev));
            _TeamMemberList.Add(new Employee("Nguyen Van G", "Ho Chi Minh", DateTime.Now, Constants.EmployeeStatus.Active, "123", Constants.Gender.Male, Constants.Role.Dev));
            _TeamMemberList.Add(new Employee("Nguyen Van H", "Ho Chi Minh", DateTime.Now, Constants.EmployeeStatus.Active, "123", Constants.Gender.Male, Constants.Role.Dev));
            _TeamMemberList.Add(new Employee("Nguyen Van I", "Ho Chi Minh", DateTime.Now, Constants.EmployeeStatus.Active, "123", Constants.Gender.Male, Constants.Role.Dev));
            _TeamMemberList.Add(new Employee("Nguyen Van J", "Ho Chi Minh", DateTime.Now, Constants.EmployeeStatus.Active, "123", Constants.Gender.Male, Constants.Role.Dev));
            _TeamMemberList.Add(new Employee("Nguyen Van K", "Ho Chi Minh", DateTime.Now, Constants.EmployeeStatus.Active, "123", Constants.Gender.Male, Constants.Role.Dev));
            _TeamMemberList.Add(new Employee("Nguyen Van L", "Ho Chi Minh", DateTime.Now, Constants.EmployeeStatus.Active, "123", Constants.Gender.Male, Constants.Role.Dev));
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
        #endregion
        #region Leader information property
        public string CurrentLeaderID
        {
            get { return _CurrentLeaderID; }
            set
            {
                _CurrentLeaderID = value;
                OnPropertyChanged(nameof(CurrentLeaderID));
            }
        }
        public string CurrentLeaderName
        {
            get { return _CurrentLeaderName; }
            set
            {
                _CurrentLeaderName = value;
                OnPropertyChanged(nameof(CurrentLeaderName));
            }
        }
        #endregion
    }
}