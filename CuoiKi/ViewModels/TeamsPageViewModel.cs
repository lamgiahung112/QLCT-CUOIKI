using CuoiKi.Controllers;
using CuoiKi.DAOs;
using CuoiKi.HelperClasses;
using CuoiKi.Models;
using CuoiKi.States;
using CuoiKi.UI.Forms;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
            _controller = new KpiController();
            _employeeDAO = new EmployeeDAO();
            _Title = "";
            _ID = "";
            _ShowID = Visibility.Collapsed;
            _StageID = "";
            _TechLead = "";
            _TeamName = "";
            _teamList = _controller.GetTeamsOfStage(TaskAssignmentState.SelectedStage!) ?? new();
            _techLeadsFromDB = _employeeDAO.GetAllTechLeads() ?? new();
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
                        p => OpenEditTeamForm(p)
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
                        p => TechLead.Length > 0 && TeamName.Length > 0,
                        p => SaveTeam()
                    );
                return _CmdSave;
            }
        }

        private void SaveTeam()
        {
            Team team = Team.CreateNewTeam(StageID, TechLead, TeamName);
            _controller.Save(team);
        }

        private void OpenEditTeamForm(object cmdParam)
        {
            if (cmdParam == null) 
            { 
                return; 
            }

            string teamID = (cmdParam as string)!;
            Team? team = _teamList.Where(x => x.ID == teamID).FirstOrDefault();
            if (team == null) { return; }
            Title = "Edit Team";
            ShowID = Visibility.Visible;

            ID = team.ID;
            StageID = team.StageID;
            TechLead = team.TechLeadID;
            TeamName = team.Name;

            var f = new TeamForm(this);
            f.Show();
        }

        private void OpenAddTeamForm()
        {
            Title = "Add New Team";
            ID = "";
            ShowID = Visibility.Collapsed;
            StageID = TaskAssignmentState.SelectedStage!.ID;
            TechLead = "";
            TeamName = "";
            var f = new TeamForm(this);
            f.Show();
        }

        #endregion

        #region Team Form Bindings
        private string _ID;
        public string ID
        {
            get { return _ID; }
            set 
            { 
                _ID = value;
                OnPropertyChanged(nameof(ID));
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

        private string _StageID;
        public string StageID
        {
            get { return _StageID; }
            set
            {
                _StageID = value;
                OnPropertyChanged(nameof(StageID));
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

        private string _TechLead;
        public string TechLead
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

        #endregion

    }
}
