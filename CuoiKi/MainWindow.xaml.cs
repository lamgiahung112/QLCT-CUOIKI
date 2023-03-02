using CuoiKi.Constants;
using CuoiKi.DAOs;
using CuoiKi.Models;
using System;
using System.Windows;

namespace CuoiKi
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            Employee test = new Employee("Võ Trọng", "Hồ Chí Minh", new DateTime(2003, 08, 08), EmployeeStatus.Active, "vovovo", Gender.Male);
            EmployeeDAO eDAO = new EmployeeDAO();
            eDAO.Add(test);
        }
    }
}
