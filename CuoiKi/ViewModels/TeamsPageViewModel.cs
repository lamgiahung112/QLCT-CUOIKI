using CuoiKi.Controllers;
using CuoiKi.DAOs;
using CuoiKi.HelperClasses;
using CuoiKi.Models;
using CuoiKi.States;
using CuoiKi.UI.Forms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Input;

namespace CuoiKi.ViewModels
{
    public class TeamsPageViewModel : ViewModelBase
    {
        private KpiController _controller;
        private EmployeeDAO _employeeDAO;

        public TeamsPageViewModel()
        {
            StageID = TaskAssignmentState.SelectedStage!.ID;
            _controller = new KpiController();
            _employeeDAO = new EmployeeDAO();
            _Title = "";
            _TeamID = "";
            _ShowID = Visibility.Collapsed;
            _TechLead = null;
            _TeamName = "";
            _techLeadsFromDB = _employeeDAO.GetAllTechLeads() ?? new();
            FetchTeamList();
        }

        #region List Team Binding
        private List<Team> _teamList;
        public List<Team> TeamList
        {
            get { return _teamList; }
            set
            {
                _teamList = value;
                OnPropertyChanged(nameof(TeamList));
            }
        }

        private void FetchTeamList()
        {
            TeamList = _controller.GetTeamsOfStage(TaskAssignmentState.SelectedStage!) ?? new();
        }

        #endregion

        #region Add/Edit Team Command Binding
        private ICommand? _CmdAddTeam;
        public ICommand CmdAddTeam
        {
            get
            {
                _CmdAddTeam ??= new RelayCommand(
                        p => true,
                        p => OpenAddTeamForm()
                    );
                return _CmdAddTeam;
            }
        }

        private ICommand? _CmdEditTeam;
        public ICommand CmdEditTeam
        {
            get
            {
                _CmdEditTeam ??= new RelayCommand(
                        p => true,
                        p => OpenEditTeamForm()
                    );
                return _CmdEditTeam;
            }
        }

        private ICommand? _CmdSave;
        public ICommand CmdSave
        {
            get
            {
                _CmdSave ??= new RelayCommand(
                        p => TechLead?.ID.Length > 0 && TeamName.Length > 0,
                        p => SaveTeam()
                    );
                return _CmdSave;
            }
        }

        private void SaveTeam()
        {
            Team team = TaskAssignmentState.SelectedTeam ?? Team.CreateNewTeam(StageID, TechLead!.ID, TeamName);
            team.TechLeadID = TechLead!.ID;
            team.Name = TeamName;
            _controller.Save(team);
            FetchTeamList();
        }

        private void OpenEditTeamForm()
        {
            Title = "Edit Team";
            ShowID = Visibility.Visible;

            StageID = TaskAssignmentState.SelectedTeam!.StageID;
            TechLead = TechLeadsFromDB.Where(x => x.ID == TaskAssignmentState.SelectedTeam!.TechLeadID).First();
            TeamName = TaskAssignmentState.SelectedTeam!.Name;

            var f = new TeamForm(this);
            f.Show();
        }

        private void OpenAddTeamForm()
        {
            Title = "Add New Team";
            TeamID = "";
            ShowID = Visibility.Collapsed;
            StageID = TaskAssignmentState.SelectedStage!.ID;
            TechLead = null;
            TeamName = "";
            var f = new TeamForm(this);
            f.Show();
        }

        #endregion

        #region Team Form Bindings
        public string StageID { get; set; }
        private string _TeamID;
        public string TeamID
        {
            get { return _TeamID; }
            set
            {
                _TeamID = value;
                OnPropertyChanged(nameof(TeamID));
            }
        }

        private string _Title;
        public string Title
        {
            get { return _Title; }
            set
            {
                _Title = value;
                OnPropertyChanged(nameof(Title));
            }
        }

        private Visibility _ShowID;
        public Visibility ShowID
        {
            get { return _ShowID; }
            set
            {
                _ShowID = value;
                OnPropertyChanged(nameof(ShowID));
            }
        }

        private List<Employee> _techLeadsFromDB;
        public List<Employee> TechLeadsFromDB
        {
            get { return _techLeadsFromDB; }
            set
            {
                _techLeadsFromDB = value;
                OnPropertyChanged(nameof(TechLeadsFromDB));
            }
        }

        private Employee? _TechLead;
        public Employee? TechLead
        {
            get { return _TechLead; }
            set
            {
                _TechLead = value;
                OnPropertyChanged(nameof(TechLead));
            }
        }

        private string _TeamName;
        public string TeamName
        {
            get { return _TeamName; }
            set
            {
                _TeamName = value;
                OnPropertyChanged(nameof(TeamName));
            }
        }

        private ICommand? _CmdUpdateTechLead;
        public ICommand CmdUpdateTechLead
        {
            get
            {
                _CmdUpdateTechLead ??= new RelayCommand(
                        p => true,
                        p =>
                        {
                            TechLead = TechLeadsFromDB.Where(x => x.ID == p.ToString()!).First();
                        }
                    );
                return _CmdUpdateTechLead;
            }
        }

        #endregion

        #region Context Menu Bindings
        public event Action CmdSaveTeamToStateCompleted;
        private ICommand? _CmdSaveTeamToState;
        public ICommand CmdSaveTeamToState
        {
            get
            {
                _CmdSaveTeamToState ??= new RelayCommand(
                        p => true,
                        p => SaveTeamToState(p)
                    );
                return _CmdSaveTeamToState;
            }
        }

        private void SaveTeamToState(object param)
        {
            if (param is not string id)
            {
                return;
            }
            TaskAssignmentState.SelectedTeam = TeamList.Where(x => x.ID == id).FirstOrDefault();
            TeamID = id;
            CmdSaveTeamToStateCompleted?.Invoke();
        }
        #endregion
    }
}
