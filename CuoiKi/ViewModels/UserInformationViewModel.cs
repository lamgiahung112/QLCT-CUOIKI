using CuoiKi.Controllers;
using CuoiKi.DAOs;
using CuoiKi.States;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CuoiKi.ViewModels
{
    public class UserInformationViewModel
    {
        private readonly EmployeeDAO _employeeDAO;
        public string UserId;
        public string UserName;
        public string UserAddress;
        public string UserBirth;
        public string UserRole;
        public string UserGender;
        public string UserStatus;
        public UserInformationViewModel()
        {
            _employeeDAO = new EmployeeDAO();
            _employeeDAO.GetOne(LoginInfoState.Id!);
        }

    }
}
