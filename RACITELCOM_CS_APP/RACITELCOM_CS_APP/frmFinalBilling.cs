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
    public partial class frmFinalBilling : Form
    {
        public int? account_num { get; set; }
        public Form parent_form { get; set; }

        public frmFinalBilling()
        {
            InitializeComponent();
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

        private void btnSubmit_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtAccountNum.Text) || txtActualMSF.ForeColor == Color.Gray || txtStartofBilling.ForeColor == Color.Gray || txtVerifiedBy.ForeColor == Color.Gray || txtRecordedBy.ForeColor == Color.Gray)
            {
                MessageBox.Show("Please ensure all required fields are filled out.", "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            DateTime? start_of_billing = parseDateString(txtStartofBilling.Text);
            if (!Regex.IsMatch(txtActualMSF.Text, @"^\d+$") || start_of_billing == null)
            {
                MessageBox.Show("Please ensure MSF is in digit and start of billing is in correct date format(MM/DD/YYYY).", "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            MySqlConnection connection = MyConnectionString.mysql_connection();
            try
            {
                connection.Open();

                string checkQuery = @"SELECT COUNT(*) FROM tbl_billing_account WHERE bilaccount_num = @bilaccount_num";

                MySqlCommand checkCmd = new MySqlCommand(checkQuery, connection);
                checkCmd.Parameters.AddWithValue("@bilaccount_num", txtAccountNum.Text.ToUpper());

                int count = Convert.ToInt32(checkCmd.ExecuteScalar());
                if (count <= 0)
                {
                    string query = @"INSERT INTO tbl_billing_account (bilaccount_num, account_num, bilaccount_start, bilaccount_actual_msf, bilaccount_verified_by, bilaccount_verified_by_date, bilaccount_recorded_by, bilaccount_recorded_by_date)
                                    VALUES (@bilaccount_num, @account_num, @bilaccount_start, @bilaccount_actual_msf, @bilaccount_verified_by, @bilaccount_verified_by_date, @bilaccount_recorded_by, @bilaccount_recorded_by_date);";
                    using (MySqlCommand cmd = new MySqlCommand(query, connection))
                    {
                        cmd.Parameters.AddWithValue("@bilaccount_num", txtAccountNum.Text.ToUpper());
                        cmd.Parameters.AddWithValue("@account_num", account_num);
                        cmd.Parameters.AddWithValue("@bilaccount_start", start_of_billing);
                        cmd.Parameters.AddWithValue("@bilaccount_actual_msf", int.Parse(txtActualMSF.Text));
                        cmd.Parameters.AddWithValue("@bilaccount_verified_by", txtVerifiedBy.Text.ToLower());
                        cmd.Parameters.AddWithValue("@bilaccount_verified_by_date", dateVerifiedBy.Value);
                        cmd.Parameters.AddWithValue("@bilaccount_recorded_by", txtRecordedBy.Text.ToLower());
                        cmd.Parameters.AddWithValue("@bilaccount_recorded_by_date", dateRecordedBy.Value);

                        cmd.ExecuteNonQuery();

                        string query_status = "UPDATE tbl_account SET account_status = 1 WHERE account_num = @account_num";
                        using (MySqlCommand cmd12 = new MySqlCommand(query_status, connection))
                        {
                            cmd12.Parameters.AddWithValue("@account_num", account_num);

                            cmd12.ExecuteNonQuery();

                            MessageBox.Show("Recorded successfully.", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }

                        // this.Dispose();
                        this.Close();

                        if (parent_form is frmRequestAdmin)
                        {
                            ((frmRequestAdmin)parent_form).btnReset_Click(null, null);
                        }
                        if (parent_form is frmRequest)
                        {
                            ((frmRequest)parent_form).btnReset_Click(null, null);
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Invalid account number or it's already exist.", "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
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

        private void btnRequestInfo_Click(object sender, EventArgs e)
        {

        }

        private DateTime? parseDateString(string dateString)
        {
            DateTime parsedDate;
            if (DateTime.TryParseExact(dateString, "MM/dd/yyyy", null, System.Globalization.DateTimeStyles.None, out parsedDate))
            {
                return parsedDate;
            }
            else if (DateTime.TryParseExact(dateString, "MM/d/yyyy", null, System.Globalization.DateTimeStyles.None, out parsedDate))
            {
                return parsedDate;
            }
            else if (DateTime.TryParseExact(dateString, "M/dd/yyyy", null, System.Globalization.DateTimeStyles.None, out parsedDate))
            {
                return parsedDate;
            }
            else if (DateTime.TryParseExact(dateString, "M/d/yyyy", null, System.Globalization.DateTimeStyles.None, out parsedDate))
            {
                return parsedDate;
            }
            else
            {
                return null;
            }
        }

    }
}
