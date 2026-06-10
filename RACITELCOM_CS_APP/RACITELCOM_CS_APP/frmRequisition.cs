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
    public partial class frmRequisition : Form
    {
        public int? account_num { get; set; }
        public Form parent_form { get; set; }

        public frmRequisition()
        {
            InitializeComponent();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            // this.Dispose();
            this.Close();
        }

        private void btnSubmit_Click(object sender, EventArgs e)
        {
            if (checkCopper.Checked == false && checkFiber.Checked == false)
            {
                MessageBox.Show("Please select copper or fiber.", "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
            if (checkModem.Checked == false && checkONU.Checked == false)
            {
                MessageBox.Show("Please select modem or onu.", "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            bool fiber = true;
            bool onu = true;
            if (checkCopper.Checked == true)
            {
                fiber = false;
            }
            else
            {
                fiber = true;
            }

            if (checkModem.Checked == true)
            {
                onu = false;
            }
            else
            {
                onu = true;
            }

            MySqlConnection connection = MyConnectionString.mysql_connection();
            try
            {
                connection.Open();

                string query_protector = @"INSERT INTO tbl_quantity (serial, requested, consumed, returned)
                                            VALUES (@serial, @requested, @consumed, @returned);
                                            SELECT LAST_INSERT_ID();";
                using (MySqlCommand cmd1 = new MySqlCommand(query_protector, connection))
                {
                    cmd1.Parameters.AddWithValue("@serial", string.IsNullOrEmpty(txtProtectorSer.Text) ? (object)DBNull.Value : int.Parse(txtProtectorSer.Text));
                    cmd1.Parameters.AddWithValue("@requested", string.IsNullOrEmpty(txtProtectorReq.Text) ? (object)DBNull.Value : int.Parse(txtProtectorReq.Text));
                    cmd1.Parameters.AddWithValue("@consumed", string.IsNullOrEmpty(txtProtectorCon.Text) ? (object)DBNull.Value : int.Parse(txtProtectorCon.Text));
                    cmd1.Parameters.AddWithValue("@returned", string.IsNullOrEmpty(txtProtectorRet.Text) ? (object)DBNull.Value : int.Parse(txtProtectorRet.Text));

                    int protector_id = Convert.ToInt32(cmd1.ExecuteScalar());

                    string query_jacketed = @"INSERT INTO tbl_quantity (serial, requested, consumed, returned)
                                                VALUES (@serial, @requested, @consumed, @returned);
                                                SELECT LAST_INSERT_ID();";
                    using (MySqlCommand cmd2 = new MySqlCommand(query_jacketed, connection))
                    {
                        cmd2.Parameters.AddWithValue("@serial", string.IsNullOrEmpty(txtJacketedSer.Text) ? (object)DBNull.Value : int.Parse(txtJacketedSer.Text));
                        cmd2.Parameters.AddWithValue("@requested", string.IsNullOrEmpty(txtJacketedReq.Text) ? (object)DBNull.Value : int.Parse(txtJacketedReq.Text));
                        cmd2.Parameters.AddWithValue("@consumed", string.IsNullOrEmpty(txtJacketedCon.Text) ? (object)DBNull.Value : int.Parse(txtJacketedCon.Text));
                        cmd2.Parameters.AddWithValue("@returned", string.IsNullOrEmpty(txtJacketedRet.Text) ? (object)DBNull.Value : int.Parse(txtJacketedRet.Text));

                        int jacketed_id = Convert.ToInt32(cmd2.ExecuteScalar());

                        string query_block = @"INSERT INTO tbl_quantity (serial, requested, consumed, returned)
                                                VALUES (@serial, @requested, @consumed, @returned);
                                                SELECT LAST_INSERT_ID();";
                        using (MySqlCommand cmd3 = new MySqlCommand(query_block, connection))
                        {
                            cmd3.Parameters.AddWithValue("@serial", string.IsNullOrEmpty(txtConnectingSer.Text) ? (object)DBNull.Value : int.Parse(txtConnectingSer.Text));
                            cmd3.Parameters.AddWithValue("@requested", string.IsNullOrEmpty(txtConnectingReq.Text) ? (object)DBNull.Value : int.Parse(txtConnectingReq.Text));
                            cmd3.Parameters.AddWithValue("@consumed", string.IsNullOrEmpty(txtConnectingCon.Text) ? (object)DBNull.Value : int.Parse(txtConnectingCon.Text));
                            cmd3.Parameters.AddWithValue("@returned", string.IsNullOrEmpty(txtConnectingRet.Text) ? (object)DBNull.Value : int.Parse(txtConnectingRet.Text));

                            int block_id = Convert.ToInt32(cmd3.ExecuteScalar());

                            string query_fclamp = @"INSERT INTO tbl_quantity (serial, requested, consumed, returned)
                                                    VALUES (@serial, @requested, @consumed, @returned);
                                                    SELECT LAST_INSERT_ID();";
                            using (MySqlCommand cmd4 = new MySqlCommand(query_fclamp, connection))
                            {
                                cmd4.Parameters.AddWithValue("@serial", string.IsNullOrEmpty(txtFClampSer.Text) ? (object)DBNull.Value : int.Parse(txtFClampSer.Text));
                                cmd4.Parameters.AddWithValue("@requested", string.IsNullOrEmpty(txtFClampReq.Text) ? (object)DBNull.Value : int.Parse(txtFClampReq.Text));
                                cmd4.Parameters.AddWithValue("@consumed", string.IsNullOrEmpty(txtFClampCon.Text) ? (object)DBNull.Value : int.Parse(txtFClampCon.Text));
                                cmd4.Parameters.AddWithValue("@returned", string.IsNullOrEmpty(txtFClampRet.Text) ? (object)DBNull.Value : int.Parse(txtFClampRet.Text));

                                int fclamp_id = Convert.ToInt32(cmd4.ExecuteScalar());

                                string query_sclamp = @"INSERT INTO tbl_quantity (serial, requested, consumed, returned)
                                                        VALUES (@serial, @requested, @consumed, @returned);
                                                        SELECT LAST_INSERT_ID();";
                                using (MySqlCommand cmd5 = new MySqlCommand(query_sclamp, connection))
                                {
                                    cmd5.Parameters.AddWithValue("@serial", string.IsNullOrEmpty(txtSClampSer.Text) ? (object)DBNull.Value : int.Parse(txtSClampSer.Text));
                                    cmd5.Parameters.AddWithValue("@requested", string.IsNullOrEmpty(txtSClampReq.Text) ? (object)DBNull.Value : int.Parse(txtSClampReq.Text));
                                    cmd5.Parameters.AddWithValue("@consumed", string.IsNullOrEmpty(txtSClampCon.Text) ? (object)DBNull.Value : int.Parse(txtSClampCon.Text));
                                    cmd5.Parameters.AddWithValue("@returned", string.IsNullOrEmpty(txtSClampRet.Text) ? (object)DBNull.Value : int.Parse(txtSClampRet.Text));

                                    int sclamp_id = Convert.ToInt32(cmd5.ExecuteScalar());

                                    string query_pclamp = @"INSERT INTO tbl_quantity (serial, requested, consumed, returned)
                                                            VALUES (@serial, @requested, @consumed, @returned);
                                                            SELECT LAST_INSERT_ID();";
                                    using (MySqlCommand cmd6 = new MySqlCommand(query_pclamp, connection))
                                    {
                                        cmd6.Parameters.AddWithValue("@serial", string.IsNullOrEmpty(txtPClampSer.Text) ? (object)DBNull.Value : int.Parse(txtPClampSer.Text));
                                        cmd6.Parameters.AddWithValue("@requested", string.IsNullOrEmpty(txtPClampReq.Text) ? (object)DBNull.Value : int.Parse(txtPClampReq.Text));
                                        cmd6.Parameters.AddWithValue("@consumed", string.IsNullOrEmpty(txtPClampCon.Text) ? (object)DBNull.Value : int.Parse(txtPClampCon.Text));
                                        cmd6.Parameters.AddWithValue("@returned", string.IsNullOrEmpty(txtPClampRet.Text) ? (object)DBNull.Value : int.Parse(txtPClampRet.Text));

                                        int pclamp_id = Convert.ToInt32(cmd6.ExecuteScalar());

                                        string query_dropwire = @"INSERT INTO tbl_quantity (serial, requested, consumed, returned)
                                                                    VALUES (@serial, @requested, @consumed, @returned);
                                                                    SELECT LAST_INSERT_ID();";
                                        using (MySqlCommand cmd7 = new MySqlCommand(query_dropwire, connection))
                                        {
                                            cmd7.Parameters.AddWithValue("@serial", string.IsNullOrEmpty(txtDropwireSer.Text) ? (object)DBNull.Value : int.Parse(txtDropwireSer.Text));
                                            cmd7.Parameters.AddWithValue("@requested", string.IsNullOrEmpty(txtDropwireReq.Text) ? (object)DBNull.Value : int.Parse(txtDropwireReq.Text));
                                            cmd7.Parameters.AddWithValue("@consumed", string.IsNullOrEmpty(txtDropwireCon.Text) ? (object)DBNull.Value : int.Parse(txtDropwireCon.Text));
                                            cmd7.Parameters.AddWithValue("@returned", string.IsNullOrEmpty(txtDropwireRet.Text) ? (object)DBNull.Value : int.Parse(txtDropwireRet.Text));

                                            int dropwire_id = Convert.ToInt32(cmd7.ExecuteScalar());

                                            string query_cable = @"INSERT INTO tbl_quantity (serial, requested, consumed, returned)
                                                                    VALUES (@serial, @requested, @consumed, @returned);
                                                                    SELECT LAST_INSERT_ID();";
                                            using (MySqlCommand cmd8 = new MySqlCommand(query_cable, connection))
                                            {
                                                cmd8.Parameters.AddWithValue("@serial", string.IsNullOrEmpty(txtCableSer.Text) ? (object)DBNull.Value : int.Parse(txtCableSer.Text));
                                                cmd8.Parameters.AddWithValue("@requested", string.IsNullOrEmpty(txtCableReq.Text) ? (object)DBNull.Value : int.Parse(txtCableReq.Text));
                                                cmd8.Parameters.AddWithValue("@consumed", string.IsNullOrEmpty(txtCableCon.Text) ? (object)DBNull.Value : int.Parse(txtCableCon.Text));
                                                cmd8.Parameters.AddWithValue("@returned", string.IsNullOrEmpty(txtCableRet.Text) ? (object)DBNull.Value : int.Parse(txtCableRet.Text));

                                                int cable_id = Convert.ToInt32(cmd8.ExecuteScalar());

                                                string query_fast = @"INSERT INTO tbl_quantity (serial, requested, consumed, returned)
                                                                        VALUES (@serial, @requested, @consumed, @returned);
                                                                        SELECT LAST_INSERT_ID();";
                                                using (MySqlCommand cmd9 = new MySqlCommand(query_fast, connection))
                                                {
                                                    cmd9.Parameters.AddWithValue("@serial", string.IsNullOrEmpty(txtFastSer.Text) ? (object)DBNull.Value : int.Parse(txtFastSer.Text));
                                                    cmd9.Parameters.AddWithValue("@requested", string.IsNullOrEmpty(txtFastReq.Text) ? (object)DBNull.Value : int.Parse(txtFastReq.Text));
                                                    cmd9.Parameters.AddWithValue("@consumed", string.IsNullOrEmpty(txtFastCon.Text) ? (object)DBNull.Value : int.Parse(txtFastCon.Text));
                                                    cmd9.Parameters.AddWithValue("@returned", string.IsNullOrEmpty(txtFastRet.Text) ? (object)DBNull.Value : int.Parse(txtFastRet.Text));

                                                    int fast_id = Convert.ToInt32(cmd9.ExecuteScalar());

                                                    string query_mac = @"INSERT INTO tbl_quantity (serial, requested, consumed, returned)
                                                                        VALUES (@serial, @requested, @consumed, @returned);
                                                                        SELECT LAST_INSERT_ID();";
                                                    using (MySqlCommand cmd10 = new MySqlCommand(query_mac, connection))
                                                    {
                                                        cmd10.Parameters.AddWithValue("@serial", string.IsNullOrEmpty(txtMacSer.Text) ? (object)DBNull.Value : int.Parse(txtMacSer.Text));
                                                        cmd10.Parameters.AddWithValue("@requested", string.IsNullOrEmpty(txtMacReq.Text) ? (object)DBNull.Value : int.Parse(txtMacReq.Text));
                                                        cmd10.Parameters.AddWithValue("@consumed", string.IsNullOrEmpty(txtMacCon.Text) ? (object)DBNull.Value : int.Parse(txtMacCon.Text));
                                                        cmd10.Parameters.AddWithValue("@returned", string.IsNullOrEmpty(txtMacRet.Text) ? (object)DBNull.Value : int.Parse(txtMacRet.Text));

                                                        int mac_id = Convert.ToInt32(cmd10.ExecuteScalar());

                                                        string query_req = @"INSERT INTO tbl_requisition (account_num, protector_id, protector_remarks, jacketed_id, jacketed_remarks, block_id, block_remarks, fclamp_id, fclamp_remarks, sclamp_id, sclamp_remarks, pclamp_id, pclamp_remarks, cable_id, cable_remarks, fast_id, fast_remarks, dropwire_fiber, dropwire_id, dropwire_remarks, mac_onu, mac_id, mac_remarks, requested_by, requested_by_date, issued_to, issued_to_date, released_verified_by, released_verified_by_date)
                                                                            VALUES (@account_num, @protector_id, @protector_remarks, @jacketed_id, @jacketed_remarks, @block_id, @block_remarks, @fclamp_id, @fclamp_remarks, @sclamp_id, @sclamp_remarks, @pclamp_id, @pclamp_remarks, @cable_id, @cable_remarks, @fast_id, @fast_remarks, @dropwire_fiber, @dropwire_id, @dropwire_remarks, @mac_onu, @mac_id, @mac_remarks, @requested_by, @requested_by_date, @issued_to, @issued_to_date, @released_verified_by, @released_verified_by_date);";
                                                        using (MySqlCommand cmd11 = new MySqlCommand(query_req, connection))
                                                        {
                                                            cmd11.Parameters.AddWithValue("@account_num", account_num);
                                                            cmd11.Parameters.AddWithValue("@protector_id", protector_id);
                                                            cmd11.Parameters.AddWithValue("@protector_remarks", string.IsNullOrEmpty(txtProtectorRemarks.Text) ? (object)DBNull.Value : txtProtectorRemarks.Text);
                                                            cmd11.Parameters.AddWithValue("@jacketed_id", jacketed_id);
                                                            cmd11.Parameters.AddWithValue("@jacketed_remarks", string.IsNullOrEmpty(txtJacketedRemarks.Text) ? (object)DBNull.Value : txtJacketedRemarks.Text);
                                                            cmd11.Parameters.AddWithValue("@block_id", block_id);
                                                            cmd11.Parameters.AddWithValue("@block_remarks", string.IsNullOrEmpty(txtConnectingRemarks.Text) ? (object)DBNull.Value : txtConnectingRemarks.Text);
                                                            cmd11.Parameters.AddWithValue("@fclamp_id", fclamp_id);
                                                            cmd11.Parameters.AddWithValue("@fclamp_remarks", string.IsNullOrEmpty(txtFClampRemarks.Text) ? (object)DBNull.Value : txtFClampRemarks.Text);
                                                            cmd11.Parameters.AddWithValue("@sclamp_id", sclamp_id);
                                                            cmd11.Parameters.AddWithValue("@sclamp_remarks", string.IsNullOrEmpty(txtSClampRemarks.Text) ? (object)DBNull.Value : txtSClampRemarks.Text);
                                                            cmd11.Parameters.AddWithValue("@pclamp_id", pclamp_id);
                                                            cmd11.Parameters.AddWithValue("@pclamp_remarks", string.IsNullOrEmpty(txtPClampRemarks.Text) ? (object)DBNull.Value : txtPClampRemarks.Text);
                                                            cmd11.Parameters.AddWithValue("@cable_id", cable_id);
                                                            cmd11.Parameters.AddWithValue("@cable_remarks", string.IsNullOrEmpty(txtCableRemarks.Text) ? (object)DBNull.Value : txtCableRemarks.Text);
                                                            cmd11.Parameters.AddWithValue("@fast_id", fast_id);
                                                            cmd11.Parameters.AddWithValue("@fast_remarks", string.IsNullOrEmpty(txtFastRemarks.Text) ? (object)DBNull.Value : txtFastRemarks.Text);
                                                            cmd11.Parameters.AddWithValue("@dropwire_fiber", fiber);
                                                            cmd11.Parameters.AddWithValue("@dropwire_id", dropwire_id);
                                                            cmd11.Parameters.AddWithValue("@dropwire_remarks", string.IsNullOrEmpty(txtDropwireRemarks.Text) ? (object)DBNull.Value : txtDropwireRemarks.Text);
                                                            cmd11.Parameters.AddWithValue("@mac_onu", onu);
                                                            cmd11.Parameters.AddWithValue("@mac_id", mac_id);
                                                            cmd11.Parameters.AddWithValue("@mac_remarks", string.IsNullOrEmpty(txtMacRemarks.Text) ? (object)DBNull.Value : txtMacRemarks.Text);
                                                            cmd11.Parameters.AddWithValue("@requested_by", string.IsNullOrEmpty(txtRequestedBy.Text) ? (object)DBNull.Value : txtRequestedBy.Text);
                                                            cmd11.Parameters.AddWithValue("@requested_by_date", dateRequestedBy.Value);
                                                            cmd11.Parameters.AddWithValue("@issued_to", string.IsNullOrEmpty(txtIssuedTo.Text) ? (object)DBNull.Value : txtIssuedTo.Text);
                                                            cmd11.Parameters.AddWithValue("@issued_to_date", dateIssuedTo.Value);
                                                            cmd11.Parameters.AddWithValue("@released_verified_by", string.IsNullOrEmpty(txtReleasedBy.Text) ? (object)DBNull.Value : txtReleasedBy.Text);
                                                            cmd11.Parameters.AddWithValue("@released_verified_by_date", dateReleasedBy.Value);

                                                            cmd11.ExecuteScalar();

                                                            string query_status = "UPDATE tbl_account SET account_status = 7 WHERE account_num = @account_num";
                                                            using (MySqlCommand cmd12 = new MySqlCommand(query_status, connection))
                                                            {
                                                                cmd12.Parameters.AddWithValue("@account_num", account_num);

                                                                cmd12.ExecuteNonQuery();

                                                                MessageBox.Show("Requisition successfuly created.", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                                            }
                                                        }
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
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

        private void btnRequestInfo_Click(object sender, EventArgs e)
        {
            frmAppForm3 appForm3 = new frmAppForm3();
            appForm3.parent_form = parent_form;
            appForm3.account_num = account_num;
            appForm3.isServiceActivation = true;
            appForm3.isServiceOrder = true;
            appForm3.ShowDialog();
        }

        private void checkCopper_Click(object sender, EventArgs e)
        {
            if (checkFiber.Checked == true)
            {
                checkFiber.Checked = false;
            }
        }

        private void checkFiber_Click(object sender, EventArgs e)
        {
            if (checkCopper.Checked == true)
            {
                checkCopper.Checked = false;
            }
        }

        private void checkModem_Click(object sender, EventArgs e)
        {
            if (checkONU.Checked == true)
            {
                checkONU.Checked = false;
            }
        }

        private void checkONU_Click(object sender, EventArgs e)
        {
            if (checkModem.Checked == true)
            {
                checkModem.Checked = false;
            }
        }
    }
}
