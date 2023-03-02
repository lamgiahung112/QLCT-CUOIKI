﻿using CuoiKi.Models;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;

namespace CuoiKi.DAOs
{
    class EmployeeDAO : IDAO<Employee>
    {
        private readonly DBConnection dbc;
        private readonly SqlConverter sqlConverter;
        public EmployeeDAO()
        {
            dbc = new DBConnection();
            sqlConverter = new SqlConverter();
        }
        public void Add(Employee entry)
        {
            string command = sqlConverter.GetAddCommandForEmployee(entry);
            dbc.Execute(command);
        }
        public void Delete(string id)
        {
            string command = sqlConverter.GetDeleteCommandForEmployee(id);
            dbc.Execute(command);
        }
        public void Modify(string id, Employee entry)
        {
            string command = sqlConverter.GetUpdateCommandForEmployee(id, entry);
            dbc.Execute(command);
        }
        public List<Employee> GetAll()
        {
            string command = "";
            SqlDataReader? reader = dbc.ExecuteReader(command);
            if (reader == null)
            {
                return new List<Employee>();
            }
            return reader.Cast<Employee>().ToList();
        }
        public Employee? GetOne(string id)
        {
            string command = "";
            return (Employee?)dbc.ExecuteScalar(command);
        }
    }
}

