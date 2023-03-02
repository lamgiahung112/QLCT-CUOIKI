﻿using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Windows;
using System.Windows.Documents;

namespace CuoiKi.DAOs
{
    public class DBConnection
    {
        private SqlConnection conn;
        public DBConnection()
        {
            conn = new SqlConnection(Properties.Settings.Default.connStr);
        }
        public void Execute(string sqlCommand)
        {
            try
            {
                string notification = "";
                if (sqlCommand[0] == 'I') notification = "Thêm";
                if (sqlCommand[0] == 'D') notification = "Xóa";
                if (sqlCommand[0] == 'U') notification = "Sửa";
                conn.Open();
                SqlCommand cmd = new SqlCommand("USE companyDB;" + sqlCommand, conn);
                if (cmd.ExecuteNonQuery() > 0)
                {
                    MessageBox.Show(notification + " thành công");
                }
                else
                {
                    MessageBox.Show(notification + " thất bại");
                    if (notification == "Xóa" || notification == "Sửa")
                    {
                        MessageBox.Show("Mã số không tồn tại. Vui lòng kiểm tra lại");
                    }
                }
                conn.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Thực thi thất bại" + ex.Message);
                conn.Close();
            }
            finally
            {
                conn.Close();
            }
        }
        public int ExecuteWithQuery(string s)
        {
            try
            {
                conn.Open();
                SqlCommand command = new SqlCommand("USE companyDB; " + s, conn);
                int noRow = (int)command.ExecuteScalar();
                conn.Close();
                return noRow;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Tìm thất bại " + ex.Message);
                conn.Close();
                return 0;
            }
            finally
            {
                conn.Close();
            }
        }

        public SqlDataReader? ExecuteReader(string s)
        {
            try
            {
                conn.Open();
                SqlCommand command = new SqlCommand("USE companyDB; " + s, conn);
                return command.ExecuteReader();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Tìm thất bại " + ex.Message);
            }
            finally
            {
                conn.Close();
            }
            return null;
        }

        public object? ExecuteScalar(string s)
        {
            try
            {
                conn.Open();
                SqlCommand command = new SqlCommand("USE companyDB; " + s, conn);
                return command.ExecuteScalar();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Tìm thất bại " + ex.Message);
            }
            finally
            {
                conn.Close();
            }
            return null;
        }
    }
}
