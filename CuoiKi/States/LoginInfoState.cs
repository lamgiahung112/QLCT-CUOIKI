﻿using CuoiKi.Constants;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CuoiKi.States
{
    public class LoginInfoState
    {
        private static LoginInfoState? _instance;
        public String Id { get; set; }
        public Role Role { get; set; }
        public String Name { get; set; }

        private LoginInfoState()
        {
            Id = "";
            Role = new Role();
            Name = "";
        }
        public static LoginInfoState getInstance()
        {
            if (_instance == null)
            {
                _instance = new LoginInfoState();
            }
            return _instance;
        }
    }
}
