using CuoiKi.Constants;
using CuoiKi.DAOs;
using CuoiKi.Models;
using CuoiKi.States;
using System.Collections.Generic;

namespace CuoiKi.ViewModels
{
    public class UserInformationViewModel : ViewModelBase
    {
        private readonly EmployeeDAO _employeeDAO;
        public string UserId { get; set; }
        public string UserName { get; set; }
        public string UserAddress { get; set; }
        public string UserBirth { get; set; }
        public string UserRole { get; set; }
        public string UserGender { get; set; }
        public string UserStatus { get; set; }
        public UserInformationViewModel()
        {
            _employeeDAO = new EmployeeDAO();
            Employee e = _employeeDAO.GetOne(LoginInfoState.Id!)!;
            UserId = e.ID;
            UserName = e.Name;
            UserAddress = e.Address;
            UserBirth = e.Birth.ToShortDateString();
            UserRole = e.Role.ToString();
            UserGender = e.Gender.ToString();
            UserStatus = e.Status.ToString();
        }

        private List<string> genderList = new()
        {
            nameof(Gender.Male), nameof(Gender.Female)
        };

        public List<string> GenderList
        {
            get => genderList;
            set => genderList = value;
        }

        private List<string> roleList = new()
        {
            nameof(Role.Dev), nameof(Role.Designer), nameof(Role.Tester), nameof(Role.TechLead), nameof(Role.Manager), nameof(Role.Hr), nameof(Role.Staff)
        };

        public List<string> RoleList
        {
            get => roleList;
            set => roleList = value;
        }
    }
}
