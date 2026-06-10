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
    public partial class frmAppForm2 : Form
    {
        public int account_num { get; set; }
        public int survey_id { get; set; }

        public Form parent_form { get; set; }

        private frmCustomerInfo customerInfo;

        public frmAppForm2()
        {
            InitializeComponent();
            setToolTip();
        }

        private void setToolTip()
        {
            ToolTip toolTip = new ToolTip();

            toolTip.SetToolTip(txtDP, txtDP.Tag.ToString());
            toolTip.SetToolTip(txtFTTHOLT, txtFTTHOLT.Tag.ToString());
            toolTip.SetToolTip(txtSpam, txtSpam.Tag.ToString());
            toolTip.SetToolTip(txtPclamp, txtPclamp.Tag.ToString());
            toolTip.SetToolTip(txtSclamp, txtSclamp.Tag.ToString());
            toolTip.SetToolTip(txtSpanclamp, txtSpanclamp.Tag.ToString());
            toolTip.SetToolTip(txtJacketed, txtJacketed.Tag.ToString());
            toolTip.SetToolTip(txtDropwire, txtDropwire.Tag.ToString());
            toolTip.SetToolTip(txtOthers, txtOthers.Tag.ToString());
            toolTip.SetToolTip(txtSurveyRemarks, txtSurveyRemarks.Tag.ToString());
            toolTip.SetToolTip(txtMSAN, txtMSAN.Tag.ToString());
            toolTip.SetToolTip(txtPort1, txtPort1.Tag.ToString());
            toolTip.SetToolTip(txtPrimary, txtPrimary.Tag.ToString());
            toolTip.SetToolTip(txtOLT, txtOLT.Tag.ToString());
            toolTip.SetToolTip(txtLCP, txtLCP.Tag.ToString());
            toolTip.SetToolTip(txtNAP, txtNAP.Tag.ToString());
            toolTip.SetToolTip(txtPort2, txtPort2.Tag.ToString());
            toolTip.SetToolTip(txtStaticIP1, txtStaticIP1.Tag.ToString());
            toolTip.SetToolTip(txtStaticIP2, txtStaticIP2.Tag.ToString());
            toolTip.SetToolTip(txtTel1, txtTel1.Tag.ToString());
            toolTip.SetToolTip(txtTel2, txtTel2.Tag.ToString());
            toolTip.SetToolTip(txtInsideRemarks, txtInsideRemarks.Tag.ToString());
            toolTip.SetToolTip(txtSurveyBy, txtSurveyBy.Tag.ToString());
            toolTip.SetToolTip(txtSurveyChecked, txtSurveyChecked.Tag.ToString());
            toolTip.SetToolTip(txtInsideChecked, txtInsideChecked.Tag.ToString());
            toolTip.SetToolTip(txtNote, txtNote.Tag.ToString());
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

        private void btnCancel_Click(object sender, EventArgs e)
        {
            // this.Dispose();
            this.Close();
        }

        private void btnSubmit_Click(object sender, EventArgs e)
        {
            if (txtDP.ForeColor == Color.Gray || txtFTTHOLT.ForeColor == Color.Gray || txtSpam.ForeColor == Color.Gray || txtSurveyBy.ForeColor == Color.Gray || txtSurveyChecked.ForeColor == Color.Gray || txtInsideChecked.ForeColor == Color.Gray ||
               ((txtPort2.ForeColor == Color.Gray || txtOLT.ForeColor == Color.Gray || txtLCP.ForeColor == Color.Gray || txtNAP.ForeColor == Color.Gray) && (txtPort1.ForeColor == Color.Gray || txtMSAN.ForeColor == Color.Gray || txtPrimary.ForeColor == Color.Gray)))
            {
                MessageBox.Show("Please ensure all required fields are filled out, including either DSL or FTTH.", "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            // survey
            string survey_dp = txtDP.ForeColor != Color.Gray ? txtDP.Text : null;
            string survey_FTTH_etc = txtFTTHOLT.ForeColor != Color.Gray ? txtFTTHOLT.Text : null;
            string survey_spam = txtSpam.ForeColor != Color.Gray ? txtSpam.Text : null;
            string survey_p_clamp = txtPclamp.ForeColor != Color.Gray ? txtPclamp.Text : null;
            string survey_s_clamp = txtSclamp.ForeColor != Color.Gray ? txtSclamp.Text : null;
            string survey_span_clamp = txtSpanclamp.ForeColor != Color.Gray ? txtSpanclamp.Text : null;
            string survey_jacket_wire = txtJacketed.ForeColor != Color.Gray ? txtJacketed.Text : null;
            string survey_dropwire = txtDropwire.ForeColor != Color.Gray ? txtDropwire.Text : null;
            string survey_others = txtOthers.ForeColor != Color.Gray ? txtOthers.Text : null;
            string survey_remarks = txtSurveyRemarks.ForeColor != Color.Gray ? txtSurveyRemarks.Text : null;
            string survey_by_name = txtSurveyBy.ForeColor != Color.Gray ? txtSurveyBy.Text : null;
            DateTime? survey_by_date = dateSurveyBy.Value;
            string survey_checked_name = txtSurveyChecked.ForeColor != Color.Gray ? txtSurveyChecked.Text : null;
            DateTime? survey_checked_date = dateSurveyChecked.Value; ;
            string survey_note = txtNote.ForeColor != Color.Gray ? txtNote.Text : null;

            // inside plant
            string inside_msan = txtMSAN.ForeColor != Color.Gray ? txtMSAN.Text : null;
            string inside_primary = txtPrimary.ForeColor != Color.Gray ? txtPrimary.Text : null;
            string inside_olt = txtOLT.ForeColor != Color.Gray ? txtOLT.Text : null;
            string inside_lcp = txtLCP.ForeColor != Color.Gray ? txtLCP.Text : null;
            string inside_nap = txtNAP.ForeColor != Color.Gray ? txtNAP.Text : null;
            string inside_port1 = txtPort1.ForeColor != Color.Gray ? txtPort1.Text : null;
            string inside_port2 = txtPort2.ForeColor != Color.Gray ? txtPort2.Text : null;
            string inside_static1 = txtStaticIP1.ForeColor != Color.Gray ? txtStaticIP1.Text : null;
            string inside_static2 = txtStaticIP2.ForeColor != Color.Gray ? txtStaticIP2.Text : null;
            string inside_tel1 = txtTel1.ForeColor != Color.Gray ? txtTel1.Text : null;
            string inside_tel2 = txtTel2.ForeColor != Color.Gray ? txtTel2.Text : null;
            string inside_remarks = txtInsideRemarks.ForeColor != Color.Gray ? txtInsideRemarks.Text : null;
            string inside_checked_name = txtInsideChecked.ForeColor != Color.Gray ? txtInsideChecked.Text : null;
            DateTime? inside_checked_date = dateInsideChecked.Value;

            // saved to database
            MySqlConnection connection = MyConnectionString.mysql_connection();
            try
            {
                connection.Open();

                string query1 = @"INSERT INTO tbl_survey (account_num, survey_dp, survey_FTTH_OLT_LCP_NAP, survey_spam, survey_p_clamp, survey_s_clamp, survey_span_clamp, survey_jacket_wire, survey_dropwire, survey_others, survey_remarks, survey_by_name, survey_by_date, survey_checked_name, survey_checked_date, survey_note)
                                 VALUES (@account_num, @survey_dp, @survey_FTTH_OLT_LCP_NAP, @survey_spam, @survey_p_clamp, @survey_s_clamp, @survey_span_clamp, @survey_jacket_wire, @survey_dropwire, @survey_others, @survey_remarks, @survey_by_name, @survey_by_date, @survey_checked_name, @survey_checked_date, @survey_note);
                                 SELECT LAST_INSERT_ID();";
                using (MySqlCommand cmd1 = new MySqlCommand(query1, connection))
                {
                    cmd1.Parameters.AddWithValue("@account_num", account_num);
                    cmd1.Parameters.AddWithValue("@survey_dp", survey_dp);
                    cmd1.Parameters.AddWithValue("@survey_FTTH_OLT_LCP_NAP", survey_FTTH_etc);
                    cmd1.Parameters.AddWithValue("@survey_spam", survey_spam);
                    cmd1.Parameters.AddWithValue("@survey_p_clamp", survey_p_clamp);
                    cmd1.Parameters.AddWithValue("@survey_s_clamp", survey_s_clamp);
                    cmd1.Parameters.AddWithValue("@survey_span_clamp", survey_span_clamp);
                    cmd1.Parameters.AddWithValue("@survey_jacket_wire", survey_jacket_wire);
                    cmd1.Parameters.AddWithValue("@survey_dropwire", survey_dropwire);
                    cmd1.Parameters.AddWithValue("@survey_others", survey_others);
                    cmd1.Parameters.AddWithValue("@survey_remarks", survey_remarks);
                    cmd1.Parameters.AddWithValue("@survey_by_name", survey_by_name);
                    cmd1.Parameters.AddWithValue("@survey_by_date", survey_by_date);
                    cmd1.Parameters.AddWithValue("@survey_checked_name", survey_checked_name);
                    cmd1.Parameters.AddWithValue("@survey_checked_date", survey_checked_date);
                    cmd1.Parameters.AddWithValue("@survey_note", survey_note);

                    survey_id = Convert.ToInt32(cmd1.ExecuteScalar());

                    string query2 = @"INSERT INTO tbl_inside_plant (survey_id, inside_msan, inside_primary, inside_olt, inside_lcp, inside_nap, inside_port1, inside_port2, inside_static1, inside_static2, inside_tel1, inside_tel2, inside_remarks, inside_checked_name, inside_checked_date) 
                                      VALUES(@survey_id, @inside_msan, @inside_primary, @inside_olt, @inside_lcp, @inside_nap, @inside_port1, @inside_port2, @inside_static1, @inside_static2, @inside_tel1, @inside_tel2, @inside_remarks, @inside_checked_name, @inside_checked_date);";
                    using (MySqlCommand cmd2 = new MySqlCommand(query2, connection))
                    {
                        cmd2.Parameters.AddWithValue("@survey_id", survey_id);
                        cmd2.Parameters.AddWithValue("@inside_msan", inside_msan);
                        cmd2.Parameters.AddWithValue("@inside_primary", inside_primary);
                        cmd2.Parameters.AddWithValue("@inside_olt", inside_olt);
                        cmd2.Parameters.AddWithValue("@inside_lcp", inside_lcp);
                        cmd2.Parameters.AddWithValue("@inside_nap", inside_nap);
                        cmd2.Parameters.AddWithValue("@inside_port1", inside_port1);
                        cmd2.Parameters.AddWithValue("@inside_port2", inside_port2);
                        cmd2.Parameters.AddWithValue("@inside_static1", inside_static1);
                        cmd2.Parameters.AddWithValue("@inside_static2", inside_static2);
                        cmd2.Parameters.AddWithValue("@inside_tel1", inside_tel1);
                        cmd2.Parameters.AddWithValue("@inside_tel2", inside_tel2);
                        cmd2.Parameters.AddWithValue("@inside_remarks", inside_remarks);
                        cmd2.Parameters.AddWithValue("@inside_checked_name", inside_checked_name);
                        cmd2.Parameters.AddWithValue("@inside_checked_date", inside_checked_date);

                        cmd2.ExecuteNonQuery();

                        string query3 = @"UPDATE tbl_account SET account_status = 3 WHERE account_num = @account_num;";
                        using (MySqlCommand cmd3 = new MySqlCommand(query3, connection))
                        {
                            cmd3.Parameters.AddWithValue("@account_num", account_num);

                            cmd3.ExecuteNonQuery();
                        }
                    }
                }
                MessageBox.Show("Survey form and inside plant successfully completed.", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
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

            if (parent_form is frmRequestAdmin)
            {
                ((frmRequestAdmin)parent_form).btnReset_Click(null, null);
            }
            if (parent_form is frmRequest)
            {
                ((frmRequest)parent_form).btnReset_Click(null, null);
            }

            // test only //

            string username = TCPClient.Instance.username;
            string log_type = null;
            log_type = $"submit a survey form and inside plant with the account number #{10000 + account_num}";

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

        private void btnCustomerInfo_Click(object sender, EventArgs e)
        {
            if (customerInfo == null || customerInfo.IsDisposed)
            {
                customerInfo = new frmCustomerInfo();
                customerInfo.FormClosed += frmCustomerInfo_FormClosed;
                customerInfo.Owner = this;
                customerInfo.TopMost = true;
                customerInfo.account_num = account_num;
                customerInfo.Show();
                btnCustomerInfo.Enabled = false;
            }
        }

        private void frmCustomerInfo_FormClosed(object sender, EventArgs e)
        {
            btnCustomerInfo.Enabled = true;
        }

        private void frmAppForm2_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (customerInfo != null)
            {
                customerInfo.Close();
            }
        }
    }
}
