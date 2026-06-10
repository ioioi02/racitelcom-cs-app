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
    public partial class frmCreateUser : Form
    {
        private frmManageUser manageUser;
        public frmCreateUser(frmManageUser form)
        {
            InitializeComponent();
            manageUser = form;
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

        private void btnCreate_Click(object sender, EventArgs e)
        {
            string lastname = txtSurname.Text.ToLower();
            string firstname = txtFirstName.Text.ToLower();
            string middlename = txtMiddleName.ForeColor != Color.Gray ? txtMiddleName.Text.ToLower() : null;
            string position = txtPosition.Text.ToLower();
            string designation = txtDesignation.Text.ToLower();
            string location = txtLocation.Text.ToLower();
            string email = txtEmail.Text.ToLower();
            string username = txtUsername.Text.ToLower();
            string password = txtPassword.Text;
            string conf_password = txtConfPassword.Text;
            int status = 1;

            if (txtSurname.ForeColor != Color.Gray && txtFirstName.ForeColor != Color.Gray && txtPosition.ForeColor != Color.Gray && txtDesignation.ForeColor != Color.Gray && txtLocation.ForeColor != Color.Gray && txtEmail.ForeColor != Color.Gray && txtUsername.ForeColor != Color.Gray && txtPassword.ForeColor != Color.Gray && txtConfPassword.ForeColor != Color.Gray)
            {
                if (password == conf_password)
                {
                    if (designation == "admin" || designation == "customer service" || designation == "accounting" || designation == "technical" || designation == "inventory")
                    {
                        MySqlConnection connection = MyConnectionString.mysql_connection();
                        try
                        {
                            connection.Open();

                            string query = "INSERT INTO tbl_user (user_name, user_password, user_email, user_lastname, user_firstname, user_middlename, user_location, user_designation, user_position, user_status) " +
                                           "VALUES (@username, @password, @email, @lastname, @firstname, @middlename, @location, @designation, @position, @status)";
                            using (MySqlCommand cmd = new MySqlCommand(query, connection))
                            {
                                cmd.Parameters.AddWithValue("@username", username);
                                cmd.Parameters.AddWithValue("@password", password);
                                cmd.Parameters.AddWithValue("@email", email);
                                cmd.Parameters.AddWithValue("@lastname", lastname);
                                cmd.Parameters.AddWithValue("@firstname", firstname);
                                cmd.Parameters.AddWithValue("@middlename", middlename);
                                cmd.Parameters.AddWithValue("@location", location);
                                cmd.Parameters.AddWithValue("@designation", designation);
                                cmd.Parameters.AddWithValue("@position", position);
                                cmd.Parameters.AddWithValue("@status", status);

                                cmd.ExecuteNonQuery();

                                MessageBox.Show("Account created successfully.", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                string r_query = manageUser.defualt_query;  // "SELECT `user_name`, `user_email`, `user_lastname`, `user_firstname`, `user_middlename`, `user_location`, `user_designation`, `user_position`, `user_status` FROM tbl_user";
                                manageUser.displayDatabase(r_query);
                                // this.Dispose();
                                this.Close();
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
                        MessageBox.Show("Please input a valid designation: 'admin', 'customer service', 'accounting', 'technical' or 'inventory'.", "Invalid Designation Input", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
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

        private void btnClear_Click(object sender, EventArgs e)
        {
            txtSurname.Text = (string)txtSurname.Tag;
            txtSurname.ForeColor = Color.Gray;
            txtFirstName.Text = (string)txtFirstName.Tag;
            txtFirstName.ForeColor = Color.Gray;
            txtMiddleName.Text = (string)txtMiddleName.Tag;
            txtMiddleName.ForeColor = Color.Gray;
            txtPosition.Text = (string)txtPosition.Tag;
            txtPosition.ForeColor = Color.Gray;
            txtDesignation.Text = (string)txtDesignation.Tag;
            txtDesignation.ForeColor = Color.Gray;
            txtLocation.Text = (string)txtLocation.Tag;
            txtLocation.ForeColor = Color.Gray;
            txtEmail.Text = (string)txtEmail.Tag;
            txtEmail.ForeColor = Color.Gray;
            txtUsername.Text = (string)txtUsername.Tag;
            txtUsername.ForeColor = Color.Gray;
            txtPassword.Text = (string)txtPassword.Tag;
            txtPassword.ForeColor = Color.Gray;
            txtConfPassword.Text = (string)txtConfPassword.Tag;
            txtConfPassword.ForeColor = Color.Gray;
        }
    }
}
