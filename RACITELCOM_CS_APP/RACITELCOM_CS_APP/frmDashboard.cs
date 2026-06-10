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
    public partial class frmDashboard : Form
    {
        public frmDashboard()
        {
            InitializeComponent();
            TCPClient.Instance.OnMessageReceived += Client_On_MessageReceived;
        }

        private void Client_On_MessageReceived(string message)
        {
            if (InvokeRequired)
            {
                Invoke(new Action(() => {
                    if (message.IndexOf("connected.", StringComparison.OrdinalIgnoreCase) >= 0)
                    {
                        frmDashboard_Load(null, null);
                    }
                    if (message.IndexOf("disconnected.", StringComparison.OrdinalIgnoreCase) >= 0)
                    {
                        frmDashboard_Load(null, null);
                    }
                }));
            }
            else
            {
                if (message.IndexOf("connected.", StringComparison.OrdinalIgnoreCase) >= 0)
                {
                    frmDashboard_Load(null, null);
                }
                if (message.IndexOf("disconnected.", StringComparison.OrdinalIgnoreCase) >= 0)
                {
                    frmDashboard_Load(null, null);
                }
            }
        }

        private void frmDashboard_Load(object sender, EventArgs e)
        {
            MySqlConnection connection = MyConnectionString.mysql_connection();
            try
            {
                connection.Open();

                string query = @"SELECT
                                    SUM(CASE WHEN user_status = 1 AND TIMESTAMPDIFF(SECOND, user_heartbeat, NOW()) <= 60 AND user_designation = 'admin' THEN 1 ELSE 0 END) AS OnlineAdmin,
                                    SUM(CASE WHEN user_status = 1 AND TIMESTAMPDIFF(SECOND, user_heartbeat, NOW()) <= 60 AND user_designation = 'technical' THEN 1 ELSE 0 END) AS OnlineTechnician,
                                    SUM(CASE WHEN user_status = 1 AND TIMESTAMPDIFF(SECOND, user_heartbeat, NOW()) <= 60 AND user_designation = 'accounting' THEN 1 ELSE 0 END) AS OnlineAccounting,
                                    SUM(CASE WHEN user_status = 1 AND TIMESTAMPDIFF(SECOND, user_heartbeat, NOW()) <= 60 AND user_designation = 'customer service' THEN 1 ELSE 0 END) AS OnlineCS,
                                    SUM(CASE WHEN user_status = 1 AND TIMESTAMPDIFF(SECOND, user_heartbeat, NOW()) <= 60 AND user_designation = 'inventory' THEN 1 ELSE 0 END) AS OnlineInventory,
                                    SUM(CASE WHEN user_status = 0 OR user_heartbeat is NULL OR (user_status = 1 AND TIMESTAMPDIFF(SECOND, user_heartbeat, NOW()) > 60) THEN 1 ELSE 0 END) AS InactiveUser
                                FROM tbl_user;";

                MySqlCommand cmd = new MySqlCommand(query, connection);
                MySqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    int InactiveUser = reader.GetInt32("InactiveUser");

                    int OnlineAdmin = reader.GetInt32("OnlineAdmin");
                    int OnlineTechnician = reader.GetInt32("OnlineTechnician");
                    int OnlineAccounting = reader.GetInt32("OnlineAccounting");
                    int OnlineCS = reader.GetInt32("OnlineCS");
                    int OnlineInventory = reader.GetInt32("OnlineInventory");

                    lblInactiveUser.Text = InactiveUser.ToString();

                    lblAdmin.Text = OnlineAdmin.ToString();
                    lblTechnical.Text = OnlineTechnician.ToString();
                    lblAccounting.Text = OnlineAccounting.ToString();
                    lblCustomerService.Text = OnlineCS.ToString();
                    lblInventory.Text = OnlineInventory.ToString();
                }
                reader.Close();
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
