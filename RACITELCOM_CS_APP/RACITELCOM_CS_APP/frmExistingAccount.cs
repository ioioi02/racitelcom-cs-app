using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Text.RegularExpressions;
using MySql.Data.MySqlClient;

namespace RACITELCOM_CS_APP
{
    public partial class frmExistingAccount : Form
    {
        Form parent_form;
        public frmExistingAccount(Form form)
        {
            InitializeComponent();
            this.parent_form = form;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            if (parent_form is frmAppForm)
            {
                ((frmAppForm)parent_form).unCheckExistingAccount();
                ((frmAppForm)parent_form).account_type = null;
                ((frmAppForm)parent_form).account_num = null;
            }
            if (parent_form is frmAppFormView)
            {
                ((frmAppFormView)parent_form).unCheckExistingAccount();
                ((frmAppFormView)parent_form).account_type = null;
                ((frmAppFormView)parent_form).account_num = null;
            }
            // this.Dispose();
            this.Close();
        }

        private void btnChangePlan_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtAccountNum.Text))
            {
                if (!IsValidAccountNumber(txtAccountNum.Text))
                {
                    MessageBox.Show("Ensure you enter a valid account number.", "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return;
                }

                string account_num_trim = txtAccountNum.Text.TrimStart('#');
                int account_num_ = int.Parse(account_num_trim);
                int account_num = account_num_ - 10000;

                if (!CheckUserInDatabase(account_num, 1))
                {
                    MessageBox.Show("The account number is either inactive or does not exist.", "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return;
                }

                if (parent_form is frmAppForm)
                {
                    ((frmAppForm)parent_form).account_type = "change plan";
                    ((frmAppForm)parent_form).account_num = account_num;
                }
                if (parent_form is frmAppFormView)
                {
                    ((frmAppFormView)parent_form).account_type = "change plan";
                    ((frmAppFormView)parent_form).account_num = account_num;
                }
                // this.Dispose();
                this.Close();
            }
            else
            {
                MessageBox.Show("Ensure you enter a valid account number.", "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void btnReconnection_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtAccountNum.Text))
            {
                if (!IsValidAccountNumber(txtAccountNum.Text))
                {
                    MessageBox.Show("Ensure you enter a valid account number.", "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return;
                }

                string account_num_trim = txtAccountNum.Text.TrimStart('#');
                int account_num_ = int.Parse(account_num_trim);
                int account_num = account_num_ - 10000;

                if (!CheckUserInDatabase(account_num, 0))
                {
                    MessageBox.Show("The account number is either active or does not exist.", "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return;
                }

                if (parent_form is frmAppForm)
                {
                    ((frmAppForm)parent_form).account_type = "reconnection";
                    ((frmAppForm)parent_form).account_num = account_num;
                }
                if (parent_form is frmAppFormView)
                {
                    ((frmAppFormView)parent_form).account_type = "reconnection";
                    ((frmAppFormView)parent_form).account_num = account_num;
                }
                // this.Dispose();
                this.Close();
            }
            else
            {
                MessageBox.Show("Ensure you enter a valid account number.", "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void btnInfoUpdate_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtAccountNum.Text))
            {
                if (!IsValidAccountNumber(txtAccountNum.Text))
                {
                    MessageBox.Show("Ensure you enter a valid account number.", "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return;
                }

                string account_num_trim = txtAccountNum.Text.TrimStart('#');
                int account_num_ = int.Parse(account_num_trim);
                int account_num = account_num_ - 10000;

                if (!CheckUserInDatabase(account_num, 1))
                {
                    MessageBox.Show("The account number is either inactive or does not exist.", "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return;
                }

                if (parent_form is frmAppForm)
                {
                    ((frmAppForm)parent_form).account_type = "information update";
                    ((frmAppForm)parent_form).account_num = account_num;
                }
                if (parent_form is frmAppFormView)
                {
                    ((frmAppFormView)parent_form).account_type = "information update";
                    ((frmAppFormView)parent_form).account_num = account_num;
                }
                // this.Dispose();
                this.Close();
            }
            else
            {
                MessageBox.Show("Ensure you enter a valid account number.", "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        static bool IsValidAccountNumber(string account_num)
        {
            string pattern = @"^#\d{5}$";
            return Regex.IsMatch(account_num, pattern);
        }

        static bool CheckUserInDatabase(int account_num, int account_status)
        {
            MySqlConnection connection = MyConnectionString.mysql_connection();
            try
            {
                connection.Open();

                string query = @"SELECT COUNT(*) FROM tbl_account WHERE account_num = @account_num AND account_status = @account_status";
                using (MySqlCommand cmd = new MySqlCommand(query, connection))
                {
                    cmd.Parameters.AddWithValue("@account_num", account_num);
                    cmd.Parameters.AddWithValue("@account_status", account_status);

                    int count = Convert.ToInt32(cmd.ExecuteScalar());
                    return count > 0;
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
            return false;
        }
    }
}
