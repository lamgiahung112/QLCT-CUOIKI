using CuoiKi.Controllers;
using CuoiKi.DAOs;
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
    public class TeamsPageViewModel : ViewModelBase
    {
        private DbController _controller;
        private EmployeeDAO _employeeDAO;

        public TeamsPageViewModel()
        {
            StageID = TaskAssignmentState.SelectedStage!.ID;
            _controller = new DbController();
            _employeeDAO = new EmployeeDAO();
            _Title = "";
            _TeamID = "";
            _ShowID = Visibility.Collapsed;
            _TechLead = null;
            _TeamName = "";
            _techLeadsFromDB = _employeeDAO.GetAllTechLeads() ?? new();
            _teamList = new List<Team>();
            _teamWrappers = new List<TeamWrapper>();
            TeamWrappers = new List<TeamWrapper>();
            FetchTeamList();
        }

        #region List Team Binding
        private List<Team>? _teamList;
        private List<TeamWrapper>? _teamWrappers;
        public List<TeamWrapper>? TeamWrappers
        {
            get => _teamWrappers;
            set
            {
                _teamWrappers = value;
                OnPropertyChanged(nameof(TeamWrappers));
            }
        }
        private void FetchTeamList()
        {
            _teamList = _controller.GetTeamsOfStage(TaskAssignmentState.SelectedStage!) ?? new();
            _teamWrappers!.Clear();
            for (int i = 0; i < _teamList.Count; i++)
            {
                TeamWrapper teamWrapper = new TeamWrapper(_teamList[i]);
                List<Task>? tasks = _controller.GetAllTaskOfTeam(teamWrapper.ID);
                int percentDone = 0;
                if (tasks is not null && tasks.Count != 0) percentDone = (tasks!.Where(t => t.Status == Constants.TaskStatus.Done).Count() * 100 / tasks.Count);
                teamWrapper.InitializeUI(percentDone);
                _teamWrappers.Add(teamWrapper);
            }
            TeamWrappers = new List<TeamWrapper>(_teamWrappers);
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
            TaskAssignmentState.SelectedTeam = _teamList.Where(x => x.ID == id).FirstOrDefault();
            TeamID = id;
        }

        private ICommand? _cmdDeleteTeam;
        public ICommand CmdDeleteTeam
        {
            get
            {
                _cmdDeleteTeam ??= new RelayCommand(
                    p => true,
                    p => DeleteTeam());
                return _cmdDeleteTeam;
            }
        }

        private void DeleteTeam()
        {
            _controller.Delete(TaskAssignmentState.SelectedTeam!);
            FetchTeamList();
        }
        #endregion

        private ICommand? _reloadCommand;
        public ICommand ReloadCommand
        {
            get
            {
                _reloadCommand ??= new RelayCommand(
                    p => true,
                    p => Reload());
                return _reloadCommand;
            }
        }

        private void Reload()
        {
            FetchTeamList();
        }
    }
}
