using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace RACITELCOM_CS_APP
{
    class InsertLogs
    {
        public string insertLogs(string user_name, string log_type)
        {
            MySqlConnection connection = MyConnectionString.mysql_connection();
            try
            {
                connection.Open();

                string query = @"INSERT INTO tbl_log (user_name, log_type, log_date)
                                     VALUES (@user_name, @log_type, @log_date);";
                using (MySqlCommand cmd = new MySqlCommand(query, connection))
                {
                    DateTime currentDate = DateTime.Now;

                    cmd.Parameters.AddWithValue("@user_name", user_name);
                    cmd.Parameters.AddWithValue("@log_type", log_type);
                    cmd.Parameters.AddWithValue("@log_date", currentDate);

                    cmd.ExecuteNonQuery();
                }
                return "0";
            }
            catch (Exception ex)
            {
                return ex.ToString();
            }
            finally
            {
                connection.Close();
            }
        }
    }
}
