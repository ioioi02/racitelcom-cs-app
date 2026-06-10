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
    public partial class frmAppForm1View : Form
    {
        public bool previousClick { get; set; }

        // Account
        public int? account_num { get; set; }
        public DateTime? account_date { get; set; }
        public string account_type { get; set; }

        // tbl_address
        public string add_type { get; set; }
        public string add_line { get; set; }
        public string add_barangay { get; set; }
        public string add_municipality { get; set; }
        public string add_province { get; set; }
        public int add_yrs_stay { get; set; }
        public string add_home_ownership { get; set; }
        // new
        public string add_type_old { get; set; }
        public string add_line_old { get; set; }
        public string add_barangay_old { get; set; }
        public string add_municipality_old { get; set; }
        public string add_province_old { get; set; }
        public int add_yrs_stay_old { get; set; }
        public string add_home_ownership_old { get; set; }

        // tbl_customer
        public string cust_lastname { get; set; }
        public string cust_firstname { get; set; }
        public string cust_middlename { get; set; }
        public string cust_email { get; set; }
        public string cust_mobile_num { get; set; }
        public string cust_tel_num { get; set; }
        public DateTime? cust_birthdate { get; set; }
        public string cust_sex { get; set; }
        public string cust_citizenship { get; set; }
        public string cust_civil_status { get; set; }
        // new
        public string cust_lastname_old { get; set; }
        public string cust_firstname_old { get; set; }
        public string cust_middlename_old { get; set; }
        public string cust_email_old { get; set; }
        public string cust_mobile_num_old { get; set; }
        public string cust_tel_num_old { get; set; }
        public DateTime? cust_birthdate_old { get; set; }
        public string cust_sex_old { get; set; }
        public string cust_citizenship_old { get; set; }
        public string cust_civil_status_old { get; set; }
        public string emp_status_old { get; set; }

        // tbl_service_plan
        public string plan_type { get; set; }
        public bool plan_fiber { get; set; }
        public string plan_speed { get; set; }
        public string plan_static_ip { get; set; }
        public bool plan_phone_service { get; set; }
        public bool plan_ndd { get; set; }
        public bool plan_local_only { get; set; }
        public bool plan_phone_num_conf { get; set; }
        public int plan_num_line { get; set; }
        public int plan_num_extension { get; set; }
        // new
        public string plan_type_old { get; set; }
        public bool plan_fiber_old { get; set; }
        public string plan_speed_old { get; set; }
        public string plan_static_ip_old { get; set; }
        public bool plan_phone_service_old { get; set; }
        public bool plan_ndd_old { get; set; }
        public bool plan_local_only_old { get; set; }
        public bool plan_phone_num_conf_old { get; set; }
        public int plan_num_line_old { get; set; }
        public int plan_num_extension_old { get; set; }

        // tbl_employment
        public string emp_status { get; set; }
        public string emp_comp_name { get; set; }
        public string emp_comp_address { get; set; }
        public int emp_comp_yrs_in_comp { get; set; }
        public string emp_monthly_income { get; set; }
        public string emp_comp_number { get; set; }
        public string emp_comp_position { get; set; }
        // old
        public string emp_comp_name_old { get; set; }
        public string emp_comp_add_old { get; set; }
        public string emp_comp_num_old { get; set; }
        public int emp_comp_yrs_in_old { get; set; }
        public string emp_comp_position_old { get; set; }
        public string emp_monthly_income_old { get; set; }

        // tbl_business
        public string bus_name { get; set; }
        public string bus_position { get; set; }
        public string bus_contact_person { get; set; }
        // old
        public string bus_name_old { get; set; }
        public string bus_position_old { get; set; }
        public string bus_contact_per_old { get; set; }

        // tbl_spouse
        public string spouse_lastname { get; set; }
        public string spouse_firstname { get; set; }
        public string spouse_middlename { get; set; }
        public DateTime? spouse_birthdate { get; set; }
        public string spouse_contact_number { get; set; }
        public string spouse_income { get; set; }
        public string spouse_business_name { get; set; }
        public string spouse_position { get; set; }
        // old
        public string spouse_lastname_old { get; set; }
        public string spouse_firstname_old { get; set; }
        public string spouse_middlename_old { get; set; }
        public DateTime? spouse_birthdate_old { get; set; }
        public string spouse_contact_num_old { get; set; }
        public string spouse_bus_name_old { get; set; }
        public string spouse_position_old { get; set; }
        public string spouse_income_old { get; set; }

        public Form parent_form { get; set; }

        public bool fiber = true;
        public int cust_id = 0;
        public int add_id = 0;
        public int plan_id = 0;

        public frmAppForm1View()
        {
            InitializeComponent();
        }

        private void frmAppForm1View_Load(object sender, EventArgs e)
        {
            if (plan_fiber == true)
            {
                // btnFiber_Click(null, null);

                if (plan_speed == "15mbps")
                {
                    checkF15MBPS.Checked = true;
                }
                else if (plan_speed == "25mbps")
                {
                    checkF25MBPS.Checked = true;
                }
                else if (plan_speed == "30mbps")
                {
                    checkF30MBPS.Checked = true;
                }
                else if (plan_speed == "50mbps")
                {
                    checkF50MBPS.Checked = true;
                }
                else
                {
                    checkF100MBPS.Checked = true;
                }

                if (!string.IsNullOrEmpty(plan_static_ip))
                {
                    txtFStaticIP.Text = plan_static_ip;
                }

                if (plan_phone_service == true)
                {
                    checkFPhoneYes.Checked = true;
                }
                else
                {
                    checkFPhoneNo.Checked = true;
                }

                if (plan_ndd == true)
                {
                    checkFNDD.Checked = true;
                }
                if (plan_local_only == true)
                {
                    checkFLocal.Checked = true;
                }
                if (plan_phone_num_conf == true)
                {
                    checkFConfidential.Checked = true;
                }
            }
            else
            {
                // btnDSL_Click(null, null);

                if (plan_speed == "1mbps")
                {
                    checkD1MBPS.Checked = true;
                }
                else if (plan_speed == "2mbps")
                {
                    checkD2MBPS.Checked = true;
                }
                else if (plan_speed == "3mbps")
                {
                    checkD3MBPS.Checked = true;
                }
                else if (plan_speed == "4mbps")
                {
                    checkD4MBPS.Checked = true;
                }
                else if (plan_speed == "5mbps")
                {
                    checkD5MBPS.Checked = true;
                }
                else
                {
                    checkD10MBPS.Checked = true;
                }

                if (!string.IsNullOrEmpty(plan_static_ip))
                {
                    txtDStaticIP.Text = plan_static_ip;
                }

                if (plan_phone_service == true)
                {
                    checkDPhoneYes.Checked = true;
                }
                else
                {
                    checkDPhoneNo.Checked = true;
                }

                if (plan_ndd == true)
                {
                    checkDNDD.Checked = true;
                }
                if (plan_local_only == true)
                {
                    checkDLocal.Checked = true;
                }
                if (plan_phone_num_conf == true)
                {
                    checkDConfidential.Checked = true;
                }

                if (plan_num_line == 1)
                {
                    checkLines1.Checked = true;
                }
                else if (plan_num_line == 2)
                {
                    checkLines2.Checked = true;
                }

                if (plan_num_extension == 1)
                {
                    checkExtension1.Checked = true;
                }
                else if (plan_num_extension == 2)
                {
                    checkExtension1.Checked = true;
                }
            }

            // added

            if (plan_type == "RESIDENTIAL")
            {
                checkRESI.Checked = true;
            }
            if (plan_type == "COMMERCIAL")
            {
                checkCOMM.Checked = true;
            }

            // added

            if (account_date.HasValue)
            {
                dateTimeConforme.Checked = true;
                dateTimeConforme.Value = account_date.Value.Date;
            }

            if (account_type == "reconnection" || account_type == "change plan")
            {
                if (plan_type != plan_type_old)
                {
                    if (checkRESI.Checked == true)
                    {
                        checkRESI.ForeColor = Color.Maroon;
                    }
                    if (checkCOMM.Checked == true)
                    {
                        checkCOMM.ForeColor = Color.Maroon;
                    }
                }
                if (plan_fiber != plan_fiber_old)
                {
                    if (plan_fiber == true)
                    {
                        btnFiber.BackColor = Color.Maroon;
                        btnFiber.ForeColor = Color.White;
                    }
                    if (plan_fiber == false)
                    {
                        btnDSL.BackColor = Color.Maroon;
                        btnDSL.ForeColor = Color.White;
                    }
                }
                if (plan_speed != plan_speed_old)
                {
                    if (checkF15MBPS.Checked == true)
                    {
                        checkF15MBPS.ForeColor = Color.Maroon;
                    }
                    if (checkF25MBPS.Checked == true)
                    {
                        checkF25MBPS.ForeColor = Color.Maroon;
                    }
                    if (checkF30MBPS.Checked == true)
                    {
                        checkF30MBPS.ForeColor = Color.Maroon;
                    }
                    if (checkF50MBPS.Checked == true)
                    {
                        checkF50MBPS.ForeColor = Color.Maroon;
                    }
                    if (checkF100MBPS.Checked == true)
                    {
                        checkF100MBPS.ForeColor = Color.Maroon;
                    }

                    if (checkD1MBPS.Checked == true)
                    {
                        checkD1MBPS.ForeColor = Color.Maroon;
                    }
                    if (checkD2MBPS.Checked == true)
                    {
                        checkD2MBPS.ForeColor = Color.Maroon;
                    }
                    if (checkD3MBPS.Checked == true)
                    {
                        checkD3MBPS.ForeColor = Color.Maroon;
                    }
                    if (checkD4MBPS.Checked == true)
                    {
                        checkD4MBPS.ForeColor = Color.Maroon;
                    }
                    if (checkD5MBPS.Checked == true)
                    {
                        checkD5MBPS.ForeColor = Color.Maroon;
                    }
                    if (checkD10MBPS.Checked == true)
                    {
                        checkD10MBPS.ForeColor = Color.Maroon;
                    }

                    if (plan_fiber == true && (plan_static_ip != plan_static_ip_old))
                    {
                        txtFStaticIP.ForeColor = Color.Maroon;
                    }
                    if (plan_fiber == false && (plan_static_ip != plan_static_ip_old))
                    {
                        txtDStaticIP.ForeColor = Color.Maroon;
                    }

                    if (plan_fiber == true && (plan_phone_service != plan_phone_service_old))
                    {
                        if (checkFPhoneYes.Checked == true)
                        {
                            checkFPhoneYes.ForeColor = Color.Maroon;
                        }
                        if (checkFPhoneNo.Checked == true)
                        {
                            checkFPhoneNo.ForeColor = Color.Maroon;
                        }
                    }
                    if (plan_fiber == false && (plan_phone_service != plan_phone_service_old))
                    {
                        if (checkDPhoneYes.Checked == true)
                        {
                            checkDPhoneYes.ForeColor = Color.Maroon;
                        }
                        if (checkDPhoneNo.Checked == true)
                        {
                            checkDPhoneNo.ForeColor = Color.Maroon;
                        }
                    }

                    if (plan_fiber == true && (plan_ndd != plan_ndd_old))
                    {
                        checkFNDD.ForeColor = Color.Maroon;
                    }
                    if (plan_fiber == false && (plan_ndd != plan_ndd_old))
                    {
                        checkDNDD.ForeColor = Color.Maroon;
                    }

                    if (plan_fiber == true && (plan_local_only != plan_local_only_old))
                    {
                        checkFLocal.ForeColor = Color.Maroon;
                    }
                    if (plan_fiber == false && (plan_local_only != plan_local_only_old))
                    {
                        checkDLocal.ForeColor = Color.Maroon;
                    }

                    if (plan_fiber == true && (plan_phone_num_conf != plan_phone_num_conf_old))
                    {
                        checkFConfidential.ForeColor = Color.Maroon;
                    }
                    if (plan_fiber == false && (plan_phone_num_conf != plan_phone_num_conf_old))
                    {
                        checkDConfidential.ForeColor = Color.Maroon;
                    }

                    if (plan_num_line != plan_num_line_old)
                    {
                        checkLines1.ForeColor = Color.Maroon;
                    }
                    if (plan_num_extension != plan_num_extension_old)
                    {
                        checkExtension1.ForeColor = Color.Maroon;
                    }
                }
            }
        }

        private string capitalizedFirstCharacters(string input)
        {
            string[] input_split = input.Split(' ');

            for (int i = 0; i < input_split.Length; i++)
            {
                if (!string.IsNullOrEmpty(input_split[i]))
                {
                    input_split[i] = char.ToUpper(input_split[i][0]) + input_split[i].Substring(1);
                }
            }

            string result = string.Join(" ", input_split);
            return result;
        }

        private string executeScalarQuery(MySqlConnection connection, string query)
        {
            using (MySqlCommand cmd = new MySqlCommand(query, connection))
            {
                var result = cmd.ExecuteScalar();

                if (result != null)
                {
                    return result.ToString();
                }
                else
                {
                    return null;
                }
            }
        }

        private void btnFiber_Click(object sender, EventArgs e)
        {
            Button btn = sender as Button;
            if (btn != null)
            {
                checkRESI.Checked = false;
                checkCOMM.Checked = false;
            }

            btnDSL.Enabled = true;
            checkD1MBPS.Enabled = false;
            checkD2MBPS.Enabled = false;
            checkD3MBPS.Enabled = false;
            checkD4MBPS.Enabled = false;
            checkD5MBPS.Enabled = false;
            checkD10MBPS.Enabled = false;
            txtDStaticIP.Enabled = false;
            checkDPhoneYes.Enabled = false;
            checkDPhoneNo.Enabled = false;
            checkLines1.Enabled = false;
            checkLines2.Enabled = false;
            checkExtension1.Enabled = false;
            checkExtension2.Enabled = false;
            checkDNDD.Enabled = false;
            checkDLocal.Enabled = false;
            checkDConfidential.Enabled = false;

            checkD1MBPS.Checked = false;
            checkD2MBPS.Checked = false;
            checkD3MBPS.Checked = false;
            checkD4MBPS.Checked = false;
            checkD5MBPS.Checked = false;
            checkD10MBPS.Checked = false;
            txtDStaticIP.Text = "";
            checkDPhoneYes.Checked = false;
            checkDPhoneNo.Checked = false;
            checkLines1.Checked = false;
            checkLines2.Checked = false;
            checkExtension1.Checked = false;
            checkExtension2.Checked = false;
            checkDNDD.Checked = false;
            checkDLocal.Checked = false;
            checkDConfidential.Checked = false;

            btnFiber.Enabled = false;
            checkF15MBPS.Enabled = true;
            checkF25MBPS.Enabled = true;
            checkF30MBPS.Enabled = true;
            checkF50MBPS.Enabled = true;
            checkF100MBPS.Enabled = true;
            txtFStaticIP.Enabled = true;
            checkFPhoneYes.Enabled = true;
            checkFPhoneNo.Enabled = true;
            checkFNDD.Enabled = true;
            checkFLocal.Enabled = true;
            checkFConfidential.Enabled = true;

            txtConforme.SelectionLength = 0;

            fiber = true;
        }

        private void btnDSL_Click(object sender, EventArgs e)
        {
            Button btn = sender as Button;
            if (btn != null)
            {
                checkRESI.Checked = false;
                checkCOMM.Checked = false;
            }

            btnFiber.Enabled = true;
            checkF15MBPS.Enabled = false;
            checkF25MBPS.Enabled = false;
            checkF30MBPS.Enabled = false;
            checkF50MBPS.Enabled = false;
            checkF100MBPS.Enabled = false;
            txtFStaticIP.Enabled = false;
            checkFPhoneYes.Enabled = false;
            checkFPhoneNo.Enabled = false;
            checkFNDD.Enabled = false;
            checkFLocal.Enabled = false;
            checkFConfidential.Enabled = false;

            checkF15MBPS.Checked = false;
            checkF25MBPS.Checked = false;
            checkF30MBPS.Checked = false;
            checkF50MBPS.Checked = false;
            checkF100MBPS.Checked = false;
            txtFStaticIP.Text = "";
            checkFPhoneYes.Checked = false;
            checkFPhoneNo.Checked = false;
            checkFNDD.Checked = false;
            checkFLocal.Checked = false;
            checkFConfidential.Checked = false;

            btnDSL.Enabled = false;
            checkD1MBPS.Enabled = true;
            checkD2MBPS.Enabled = true;
            checkD3MBPS.Enabled = true;
            checkD4MBPS.Enabled = true;
            checkD5MBPS.Enabled = true;
            checkD10MBPS.Enabled = true;
            txtDStaticIP.Enabled = true;
            checkDPhoneYes.Enabled = true;
            checkDPhoneNo.Enabled = true;
            checkLines1.Enabled = true;
            checkLines2.Enabled = true;
            checkExtension1.Enabled = true;
            checkExtension2.Enabled = true;
            checkDNDD.Enabled = true;
            checkDLocal.Enabled = true;
            checkDConfidential.Enabled = true;

            txtConforme.SelectionLength = 0;

            fiber = false;
        }

        private void checkF15MBPS_Click(object sender, EventArgs e)
        {
            if (checkF25MBPS.Checked == true)
            {
                checkF25MBPS.Checked = false;
            }
            if (checkF30MBPS.Checked == true)
            {
                checkF30MBPS.Checked = false;
            }
            if (checkF50MBPS.Checked == true)
            {
                checkF50MBPS.Checked = false;
            }
            if (checkF100MBPS.Checked == true)
            {
                checkF100MBPS.Checked = false;
            }
        }

        private void checkF25MBPS_Click(object sender, EventArgs e)
        {
            if (checkF15MBPS.Checked == true)
            {
                checkF15MBPS.Checked = false;
            }
            if (checkF30MBPS.Checked == true)
            {
                checkF30MBPS.Checked = false;
            }
            if (checkF50MBPS.Checked == true)
            {
                checkF50MBPS.Checked = false;
            }
            if (checkF100MBPS.Checked == true)
            {
                checkF100MBPS.Checked = false;
            }
        }

        private void checkF30MBPS_Click(object sender, EventArgs e)
        {
            if (checkF15MBPS.Checked == true)
            {
                checkF15MBPS.Checked = false;
            }
            if (checkF25MBPS.Checked == true)
            {
                checkF25MBPS.Checked = false;
            }
            if (checkF50MBPS.Checked == true)
            {
                checkF50MBPS.Checked = false;
            }
            if (checkF100MBPS.Checked == true)
            {
                checkF100MBPS.Checked = false;
            }
        }


        private void checkF50MBPS_Click(object sender, EventArgs e)
        {
            if (checkF15MBPS.Checked == true)
            {
                checkF15MBPS.Checked = false;
            }
            if (checkF25MBPS.Checked == true)
            {
                checkF25MBPS.Checked = false;
            }
            if (checkF30MBPS.Checked == true)
            {
                checkF30MBPS.Checked = false;
            }
            if (checkF100MBPS.Checked == true)
            {
                checkF100MBPS.Checked = false;
            }
        }

        private void checkF100MBPS_Click(object sender, EventArgs e)
        {
            if (checkF15MBPS.Checked == true)
            {
                checkF15MBPS.Checked = false;
            }
            if (checkF25MBPS.Checked == true)
            {
                checkF25MBPS.Checked = false;
            }
            if (checkF30MBPS.Checked == true)
            {
                checkF30MBPS.Checked = false;
            }
            if (checkF50MBPS.Checked == true)
            {
                checkF50MBPS.Checked = false;
            }
        }

        private void checkFPhoneYes_Click(object sender, EventArgs e)
        {
            if (checkFPhoneNo.Checked == true)
            {
                checkFPhoneNo.Checked = false;
            }
        }

        private void checkFPhoneNo_Click(object sender, EventArgs e)
        {
            if (checkFPhoneYes.Checked == true)
            {
                checkFPhoneYes.Checked = false;
            }
        }

        private void checkD1MBPS_Click(object sender, EventArgs e)
        {
            if (checkD2MBPS.Checked == true)
            {
                checkD2MBPS.Checked = false;
            }
            if (checkD3MBPS.Checked == true)
            {
                checkD3MBPS.Checked = false;
            }
            if (checkD4MBPS.Checked == true)
            {
                checkD4MBPS.Checked = false;
            }
            if (checkD5MBPS.Checked == true)
            {
                checkD5MBPS.Checked = false;
            }
            if (checkD10MBPS.Checked == true)
            {
                checkD10MBPS.Checked = false;
            }
        }

        private void checkD2MBPS_Click(object sender, EventArgs e)
        {
            if (checkD1MBPS.Checked == true)
            {
                checkD1MBPS.Checked = false;
            }
            if (checkD3MBPS.Checked == true)
            {
                checkD3MBPS.Checked = false;
            }
            if (checkD4MBPS.Checked == true)
            {
                checkD4MBPS.Checked = false;
            }
            if (checkD5MBPS.Checked == true)
            {
                checkD5MBPS.Checked = false;
            }
            if (checkD10MBPS.Checked == true)
            {
                checkD10MBPS.Checked = false;
            }
        }

        private void checkD3MBPS_Click(object sender, EventArgs e)
        {
            if (checkD1MBPS.Checked == true)
            {
                checkD1MBPS.Checked = false;
            }
            if (checkD2MBPS.Checked == true)
            {
                checkD2MBPS.Checked = false;
            }
            if (checkD4MBPS.Checked == true)
            {
                checkD4MBPS.Checked = false;
            }
            if (checkD5MBPS.Checked == true)
            {
                checkD5MBPS.Checked = false;
            }
            if (checkD10MBPS.Checked == true)
            {
                checkD10MBPS.Checked = false;
            }
        }

        private void checkD4MBPS_Click(object sender, EventArgs e)
        {
            if (checkD1MBPS.Checked == true)
            {
                checkD1MBPS.Checked = false;
            }
            if (checkD2MBPS.Checked == true)
            {
                checkD2MBPS.Checked = false;
            }
            if (checkD3MBPS.Checked == true)
            {
                checkD3MBPS.Checked = false;
            }
            if (checkD5MBPS.Checked == true)
            {
                checkD5MBPS.Checked = false;
            }
            if (checkD10MBPS.Checked == true)
            {
                checkD10MBPS.Checked = false;
            }
        }

        private void checkD5MBPS_Click(object sender, EventArgs e)
        {
            if (checkD1MBPS.Checked == true)
            {
                checkD1MBPS.Checked = false;
            }
            if (checkD2MBPS.Checked == true)
            {
                checkD2MBPS.Checked = false;
            }
            if (checkD3MBPS.Checked == true)
            {
                checkD3MBPS.Checked = false;
            }
            if (checkD4MBPS.Checked == true)
            {
                checkD4MBPS.Checked = false;
            }
            if (checkD10MBPS.Checked == true)
            {
                checkD10MBPS.Checked = false;
            }
        }

        private void checkD10MBPS_Click(object sender, EventArgs e)
        {
            if (checkD1MBPS.Checked == true)
            {
                checkD1MBPS.Checked = false;
            }
            if (checkD2MBPS.Checked == true)
            {
                checkD2MBPS.Checked = false;
            }
            if (checkD3MBPS.Checked == true)
            {
                checkD3MBPS.Checked = false;
            }
            if (checkD4MBPS.Checked == true)
            {
                checkD4MBPS.Checked = false;
            }
            if (checkD5MBPS.Checked == true)
            {
                checkD5MBPS.Checked = false;
            }
        }

        private void checkDPhoneYes_Click(object sender, EventArgs e)
        {
            if (checkDPhoneNo.Checked == true)
            {
                checkDPhoneNo.Checked = false;
            }
        }

        private void checkDPhoneNo_Click(object sender, EventArgs e)
        {
            if (checkDPhoneYes.Checked == true)
            {
                checkDPhoneYes.Checked = false;
            }
        }

        private void checkLines1_Click(object sender, EventArgs e)
        {
            if (checkLines2.Checked == true)
            {
                checkLines2.Checked = false;
            }
        }

        private void checkLines2_Click(object sender, EventArgs e)
        {
            if (checkLines1.Checked == true)
            {
                checkLines1.Checked = false;
            }
        }

        private void checkExtension1_Click(object sender, EventArgs e)
        {
            if (checkExtension2.Checked == true)
            {
                checkExtension2.Checked = false;
            }
        }

        private void checkExtension2_Click(object sender, EventArgs e)
        {
            if (checkExtension1.Checked == true)
            {
                checkExtension1.Checked = false;
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            // this.Dispose();
            this.Close();
        }

        private void btnPrevForm_Click(object sender, EventArgs e)
        {
            // tbl_service_plan

            // added

            if (checkRESI.Checked == true)
            {
                plan_type = "RESIDENTIAL";
            }
            if (checkCOMM.Checked == true)
            {
                plan_type = "COMMERCIAL";
            }
            /* if (string.IsNullOrEmpty(plan_type))
            {
                MessageBox.Show("Please select plan type.", "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }*/

            // added

            if (btnFiber.Enabled == false || plan_fiber == true)
            {
                plan_fiber = true;
                if (checkF15MBPS.Checked == true)
                {
                    plan_speed = "15mbps";
                }
                if (checkF25MBPS.Checked == true)
                {
                    plan_speed = "25mbps";
                }
                if (checkF30MBPS.Checked == true)
                {
                    plan_speed = "30mbps";
                }
                if (checkF50MBPS.Checked == true)
                {
                    plan_speed = "50mbps";
                }
                if (checkF100MBPS.Checked == true)
                {
                    plan_speed = "100mbps";
                }

                if (!string.IsNullOrEmpty(txtFStaticIP.Text))
                {
                    plan_static_ip = txtFStaticIP.Text;
                }

                if (checkFPhoneYes.Checked == true)
                {
                    plan_phone_service = true;
                }
                else
                {
                    plan_phone_service = false;
                }

                if (checkFNDD.Checked == true)
                {
                    plan_ndd = true;
                }
                else
                {
                    plan_ndd = false;
                }

                if (checkFLocal.Checked == true)
                {
                    plan_local_only = true;
                }
                else
                {
                    plan_local_only = false;
                }

                if (checkFConfidential.Checked == true)
                {
                    plan_phone_num_conf = true;
                }
                else
                {
                    plan_phone_num_conf = false;
                }
            }
            else if (btnDSL.Enabled == false || plan_fiber == false)
            {
                plan_fiber = false;
                if (checkD1MBPS.Checked == true)
                {
                    plan_speed = "1mbps";
                }
                if (checkD2MBPS.Checked == true)
                {
                    plan_speed = "2mbps";
                }
                if (checkD3MBPS.Checked == true)
                {
                    plan_speed = "3mbps";
                }
                if (checkD4MBPS.Checked == true)
                {
                    plan_speed = "4mbps";
                }
                if (checkD5MBPS.Checked == true)
                {
                    plan_speed = "5mbps";
                }
                if (checkD10MBPS.Checked == true)
                {
                    plan_speed = "10mbps";
                }

                if (!string.IsNullOrEmpty(txtDStaticIP.Text))
                {
                    plan_static_ip = txtDStaticIP.Text;
                }

                if (checkDPhoneYes.Checked == true)
                {
                    plan_phone_service = true;
                }
                else
                {
                    plan_phone_service = false;
                }

                if (checkDNDD.Checked == true)
                {
                    plan_ndd = true;
                }
                else
                {
                    plan_ndd = false;
                }

                if (checkDLocal.Checked == true)
                {
                    plan_local_only = true;
                }
                else
                {
                    plan_local_only = false;
                }

                if (checkDConfidential.Checked == true)
                {
                    plan_phone_num_conf = true;
                }
                else
                {
                    plan_phone_num_conf = false;
                }

                if (checkLines1.Checked == true)
                {
                    plan_num_line = 1;
                }
                else if (checkLines2.Checked == true)
                {
                    plan_num_line = 2;
                }
                else
                {
                    plan_num_line = 0;
                }

                if (checkExtension1.Checked == true)
                {
                    plan_num_extension = 1;
                }
                else if (checkExtension2.Checked == true)
                {
                    plan_num_extension = 2;
                }
                else
                {
                    plan_num_extension = 0;
                }
            }

            frmAppFormView appFormView = new frmAppFormView();

            appFormView.parent_form = parent_form;
            appFormView.previousClick = true;

            // Account
            appFormView.account_num = account_num;
            appFormView.add_id = add_id;
            appFormView.plan_id = plan_id;
            appFormView.account_date = account_date;
            appFormView.account_type = account_type;

            // Address
            appFormView.cust_id = cust_id;
            appFormView.add_type = add_type;
            appFormView.add_line = add_line;
            appFormView.add_barangay = add_barangay;
            appFormView.add_municipality = add_municipality;
            appFormView.add_province = add_province;
            appFormView.add_yrs_stay = add_yrs_stay;
            appFormView.add_home_ownership = add_home_ownership;

            // Customer
            appFormView.cust_lastname = cust_lastname;
            appFormView.cust_firstname = cust_firstname;
            appFormView.cust_middlename = cust_middlename;
            appFormView.cust_email = cust_email;
            appFormView.cust_mobile_num = cust_mobile_num;
            appFormView.cust_tel_num = cust_tel_num;
            appFormView.cust_birthdate = cust_birthdate;
            appFormView.cust_sex = cust_sex;
            appFormView.cust_citizenship = cust_citizenship;
            appFormView.cust_civil_status = cust_civil_status;
            appFormView.emp_status = emp_status;

            // Plan
            appFormView.plan_type = plan_type;
            appFormView.plan_fiber = plan_fiber;
            appFormView.plan_speed = plan_speed;
            appFormView.plan_static_ip = plan_static_ip;
            appFormView.plan_phone_service = plan_phone_service;
            appFormView.plan_ndd = plan_ndd;
            appFormView.plan_local_only = plan_local_only;
            appFormView.plan_phone_num_conf = plan_phone_num_conf;
            appFormView.plan_num_line = plan_num_line;
            appFormView.plan_num_extension = plan_num_extension;

            // Employment
            appFormView.emp_comp_name = emp_comp_name;
            appFormView.emp_comp_add = emp_comp_address;
            appFormView.emp_comp_num = emp_comp_number;
            appFormView.emp_comp_yrs_in = emp_comp_yrs_in_comp;
            appFormView.emp_comp_position = emp_comp_position;
            appFormView.emp_monthly_income = emp_monthly_income;

            // Business
            appFormView.bus_name = bus_name;
            appFormView.bus_position = bus_position;
            appFormView.bus_contact_per = bus_contact_person;

            // Spouse
            appFormView.spouse_lastname = spouse_lastname;
            appFormView.spouse_firstname = spouse_firstname;
            appFormView.spouse_middlename = spouse_middlename;
            appFormView.spouse_birthdate = spouse_birthdate;
            appFormView.spouse_contact_num = spouse_contact_number;
            appFormView.spouse_bus_name = spouse_business_name;
            appFormView.spouse_position = spouse_position;
            appFormView.spouse_income = spouse_income;

            if (account_type == "reconnection" || account_type == "information update")
            {
                // tbl_address
                appFormView.add_type_old = add_type_old;
                appFormView.add_line_old = add_line_old;
                appFormView.add_barangay_old = add_barangay_old;
                appFormView.add_municipality_old = add_municipality_old;
                appFormView.add_province_old = add_province_old;
                appFormView.add_yrs_stay_old = add_yrs_stay_old;
                appFormView.add_home_ownership_old = add_home_ownership_old;

                // tbl_customer
                appFormView.cust_lastname_old = cust_lastname_old;
                appFormView.cust_firstname_old = cust_firstname_old;
                appFormView.cust_middlename_old = cust_middlename_old;
                appFormView.cust_email_old = cust_email_old;
                appFormView.cust_mobile_num_old = cust_mobile_num_old;
                appFormView.cust_tel_num_old = cust_tel_num_old;
                appFormView.cust_birthdate_old = cust_birthdate_old;
                appFormView.cust_sex_old = cust_sex_old;
                appFormView.cust_citizenship_old = cust_citizenship_old;
                appFormView.cust_civil_status_old = cust_civil_status_old;

                // tbl_employment
                appFormView.emp_status_old = emp_status_old;
                appFormView.emp_comp_name_old = emp_comp_name_old;
                appFormView.emp_comp_add_old = emp_comp_add_old;
                appFormView.emp_comp_yrs_in_old = emp_comp_yrs_in_old;
                appFormView.emp_monthly_income_old = emp_monthly_income_old;
                appFormView.emp_comp_num_old = emp_comp_num_old;
                appFormView.emp_comp_position_old = emp_comp_position_old;

                // tbl_business
                appFormView.bus_name_old = bus_name_old;
                appFormView.bus_position_old = bus_position_old;
                appFormView.bus_contact_per_old = bus_contact_per_old;

                // tbl_spouse
                appFormView.spouse_lastname_old = spouse_lastname_old;
                appFormView.spouse_firstname_old = spouse_firstname_old;
                appFormView.spouse_middlename_old = spouse_middlename_old;
                appFormView.spouse_birthdate_old = spouse_birthdate_old;
                appFormView.spouse_contact_num_old = spouse_contact_num_old;
                appFormView.spouse_income_old = spouse_income_old;
                appFormView.spouse_bus_name_old = spouse_bus_name_old;
                appFormView.spouse_position_old = spouse_position_old;
            }
            if (account_type == "reconnection" || account_type == "change plan")
            {
                // tbl_service_plan
                appFormView.plan_type_old = plan_type_old;
                appFormView.plan_fiber_old = plan_fiber_old;
                appFormView.plan_speed_old = plan_speed_old;
                appFormView.plan_static_ip_old = plan_static_ip_old;
                appFormView.plan_phone_service_old = plan_phone_service_old;
                appFormView.plan_ndd_old = plan_ndd_old;
                appFormView.plan_local_only_old = plan_local_only_old;
                appFormView.plan_phone_num_conf_old = plan_phone_num_conf_old;
                appFormView.plan_num_line_old = plan_num_line_old;
                appFormView.plan_num_extension_old = plan_num_extension_old;
            }

            appFormView.ShowDialog();
            // this.Dispose();
            this.Close();
        }

        private void btnVerify_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Updating is still in development, edit will not saved in the database yet.", "", MessageBoxButtons.OK, MessageBoxIcon.Information);

            if (account_type == "new account")
            {
                MySqlConnection connection = MyConnectionString.mysql_connection();
                try
                {
                    connection.Open();

                    string query = "UPDATE tbl_account SET account_status = 4 WHERE account_num = @account_num";
                    using (MySqlCommand cmd = new MySqlCommand(query, connection))
                    {
                        cmd.Parameters.AddWithValue("@account_num", account_num);
                        cmd.ExecuteNonQuery();

                        MessageBox.Show("Customer information verified.", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
                if (!string.IsNullOrEmpty(cust_middlename))
                {
                    log_type = $"verify an application form with the name {capitalizedFirstCharacters(cust_firstname)} {capitalizedFirstCharacters(cust_middlename)} {capitalizedFirstCharacters(cust_lastname)}";
                }
                else
                {
                    log_type = $"verify an application form with the name {capitalizedFirstCharacters(cust_firstname)} {capitalizedFirstCharacters(cust_lastname)}";
                }

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

            }
        }

        private void setEnableAllControls(Control control)
        {
            foreach (Control ctrl in control.Controls)
            {
                if (ctrl is TextBox)
                {
                    TextBox textBox = (TextBox)ctrl;
                    textBox.ReadOnly = false;
                }
                else if (ctrl is CheckBox)
                {
                    CheckBox checkbox = (CheckBox)ctrl;
                    checkbox.Enabled = true;
                }
                else if (ctrl.HasChildren)
                {
                    setEnableAllControls(ctrl);
                }
            }
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Edit is still in development and not yet tested.", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
            if (account_type != "information update")
            {
                setEnableAllControls(this);
                dateTimeConforme.Enabled = false;
            }
            else
            {
                MessageBox.Show("You can't edit this section of form.", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

            if (plan_type == "RESIDENTIAL")
            {
                checkF25MBPS.Enabled = false;
            }
            if (plan_type == "COMMERCIAL")
            {
                checkF30MBPS.Enabled = false;
            }
        }

        private void checkRESI_Click(object sender, EventArgs e)
        {
            if (checkCOMM.Checked == true)
            {
                checkCOMM.Checked = false;
            }
            if (btnFiber.Enabled == false)
            {
                if (checkF25MBPS.Checked == true)
                {
                    checkF25MBPS.Checked = false;
                }
                if (checkF30MBPS.Enabled == false)
                {
                    checkF30MBPS.Enabled = true;
                }
                checkF25MBPS.Enabled = false;
            }
        }

        private void checkCOMM_Click(object sender, EventArgs e)
        {
            if (checkRESI.Checked == true)
            {
                checkRESI.Checked = false;
            }
            if (btnFiber.Enabled == false)
            {
                if (checkF30MBPS.Checked == true)
                {
                    checkF30MBPS.Checked = false;
                }
                if (checkF25MBPS.Enabled == false)
                {
                    checkF25MBPS.Enabled = true;
                }
                checkF30MBPS.Enabled = false;
            }
        }

        private void lbl_DoubleClick(object sender, EventArgs e)
        {
            Label lbl = sender as Label;
            if (lbl == lblFBandwidth)
            {
                if (plan_speed != plan_speed_old)
                {
                    MessageBox.Show($"{plan_speed_old}  -->  {plan_speed}");
                }
            }
            if (lbl == lblDBandwidth)
            {
                if (plan_speed != plan_speed_old)
                {
                    MessageBox.Show($"{plan_speed_old}  -->  {plan_speed}");
                }
            }
        }
    }
}
