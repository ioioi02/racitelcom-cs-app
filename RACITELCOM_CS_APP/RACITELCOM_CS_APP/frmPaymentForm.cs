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
    public partial class frmPaymentForm : Form
    {
        public int account_num { get; set; }
        public int nextAutoIncrementValue { get; set; }

        DateTime? account_due_date = null;
        DateTime? next_due_date = null;

        frmAccountingPortal accounting_portal;
        public frmPaymentForm(frmAccountingPortal form)
        {
            InitializeComponent();
            accounting_portal = form;
            setToolTip();
        }

        private void frmPaymentForm_Load(object sender, EventArgs e)
        {
            MySqlConnection connection = MyConnectionString.mysql_connection();
            try
            {
                connection.Open();

                string query = @"SELECT AUTO_INCREMENT
                                   FROM information_schema.TABLES
                                   WHERE TABLE_SCHEMA = @databaseName
                                   AND TABLE_NAME = @tableName;";
                MySqlCommand cmd = new MySqlCommand(query, connection);
                cmd.Parameters.AddWithValue("@databaseName", "db_cs_app");
                cmd.Parameters.AddWithValue("@tableName", "tbl_billing");
                nextAutoIncrementValue = Convert.ToInt32(cmd.ExecuteScalar());
                txtORNum.Text = $"OR-{80000 + nextAutoIncrementValue}";
                txtORNum.ForeColor = Color.FromArgb(17, 7, 100);
            }
            catch (Exception ex)
            {
                // MessageBox.Show($"Error: {ex.ToString()}", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);  // an error has occured
                MessageBox.Show("An error has occured.", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                connection.Close();
            }
        }

        private void setToolTip()
        {
            ToolTip toolTip = new ToolTip();

            toolTip.SetToolTip(txtAmountPaid, txtAmountPaid.Tag.ToString());
            toolTip.SetToolTip(txtBalance, txtBalance.Tag.ToString());
            toolTip.SetToolTip(txtORNum, txtORNum.Tag.ToString());
            toolTip.SetToolTip(txtBankCheckNum, txtBankCheckNum.Tag.ToString());
            toolTip.SetToolTip(txtSalesAgent, txtSalesAgent.Tag.ToString());
            toolTip.SetToolTip(txtReceivedBy, txtReceivedBy.Tag.ToString());
            toolTip.SetToolTip(txtVerifiedBy, txtVerifiedBy.Tag.ToString());
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            // this.Dispose();
            this.Close();
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
        private void checkCash_Click(object sender, EventArgs e)
        {
            if (checkCheck.Checked == true)
            {
                checkCheck.Checked = false;
            }
        }

        private void checkCheck_Click(object sender, EventArgs e)
        {
            if (checkCash.Checked == true)
            {
                checkCash.Checked = false;
            }
        }

        private void btnSubmit_Click(object sender, EventArgs e)
        {
            if ((checkCash.Checked == false && checkCheck.Checked == false) || txtAmountPaid.ForeColor == Color.Gray || txtBalance.ForeColor == Color.Gray || txtORNum.ForeColor == Color.Gray ||
                 txtSalesAgent.ForeColor == Color.Gray || txtReceivedBy.ForeColor == Color.Gray || txtVerifiedBy.ForeColor == Color.Gray)
            {
                MessageBox.Show("Please ensure that all required fields are completed.", "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            try
            {
                int var1 = txtAmountPaid.ForeColor != Color.Gray ? int.Parse(txtAmountPaid.Text) : 0;
                int var2 = txtBalance.ForeColor != Color.Gray ? int.Parse(txtBalance.Text) : 0;
            }
            catch
            {
                MessageBox.Show("Please enter a valid integer in the currency fields.", "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            string payment_type = null;
            if (checkCash.Checked == true)
            {
                payment_type = "cash";
            }
            if (checkCheck.Checked == true)
            {
                payment_type = "check";
            }

            MySqlConnection connection = MyConnectionString.mysql_connection();
            try
            {
                connection.Open();

                string query1 = @"INSERT INTO tbl_billing (account_num, bill_payment_type, bill_amount_paid, bill_balance, bill_check_num, bill_sales_agent, bill_sales_agent_date, bill_received_by, bill_received_by_date, bill_verified_by, bill_verified_by_date, bill_transac_type)
                                 VALUES (@account_num, @bill_payment_type, @bill_amount_paid, @bill_balance, @bill_check_num, @bill_sales_agent, @bill_sales_agent_date, @bill_received_by, @bill_received_by_date, @bill_verified_by, @bill_verified_by_date, @bill_transac_type);";
                using (MySqlCommand cmd1 = new MySqlCommand(query1, connection))
                {
                    cmd1.Parameters.AddWithValue("@account_num", account_num);
                    cmd1.Parameters.AddWithValue("@bill_payment_type", payment_type);
                    cmd1.Parameters.AddWithValue("@bill_amount_paid", Convert.ToInt32(txtAmountPaid.Text));
                    cmd1.Parameters.AddWithValue("@bill_balance", Convert.ToInt32(txtBalance.Text));
                    cmd1.Parameters.AddWithValue("@bill_check_num", txtBankCheckNum.ForeColor != Color.Gray ? txtBankCheckNum.Text : null);
                    cmd1.Parameters.AddWithValue("@bill_sales_agent", txtSalesAgent.Text.ToLower());
                    cmd1.Parameters.AddWithValue("@bill_sales_agent_date", dateSalesAgent.Value);
                    cmd1.Parameters.AddWithValue("@bill_received_by", txtReceivedBy.Text.ToLower());
                    cmd1.Parameters.AddWithValue("@bill_received_by_date", dateReceivedBy.Value);
                    cmd1.Parameters.AddWithValue("@bill_verified_by", txtVerifiedBy.Text.ToLower());
                    cmd1.Parameters.AddWithValue("@bill_verified_by_date", dateVerifiedBy.Value);
                    cmd1.Parameters.AddWithValue("@bill_transac_type", "MONTHLY BILL");  // note

                    cmd1.ExecuteNonQuery();

                    string query2 = @"SELECT account_due_date FROM tbl_account WHERE account_num = @account_num;";
                    MySqlCommand cmd2 = new MySqlCommand(query2, connection);
                    cmd2.Parameters.AddWithValue("@account_num", account_num);
                    using (MySqlDataReader reader = cmd2.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            account_due_date = reader.IsDBNull(reader.GetOrdinal("account_due_date")) ? (DateTime?)null : Convert.ToDateTime(reader["account_due_date"]);
                            next_due_date = account_due_date.Value.AddMonths(1);
                        }
                        reader.Close();

                        string query3 = @"UPDATE tbl_account
                                            SET account_last_due_date = @account_last_due_date,
                                                account_due_date = @account_due_date
                                            WHERE account_num = @account_num;";
                        using (MySqlCommand cmd3 = new MySqlCommand(query3, connection))
                        {
                            cmd3.Parameters.AddWithValue("@account_last_due_date", account_due_date);
                            cmd3.Parameters.AddWithValue("@account_due_date", next_due_date);
                            cmd3.Parameters.AddWithValue("@account_num", account_num);

                            cmd3.ExecuteNonQuery();
                        }
                    }
                }
                MessageBox.Show("Customer billing successfully completed.", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
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

            // this.Dispose();
            this.Close();

            accounting_portal.btnReset_Click(null, null);

            // test only //

            string username = TCPClient.Instance.username;
            string log_type = null;
            log_type = $"submit a customer billing statement for MONTHLY BILL with the account number #{10000 + account_num} and ORnumber OR-{80000 + nextAutoIncrementValue}";

            InsertLogs inserLogs = new InsertLogs();
            string log_result = inserLogs.insertLogs(username, log_type);
            if (log_result != "0")
            {
                // MessageBox.Show($"Error: {log_result}", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                MessageBox.Show("An error has occured.", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            TCPClient.Instance.SendMessage($"{log_type}.");

            // test only //
        }
    }
}
