using CuoiKi.DAOs;
using CuoiKi.Models;
using System;
using System.Windows;

namespace CuoiKi.Controllers
{
    public class WorkSessionController
    {
        private EmployeeDAO employeeDAO;
        private WorkSessionDAO workSessionDAO;
        public WorkSessionController()
        {
            employeeDAO = new EmployeeDAO();
            workSessionDAO = new WorkSessionDAO();
        }
        // Thinking a better name for these function...
        public bool CheckInAndReturnSuccessOrNot(string employeeId)
        {
            Employee? foundEmployee = employeeDAO.GetOne(employeeId);
            if (foundEmployee is object)
            {
                WorkSession newWorkSession = new WorkSession(employeeId);
                workSessionDAO.Add(newWorkSession);
                MessageBox.Show("Check in success");
                return true;
            }
            MessageBox.Show("EmployeeId not found");
            return false;
        }
        public bool CheckOutAndReturnSuccessOrNot(string employeeId)
        {
            Employee? foundEmployee = employeeDAO.GetOne(employeeId);
            if (!(foundEmployee is object))
            {
                MessageBox.Show("EmployeeId not found");
                return false;
            }
            WorkSession? foundWorkSession = workSessionDAO.GetOne(employeeId);
            if (foundWorkSession is object && !(foundWorkSession.EndingTime is object))
            {
                foundWorkSession.EndingTime = DateTime.Now;
                workSessionDAO.Modify(employeeId, foundWorkSession);
                MessageBox.Show("Check out success");
                return true;
            }
            MessageBox.Show("Check already checked out");
            return false;
        }
    }
}
