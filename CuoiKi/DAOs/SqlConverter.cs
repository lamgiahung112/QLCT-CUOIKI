﻿using CuoiKi.Models;

namespace CuoiKi.DAOs
{
    public class SqlConverter
    {
        public SqlConverter() { }
        public string GetAddCommandForEmployee(Employee employee)
        {
            return string.Format(
                "INSERT INTO Employees (" +
                "ID, [Name], [Address], Birth, EmployeeStatus, [Password], Gender) " +
                "VALUES (" +
                "N'{0}', " +
                "N'{1}', " +
                "N'{2}', " +
                "'{3}', " +
                "'{4}', " +
                "'{5}', " +
                "'{6}');",
                employee.Id,
                employee.Name,
                employee.Address,
                employee.Birth.ToString("yyyy-MM-dd"),
                employee.Status.ToString(),
                employee.Password,
                employee.Gender.ToString());
        }
        public string GetDeleteCommandForEmployee(string id)
        {
            return string.Format("DELETE FROM Employees WHERE ID = N'{0}'", id);
        }
        public string GetUpdateCommandForEmployee(string id, Employee employee)
        {
            return string.Format(
                "UPDATE Employees " +
                "SET " +
                "[Name] = N'{0}', " +
                "[Address] = N'{1}', " +
                "Birth = '{2}', " +
                "EmployeeStatus = '{3}', " +
                "[Password] = '{4}', " +
                "Gender = '{5}' " +
                "WHERE ID = N'{6}';",
                employee.Name,
                employee.Address,
                employee.Birth.ToString("yyyy-MM-dd"),
                employee.Status.ToString(),
                employee.Password,
                employee.Gender.ToString(),
                id);
        }
    }
}
