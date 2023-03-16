using CuoiKi.Constants;
using System;

namespace CuoiKi.Models
{
    public class Employee : ModelBase
    {
        public string Name { get; set; }
        public string Address { get; set; }
        public DateTime Birth { get; set; }
        public EmployeeStatus Status { get; set; }
        public Role Role { get; set; }
        public string Password { get; set; }
        public Gender Gender { get; set; }

        public Employee(string name, string address, DateTime birth, EmployeeStatus status, string password, Gender gender, Role role) 
            : base(name.Trim() + DateTime.Now.ToShortDateString().Replace("/", ""))
        {
            Name = name;
            Address = address;
            Birth = birth;
            Status = status;
            Gender = gender;
            Role = role;
            string salt = BCrypt.Net.BCrypt.GenerateSalt();
            Password = BCrypt.Net.BCrypt.HashPassword(password, salt);
        }
        /// <summary>
        /// Constructor method used to map data from SQL to Employee
        /// </summary>
        public Employee(string ID, string Name, string Address, DateTime Birth, string EmployeeStatus, string Password, string Gender, string Role) : base(ID)
        {
            this.Name = Name;
            this.Address = Address;
            this.Birth = Birth;
            this.Status = (EmployeeStatus)Enum.Parse(typeof(EmployeeStatus), EmployeeStatus);
            this.Password = Password;
            this.Gender = (Gender)Enum.Parse(typeof(Gender), Gender);
            this.Role = (Role)Enum.Parse(typeof(Role), Role);
        }
    }
}
