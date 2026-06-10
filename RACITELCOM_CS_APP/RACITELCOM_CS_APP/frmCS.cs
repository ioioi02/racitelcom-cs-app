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
    public partial class frmCS : Form
    {
        Form form;
        Button btn;
        Button btn_;

        private Timer heartbeatTimer;

        public string username { get; set; }
        public string user_lastname { get; set; }
        public string user_firstname { get; set; }
        public string user_middlename { get; set; }

        public frmCS()
        {
            InitializeComponent();
            InitializeHeartbeatTimer();
        }

        private void frmCS_Load(object sender, EventArgs e)
        {
            Left = Top = 0;
            Width = Screen.PrimaryScreen.WorkingArea.Width;
            Height = Screen.PrimaryScreen.WorkingArea.Height;

            if (!string.IsNullOrEmpty(user_middlename))
            {
                labelUserName.Text = $"{capitalizedFirstCharacters(user_firstname)} {capitalizedFirstCharacters(user_middlename)} {capitalizedFirstCharacters(user_lastname)}";
            }
            else
            {
                labelUserName.Text = $"{capitalizedFirstCharacters(user_firstname)} {capitalizedFirstCharacters(user_lastname)}";
            }

            // at start

            btnCustDetails_Click(btnCustDetails, null);

            // at start
        }

        private void InitializeHeartbeatTimer()
        {
            heartbeatTimer = new Timer();
            heartbeatTimer.Interval = 50000;
            heartbeatTimer.Tick += new EventHandler(UpdateUserHeartbeat);
            heartbeatTimer.Start();
        }

        private void UpdateUserHeartbeat(object sender, EventArgs e)
        {
            MySqlConnection connection = MyConnectionString.mysql_connection();
            try
            {
                connection.Open();

                string query = @"UPDATE tbl_user SET user_heartbeat = NOW() WHERE user_name = @username";
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

        public void openFormContent(Form formToOpen, object btnSender)
        {
            if (form != null)
            {
                form.Dispose();
            }
            form = formToOpen;

            form.TopLevel = false;
            form.Dock = DockStyle.Fill;
            form.BringToFront();
            this.pnlContentPanel.Controls.Add(form);

            form.Show();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            updateUserStatus();

            string username = TCPClient.Instance.username;
            string log_type = "is offline";

            InsertLogs inserLogs = new InsertLogs();
            string log_result = inserLogs.insertLogs(username, log_type);
            if (log_result != "0")
            {
                // MessageBox.Show($"Error: {log_result}", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                MessageBox.Show("An error has occured.", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            // TCPClient.Instance.SendMessage("is offline.");
            TCPClient.Instance.Disconnect();

            Application.Exit();
        }

        private void btnMinimize_Click(object sender, EventArgs e)
        {
            WindowState = FormWindowState.Minimized;
        }

        private void picLogout_Click(object sender, EventArgs e)
        {
            if (btnLogout.Visible == false)
            {
                btnLogout.Visible = true;
            }
            else
            {
                btnLogout.Visible = false;
            }
        }

        private void btnLogout_Click(object sender, EventArgs e)
        {
            updateUserStatus();

            string username = TCPClient.Instance.username;
            string log_type = "is offline";

            InsertLogs inserLogs = new InsertLogs();
            string log_result = inserLogs.insertLogs(username, log_type);
            if (log_result != "0")
            {
                // MessageBox.Show($"Error: {log_result}", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                MessageBox.Show("An error has occured.", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            // TCPClient.Instance.SendMessage("is offline.");
            TCPClient.Instance.Disconnect();

            heartbeatTimer.Stop();

            frmLogin login = new frmLogin();
            this.Dispose();
            login.Show();
        }

        private void btnHighlight(Button btn, Button btn_)
        {
            if (btn != null)
            {
                btn.ForeColor = Color.White;
                btn.Font = new Font("Segoe UI", 14, FontStyle.Bold);
                btn.BackColor = Color.FromArgb(17, 7, 100);
            }
            btn_.ForeColor = Color.Maroon;
            btn_.Font = new Font("Segoe UI", 14, FontStyle.Bold);
            btn_.BackColor = SystemColors.Control;
        }

        private void btnCustDetails_Click(object sender, EventArgs e)
        {
            btn_ = sender as Button;
            if (btn_ != btn)
            {
                openFormContent(new frmCSPortal(), null);
                btnHighlight(btn, btn_);
            }
            btn = btn_;
            lblHeaderText.Text = btn.Text;
        }

        private void btnAppForm_Click(object sender, EventArgs e)
        {
            btn_ = sender as Button;
            if (btn_ != btn)
            {
                openFormContent(new frmAppForm(this), null);
                btnHighlight(btn, btn_);
            }
            btn = btn_;
            lblHeaderText.Text = btn.Text;
        }

        private void btnRequest_Click(object sender, EventArgs e)
        {
            btn_ = sender as Button;
            if (btn_ != btn)
            {
                openFormContent(new frmRequest(this), null);
                btnHighlight(btn, btn_);
            }
            btn = btn_;
            lblHeaderText.Text = btn.Text;
        }

        private void btn_MouseHover(object sender, EventArgs e)
        {
            Button button = sender as Button;
            if (button.ForeColor != Color.Maroon)
            {
                button.ForeColor = Color.FromArgb(17, 7, 100);
            }
            button.Font = new Font("Segoe UI", 14, FontStyle.Bold);
            button.BackColor = SystemColors.Control;
        }

        private void btn_MouseLeave(object sender, EventArgs e)
        {
            Button button = sender as Button;
            if (button != btn)
            {
                button.ForeColor = Color.White;
                button.Font = new Font("Segoe UI", 14, FontStyle.Bold);
                button.BackColor = Color.FromArgb(17, 7, 100);
            }
        }

        private void updateUserStatus()
        {
            MySqlConnection connection = MyConnectionString.mysql_connection();
            try
            {
                connection.Open();

                string query = @"UPDATE tbl_user SET user_status = 0 WHERE user_name = @username";
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
    }
}
