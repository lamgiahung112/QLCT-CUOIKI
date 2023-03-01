using CuoiKi.Constants;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BCrypt.Net;
using System.CodeDom;

namespace CuoiKi.Models
{
    class Employee
    {
        public String Id { get; set; }
        public String Name { get; set; }
        public String Address { get; set; }
        public DateTime Birth { get; set; }
        public EmployeeStatus Status { get; set; }
        public String Password { get; set; }
        public Gender Gender { get; set; }

        public Employee(string name, string address, DateTime birth, EmployeeStatus status, string password, Gender gender)
        {
            Id = name.Trim() + DateTime.Now.ToShortDateString() + new Random().Next().ToString();
            Name = name;
            Address = address;
            Birth = birth;
            Status = status;
            Gender = gender;
            
            string salt = BCrypt.Net.BCrypt.GenerateSalt();
            Password = BCrypt.Net.BCrypt.HashPassword(password, salt);
        }
        public Employee(string id, string name, string address, DateTime birth, EmployeeStatus status, string password, Gender gender)
        {
            Id = id;
            Name = name;
            Address = address;
            Birth = birth;
            Status = status;
            Password = password;
            Gender = gender;
        }
    }
}
