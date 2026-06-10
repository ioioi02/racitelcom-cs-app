using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace RACITELCOM_CS_APP
{
    public partial class frmLogin : Form
    {
        private string lastname = null;
        private string firstname = null;
        private string middlename = null;
        private string designation = null;

        public frmLogin()
        {
            InitializeComponent();
        }

        private string executeScalarQuery(MySqlConnection connection, string query)
        {
            using (MySqlCommand cmd = new MySqlCommand(query, connection))
            {
                var result = cmd.ExecuteScalar();

                if (result != null)
                {
                    return result.ToString();
                }
                else
                {
                    return null;
                }
            }
        }

        private string validateUser(string username, string password)
        {
            MySqlConnection connection = MyConnectionString.mysql_connection();
            try
            {
                connection.Open();

                string query = "SELECT user_password FROM tbl_user WHERE user_name = @username";
                using (MySqlCommand cmd = new MySqlCommand(query, connection))
                {
                    cmd.Parameters.AddWithValue("@username", username);

                    var result = cmd.ExecuteScalar();
                    if (result != null)
                    {
                        string storedPassword = result.ToString();
                        if (storedPassword == password)
                        {
                            string query_lastname = $"SELECT user_lastname FROM tbl_user WHERE user_name='{username}'";
                            lastname = executeScalarQuery(connection, query_lastname);

                            string query_firstname = $"SELECT user_firstname FROM tbl_user WHERE user_name='{username}'";
                            firstname = executeScalarQuery(connection, query_firstname);

                            string query_middlename = $"SELECT user_middlename FROM tbl_user WHERE user_name='{username}'";
                            middlename = executeScalarQuery(connection, query_middlename);

                            string query_designation = $"SELECT user_designation FROM tbl_user WHERE user_name='{username}'";
                            designation = executeScalarQuery(connection, query_designation);

                            if (lastname != null && firstname != null && designation != null)
                            {
                                return "0";  // correct password and account information successfully retrieved
                            }
                            else
                            {
                                return "1";  // correct password but account information cannot retrieved
                            }
                        }
                        else
                        {
                            return "2";  // incorrect password
                        }
                    }
                    else
                    {
                        return "3";  // username account doesnt exit
                    }
                }
            }
            catch (Exception ex)
            {
                return ex.ToString();  // an error has occured
            }
            finally
            {
                connection.Close();
            }
        }

        private bool checkUserHeartBeat(string username)
        {
            MySqlConnection connection = MyConnectionString.mysql_connection();
            try
            {
                connection.Open();

                string query = "SELECT user_status, user_heartbeat FROM tbl_user WHERE user_name = @username";
                using (MySqlCommand cmd = new MySqlCommand(query, connection))
                {
                    cmd.Parameters.AddWithValue("@username", username);

                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            int user_status = Convert.ToInt32(reader["user_status"]);
                            DateTime? user_heartbeat = reader["user_heartbeat"] != DBNull.Value ? (DateTime?)Convert.ToDateTime(reader["user_heartbeat"]) : null;
                            if (user_heartbeat.HasValue)
                            {
                                if (user_status == 1)
                                {
                                    TimeSpan? timeSinceHeartbeat = DateTime.Now - user_heartbeat;
                                    if (timeSinceHeartbeat?.TotalSeconds >= 60)
                                    {
                                        return true;
                                    }
                                    else
                                    {
                                        return false;
                                    }
                                }
                                else
                                {
                                    return true;
                                }
                            }
                            else
                            {
                                return true;
                            }
                        }
                        reader.Close();
                    }
                }
                return false;
            }
            catch (Exception ex)
            {
                return false;
                // return ex.ToString();  // an error has occured
            }
            finally
            {
                connection.Close();
            }
        }

        private void updateUserHeartbeat(string username)
        {
            MySqlConnection connection = MyConnectionString.mysql_connection();
            try
            {
                connection.Open();

                string query = @"UPDATE tbl_user SET user_status = 1, user_heartbeat = NOW() WHERE user_name = @username";
                MySqlCommand cmd = new MySqlCommand(query, connection);
                cmd.Parameters.AddWithValue("@username", username);
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                // MessageBox.Show($"Error: {ex.ToString()}", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                MessageBox.Show("An error has occured.", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                connection.Close();
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            string username = txtUsername.Text;
            string password = txtPassword.Text;

            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            {
                MessageBox.Show("Invalid username or password.", "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            else
            {
                string validateResult = validateUser(username, password);
                if (validateResult == "0")
                {
                    // check heartbeat

                    bool heartBeatResult = checkUserHeartBeat(username);
                    if (heartBeatResult == false)
                    {
                        MessageBox.Show("Username is still online.", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                    else
                    {
                        updateUserHeartbeat(username);
                    }

                    // check heartbeat

                    bool result = TCPClient.Instance.Connect(username);
                    if (result == false)
                    {
                        MessageBox.Show("An error has occured. Make sure you are connected to your server.", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    // test only //

                    string log_type = "is online";

                    InsertLogs inserLogs = new InsertLogs();
                    string log_result = inserLogs.insertLogs(username, log_type);
                    if (log_result != "0")
                    {
                        MessageBox.Show($"Error: {log_result}", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        // MessageBox.Show("An error has occured.", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }

                    // test only //

                    MessageBox.Show($"Successfully logged in to {designation} account.", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    if (designation == "admin")
                    {
                        frmAdmin admin = new frmAdmin();
                        admin.username = username;
                        admin.user_lastname = lastname;
                        admin.user_firstname = firstname;
                        admin.user_middlename = middlename;
                        this.Dispose();
                        admin.Show();
                    }
                    if (designation == "customer service")
                    {
                        frmCS cs = new frmCS();
                        cs.username = username;
                        cs.user_lastname = lastname;
                        cs.user_firstname = firstname;
                        cs.user_middlename = middlename;
                        this.Dispose();
                        cs.Show();
                    }
                    if (designation == "accounting")
                    {
                        frmAccounting accounting = new frmAccounting();
                        accounting.username = username;
                        accounting.user_lastname = lastname;
                        accounting.user_firstname = firstname;
                        accounting.user_middlename = middlename;
                        this.Dispose();
                        accounting.Show();
                    }
                    if (designation == "technical")
                    {
                        frmTechnical technical = new frmTechnical();
                        technical.username = username;
                        technical.user_lastname = lastname;
                        technical.user_firstname = firstname;
                        technical.user_middlename = middlename;
                        this.Dispose();
                        technical.Show();
                    }

                    if (designation == "inventory")
                    {
                        frmInventory inventory = new frmInventory();
                        inventory.username = username;
                        inventory.user_lastname = lastname;
                        inventory.user_firstname = firstname;
                        inventory.user_middlename = middlename;
                        this.Dispose();
                        inventory.Show();
                    }
                }
                else if (validateResult == "1")
                {
                    MessageBox.Show("An error has occured. Cannot retrieved your account information.", "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
                else if (validateResult == "2" || validateResult == "3")
                {
                    MessageBox.Show("Invalid username or password.", "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
                else
                {
                    // MessageBox.Show($"Error: {validateResult}", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    MessageBox.Show("An error has occured. Make sure you are connected to your server.", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            txtUsername.Text = "";
            txtPassword.Text = "";
        }

        private void checkShowPass_CheckedChanged(object sender, EventArgs e)
        {
            if (checkShowPass.Checked)
            {
                txtPassword.PasswordChar = '\0';
            }
            else
            {
                txtPassword.PasswordChar = '•';
            }
        }
    }
}
