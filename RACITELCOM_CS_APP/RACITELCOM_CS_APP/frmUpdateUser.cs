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
    public partial class frmUpdateUser : Form
    {
        public string user_name { get; set; }
        public bool owned_account { get; set; }

        private string lastname = null;
        private string firstname = null;
        private string middlename = null;
        private string position = null;
        private string designation = null;
        private string location = null;
        private string email = null;
        private string password = null;

        private frmManageUser manageUser;
        public frmUpdateUser(frmManageUser form)
        {
            InitializeComponent();
            manageUser = form;
        }

        private void frmUpdateUser_Load(object sender, EventArgs e)
        {
            string loadUserResult = loadUser(user_name);
            if (loadUserResult == "0")
            {
                fillUpForm();
            }
            else
            {
                // MessageBox.Show($"Error: {loadUserResult}", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                MessageBox.Show("An error has occured.", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                // this.Dispose();
                this.Close();
            }

            if (owned_account == true)
            {
                txtDesignation.Enabled = false;
            }
        }

        private void fillUpForm()
        {
            txtSurname.Text = lastname;
            txtSurname.ForeColor = Color.FromArgb(17, 7, 100);
            txtFirstName.Text = firstname;
            txtFirstName.ForeColor = Color.FromArgb(17, 7, 100);
            txtMiddleName.Text = middlename;
            txtMiddleName.ForeColor = Color.FromArgb(17, 7, 100);
            txtPosition.Text = position;
            txtPosition.ForeColor = Color.FromArgb(17, 7, 100);
            txtDesignation.Text = designation;
            txtDesignation.ForeColor = Color.FromArgb(17, 7, 100);
            txtLocation.Text = location;
            txtLocation.ForeColor = Color.FromArgb(17, 7, 100);
            txtEmail.Text = email;
            txtEmail.ForeColor = Color.FromArgb(17, 7, 100);
            txtUsername.Text = user_name;
            txtUsername.ForeColor = Color.FromArgb(17, 7, 100);
            txtPassword.Text = password;
            txtPassword.ForeColor = Color.FromArgb(17, 7, 100);
            txtConfPassword.Text = (string)txtConfPassword.Tag;
            txtConfPassword.ForeColor = Color.Gray;
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

        private string loadUser(string username)
        {
            MySqlConnection connection = MyConnectionString.mysql_connection();
            try
            {
                connection.Open();

                string query_lastname = $"SELECT user_lastname FROM tbl_user WHERE user_name='{username}'";
                lastname = executeScalarQuery(connection, query_lastname);

                string query_firstname = $"SELECT user_firstname FROM tbl_user WHERE user_name='{username}'";
                firstname = executeScalarQuery(connection, query_firstname);

                string query_middlename = $"SELECT user_middlename FROM tbl_user WHERE user_name='{username}'";
                middlename = executeScalarQuery(connection, query_middlename);

                string query_position = $"SELECT user_position FROM tbl_user WHERE user_name='{username}'";
                position = executeScalarQuery(connection, query_position);

                string query_designation = $"SELECT user_designation FROM tbl_user WHERE user_name='{username}'";
                designation = executeScalarQuery(connection, query_designation);

                string query_location = $"SELECT user_location FROM tbl_user WHERE user_name='{username}'";
                location = executeScalarQuery(connection, query_location);

                string query_email = $"SELECT user_email FROM tbl_user WHERE user_name='{username}'";
                email = executeScalarQuery(connection, query_email);

                string query_password = $"SELECT user_password FROM tbl_user WHERE user_name='{username}'";
                password = executeScalarQuery(connection, query_password);

                return "0";
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

        private void removePlaceholder(object sender, EventArgs e)
        {
            TextBox textBox = sender as TextBox;
            if (textBox != null && textBox.ForeColor == Color.Gray)
            {
                textBox.Text = "";
                textBox.ForeColor = Color.FromArgb(17, 7, 100);
            }
        }

        private void placePlaceholder(object sender, EventArgs e)
        {
            TextBox textBox = sender as TextBox;
            if (textBox != null && string.IsNullOrWhiteSpace(textBox.Text))
            {
                textBox.Text = (string)textBox.Tag;
                textBox.ForeColor = Color.Gray;
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            // this.Dispose();
            this.Close();
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (txtPosition.ForeColor != Color.Gray && txtDesignation.ForeColor != Color.Gray && txtLocation.ForeColor != Color.Gray && txtEmail.ForeColor != Color.Gray && txtPassword.ForeColor != Color.Gray && txtConfPassword.ForeColor != Color.Gray)
            {
                if (txtPassword.Text == txtConfPassword.Text)
                {
                    if (txtDesignation.Text.ToLower() == "admin" || txtDesignation.Text.ToLower() == "customer service" || txtDesignation.Text.ToLower() == "accounting" || txtDesignation.Text.ToLower() == "technical")
                    {
                        MySqlConnection connection = MyConnectionString.mysql_connection();
                        try
                        {
                            connection.Open();

                            string query = "UPDATE tbl_user SET user_position = @position, user_designation = @designation, user_location = @location, user_email = @email, user_password = @password WHERE user_name = @username";
                            using (MySqlCommand cmd = new MySqlCommand(query, connection))
                            {
                                cmd.Parameters.AddWithValue("@position", txtPosition.Text.ToLower());
                                cmd.Parameters.AddWithValue("@designation", txtDesignation.Text.ToLower());
                                cmd.Parameters.AddWithValue("@location", txtLocation.Text.ToLower());
                                cmd.Parameters.AddWithValue("@email", txtEmail.Text.ToLower());
                                cmd.Parameters.AddWithValue("@password", txtPassword.Text);
                                cmd.Parameters.AddWithValue("@username", user_name);

                                int rowsAffected = cmd.ExecuteNonQuery();
                                if (rowsAffected > 0)
                                {
                                    MessageBox.Show("Update successful.", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    string r_query = "SELECT `user_name`, `user_email`, `user_lastname`, `user_firstname`, `user_middlename`, `user_location`, `user_designation`, `user_position`, `user_status` FROM tbl_user";
                                    manageUser.displayDatabase(r_query);
                                    // this.Dispose();
                                    this.Close();
                                }
                                else
                                {
                                    MessageBox.Show("No rows updated.", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    string r_query = manageUser.defualt_query;  // "SELECT `user_name`, `user_email`, `user_lastname`, `user_firstname`, `user_middlename`, `user_location`, `user_designation`, `user_position`, `user_status` FROM tbl_user";
                                    manageUser.displayDatabase(r_query);
                                    // this.Dispose();
                                    this.Close();
                                }
                            }
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
                    else
                    {
                        MessageBox.Show("Please input a valid designation: 'admin', 'customer service', 'accounting', or 'technical'.", "Invalid Designation Input", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    }
                }
                else
                {
                    MessageBox.Show("Password not matched.", "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
            }
            else
            {
                MessageBox.Show("Please ensure all required fields are filled out.", "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void btnRestore_Click(object sender, EventArgs e)
        {
            fillUpForm();
        }
    }
}
