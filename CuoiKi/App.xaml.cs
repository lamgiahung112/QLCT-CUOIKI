using CuoiKi.DAOs;
using CuoiKi.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace CuoiKi
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            //Employee hung = new Employee("trikohung", "address", DateTime.Now, Constants.EmployeeStatus.Active, "123", Constants.Gender.Male, Constants.Role.Staff);
            //new EmployeeDAO().Add(hung);
            base.OnStartup(e);
        }
    }
}
