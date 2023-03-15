using CuoiKi.Constants;
using System;

namespace CuoiKi.Models
{
    public class Employee
    {
        public String Id;// { get; set; }
        public String Name { get; set; }
        public String Address { get; set; }
        public DateTime Birth { get; set; }
        public EmployeeStatus Status { get; set; }
        public Role Role { get; set; }
        public String Password { get; set; }
        public Gender Gender { get; set; }
        public String DepartmentId { get; set; }

        public Employee(string name, string address, DateTime birth, EmployeeStatus status, string password, Gender gender, Role role, string departmentId)
        {
            Id = name.Trim() + DateTime.Now.ToShortDateString().Replace("/", "");
            Name = name;
            Address = address;
            Birth = birth;
            Status = status;
            Gender = gender;
            Role = role;
            string salt = BCrypt.Net.BCrypt.GenerateSalt();
            Password = BCrypt.Net.BCrypt.HashPassword(password, salt);
            DepartmentId = departmentId;
        }
        /// <summary>
        /// Constructor method used to map data from SQL to Employee
        /// </summary>
        public Employee(String ID, String Name, String Address, DateTime Birth, String EmployeeStatus, String Password, String Gender, String Role, String DepartmentId)
        {
            this.Id = ID;
            this.Name = Name;
            this.Address = Address;
            this.Birth = Birth;
            this.Status = (EmployeeStatus)Enum.Parse(typeof(EmployeeStatus), EmployeeStatus);
            this.Password = Password;
            this.Gender = (Gender)Enum.Parse(typeof(Gender), Gender);
            this.Role = (Role)Enum.Parse(typeof(Role), Role);
            this.DepartmentId = DepartmentId;
        }
    }
}
