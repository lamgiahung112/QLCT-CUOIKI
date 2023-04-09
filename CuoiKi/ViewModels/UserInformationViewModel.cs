using CuoiKi.DAOs;
using CuoiKi.Models;
using CuoiKi.States;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace CuoiKi.ViewModels
{
    public class UserInformationViewModel
    {
        private readonly EmployeeDAO _employeeDAO;
        public string UserId { get; set; }
        public string UserName { get; set; }    
        public string UserAddress { get; set; }
        public string UserBirth { get; set; }
        public string UserRole { get; set; }
        public string UserGender { get; set; }
        public string UserStatus { get; set; }
        public BitmapImage ImageSrc { get; set; }
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
            ImageSrc = new BitmapImage();
        }

    }
}
