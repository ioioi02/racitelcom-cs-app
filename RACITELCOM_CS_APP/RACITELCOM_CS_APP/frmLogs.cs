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
    public partial class frmLogs : Form
    {
        public frmLogs()
        {
            InitializeComponent();
            TCPClient.Instance.OnMessageReceived += Client_On_MessageReceived;
        }

        private void Client_On_MessageReceived(string message)
        {
            if (InvokeRequired)
            {
                Invoke(new Action(() => {
                    removeAllControls();
                    UpdateLogs();
                }));
            }
            else
            {
                removeAllControls();
                UpdateLogs();
            }
        }

        private void frmLogs_Load(object sender, EventArgs e)
        {
            UpdateLogs();
        }

        private void UpdateLogs()
        {
            MySqlConnection connection = MyConnectionString.mysql_connection();
            try
            {
                connection.Open();

                string query = "SELECT * FROM tbl_log ORDER BY log_date DESC";

                MySqlCommand cmd = new MySqlCommand(query, connection);
                using (MySqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            string user_name = reader["user_name"].ToString();
                            string log_type = reader["log_type"].ToString();
                            string log_message = $"{user_name}: {log_type}.";
                            DateTime log_date = Convert.ToDateTime(reader["log_date"]);
                            string formattedLogDate = log_date.ToString("MMMM/dd/yyyy HH:mm:ss");

                            LogPanel logPanel = new LogPanel();

                            logPanel.Width = requestLayoutPanel.ClientSize.Width;
                            logPanel.logMessage = log_message;
                            logPanel.dateTime = formattedLogDate;
                            requestLayoutPanel.Controls.Add(logPanel);
                        }
                        reader.Close();
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

        private void requestLayoutPanel_SizeChanged(object sender, EventArgs e)
        {
            foreach (Control control in requestLayoutPanel.Controls)
            {
                control.Width = requestLayoutPanel.ClientSize.Width;
            }
        }

        private void removeAllControls()
        {
            // requestLayoutPanel.SuspendLayout();
            // requestLayoutPanel.Visible = false;

            for (int i = requestLayoutPanel.Controls.Count - 1; i >= 0; i--)
            {
                Control control = requestLayoutPanel.Controls[i];
                if (control is UserControl)
                {
                    requestLayoutPanel.Controls.Remove(control);
                    control.Dispose();
                }
            }

            // requestLayoutPanel.ResumeLayout();
            // requestLayoutPanel.Visible = true;

            // this.Refresh();
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            removeAllControls();
            UpdateLogs();
        }
    }
}
