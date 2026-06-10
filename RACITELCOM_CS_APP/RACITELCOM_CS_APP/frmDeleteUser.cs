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
    public partial class frmDeleteUser : Form
    {
        public string user_name { get; set; }

        private frmManageUser manageUser;
        public frmDeleteUser(frmManageUser form)
        {
            InitializeComponent();
            manageUser = form;
        }

        private void frmDeleteUser_Load(object sender, EventArgs e)
        {
            txtUsername.Text = user_name;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            // this.Dispose();
            this.Close();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (txtConfirmation.Text == user_name)
            {
                MySqlConnection connection = MyConnectionString.mysql_connection();
                try
                {
                    connection.Open();

                    string query = "DELETE FROM tbl_user WHERE user_name = @username";
                    using (MySqlCommand cmd = new MySqlCommand(query, connection))
                    {
                        cmd.Parameters.AddWithValue("@username", txtConfirmation.Text);

                        cmd.ExecuteNonQuery();

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
                MessageBox.Show("Username not matched.", "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }
    }
}
