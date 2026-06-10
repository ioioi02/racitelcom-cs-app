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
using System.Drawing.Drawing2D;

namespace RACITELCOM_CS_APP
{
    public partial class frmManageUser : Form
    {
        public string main_user_name { get; set; }

        public string defualt_query = @"
                                        SELECT 
                                            user_name, 
                                            user_email, 
                                            user_lastname, 
                                            user_firstname, 
                                            user_middlename, 
                                            user_location, 
                                            user_designation, 
                                            user_position, 
                                            CASE 
                                                WHEN user_status = 1 AND TIMESTAMPDIFF(SECOND, user_heartbeat, NOW()) <= 60 THEN 'ONLINE'
                                                ELSE 'OFFLINE' 
                                            END AS user_status 
                                        FROM tbl_user";

        private frmAdmin admin;
        public frmManageUser(frmAdmin form)
        {
            InitializeComponent();
            admin = form;
        }

        private void frmManageUser_Load(object sender, EventArgs e)
        {
            comboSearchOption.Items.AddRange(new string[] { "Username", "Lastname" });
            comboSearchOption.SelectedItem = "Username";

            displayDatabase(defualt_query);

            string[] customHeaderText = { "Username", "Email", "Lastname", "Firstname", "Middlename", "Location", "Designation", "Position", "Status" };
            for (int i = 0; i < dataBaseGridView.Columns.Count && i < customHeaderText.Length; i++)
            {
                dataBaseGridView.Columns[i].HeaderText = customHeaderText[i];
            }

            dataBaseGridView.DataBindingComplete += DataBaseGridView_DataBindingComplete;
        }

        private void DataBaseGridView_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            ColorStatusCells();
            dataBaseGridView.ClearSelection();
        }

        public void displayDatabase(string query)
        {
            MySqlConnection connection = MyConnectionString.mysql_connection();
            MySqlCommand cmd = new MySqlCommand(query, connection);
            MySqlDataAdapter da = new MySqlDataAdapter(cmd);
            DataTable dt = new DataTable();

            try
            {
                connection.Open();
                da.Fill(dt);
                dataBaseGridView.DataSource = dt;

                txtSearchBox.Text = "";
                txtUsername.Text = "";
                txtUserFullName.Text = "";
                txtDesignation.Text = "";
            }
            catch (Exception ex)
            {
                // MessageBox.Show($"Error: {ex}", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                MessageBox.Show("An error has occured.", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                connection.Close();
            }
        }

        private void ColorStatusCells()
        {
            foreach (DataGridViewRow row in dataBaseGridView.Rows)
            {
                if (row.Cells["user_status"].Value != null)
                {
                    string status = row.Cells["user_status"].Value.ToString();
                    if (status == "ONLINE")
                    {
                        row.Cells["user_status"].Style.BackColor = Color.Green;
                        row.Cells["user_status"].Style.ForeColor = Color.White;
                    }
                    else if (status == "OFFLINE")
                    {
                        row.Cells["user_status"].Style.BackColor = Color.Red;
                        row.Cells["user_status"].Style.ForeColor = Color.White;
                    }
                }
            }
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            string searchText = txtSearchBox.Text;
            if (searchText != "")
            {
                string column = null;
                if (comboSearchOption.SelectedItem.ToString() == "Username")
                {
                    column = "user_name";
                }

                else if (comboSearchOption.SelectedItem.ToString() == "Lastname")
                {
                    column = "user_lastname";
                }

                string query = $@"
                                SELECT 
                                    user_name, 
                                    user_email, 
                                    user_lastname, 
                                    user_firstname, 
                                    user_middlename, 
                                    user_location, 
                                    user_designation, 
                                    user_position, 
                                    CASE 
                                        WHEN user_status = 1 THEN 'ONLINE' 
                                        ELSE 'OFFLINE' 
                                    END AS user_status 
                                FROM tbl_user WHERE {column} = '{searchText.ToLower()}';";
                displayDatabase(query);
                txtSearchBox.Text = "";
            }
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            displayDatabase(defualt_query);
            txtSearchBox.Text = "";
        }

        private void dataBaseGridView_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dataBaseGridView.Rows[e.RowIndex];

                string username = row.Cells["user_name"].Value.ToString();
                string lastName = row.Cells["user_lastname"].Value.ToString();
                string firstName = row.Cells["user_firstname"].Value.ToString();
                string middlename = row.Cells["user_middlename"].Value.ToString();
                string designation = row.Cells["user_designation"].Value.ToString();

                if (string.IsNullOrEmpty(username) && string.IsNullOrEmpty(lastName) && string.IsNullOrEmpty(firstName) && string.IsNullOrEmpty(middlename) && string.IsNullOrEmpty(designation))
                {
                    txtUsername.Text = "";
                    txtUserFullName.Text = "";
                    txtDesignation.Text = "";
                }
                else
                {
                    txtUsername.Text = username;
                    txtDesignation.Text = designation.ToUpper();
                    if (!string.IsNullOrEmpty(middlename))
                    {
                        txtUserFullName.Text = $"{capitalizedFirstCharacters(firstName)} {capitalizedFirstCharacters(middlename)} {capitalizedFirstCharacters(lastName)}";
                    }
                    else
                    {
                        txtUserFullName.Text = $"{capitalizedFirstCharacters(firstName)} {capitalizedFirstCharacters(lastName)}";
                    }
                }
            }
        }

        private string capitalizedFirstCharacters(string input)
        {
            string[] input_split = input.Split(' ');

            for (int i = 0; i < input_split.Length; i++)
            {
                if (!string.IsNullOrEmpty(input_split[i]))
                {
                    input_split[i] = char.ToUpper(input_split[i][0]) + input_split[i].Substring(1);
                }
            }

            string result = string.Join(" ", input_split);
            return result;
        }

        private void btnCreateUser_Click(object sender, EventArgs e)
        {
            frmCreateUser createUser = new frmCreateUser(this);
            createUser.ShowDialog();
        }

        private void btnUpdateUser_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtUsername.Text))
            {
                frmUpdateUser updateUser = new frmUpdateUser(this);
                updateUser.user_name = txtUsername.Text;
                if (txtUsername.Text == main_user_name)
                {
                    updateUser.owned_account = true;
                }
                updateUser.ShowDialog();
            }
            else
            {
                MessageBox.Show("Please select user to update.", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnDeleteUser_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtUsername.Text))
            {
                if (txtDesignation.Text != "admin")
                {
                    frmDeleteUser deleteUser = new frmDeleteUser(this);
                    deleteUser.user_name = txtUsername.Text;
                    deleteUser.ShowDialog();
                }
                else
                {
                    MessageBox.Show("This account can't be deleted.", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            else
            {
                MessageBox.Show("Please select user to delete.", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
    }
}