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
            if (foundEmployee is null)
            {
                MessageBox.Show("EmployeeId not found");
                return false;
            }
            if (GetWorkSessionStatus(employeeId) == WorkSessionStatus.CheckedIn)
            {
                MessageBox.Show("You already checked in");
                return false;
            }
            WorkSession newWorkSession = new WorkSession(employeeId);
            workSessionDAO.Add(newWorkSession);
            MessageBox.Show("Check in success");
            return true;
        }
        public bool CheckOutAndReturnSuccessOrNot(string employeeId)
        {
            Employee? foundEmployee = employeeDAO.GetOne(employeeId);
            if (foundEmployee is null)
            {
                MessageBox.Show("EmployeeId not found");
                return false;
            }
            List<WorkSession>? unfinishedWorkSessions = GetUnfinishedWorkSession(employeeId);
            if (unfinishedWorkSessions is null)
            {
                MessageBox.Show("You already checked out");
                return false;
            }
            unfinishedWorkSessions[0].EndingTime = DateTime.Now;
            workSessionDAO.Modify(unfinishedWorkSessions[0].Id, unfinishedWorkSessions[0]);
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
        public List<WorkSession>? GetUnfinishedWorkSession(string employeeId)
        {
            // If employee not found return null
            Employee? foundEmployee = employeeDAO.GetOne(employeeId);
            if (foundEmployee is null)
            {
                MessageBox.Show("EmployeeId not found");
                return null;
            }
            // If employee doesn't have any work sessions then return null
            List<WorkSession>? workSessions = workSessionDAO.GetAll(employeeId);
            if (workSessions is null) return null;
            // If have unfinished work session then append it to a list and return
            List<WorkSession>? res = null;
            for (int i = 0; i < workSessions.Count; i++)
            {
                if (workSessions[i].EndingTime == DateTime.MinValue)
                {
                    if (res is null)
                    {
                        res = new List<WorkSession>();
                    }
                    res.Add(workSessions[i]);
                }
            }
            return res;
        }
    }
}
