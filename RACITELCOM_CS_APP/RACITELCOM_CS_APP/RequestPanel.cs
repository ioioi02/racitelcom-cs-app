using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace RACITELCOM_CS_APP
{
    public partial class RequestPanel : UserControl
    {
        public string accountType
        {
            get { return lblAccountType.Text; }
            set { lblAccountType.Text = value.ToUpper(); }
        }

        public string requestType
        {
            get { return lblRequestType.Text; }
            set { lblRequestType.Text = value.ToUpper(); }
        }
        public string accountNum
        {
            get { return lblAccountNum.Text; }
            set { lblAccountNum.Text = value.ToUpper(); }
        }

        public string locationString
        {
            get { return lblLocation.Text; }
            set { lblLocation.Text = value.ToUpper(); }
        }

        public string planType
        {
            get { return lblPlanType.Text; }
            set { lblPlanType.Text = value.ToUpper(); }
        }

        public string planFiber
        {
            get { return lblFiber.Text; }
            set { lblFiber.Text = value.ToUpper(); }
        }

        public string planSpeed
        {
            get { return lblPlanSpeed.Text; }
            set { lblPlanSpeed.Text = value.ToUpper(); }
        }

        public string customerFullName
        {
            get { return lblFullName.Text; }
            set { lblFullName.Text = value.ToUpper(); }
        }

        public Form parent_form { get; set; }

        public int account_num_;

        public RequestPanel()
        {
            InitializeComponent();
        }

        private void RequestPanel_Load(object sender, EventArgs e)
        {
            if (requestType == "NEED TO SURVEY SITE")
            {
                btnView.Text = "SURVEY";
                panelColor.BackColor = Color.FromArgb(255, 255, 128);
            }
            if (requestType == "CUSTOMER BILLING STATEMENT")
            {
                btnView.Text = "PAYMENT";
                panelColor.BackColor = Color.FromArgb(255, 128, 128);
            }
            if (requestType == "NEED TO CREATE REQUISITION")
            {
                btnView.Text = "CREATE";
                panelColor.BackColor = Color.FromArgb(128, 255, 255);
            }
            if (requestType == "SERVICE ORDER COMPLETION")
            {
                btnView.Text = "COMPLETE";
                panelColor.BackColor = Color.FromArgb(128, 128, 255);
            }
            if (requestType == "CUSTOMER FINAL BILLING STATEMENT")
            {
                btnView.Text = "PAYMENT";
                panelColor.BackColor = Color.Silver;
            }
            if (requestType == "SERVICE ACTIVATION")
            {
                btnView.Text = "COMPLETE";
                setDifferentFormat(this);
                panelUpper.BackColor = Color.FromArgb(17, 7, 100);
                panelLower.BackColor = Color.FromArgb(17, 7, 100);
                btnView.BackColor = Color.FromArgb(17, 7, 100);
                btnView.ForeColor = Color.White;
            }
        }

        private void setDifferentFormat(Control control)
        {
            foreach (Control ctrl in control.Controls)
            {
                if (ctrl is Panel)
                {
                    Panel panel = (Panel)ctrl;
                    panel.BackColor = Color.White;
                }
                if (ctrl is Label)
                {
                    Label label = (Label)ctrl;
                    label.ForeColor = Color.FromArgb(17, 7, 100);
                }
                if (ctrl.HasChildren)
                {
                    setDifferentFormat(ctrl);
                }
            }
        }

        private void convertAccountNum()
        {
            if (!string.IsNullOrEmpty(accountNum))
            {
                string account_num_trim = accountNum.TrimStart('#');
                account_num_ = int.Parse(account_num_trim);
                account_num_ = account_num_ - 10000;
            }
        }

        private void btnView_Click(object sender, EventArgs e)
        {
            convertAccountNum();

            if (requestType == "NEED A VERIFICATION")
            {
                frmAppFormView appFormView = new frmAppFormView();
                appFormView.parent_form = parent_form;
                appFormView.account_num_string = accountNum;
                appFormView.ShowDialog();
            }
            if (requestType == "NEED TO SURVEY SITE")
            {
                frmAppForm2 appForm2 = new frmAppForm2();
                appForm2.parent_form = parent_form;
                appForm2.account_num = account_num_;
                appForm2.ShowDialog();
            }
            if (requestType == "CUSTOMER BILLING STATEMENT")
            {
                frmAppForm3 appForm3 = new frmAppForm3();
                appForm3.parent_form = parent_form;
                appForm3.account_num = account_num_;
                appForm3.isServiceActivation = false;
                appForm3.ShowDialog();
            }
            if (requestType == "SERVICE ACTIVATION")
            {
                frmAppForm3 appForm3 = new frmAppForm3();
                appForm3.parent_form = parent_form;
                appForm3.account_num = account_num_;
                appForm3.isServiceActivation = true;
                appForm3.ShowDialog();
            }
            if (requestType == "NEED TO CREATE REQUISITION")
            {
                frmRequisition req = new frmRequisition();
                req.parent_form = parent_form;
                req.account_num = account_num_;
                req.ShowDialog();
            }
            if (requestType == "SERVICE ORDER COMPLETION")
            {
                frmServiceOrder so = new frmServiceOrder();
                so.parent_form = parent_form;
                so.account_num = account_num_;
                so.ShowDialog();
            }
            if (requestType == "CUSTOMER FINAL BILLING STATEMENT")
            {
                frmFinalBilling f_billing = new frmFinalBilling();
                f_billing.parent_form = parent_form;
                f_billing.account_num = account_num_;
                f_billing.ShowDialog();
            }
            if (requestType == "SERVICE ACTIVATION")
            {
                frmAppForm3 appForm3 = new frmAppForm3();
                appForm3.parent_form = parent_form;
                appForm3.account_num = account_num_;
                appForm3.isServiceActivation = true;
                appForm3.ShowDialog();
            }
        }

        private void btnDrop_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Are you sure you want to DROP or DELETE the request?", "", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

            MessageBox.Show("This section is still under development.", "", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            return;

            if (result == DialogResult.Yes)
            {
                convertAccountNum();

                MySqlConnection connection = MyConnectionString.mysql_connection();
                connection.Open();
                using (MySqlTransaction transaction = connection.BeginTransaction())
                {
                    try
                    {
                        string query = $@"
                                        DELETE FROM tbl_spouse
                                        WHERE cust_id IN (
                                            SELECT customer.cust_id
                                            FROM tbl_customer AS customer
                                            INNER JOIN tbl_address AS address ON customer.cust_id = address.cust_id
                                            INNER JOIN tbl_account AS account ON address.add_id = account.add_id
                                            WHERE account.account_num = {account_num_});

                                        DELETE FROM tbl_employment
                                        WHERE cust_id IN (
                                            SELECT customer.cust_id
                                            FROM tbl_customer AS customer
                                            INNER JOIN tbl_address AS address ON customer.cust_id = address.cust_id
                                            INNER JOIN tbl_account AS account ON address.add_id = account.add_id
                                            WHERE account.account_num = {account_num_});

                                        DELETE FROM tbl_business
                                        WHERE add_id IN (
                                            SELECT address.add_id
                                            FROM tbl_address AS address
                                            INNER JOIN tbl_account AS account ON address.add_id = account.add_id
                                            WHERE account.account_num = {account_num_});

                                        DELETE FROM tbl_account WHERE account_num = {account_num_};

                                        DELETE FROM tbl_service_plan
                                        WHERE plan_id IN (
                                            SELECT plan_id FROM tbl_account WHERE account_num = {account_num_});

                                        DELETE FROM tbl_address
                                        WHERE add_id IN (
                                            SELECT address.add_id
                                            FROM tbl_address AS address
                                            INNER JOIN tbl_account AS account ON address.add_id = account.add_id
                                            WHERE account.account_num = {account_num_});

                                        DELETE FROM tbl_customer
                                        WHERE cust_id IN (
                                            SELECT customer.cust_id
                                            FROM tbl_customer AS customer
                                            INNER JOIN tbl_address AS address ON customer.cust_id = address.cust_id
                                            INNER JOIN tbl_account AS account ON address.add_id = account.add_id
                                            WHERE account.account_num = {account_num_});";
                        using (MySqlCommand command = new MySqlCommand(query, connection, transaction))
                        {
                            command.ExecuteNonQuery();
                        }
                        transaction.Commit();
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                        // MessageBox.Show($"Error: {ex.ToString()}", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        MessageBox.Show("An error has occured.", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    finally
                    {
                        connection.Close();
                    }
                }
                this.Dispose();
            }      
        }
    }
}
