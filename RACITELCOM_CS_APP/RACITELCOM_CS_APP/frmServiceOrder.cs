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
    public partial class frmServiceOrder : Form
    {
        public int? account_num { get; set; }
        public Form parent_form { get; set; }

        public frmServiceOrder()
        {
            InitializeComponent();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            // this.Dispose();
            this.Close();
        }

        private void btnComplete_Click(object sender, EventArgs e)
        {
            MySqlConnection connection = MyConnectionString.mysql_connection();
            try
            {
                connection.Open();

                string query = @"INSERT INTO tbl_service_order (account_num, so_static, so_mac, so_serial, so_tel, so_pw, so_olt, so_lcp, so_nap, so_port_ftth, so_vlan, so_slot_pon, so_location, so_primary, so_dp, so_msan, so_port_dsl, so_dsl_profile, so_operator, so_operator_date, so_tel_created_by, so_tel_created_by_date, so_onu_modem_by, so_onu_modem_by_date, so_onu_activated_by, so_onu_activated_by_date, so_cable_installed_by, so_cable_installed_by_date, so_helper, so_helper_date, so_technician, so_technician_date, so_lineman, so_lineman_date, so_inside_plant, so_inside_plant_date, so_outside_plant, so_outside_plant_date)
                                VALUES (@account_num, @so_static, @so_mac, @so_serial, @so_tel, @so_pw, @so_olt, @so_lcp, @so_nap, @so_port_ftth, @so_vlan, @so_slot_pon, @so_location, @so_primary, @so_dp, @so_msan, @so_port_dsl, @so_dsl_profile, @so_operator, @so_operator_date, @so_tel_created_by, @so_tel_created_by_date, @so_onu_modem_by, @so_onu_modem_by_date, @so_onu_activated_by, @so_onu_activated_by_date, @so_cable_installed_by, @so_cable_installed_by_date, @so_helper, @so_helper_date, @so_technician, @so_technician_date, @so_lineman, @so_lineman_date, @so_inside_plant, @so_inside_plant_date, @so_outside_plant, @so_outside_plant_date);";
                using (MySqlCommand cmd = new MySqlCommand(query, connection))
                {
                    cmd.Parameters.AddWithValue("@account_num", account_num);
                    cmd.Parameters.AddWithValue("@so_static", string.IsNullOrEmpty(txtStatic.Text) ? (object)DBNull.Value : txtStatic.Text);
                    cmd.Parameters.AddWithValue("@so_mac", string.IsNullOrEmpty(txtMac.Text) ? (object)DBNull.Value : txtMac.Text);
                    cmd.Parameters.AddWithValue("@so_serial", string.IsNullOrEmpty(txtSerial.Text) ? (object)DBNull.Value : txtSerial.Text);
                    cmd.Parameters.AddWithValue("@so_tel", string.IsNullOrEmpty(txtTel.Text) ? (object)DBNull.Value : txtTel.Text);
                    cmd.Parameters.AddWithValue("@so_pw", string.IsNullOrEmpty(txtPW.Text) ? (object)DBNull.Value : txtPW.Text);
                    cmd.Parameters.AddWithValue("@so_olt", string.IsNullOrEmpty(txtOLT.Text) ? (object)DBNull.Value : txtOLT.Text);
                    cmd.Parameters.AddWithValue("@so_lcp", string.IsNullOrEmpty(txtLCP.Text) ? (object)DBNull.Value : txtLCP.Text);
                    cmd.Parameters.AddWithValue("@so_nap", string.IsNullOrEmpty(txtNAP.Text) ? (object)DBNull.Value : txtNAP.Text);
                    cmd.Parameters.AddWithValue("@so_port_ftth", string.IsNullOrEmpty(txtPort1.Text) ? (object)DBNull.Value : txtPort1.Text);
                    cmd.Parameters.AddWithValue("@so_vlan", string.IsNullOrEmpty(txtVLAN.Text) ? (object)DBNull.Value : txtVLAN.Text);
                    cmd.Parameters.AddWithValue("@so_slot_pon", string.IsNullOrEmpty($"{txtSlot1.Text}/{txtSlot2.Text}/{txtSlot3.Text}") ? (object)DBNull.Value : "");
                    cmd.Parameters.AddWithValue("@so_location", string.IsNullOrEmpty(txtLocation.Text) ? (object)DBNull.Value : txtLocation.Text);
                    cmd.Parameters.AddWithValue("@so_primary", string.IsNullOrEmpty(txtPrimary.Text) ? (object)DBNull.Value : txtPrimary.Text);
                    cmd.Parameters.AddWithValue("@so_dp", string.IsNullOrEmpty(txtDP.Text) ? (object)DBNull.Value : txtDP.Text);
                    cmd.Parameters.AddWithValue("@so_msan", string.IsNullOrEmpty(txtMSAN.Text) ? (object)DBNull.Value : txtMSAN.Text);
                    cmd.Parameters.AddWithValue("@so_port_dsl", string.IsNullOrEmpty(txtPort2.Text) ? (object)DBNull.Value : txtPort2.Text);
                    cmd.Parameters.AddWithValue("@so_dsl_profile", string.IsNullOrEmpty(txtDSLProfile.Text) ? (object)DBNull.Value : txtDSLProfile.Text);
                    cmd.Parameters.AddWithValue("@so_operator", string.IsNullOrEmpty(txtOperatorName.Text) ? (object)DBNull.Value : txtOperatorName.Text);
                    cmd.Parameters.AddWithValue("@so_operator_date", dateOperatorName.Value);
                    cmd.Parameters.AddWithValue("@so_tel_created_by", string.IsNullOrEmpty(txtTelCreated.Text) ? (object)DBNull.Value : txtTelCreated.Text);
                    cmd.Parameters.AddWithValue("@so_tel_created_by_date", dateTelCreated.Value);
                    cmd.Parameters.AddWithValue("@so_onu_modem_by", string.IsNullOrEmpty(txtModemONU.Text) ? (object)DBNull.Value : txtModemONU.Text);
                    cmd.Parameters.AddWithValue("@so_onu_modem_by_date", dateModemONU.Value);
                    cmd.Parameters.AddWithValue("@so_onu_activated_by", string.IsNullOrEmpty(txtONUActivatedBy.Text) ? (object)DBNull.Value : txtONUActivatedBy.Text);
                    cmd.Parameters.AddWithValue("@so_onu_activated_by_date", dateONUActivatedBy.Value);
                    cmd.Parameters.AddWithValue("@so_cable_installed_by", string.IsNullOrEmpty(txtCableInstalledBy.Text) ? (object)DBNull.Value : txtCableInstalledBy.Text);
                    cmd.Parameters.AddWithValue("@so_cable_installed_by_date", dateCableInstalledBy.Value);
                    cmd.Parameters.AddWithValue("@so_helper", string.IsNullOrEmpty(txtHelper.Text) ? (object)DBNull.Value : txtHelper.Text);
                    cmd.Parameters.AddWithValue("@so_helper_date", dateHelper.Value);
                    cmd.Parameters.AddWithValue("@so_technician", string.IsNullOrEmpty(txtTechnician.Text) ? (object)DBNull.Value : txtTechnician.Text);
                    cmd.Parameters.AddWithValue("@so_technician_date", dateTechnician.Value);
                    cmd.Parameters.AddWithValue("@so_lineman", string.IsNullOrEmpty(txtLineman.Text) ? (object)DBNull.Value : txtLineman.Text);
                    cmd.Parameters.AddWithValue("@so_lineman_date", dateLineman.Value);
                    cmd.Parameters.AddWithValue("@so_inside_plant", string.IsNullOrEmpty(txtInsidePlant.Text) ? (object)DBNull.Value : txtInsidePlant.Text);
                    cmd.Parameters.AddWithValue("@so_inside_plant_date", dateInsidePlant.Value);
                    cmd.Parameters.AddWithValue("@so_outside_plant", string.IsNullOrEmpty(txtOutsidePlant.Text) ? (object)DBNull.Value : txtOutsidePlant.Text);
                    cmd.Parameters.AddWithValue("@so_outside_plant_date", dateOutsidePlant.Value);

                    cmd.ExecuteScalar();

                    string query_status = "UPDATE tbl_account SET account_status = 8 WHERE account_num = @account_num";
                    using (MySqlCommand cmd12 = new MySqlCommand(query_status, connection))
                    {
                        cmd12.Parameters.AddWithValue("@account_num", account_num);

                        cmd12.ExecuteNonQuery();

                        MessageBox.Show("Service order successfuly completed.", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
            catch (Exception ex)
            {
                txtExtra.Text = $"Error: {ex.ToString()}";
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
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {

        }

        private void btnRequisition_Click(object sender, EventArgs e)
        {

        }

        private void btnRequestInfo_Click(object sender, EventArgs e)
        {
            frmAppForm3 appForm3 = new frmAppForm3();
            appForm3.parent_form = parent_form;
            appForm3.account_num = account_num;
            appForm3.isServiceActivation = true;
            appForm3.isServiceOrder = true;
            appForm3.ShowDialog();
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
    }
}
