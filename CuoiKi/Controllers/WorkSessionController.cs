using CuoiKi.Constants;
using CuoiKi.DAOs;
using CuoiKi.Models;
using System;
using System.Collections.Generic;
using System.Windows;

namespace CuoiKi.Controllers
{
    public class WorkSessionController
    {
        private readonly EmployeeDAO employeeDAO;
        private readonly WorkSessionDAO workSessionDAO;
        public WorkSessionController()
        {
            employeeDAO = new EmployeeDAO();
            workSessionDAO = new WorkSessionDAO();
        }
        // Thinking a better name for these function...
        public bool CheckInAndReturnSuccessOrNot(string employeeId)
        {
            Employee? foundEmployee = employeeDAO.GetOne(employeeId);
            if (foundEmployee is null)
            {
                // Maybe admin deleted this account but client side didn't recornize it
                MessageBox.Show("EmployeeId not found");
                return false;
            }
            if (GetWorkSessionStatus(employeeId) == WorkSessionStatus.CheckedIn)
            {
                MessageBox.Show("You already checked in");
                return false;
            }
            WorkSession newWorkSession = new(employeeId);
            workSessionDAO.Add(newWorkSession);
            MessageBox.Show("Check in success");
            return true;
        }
        public bool CheckOutAndReturnSuccessOrNot(string employeeId)
        {
            Employee? foundEmployee = employeeDAO.GetOne(employeeId);
            if (foundEmployee is null)
            {
                // Maybe admin deleted this account but client side didn't recornize it
                MessageBox.Show("EmployeeId not found");
                return false;
            }
            WorkSession? unfinishedWorkSession = GetUnfinishedWorkSession(employeeId);
            if (unfinishedWorkSession is null)
            {
                MessageBox.Show("You already checked out");
                return false;
            }
            unfinishedWorkSession.EndingTime = DateTime.Now;
            workSessionDAO.Modify(unfinishedWorkSession.Id, unfinishedWorkSession);
            MessageBox.Show("Check out success");
            return true;
        }
        public WorkSessionStatus GetWorkSessionStatus(string employeeId)
        {
            if (GetUnfinishedWorkSession(employeeId) is not null)
            {
                return WorkSessionStatus.CheckedIn;
            }
            return WorkSessionStatus.CheckedOut;
        }
        public List<WorkSession>? GetAllWorkSessionOf(string employeeId)
        {
            List<WorkSession>? workSessions = workSessionDAO.GetAll(employeeId);
            return workSessions;
        }
        public WorkSession? GetUnfinishedWorkSession(string employeeId)
        {
            // If employee not found return null
            Employee? foundEmployee = employeeDAO.GetOne(employeeId);
            if (foundEmployee is null)
            {
                // Maybe admin deleted this account but client side didn't recornize it
                MessageBox.Show("EmployeeId not found");
                return null;
            }
            return workSessionDAO.GetUnfinished(employeeId);
        }

        public WorkSession? GetLastestWorkSession(string employeeID)
        {
            return workSessionDAO.GetLastest(employeeID);
        }
    }
}
