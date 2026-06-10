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
    public partial class frmRequest : Form
    {
        Form parent_form;
        public frmRequest(Form form)
        {
            InitializeComponent();
            this.parent_form = form;
        }

        private void frmRequest_Load(object sender, EventArgs e)
        {
            if (parent_form is frmTechnical)
            {
                getRequestStatus(2);
            }
            if (parent_form is frmCS)
            {
                getRequestStatus(5);
            }
            if (parent_form is frmTechnical)
            {
                getRequestStatus(4);
            }
            if (parent_form is frmAccounting)
            {
                getRequestStatus(3);
            }
            if (parent_form is frmInventory)
            {
                getRequestStatus(6);
            }
            if (parent_form is frmTechnical)
            {
                getRequestStatus(7);
            }
            if (parent_form is frmAccounting)
            {
                getRequestStatus(8);
            }
        }

        public void getRequestStatus(int status)
        {
            MySqlConnection connection = MyConnectionString.mysql_connection();
            try
            {
                connection.Open();

                string query = $@"SELECT
                                    account.account_num,
                                    account.account_type,
                                    address.add_line,
                                    address.add_barangay,
                                    address.add_municipality,
                                    address.add_province,
                                    plan.plan_type,
                                    plan.plan_fiber,
                                    plan.plan_speed,
                                    customer.cust_lastname,
                                    customer.cust_firstname,
                                    customer.cust_middlename
                                  FROM
                                    tbl_account as account
                                    INNER JOIN tbl_address as address ON account.add_id = address.add_id
                                    INNER JOIN tbl_service_plan as plan ON account.plan_id = plan.plan_id
                                    INNER JOIN tbl_customer as customer ON address.cust_id = customer.cust_id
                                  WHERE
                                    account.account_status = {status} OR
                                    account.account_update_status = {status};";
                MySqlCommand cmd = new MySqlCommand(query, connection);
                using (MySqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            int account_num = Convert.ToInt32(reader["account_num"]);
                            string account_type = reader["account_type"].ToString();
                            string add_line = reader["add_line"].ToString();
                            string add_barangay = reader["add_barangay"].ToString();
                            string add_municipality = reader["add_municipality"].ToString();
                            string add_province = reader["add_province"].ToString();
                            string plan_type = reader["plan_type"].ToString();
                            string plan_fiber = "FIBER";
                            int plan_fiber_bool = Convert.ToInt32(reader["plan_fiber"]);
                            if (plan_fiber_bool == 1)
                            {
                                plan_fiber = "FIBER";
                            }
                            else
                            {
                                plan_fiber = "DSL";
                            }
                            string plan_speed = reader["plan_speed"].ToString();
                            string cust_lastname = reader["cust_lastname"].ToString();
                            string cust_firstname = reader["cust_firstname"].ToString();
                            string cust_middlename = reader["cust_middlename"].ToString();

                            RequestPanel requestPanel = new RequestPanel();
                            string request_type = null;
                            if (status == 5)
                            {
                                request_type = "Need a verification";
                            }
                            if (status == 4)
                            {
                                request_type = "Need to survey site";
                            }
                            if (status == 3)
                            {
                                request_type = "Customer billing statement";
                            }
                            if (status == 6)
                            {
                                request_type = "Need to create requisition";
                            }
                            if (status == 7)
                            {
                                request_type = "Service Order Completion";
                            }
                            if (status == 8)
                            {
                                request_type = "Customer final billing statement";
                            }
                            if (status == 2)
                            {
                                request_type = "Service Activation";
                            }
                            requestPanel.Width = requestLayoutPanel.ClientSize.Width;
                            requestPanel.requestType = request_type;
                            requestPanel.accountType = account_type;
                            requestPanel.accountNum = $"#{10000 + account_num}";
                            requestPanel.locationString = $"{add_line}, {add_barangay}, {add_municipality}, {add_province}";
                            requestPanel.planType = plan_type;
                            requestPanel.planFiber = plan_fiber;
                            requestPanel.planSpeed = plan_speed;
                            requestPanel.customerFullName = $"{cust_lastname}, {cust_firstname} {cust_middlename}";
                            requestPanel.parent_form = this;
                            requestLayoutPanel.Controls.Add(requestPanel);
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

        public void btnReset_Click(object sender, EventArgs e)
        {
            removeAllControls();
            frmRequest_Load(null, null);
        }
    }
}
