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
    public partial class frmAppForm3 : Form
    {
        public int? account_num { get; set; }

        public int nextAutoIncrementValue { get; set; }

        public int orNum { get; set; }

        public bool isServiceActivation { get; set; }

        public bool isServiceOrder { get; set; }

        public Form parent_form { get; set; }

        private frmCustomerInfo customerInfo;

        public frmAppForm3()
        {
            InitializeComponent();
            setToolTip();
        }

        private void frmAppForm3_Load(object sender, EventArgs e)
        {
            loadForm(account_num);
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

            toolTip.SetToolTip(txtPromoCode, txtPromoCode.Tag.ToString());
            toolTip.SetToolTip(txtInstallationFee, txtInstallationFee.Tag.ToString());
            toolTip.SetToolTip(txtModemFee, txtModemFee.Tag.ToString());
            toolTip.SetToolTip(txtDeposit, txtDeposit.Tag.ToString());
            toolTip.SetToolTip(txtExcessSpan, txtExcessSpan.Tag.ToString());
            toolTip.SetToolTip(txtInstallationAmount, txtInstallationAmount.Tag.ToString());
            toolTip.SetToolTip(txtAmountPaid, txtAmountPaid.Tag.ToString());
            toolTip.SetToolTip(txtBalance, txtBalance.Tag.ToString());
            toolTip.SetToolTip(txtORNum, txtORNum.Tag.ToString());
            toolTip.SetToolTip(txtBankCheckNum, txtBankCheckNum.Tag.ToString());
            toolTip.SetToolTip(txtSalesAgent, txtSalesAgent.Tag.ToString());
            toolTip.SetToolTip(txtReceivedBy, txtReceivedBy.Tag.ToString());
            toolTip.SetToolTip(txtVerifiedBy, txtVerifiedBy.Tag.ToString());
        }

        private void loadForm(int? account_num)
        {
            MySqlConnection connection = MyConnectionString.mysql_connection();
            try
            {
                connection.Open();

                string query1 = $@"SELECT
                                    survey.*,
                                    inside.*
                                  FROM
                                    tbl_survey as survey
                                    INNER JOIN tbl_account as account ON survey.account_num = account.account_num
                                    INNER JOIN tbl_inside_plant as inside ON survey.survey_id = inside.survey_id
                                  WHERE
                                    account.account_num = @account_num;";
                MySqlCommand cmd = new MySqlCommand(query1, connection);
                cmd.Parameters.AddWithValue("@account_num", account_num);
                using (MySqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        string survey_dp = reader.IsDBNull(reader.GetOrdinal("survey_dp")) ? null : reader.GetString("survey_dp");
                        string survey_FTTH_OLT_LCP_NAP = reader.IsDBNull(reader.GetOrdinal("survey_FTTH_OLT_LCP_NAP")) ? null : reader.GetString("survey_FTTH_OLT_LCP_NAP");
                        string survey_spam = reader.IsDBNull(reader.GetOrdinal("survey_spam")) ? null : reader.GetString("survey_spam");
                        string survey_p_clamp = reader.IsDBNull(reader.GetOrdinal("survey_p_clamp")) ? null : reader.GetString("survey_p_clamp");
                        string survey_s_clamp = reader.IsDBNull(reader.GetOrdinal("survey_s_clamp")) ? null : reader.GetString("survey_s_clamp");
                        string survey_span_clamp = reader.IsDBNull(reader.GetOrdinal("survey_span_clamp")) ? null : reader.GetString("survey_span_clamp");
                        string survey_jacket_wire = reader.IsDBNull(reader.GetOrdinal("survey_jacket_wire")) ? null : reader.GetString("survey_jacket_wire");
                        string survey_dropwire = reader.IsDBNull(reader.GetOrdinal("survey_dropwire")) ? null : reader.GetString("survey_dropwire");
                        string survey_others = reader.IsDBNull(reader.GetOrdinal("survey_others")) ? null : reader.GetString("survey_others");
                        string survey_remarks = reader.IsDBNull(reader.GetOrdinal("survey_remarks")) ? null : reader.GetString("survey_remarks");
                        string survey_by_name = reader.IsDBNull(reader.GetOrdinal("survey_by_name")) ? null : reader.GetString("survey_by_name");
                        DateTime? survey_by_date = reader["survey_by_date"] != DBNull.Value ? (DateTime?)Convert.ToDateTime(reader["survey_by_date"]) : null;
                        string survey_checked_name = reader.IsDBNull(reader.GetOrdinal("survey_checked_name")) ? null : reader.GetString("survey_checked_name");
                        DateTime? survey_checked_date = reader["survey_checked_date"] != DBNull.Value ? (DateTime?)Convert.ToDateTime(reader["survey_checked_date"]) : null;
                        string survey_note = reader.IsDBNull(reader.GetOrdinal("survey_note")) ? null : reader.GetString("survey_note");

                        string inside_port1 = reader.IsDBNull(reader.GetOrdinal("inside_port1")) ? null : reader.GetString("inside_port1");
                        string inside_msan = reader.IsDBNull(reader.GetOrdinal("inside_msan")) ? null : reader.GetString("inside_msan");
                        string inside_primary = reader.IsDBNull(reader.GetOrdinal("inside_primary")) ? null : reader.GetString("inside_primary");
                        string inside_port2 = reader.IsDBNull(reader.GetOrdinal("inside_port2")) ? null : reader.GetString("inside_port2");
                        string inside_olt = reader.IsDBNull(reader.GetOrdinal("inside_olt")) ? null : reader.GetString("inside_olt");
                        string inside_lcp = reader.IsDBNull(reader.GetOrdinal("inside_lcp")) ? null : reader.GetString("inside_lcp");
                        string inside_nap = reader.IsDBNull(reader.GetOrdinal("inside_nap")) ? null : reader.GetString("inside_nap");
                        string inside_static1 = reader.IsDBNull(reader.GetOrdinal("inside_static1")) ? null : reader.GetString("inside_static1");
                        string inside_static2 = reader.IsDBNull(reader.GetOrdinal("inside_static2")) ? null : reader.GetString("inside_static2");
                        string inside_tel1 = reader.IsDBNull(reader.GetOrdinal("inside_tel1")) ? null : reader.GetString("inside_tel1");
                        string inside_tel2 = reader.IsDBNull(reader.GetOrdinal("inside_tel2")) ? null : reader.GetString("inside_tel2");
                        string inside_remarks = reader.IsDBNull(reader.GetOrdinal("inside_remarks")) ? null : reader.GetString("inside_remarks");
                        string inside_checked_name = reader.IsDBNull(reader.GetOrdinal("inside_checked_name")) ? null : reader.GetString("inside_checked_name");
                        DateTime? inside_checked_date = reader["inside_checked_date"] != DBNull.Value ? (DateTime?)Convert.ToDateTime(reader["inside_checked_date"]) : null;

                        txtDP.Text = survey_dp != null ? survey_dp : txtDP.Text;
                        txtDP.ForeColor = !string.IsNullOrEmpty(survey_dp) ? Color.FromArgb(17, 7, 100) : txtDP.ForeColor;
                        txtFTTHOLT.Text = survey_FTTH_OLT_LCP_NAP != null ? survey_FTTH_OLT_LCP_NAP : txtFTTHOLT.Text;
                        txtFTTHOLT.ForeColor = !string.IsNullOrEmpty(survey_FTTH_OLT_LCP_NAP) ? Color.FromArgb(17, 7, 100) : txtFTTHOLT.ForeColor;
                        txtSpam.Text = survey_spam != null ? survey_spam : txtSpam.Text;
                        txtSpam.ForeColor = !string.IsNullOrEmpty(survey_spam) ? Color.FromArgb(17, 7, 100) : txtSpam.ForeColor;
                        txtPclamp.Text = survey_p_clamp != null ? survey_p_clamp : txtPclamp.Text;
                        txtPclamp.ForeColor = !string.IsNullOrEmpty(survey_p_clamp) ? Color.FromArgb(17, 7, 100) : txtPclamp.ForeColor;
                        txtSpam.Text = survey_s_clamp != null ? survey_s_clamp : txtSpam.Text;
                        txtSpam.ForeColor = !string.IsNullOrEmpty(survey_s_clamp) ? Color.FromArgb(17, 7, 100) : txtSpam.ForeColor;
                        txtSpanclamp.Text = survey_span_clamp != null ? survey_span_clamp : txtSpanclamp.Text;
                        txtSpanclamp.ForeColor = !string.IsNullOrEmpty(survey_span_clamp) ? Color.FromArgb(17, 7, 100) : txtSpanclamp.ForeColor;
                        txtJacketed.Text = survey_jacket_wire != null ? survey_jacket_wire : txtJacketed.Text;
                        txtJacketed.ForeColor = !string.IsNullOrEmpty(survey_jacket_wire) ? Color.FromArgb(17, 7, 100) : txtJacketed.ForeColor;
                        txtDropwire.Text = survey_dropwire != null ? survey_dropwire : txtDropwire.Text;
                        txtDropwire.ForeColor = !string.IsNullOrEmpty(survey_dropwire) ? Color.FromArgb(17, 7, 100) : txtDropwire.ForeColor;
                        txtOthers.Text = survey_others != null ? survey_others : txtOthers.Text;
                        txtOthers.ForeColor = !string.IsNullOrEmpty(survey_others) ? Color.FromArgb(17, 7, 100) : txtOthers.ForeColor;
                        txtSurveyRemarks.Text = survey_remarks != null ? survey_remarks : txtSurveyRemarks.Text;
                        txtSurveyRemarks.ForeColor = !string.IsNullOrEmpty(survey_remarks) ? Color.FromArgb(17, 7, 100) : txtSurveyRemarks.ForeColor;
                        txtSurveyBy.Text = survey_by_name != null ? survey_by_name : txtSurveyBy.Text;
                        txtSurveyBy.ForeColor = !string.IsNullOrEmpty(survey_by_name) ? Color.FromArgb(17, 7, 100) : txtSurveyBy.ForeColor;
                        if (survey_by_date.HasValue)
                        {
                            dateSurveyBy.Value = survey_by_date.Value;
                            dateSurveyBy.Format = DateTimePickerFormat.Custom;
                            dateSurveyBy.CustomFormat = "MM/dd/yyyy";
                        }

                        txtSurveyChecked.Text = survey_checked_name;
                        txtSurveyChecked.ForeColor = !string.IsNullOrEmpty(survey_checked_name) ? Color.FromArgb(17, 7, 100) : txtSurveyChecked.ForeColor;
                        if (survey_checked_date.HasValue)
                        {
                            dateSurveyChecked.Value = survey_checked_date.Value;
                            dateSurveyChecked.Format = DateTimePickerFormat.Custom;
                            dateSurveyChecked.CustomFormat = "MM/dd/yyyy";
                        }
                        txtNote.Text = survey_note != null ? survey_note : txtNote.Text;
                        txtNote.ForeColor = !string.IsNullOrEmpty(survey_note) ? Color.FromArgb(17, 7, 100) : txtNote.ForeColor;

                        txtPort1.Text = inside_port1 != null ? inside_port1 : txtPort1.Text;
                        txtPort1.ForeColor = !string.IsNullOrEmpty(inside_port1) ? Color.FromArgb(17, 7, 100) : txtPort1.ForeColor;
                        txtMSAN.Text = inside_msan != null ? inside_msan : txtMSAN.Text;
                        txtMSAN.ForeColor = !string.IsNullOrEmpty(inside_msan) ? Color.FromArgb(17, 7, 100) : txtMSAN.ForeColor;
                        txtPrimary.Text = inside_primary != null ? inside_primary : txtPrimary.Text;
                        txtPrimary.ForeColor = !string.IsNullOrEmpty(inside_primary) ? Color.FromArgb(17, 7, 100) : txtPrimary.ForeColor;
                        txtPort2.Text = inside_port2 != null ? inside_port2 : txtPort2.Text;
                        txtPort2.ForeColor = !string.IsNullOrEmpty(inside_port2) ? Color.FromArgb(17, 7, 100) : txtPort2.ForeColor;
                        txtOLT.Text = inside_olt != null ? inside_olt : txtOLT.Text;
                        txtOLT.ForeColor = !string.IsNullOrEmpty(inside_olt) ? Color.FromArgb(17, 7, 100) : txtOLT.ForeColor;
                        txtLCP.Text = inside_lcp != null ? inside_lcp : txtLCP.Text;
                        txtLCP.ForeColor = !string.IsNullOrEmpty(inside_lcp) ? Color.FromArgb(17, 7, 100) : txtLCP.ForeColor;
                        txtNAP.Text = inside_nap != null ? inside_nap : txtNAP.Text;
                        txtNAP.ForeColor = !string.IsNullOrEmpty(inside_nap) ? Color.FromArgb(17, 7, 100) : txtNAP.ForeColor;
                        txtStaticIP1.Text = inside_static1 != null ? inside_static1 : txtStaticIP1.Text;
                        txtStaticIP1.ForeColor = !string.IsNullOrEmpty(inside_static1) ? Color.FromArgb(17, 7, 100) : txtStaticIP1.ForeColor;
                        txtStaticIP2.Text = inside_static2 != null ? inside_static2 : txtStaticIP2.Text;
                        txtStaticIP2.ForeColor = !string.IsNullOrEmpty(inside_static2) ? Color.FromArgb(17, 7, 100) : txtStaticIP2.ForeColor;
                        txtTel1.Text = inside_tel1 != null ? inside_tel1 : txtTel1.Text;
                        txtTel1.ForeColor = !string.IsNullOrEmpty(inside_tel1) ? Color.FromArgb(17, 7, 100) : txtTel1.ForeColor;
                        txtTel2.Text = inside_tel2 != null ? inside_tel2 : txtTel2.Text;
                        txtTel2.ForeColor = !string.IsNullOrEmpty(inside_tel2) ? Color.FromArgb(17, 7, 100) : txtTel2.ForeColor;
                        txtInsideRemarks.Text = inside_remarks != null ? inside_remarks : txtInsideRemarks.Text;
                        txtInsideRemarks.ForeColor = !string.IsNullOrEmpty(inside_remarks) ? Color.FromArgb(17, 7, 100) : txtInsideRemarks.ForeColor;
                        txtInsideChecked.Text = inside_checked_name != null ? inside_checked_name : txtInsideChecked.Text;
                        txtInsideChecked.ForeColor = !string.IsNullOrEmpty(inside_checked_name) ? Color.FromArgb(17, 7, 100) : txtInsideChecked.ForeColor;
                        if (inside_checked_date.HasValue)
                        {
                            dateInsideChecked.Value = inside_checked_date.Value;
                            dateInsideChecked.Format = DateTimePickerFormat.Custom;
                            dateInsideChecked.CustomFormat = "MM/dd/yyyy";
                        }
                    }
                    reader.Close();

                    string query2 = @"SELECT AUTO_INCREMENT
                                   FROM information_schema.TABLES
                                   WHERE TABLE_SCHEMA = @databaseName
                                   AND TABLE_NAME = @tableName;";
                    MySqlCommand cmd2 = new MySqlCommand(query2, connection);
                    cmd2.Parameters.AddWithValue("@databaseName", "db_cs_app");
                    cmd2.Parameters.AddWithValue("@tableName", "tbl_billing");
                    nextAutoIncrementValue = Convert.ToInt32(cmd2.ExecuteScalar());
                    txtORNum.Text = $"OR-{80000+nextAutoIncrementValue}";
                    txtORNum.ForeColor = Color.FromArgb(17, 7, 100);

                    if (isServiceActivation == true)
                    {
                        string query3 = $@"SELECT
                                            billing.*
                                          FROM
                                            tbl_billing as billing
                                            INNER JOIN tbl_account as account ON billing.account_num = account.account_num
                                          WHERE
                                            account.account_num = @account_num;";
                        MySqlCommand cmd3 = new MySqlCommand(query3, connection);
                        cmd3.Parameters.AddWithValue("@account_num", account_num);
                        using (MySqlDataReader reader1 = cmd3.ExecuteReader())
                        {
                            while (reader1.Read())
                            {
                                int? or_num = reader1.IsDBNull(reader1.GetOrdinal("or_num")) ? (int?)null : reader1.GetInt32(reader1.GetOrdinal("or_num"));
                                string bill_payment_type = reader1.IsDBNull(reader1.GetOrdinal("bill_payment_type")) ? null : reader1.GetString("bill_payment_type");
                                string bill_promo_code = reader1.IsDBNull(reader1.GetOrdinal("bill_promo_code")) ? null : reader1.GetString("bill_promo_code");
                                int? bill_installation_fee = reader1.IsDBNull(reader1.GetOrdinal("bill_installation_fee")) ? (int?)null : reader1.GetInt32(reader1.GetOrdinal("bill_installation_fee"));
                                int? bill_modem_fee = reader1.IsDBNull(reader1.GetOrdinal("bill_modem_fee")) ? (int?)null : reader1.GetInt32(reader1.GetOrdinal("bill_modem_fee"));
                                int? bill_security_depo = reader1.IsDBNull(reader1.GetOrdinal("bill_security_depo")) ? (int?)null : reader1.GetInt32(reader1.GetOrdinal("bill_security_depo"));
                                int? bill_excess_span = reader1.IsDBNull(reader1.GetOrdinal("bill_excess_span")) ? (int?)null : reader1.GetInt32(reader1.GetOrdinal("bill_excess_span"));
                                int? bill_total_installation_fee = reader1.IsDBNull(reader1.GetOrdinal("bill_total_installation_fee")) ? (int?)null : reader1.GetInt32(reader1.GetOrdinal("bill_total_installation_fee"));
                                int? bill_amount_paid = reader1.IsDBNull(reader1.GetOrdinal("bill_amount_paid")) ? (int?)null : reader1.GetInt32(reader1.GetOrdinal("bill_amount_paid"));
                                int? bill_balance = reader1.IsDBNull(reader1.GetOrdinal("bill_balance")) ? (int?)null : reader1.GetInt32(reader1.GetOrdinal("bill_balance"));
                                string bill_check_num = reader1.IsDBNull(reader1.GetOrdinal("bill_check_num")) ? null : reader1.GetString("bill_check_num");
                                string bill_sales_agent = reader1.IsDBNull(reader1.GetOrdinal("bill_sales_agent")) ? null : reader1.GetString("bill_sales_agent");
                                DateTime? bill_sales_agent_date = reader1["bill_sales_agent_date"] != DBNull.Value ? (DateTime?)Convert.ToDateTime(reader1["bill_sales_agent_date"]) : null;
                                string bill_received_by = reader1.IsDBNull(reader1.GetOrdinal("bill_received_by")) ? null : reader1.GetString("bill_received_by");
                                DateTime? bill_received_by_date = reader1["bill_received_by_date"] != DBNull.Value ? (DateTime?)Convert.ToDateTime(reader1["bill_received_by_date"]) : null;
                                string bill_verified_by = reader1.IsDBNull(reader1.GetOrdinal("bill_verified_by")) ? null : reader1.GetString("bill_verified_by");
                                DateTime? bill_verified_by_date = reader1["bill_verified_by_date"] != DBNull.Value ? (DateTime?)Convert.ToDateTime(reader1["bill_verified_by_date"]) : null;

                                if (bill_payment_type == "cash")
                                {
                                    checkCash.Checked = true;
                                }
                                if (bill_payment_type == "check")
                                {
                                    checkCheck.Checked = true;
                                }
                                checkCash.Enabled = false;
                                checkCheck.Enabled = false;
                                txtPromoCode.Text = bill_promo_code != null ? bill_promo_code : txtPromoCode.Text;
                                txtPromoCode.ForeColor = !string.IsNullOrEmpty(bill_promo_code) ? Color.FromArgb(17, 7, 100) : txtPromoCode.ForeColor;
                                txtPromoCode.ReadOnly = true;
                                txtInstallationFee.Text = bill_installation_fee?.ToString() ?? txtInstallationFee.Text;
                                txtInstallationFee.ForeColor = bill_installation_fee.HasValue ? Color.FromArgb(17, 7, 100) : txtInstallationFee.ForeColor;
                                txtInstallationFee.ReadOnly = true;
                                txtModemFee.Text = bill_modem_fee ?.ToString() ?? txtModemFee.Text;
                                txtModemFee.ForeColor = bill_modem_fee.HasValue ? Color.FromArgb(17, 7, 100) : txtModemFee.ForeColor;
                                txtModemFee.ReadOnly = true;
                                txtDeposit.Text = bill_security_depo?.ToString() ?? txtDeposit.Text;
                                txtDeposit.ForeColor = bill_security_depo.HasValue ? Color.FromArgb(17, 7, 100) : txtDeposit.ForeColor;
                                txtDeposit.ReadOnly = true;
                                txtExcessSpan.Text = bill_excess_span?.ToString() ?? txtExcessSpan.Text;
                                txtExcessSpan.ForeColor = bill_excess_span.HasValue ? Color.FromArgb(17, 7, 100) : txtExcessSpan.ForeColor;
                                txtExcessSpan.ReadOnly = true;
                                txtInstallationAmount.Text = bill_total_installation_fee?.ToString() ?? txtInstallationAmount.Text;
                                txtInstallationAmount.ForeColor = bill_total_installation_fee.HasValue ? Color.FromArgb(17, 7, 100) : txtInstallationAmount.ForeColor;
                                txtInstallationAmount.ReadOnly = true;
                                txtAmountPaid.Text = bill_amount_paid?.ToString() ?? txtAmountPaid.Text;
                                txtAmountPaid.ForeColor = bill_amount_paid.HasValue ? Color.FromArgb(17, 7, 100) : txtAmountPaid.ForeColor;
                                txtAmountPaid.ReadOnly = true;
                                txtBalance.Text = bill_balance?.ToString() ?? txtBalance.Text;
                                txtBalance.ForeColor = bill_balance.HasValue ? Color.FromArgb(17, 7, 100) : txtBalance.ForeColor;
                                txtBalance.ReadOnly = true;
                                txtORNum.Text = $"OR-{80000 + or_num}" ?? txtORNum.Text;
                                txtORNum.ForeColor = or_num.HasValue ? Color.FromArgb(17, 7, 100) : txtORNum.ForeColor;
                                txtORNum.ReadOnly = true;
                                txtBankCheckNum.Text = bill_check_num != null ? bill_check_num : txtBankCheckNum.Text;
                                txtBankCheckNum.ForeColor = !string.IsNullOrEmpty(bill_check_num) ? Color.FromArgb(17, 7, 100) : txtBankCheckNum.ForeColor;
                                txtBankCheckNum.ReadOnly = true;
                                txtSalesAgent.Text = bill_sales_agent != null ? bill_sales_agent : txtSalesAgent.Text;
                                txtSalesAgent.ForeColor = !string.IsNullOrEmpty(bill_sales_agent) ? Color.FromArgb(17, 7, 100) : txtSalesAgent.ForeColor;
                                txtSalesAgent.ReadOnly = true;
                                txtReceivedBy.Text = bill_received_by != null ? bill_received_by : txtReceivedBy.Text;
                                txtReceivedBy.ForeColor = !string.IsNullOrEmpty(bill_received_by) ? Color.FromArgb(17, 7, 100) : txtReceivedBy.ForeColor;
                                txtReceivedBy.ReadOnly = true;
                                txtVerifiedBy.Text = bill_verified_by != null ? bill_verified_by : txtVerifiedBy.Text;
                                txtVerifiedBy.ForeColor = !string.IsNullOrEmpty(bill_verified_by) ? Color.FromArgb(17, 7, 100) : txtVerifiedBy.ForeColor;
                                txtVerifiedBy.ReadOnly = true;
                                if (bill_sales_agent_date.HasValue)
                                {
                                    dateSalesAgent.Value = bill_sales_agent_date.Value;
                                    dateSalesAgent.Format = DateTimePickerFormat.Custom;
                                    dateSalesAgent.CustomFormat = "MM/dd/yyyy";
                                }
                                if (bill_received_by_date.HasValue)
                                {
                                    dateReceivedBy.Value = bill_received_by_date.Value;
                                    dateReceivedBy.Format = DateTimePickerFormat.Custom;
                                    dateReceivedBy.CustomFormat = "MM/dd/yyyy";
                                }
                                if (bill_verified_by_date.HasValue)
                                {
                                    dateVerifiedBy.Value = bill_verified_by_date.Value;
                                    dateVerifiedBy.Format = DateTimePickerFormat.Custom;
                                    dateVerifiedBy.CustomFormat = "MM/dd/yyyy";
                                }
                                dateSalesAgent.Enabled = false;
                                dateReceivedBy.Enabled = false;
                                dateVerifiedBy.Enabled = false;
                                btnSubmit.Text = "COMPLETE";
                                if (isServiceOrder == true)
                                {
                                    btnSubmit.Visible = false;
                                }
                            }
                        }
                    }
                }
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
            if (isServiceActivation == true)
            {
                MySqlConnection connection = MyConnectionString.mysql_connection();
                try
                {
                    connection.Open();

                    string query1 = "UPDATE tbl_account SET account_status = 1 WHERE account_num = @account_num";
                    using (MySqlCommand cmd1 = new MySqlCommand(query1, connection))
                    {
                        cmd1.Parameters.AddWithValue("@account_num", account_num);
                        cmd1.ExecuteNonQuery();

                        string query2 = "SELECT or_num FROM tbl_billing WHERE account_num = @account_num";
                        using (MySqlCommand cmd2 = new MySqlCommand(query2, connection))
                        {
                            cmd2.Parameters.AddWithValue("@account_num", account_num);
                            using (MySqlDataReader reader = cmd2.ExecuteReader())
                            {
                                while (reader.Read())
                                {
                                    orNum = Convert.ToInt32(reader["or_num"]);
                                }
                            }
                        }

                        MessageBox.Show("Service activation completed.", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
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
                log_type = $"completed service activation on account number #{10000 + account_num} with ORnumber OR-{80000 + orNum}";

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
            else
            {
                if ((checkCash.Checked == false && checkCheck.Checked == false) || txtInstallationAmount.ForeColor == Color.Gray || txtAmountPaid.ForeColor == Color.Gray || txtBalance.ForeColor == Color.Gray || txtORNum.ForeColor == Color.Gray ||
                    txtSalesAgent.ForeColor == Color.Gray || txtReceivedBy.ForeColor == Color.Gray || txtVerifiedBy.ForeColor == Color.Gray || txtInstallationFee.ForeColor == Color.Gray || txtModemFee.ForeColor == Color.Gray || txtDeposit.ForeColor == Color.Gray)
                {
                    MessageBox.Show("Please ensure that all required fields are completed. For new subscribers, kindly ensure that the installation fee, modem fee, security deposit and other applicable charges are properly filled out.", "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return;
                }

                try
                {
                    int var1 = txtInstallationFee.ForeColor != Color.Gray ? int.Parse(txtInstallationFee.Text) : 0;
                    int var2 = txtModemFee.ForeColor != Color.Gray ? int.Parse(txtModemFee.Text) : 0;
                    int var3 = txtDeposit.ForeColor != Color.Gray ? int.Parse(txtDeposit.Text) : 0;
                    int var4 = txtInstallationAmount.ForeColor != Color.Gray ? int.Parse(txtInstallationAmount.Text) : 0;
                    int var5 = txtExcessSpan.ForeColor != Color.Gray ? int.Parse(txtExcessSpan.Text) : 0;
                    int var6 = txtAmountPaid.ForeColor != Color.Gray ? int.Parse(txtAmountPaid.Text) : 0;
                    int var7 = txtBalance.ForeColor != Color.Gray ? int.Parse(txtBalance.Text) : 0;
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

                    string query1 = @"INSERT INTO tbl_billing (account_num, bill_payment_type, bill_promo_code, bill_installation_fee, bill_modem_fee, bill_security_depo, bill_excess_span, bill_total_installation_fee, bill_amount_paid, bill_balance, bill_check_num, bill_sales_agent, bill_sales_agent_date, bill_received_by, bill_received_by_date, bill_verified_by, bill_verified_by_date, bill_transac_type)
                                 VALUES (@account_num, @bill_payment_type, @bill_promo_code, @bill_installation_fee, @bill_modem_fee, @bill_security_depo, @bill_excess_span, @bill_total_installation_fee, @bill_amount_paid, @bill_balance, @bill_check_num, @bill_sales_agent, @bill_sales_agent_date, @bill_received_by, @bill_received_by_date, @bill_verified_by, @bill_verified_by_date, @bill_transac_type);";
                    using (MySqlCommand cmd1 = new MySqlCommand(query1, connection))
                    {
                        cmd1.Parameters.AddWithValue("@account_num", account_num);
                        cmd1.Parameters.AddWithValue("@bill_payment_type", payment_type);
                        cmd1.Parameters.AddWithValue("@bill_promo_code", txtPromoCode.ForeColor != Color.Gray ? txtPromoCode.Text.ToLower() : null);
                        cmd1.Parameters.AddWithValue("@bill_installation_fee", Convert.ToInt32(txtInstallationFee.Text));
                        cmd1.Parameters.AddWithValue("@bill_modem_fee", Convert.ToInt32(txtModemFee.Text));
                        cmd1.Parameters.AddWithValue("@bill_security_depo", Convert.ToInt32(txtDeposit.Text));
                        cmd1.Parameters.AddWithValue("@bill_excess_span", txtExcessSpan.ForeColor != Color.Gray ? (object)Convert.ToInt32(txtExcessSpan.Text) : DBNull.Value);
                        cmd1.Parameters.AddWithValue("@bill_total_installation_fee", Convert.ToInt32(txtInstallationAmount.Text));
                        cmd1.Parameters.AddWithValue("@bill_amount_paid", Convert.ToInt32(txtAmountPaid.Text));
                        cmd1.Parameters.AddWithValue("@bill_balance", Convert.ToInt32(txtBalance.Text));
                        cmd1.Parameters.AddWithValue("@bill_check_num", txtBankCheckNum.ForeColor != Color.Gray ? txtBankCheckNum.Text : null);
                        cmd1.Parameters.AddWithValue("@bill_sales_agent", txtSalesAgent.Text.ToLower());
                        cmd1.Parameters.AddWithValue("@bill_sales_agent_date", dateSalesAgent.Value);
                        cmd1.Parameters.AddWithValue("@bill_received_by", txtReceivedBy.Text.ToLower());
                        cmd1.Parameters.AddWithValue("@bill_received_by_date", dateReceivedBy.Value);
                        cmd1.Parameters.AddWithValue("@bill_verified_by", txtVerifiedBy.Text.ToLower());
                        cmd1.Parameters.AddWithValue("@bill_verified_by_date", dateVerifiedBy.Value);
                        cmd1.Parameters.AddWithValue("@bill_transac_type", "SERVICE ACTIVATION");  // note

                        cmd1.ExecuteNonQuery();

                        /*string query2 = @"SELECT
                                            address.add_type, 
                                            plan.plan_fiber, 
                                            plan.plan_speed
                                         FROM tbl_account as account 
                                         INNER JOIN tbl_address as address ON account.add_id = address.add_id 
                                         INNER JOIN tbl_service_plan as plan ON account.plan_id = plan.plan_id 
                                         WHERE account.account_num = @account_num;";
                        MySqlCommand cmd2 = new MySqlCommand(query2, connection);
                        cmd2.Parameters.AddWithValue("@account_num", account_num);
                        using (MySqlDataReader reader1 = cmd2.ExecuteReader())
                        {
                            string add_type = null;
                            bool plan_fiber = true;
                            string plan_speed = null;
                            if (reader1.Read())
                            {
                                add_type = reader1.IsDBNull(reader1.GetOrdinal("add_type")) ? null : reader1.GetString("add_type");
                                plan_fiber = reader1.IsDBNull(reader1.GetOrdinal("plan_fiber")) ? false : reader1.GetBoolean("plan_fiber");
                                plan_speed = reader1.IsDBNull(reader1.GetOrdinal("plan_speed")) ? null : reader1.GetString("plan_speed");
                            }
                            reader1.Close();

                            string query3 = @"SELECT
                                                price.price_id
                                             FROM tbl_plan_price as price 
                                             WHERE price.add_type = @add_type
                                             AND
                                                price.plan_fiber = @plan_fiber
                                             AND
                                                price.plan_speed = @plan_speed;";
                            MySqlCommand cmd3 = new MySqlCommand(query3, connection);
                            cmd3.Parameters.AddWithValue("@add_type", add_type);
                            cmd3.Parameters.AddWithValue("@plan_fiber", plan_fiber);
                            cmd3.Parameters.AddWithValue("@plan_speed", plan_speed);
                            using (MySqlDataReader reader2 = cmd3.ExecuteReader())
                            {
                                int price_id = 0;
                                if (reader2.Read())
                                {
                                    price_id = reader2.IsDBNull(reader2.GetOrdinal("price_id")) ? 0 : reader2.GetInt32("price_id");
                                }
                                reader2.Close();*/

                        string query2 = @"UPDATE tbl_account
                                        SET account_status = @account_status,
                                            account_due_date = @account_due_date
                                        WHERE account_num = @account_num;";
                        using (MySqlCommand cmd2 = new MySqlCommand(query2, connection))
                        {
                            DateTime currentDate = DateTime.Now;
                            DateTime nextMonthDate = currentDate.AddMonths(1);

                            cmd2.Parameters.AddWithValue("@account_status", 6);
                            cmd2.Parameters.AddWithValue("@account_due_date", nextMonthDate);
                            cmd2.Parameters.AddWithValue("@account_num", account_num);

                            cmd2.ExecuteNonQuery();
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
                log_type = $"submit a customer billing statement for SERVICE ACTIVATION with the account number #{10000 + account_num} and ORnumber OR-{80000 + nextAutoIncrementValue}";

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

        private void frmAppForm3_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (customerInfo != null)
            {
                customerInfo.Close();
            }
        }
    }
}
