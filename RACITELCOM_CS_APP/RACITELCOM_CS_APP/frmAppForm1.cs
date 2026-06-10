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
    public partial class frmAppForm1 : Form
    {
        public int? account_num { get; set; }
        public DateTime? account_date { get; set; }
        public bool previousClick { get; set; }
        public string account_type { get; set; }

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

        // tbl_address
        public string add_type { get; set; }
        public string add_line { get; set; }
        public string add_barangay { get; set; }
        public string add_municipality { get; set; }
        public string add_province { get; set; }
        public int add_yrs_stay { get; set; }
        public string add_home_ownership { get; set; }

        // tbl_business
        public string bus_name { get; set; }
        public string bus_position { get; set; }
        public string bus_contact_person { get; set; }

        // tbl_spouse
        public string spouse_lastname { get; set; }
        public string spouse_firstname { get; set; }
        public string spouse_middlename { get; set; }
        public DateTime? spouse_birthdate { get; set; }
        public string spouse_contact_number { get; set; }
        public string spouse_income { get; set; }
        public string spouse_business_name { get; set; }
        public string spouse_position { get; set; }

        // tbl_employment
        public string emp_status { get; set; }
        public string emp_comp_name { get; set; }
        public string emp_comp_address { get; set; }
        public int emp_comp_yrs_in_comp { get; set; }
        public string emp_monthly_income { get; set; }
        public string emp_comp_number { get; set; }
        public string emp_comp_position { get; set; }

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

        public int cust_id = 0;
        public int add_id = 0;
        public int plan_id = 0;
        public int temp_id = 0;

        private Form parentForm;
        public frmAppForm1(Form form)
        {
            InitializeComponent();
            this.parentForm = form;
        }

        private void frmAppForm1_Load(object sender, EventArgs e)
        {
            // test

            if (account_type == "information update")
            {
                setControls(this, true, false);
                dateTimeConforme.Enabled = false;
            }
            if (account_type == "change plan" || account_type == "reconnection")
            {
                setControls(this, false, true);
                dateTimeConforme.Enabled = false;
            }

            // test

            dateTimeConforme.CustomFormat = "MMMM dd, yyyy";
            if (account_date != null)
            {
                dateTimeConforme.Value = account_date.Value;
                dateTimeConforme.Format = DateTimePickerFormat.Custom;
                dateTimeConforme.CustomFormat = "MMMM dd, yyyy";
            }

            // dateTimeConforme.Value = DateTime.Now;
            // dateTimeConforme.Checked = false;

            if (previousClick == false)
            {
                plan_fiber = true;
            }
            if (plan_fiber == false)
            {
                if (account_type != "information update")
                {
                    btnDSL_Click(null, null);
                }

                if (plan_speed == "1mbps")
                {
                    checkD1MBPS.Checked = true;
                }
                if (plan_speed == "2mbps")
                {
                    checkD2MBPS.Checked = true;
                }
                if (plan_speed == "3mbps")
                {
                    checkD3MBPS.Checked = true;
                }
                if (plan_speed == "4mbps")
                {
                    checkD4MBPS.Checked = true;
                }
                if (plan_speed == "5mbps")
                {
                    checkD5MBPS.Checked = true;
                }
                if (plan_speed == "10mbps")
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
                if (plan_phone_service == false)
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
                if (plan_num_line == 2)
                {
                    checkLines2.Checked = true;
                }
                if (plan_num_extension == 1)
                {
                    checkExtension1.Checked = true;
                }
                if (plan_num_extension == 2)
                {
                    checkExtension2.Checked = true;
                }
            }
            else
            {
                if (account_type != "information update")
                {
                    btnFiber_Click(null, null);
                }

                if (plan_speed == "15mbps")
                {
                    checkF15MBPS.Checked = true;
                }
                if (plan_speed == "25mbps")
                {
                    checkF25MBPS.Checked = true;
                }
                if (plan_speed == "30mbps")
                {
                    checkF30MBPS.Checked = true;
                }
                if (plan_speed == "50mbps")
                {
                    checkF50MBPS.Checked = true;
                }
                if (plan_speed == "100mbps")
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
                if (plan_phone_service == false)
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

            // added

            if (plan_type == "RESIDENTIAL")
            {
                checkRESI.Checked = true;
                checkF25MBPS.Enabled = false;
            }
            if (plan_type == "COMMERCIAL")
            {
                checkCOMM.Checked = true;
                checkF30MBPS.Enabled = false;
            }

            // added
        }

        private void setControls(Control control, bool bool1, bool bool2)
        {
            foreach (Control ctrl in control.Controls)
            {
                if (ctrl is TextBox)
                {
                    TextBox textBox = (TextBox)ctrl;
                    textBox.ReadOnly = bool1;
                }
                else if (ctrl is CheckBox)
                {
                    CheckBox checkbox = (CheckBox)ctrl;
                    checkbox.Enabled = bool2;
                }
                else if (ctrl is Button)
                {
                    Button button = (Button)ctrl;
                    if(button == btnFiber || button == btnDSL)
                    {
                        button.Enabled = bool2;
                    }
                }
                else if (ctrl.HasChildren)
                {
                    setControls(ctrl, bool1, bool2);
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

            plan_fiber = true;
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

            plan_fiber = false;
        }

        private void updateAccountUpdateStatus(int status)
        {
            MySqlConnection connection = MyConnectionString.mysql_connection();
            try
            {
                connection.Open();

                string query1 = @"UPDATE tbl_account
                                 SET account_type = @account_type,
                                 account_update_status = @account_update_status
                                 WHERE account_num = @account_num;";
                using (MySqlCommand cmd1 = new MySqlCommand(query1, connection))
                {
                    cmd1.Parameters.AddWithValue("@account_type", account_type);
                    cmd1.Parameters.AddWithValue("@account_update_status", status);
                    cmd1.Parameters.AddWithValue("@account_num", account_num);

                    cmd1.ExecuteNonQuery();

                    string query2 = @"INSERT INTO tbl_temp (account_num, temp_status)
                                        VALUES (@account_num, @temp_status);
                                        SELECT LAST_INSERT_ID();";
                    using (MySqlCommand cmd2 = new MySqlCommand(query2, connection))
                    {
                        cmd2.Parameters.AddWithValue("@account_num", account_num);
                        cmd2.Parameters.AddWithValue("@temp_status", status);

                        temp_id = Convert.ToInt32(cmd2.ExecuteScalar());
                    }
                }
            }
            catch (Exception ex)
            {
                txtFStaticIP.Text = $"Error: {ex.ToString()}";
                MessageBox.Show($"Error: {ex.ToString()}", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                // MessageBox.Show("An error has occured.", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                connection.Close();
            }
        }

        private void insertCustomer(bool temp)
        {
            MySqlConnection connection = MyConnectionString.mysql_connection();
            try
            {
                connection.Open();

                string query = null;
                if (temp == true)
                {
                    query = @"INSERT INTO tbl_customer_temp (temp_id, cust_lastname, cust_firstname, cust_middlename, cust_email, cust_mobile_num, cust_tel_num, cust_birthdate, cust_sex, cust_citizenship, cust_civil_status, emp_status)
                                VALUES (@temp_id, @lastname, @firstname, @middlename, @email, @mobile_num, @tel_num, @birthdate, @sex, @citizenship, @civil_status, @emp_status);";
                }
                else
                {
                    query = @"INSERT INTO tbl_customer (cust_lastname, cust_firstname, cust_middlename, cust_email, cust_mobile_num, cust_tel_num, cust_birthdate, cust_sex, cust_citizenship, cust_civil_status, emp_status)
                                VALUES (@lastname, @firstname, @middlename, @email, @mobile_num, @tel_num, @birthdate, @sex, @citizenship, @civil_status, @emp_status);
                                SELECT LAST_INSERT_ID();";
                }
                MessageBox.Show("123");
                using (MySqlCommand cmd = new MySqlCommand(query, connection))
                {
                    cmd.Parameters.AddWithValue("@lastname", cust_lastname);
                    cmd.Parameters.AddWithValue("@firstname", cust_firstname);
                    cmd.Parameters.AddWithValue("@middlename", cust_middlename);
                    cmd.Parameters.AddWithValue("@email", cust_email);
                    cmd.Parameters.AddWithValue("@mobile_num", cust_mobile_num);
                    cmd.Parameters.AddWithValue("@tel_num", cust_tel_num);
                    cmd.Parameters.AddWithValue("@birthdate", cust_birthdate);
                    cmd.Parameters.AddWithValue("@sex", cust_sex);
                    cmd.Parameters.AddWithValue("@citizenship", cust_citizenship);
                    cmd.Parameters.AddWithValue("@civil_status", cust_civil_status);
                    cmd.Parameters.AddWithValue("@emp_status", emp_status);

                    if (temp == true)
                    {
                        cmd.Parameters.AddWithValue("@temp_id", temp_id);
                        cmd.ExecuteScalar();
                    }
                    else
                    {
                        cust_id = Convert.ToInt32(cmd.ExecuteScalar());
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

        private void updateCustomer()
        {
            MySqlConnection connection = MyConnectionString.mysql_connection();
            try
            {
                connection.Open();

                string query = @"UPDATE tbl_customer as customer
                                 INNER JOIN tbl_address as address ON customer.cust_id ON address.cust_id
                                 INNER JOIN tbl_account as account ON address.add_id = account.add_id
                                 SET customer.lastname = @lastname,
                                     customer.firstname = @firstname,
                                     customer.middlename = @middlename,
                                     customer.email = @email,
                                     customer.mobile_num = @mobile_num,
                                     customer.tel_num = @tel_num,
                                     customer.birthdate = @birthdate,
                                     customer.sex = @sex,
                                     customer.citizenship = @citizenship,
                                     customer.civil_status = @civil_status,
                                     customer.emp_status = @emp_status
                                 WHERE account.account_num = @account_num;";
                using (MySqlCommand cmd = new MySqlCommand(query, connection))
                {
                    cmd.Parameters.AddWithValue("@lastname", cust_lastname);
                    cmd.Parameters.AddWithValue("@firstname", cust_firstname);
                    cmd.Parameters.AddWithValue("@middlename", cust_middlename);
                    cmd.Parameters.AddWithValue("@email", cust_email);
                    cmd.Parameters.AddWithValue("@mobile_num", cust_mobile_num);
                    cmd.Parameters.AddWithValue("@tel_num", cust_tel_num);
                    cmd.Parameters.AddWithValue("@birthdate", cust_birthdate);
                    cmd.Parameters.AddWithValue("@sex", cust_sex);
                    cmd.Parameters.AddWithValue("@citizenship", cust_citizenship);
                    cmd.Parameters.AddWithValue("@civil_status", cust_civil_status);
                    cmd.Parameters.AddWithValue("@emp_status", emp_status);
                    cmd.Parameters.AddWithValue("@account_num", account_num);

                    cmd.ExecuteNonQuery();

                    /*string query2 = @"SELECT customer.cust_id 
                                           FROM tbl_customer as customer
                                           INNER JOIN tbl_address as address ON customer.cust_id ON address.cust_id
                                           INNER JOIN tbl_account as account ON address.add_id = account.add_id
                                           WHERE account.account_num = @account_num;";

                    using (MySqlCommand cmd2 = new MySqlCommand(query2, connection))
                    {
                        cmd2.Parameters.AddWithValue("@account_num", account_num);

                        cust_id = Convert.ToInt32(cmd2.ExecuteScalar());
                    }*/
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
        
        private void insertAddress(bool temp)
        {
            MySqlConnection connection = MyConnectionString.mysql_connection();
            try
            {
                connection.Open();

                string query = null;
                if (temp == true)
                {
                    query = @"INSERT INTO tbl_address_temp (temp_id, add_type, add_line, add_barangay, add_municipality, add_province, add_yrs_stay, add_home_ownership)
                                VALUES (@temp_id, @add_type, @add_line, @add_barangay, @add_municipality, @add_province, @add_yrs_stay, @add_home_ownership);";
                }
                else
                {
                    query = @"INSERT INTO tbl_address (cust_id, add_type, add_line, add_barangay, add_municipality, add_province, add_yrs_stay, add_home_ownership)
                                 VALUES (@cust_id, @add_type, @add_line, @add_barangay, @add_municipality, @add_province, @add_yrs_stay, @add_home_ownership);
                                 SELECT LAST_INSERT_ID();";
                }
                using (MySqlCommand cmd = new MySqlCommand(query, connection))
                {
                    cmd.Parameters.AddWithValue("@add_type", add_type);
                    cmd.Parameters.AddWithValue("@add_line", add_line);
                    cmd.Parameters.AddWithValue("@add_barangay", add_barangay);
                    cmd.Parameters.AddWithValue("@add_municipality", add_municipality);
                    cmd.Parameters.AddWithValue("@add_province", add_province);
                    cmd.Parameters.AddWithValue("@add_yrs_stay", add_yrs_stay);
                    cmd.Parameters.AddWithValue("@add_home_ownership", add_home_ownership);

                    if (temp == true)
                    {
                        cmd.Parameters.AddWithValue("@temp_id", temp_id);
                        cmd.ExecuteScalar();
                    }
                    else
                    {
                        cmd.Parameters.AddWithValue("@cust_id", cust_id);
                        add_id = Convert.ToInt32(cmd.ExecuteScalar());
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

        private void updateAddress()
        {
            MySqlConnection connection = MyConnectionString.mysql_connection();
            try
            {
                connection.Open();

                string query = @"UPDATE tbl_address as address
                                       INNER JOIN tbl_account as account ON address.add_id = account.add_id
                                       SET address.add_type = @add_type, 
                                           address.add_line = @add_line, 
                                           address.add_barangay = @add_barangay, 
                                           address.add_municipality = @add_municipality, 
                                           address.add_province = @add_province, 
                                           address.add_yrs_stay = @add_yrs_stay, 
                                           address.add_home_ownership = @add_home_ownership
                                       WHERE account.account_num = @account_num;";
                using (MySqlCommand cmd = new MySqlCommand(query, connection))
                {
                    cmd.Parameters.AddWithValue("@add_type", add_type);
                    cmd.Parameters.AddWithValue("@add_line", add_line);
                    cmd.Parameters.AddWithValue("@add_barangay", add_barangay);
                    cmd.Parameters.AddWithValue("@add_municipality", add_municipality);
                    cmd.Parameters.AddWithValue("@add_province", add_province);
                    cmd.Parameters.AddWithValue("@add_yrs_stay", add_yrs_stay);
                    cmd.Parameters.AddWithValue("@add_home_ownership", add_home_ownership);
                    cmd.Parameters.AddWithValue("@account_num", account_num);

                    cmd.ExecuteNonQuery();

                    /*string query2 = @"SELECT adress.add_id 
                                           FROM tbl_address as adress
                                           INNER JOIN tbl_account as account ON address.add_id = account.add_id
                                           WHERE account.account_num = @account_num;";

                    using (MySqlCommand cmd2 = new MySqlCommand(query2, connection))
                    {
                        cmd2.Parameters.AddWithValue("@account_num", account_num);

                        add_id = Convert.ToInt32(cmd2.ExecuteScalar());
                    }*/
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

        private void insertEmployment()
        {
            MySqlConnection connection = MyConnectionString.mysql_connection();
            try
            {
                connection.Open();

                string query = @"INSERT INTO tbl_employment (cust_id, emp_comp_name, emp_comp_add, emp_comp_num, emp_comp_yrs_in, emp_comp_position, emp_monthly_income)
                                 VALUES (@cust_id, @emp_comp_name, @emp_comp_add, @emp_comp_num, @emp_comp_yrs_in, @emp_comp_position, @emp_monthly_income);";
                using (MySqlCommand cmd = new MySqlCommand(query, connection))
                {
                    cmd.Parameters.AddWithValue("@cust_id", cust_id);
                    cmd.Parameters.AddWithValue("@emp_comp_name", emp_comp_name);
                    cmd.Parameters.AddWithValue("@emp_comp_add", emp_comp_address);
                    cmd.Parameters.AddWithValue("@emp_comp_num", emp_comp_number);
                    cmd.Parameters.AddWithValue("@emp_comp_yrs_in", emp_comp_yrs_in_comp);
                    cmd.Parameters.AddWithValue("@emp_comp_position", emp_comp_position);
                    cmd.Parameters.AddWithValue("@emp_monthly_income", emp_monthly_income);

                    cmd.ExecuteNonQuery();
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

        private void updateEmployment()
        {
            MySqlConnection connection = MyConnectionString.mysql_connection();
            try
            {
                connection.Open();

                string query = @"UPDATE tbl_employment as employment
                                 INNER JOIN tbl_customer as customer ON employment.cust_id = customer.cust_id
                                 INNER JOIN tbl_address as address ON customer.cust_id ON address.cust_id
                                 INNER JOIN tbl_account as account ON address.add_id = account.add_id
                                 SET employment.emp_comp_name = @emp_comp_name,
                                     employment.emp_comp_add = @emp_comp_add,
                                     employment.emp_comp_num = @emp_comp_num,
                                     employment.emp_comp_yrs_in = @emp_comp_yrs_in,
                                     employment.emp_comp_position = @emp_comp_position,
                                     employment.emp_monthly_income = @emp_monthly_income
                                 WHERE account.account_num = @account_num;";
                using (MySqlCommand cmd = new MySqlCommand(query, connection))
                {
                    cmd.Parameters.AddWithValue("@emp_comp_name", emp_comp_name);
                    cmd.Parameters.AddWithValue("@emp_comp_add", emp_comp_address);
                    cmd.Parameters.AddWithValue("@emp_comp_num", emp_comp_number);
                    cmd.Parameters.AddWithValue("@emp_comp_yrs_in", emp_comp_yrs_in_comp);
                    cmd.Parameters.AddWithValue("@emp_comp_position", emp_comp_position);
                    cmd.Parameters.AddWithValue("@emp_monthly_income", emp_monthly_income);
                    cmd.Parameters.AddWithValue("@account_num", account_num);

                    cmd.ExecuteNonQuery();
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

        private void insertSpouse()
        {
            MySqlConnection connection = MyConnectionString.mysql_connection();
            try
            {
                connection.Open();

                string query = @"INSERT INTO tbl_spouse (cust_id, spouse_lastname, spouse_firstname, spouse_middlename, spouse_birthdate, spouse_contact_num, spouse_bus_name, spouse_position, spouse_income)
                                 VALUES (@cust_id, @spouse_lastname, @spouse_firstname, @spouse_middlename, @spouse_birthdate, @spouse_contact_num, @spouse_bus_name, @spouse_position, @spouse_income);";
                using (MySqlCommand cmd = new MySqlCommand(query, connection))
                {
                    cmd.Parameters.AddWithValue("@cust_id", cust_id);
                    cmd.Parameters.AddWithValue("@spouse_lastname", spouse_lastname);
                    cmd.Parameters.AddWithValue("@spouse_firstname", spouse_firstname);
                    cmd.Parameters.AddWithValue("@spouse_middlename", spouse_middlename);
                    cmd.Parameters.AddWithValue("@spouse_birthdate", spouse_birthdate);
                    cmd.Parameters.AddWithValue("@spouse_contact_num", spouse_contact_number);
                    cmd.Parameters.AddWithValue("@spouse_bus_name", spouse_business_name);
                    cmd.Parameters.AddWithValue("@spouse_position", spouse_position);
                    cmd.Parameters.AddWithValue("@spouse_income", spouse_income);

                    cmd.ExecuteNonQuery();
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

        private void updateSpouse()
        {
            MySqlConnection connection = MyConnectionString.mysql_connection();
            try
            {
                connection.Open();

                string query = @"UPDATE tbl_spouse as spouse
                                 INNER JOIN tbl_customer as customer ON spouse.cust_id = customer.cust_id
                                 INNER JOIN tbl_address as address ON customer.cust_id = address.cust_id
                                 INNER JOIN tbl_account as account ON address.add_id = account.add_id
                                 SET spouse.lastname = @spouse_lastname,
                                     spouse.firstname = @spouse_firstname,
                                     spouse.middlename = @spouse_middlename,
                                     spouse.birthdate = @spouse_birthdate,
                                     spouse.contact_num = @spouse_contact_num,
                                     spouse.bus_name = @spouse_bus_name,
                                     spouse.position = @spouse_position,
                                     spouse.income = @spouse_income
                                 WHERE account.account_num = @account_num;";
                using (MySqlCommand cmd = new MySqlCommand(query, connection))
                {
                    cmd.Parameters.AddWithValue("@spouse_lastname", spouse_lastname);
                    cmd.Parameters.AddWithValue("@spouse_firstname", spouse_firstname);
                    cmd.Parameters.AddWithValue("@spouse_middlename", spouse_middlename);
                    cmd.Parameters.AddWithValue("@spouse_birthdate", spouse_birthdate);
                    cmd.Parameters.AddWithValue("@spouse_contact_num", spouse_contact_number);
                    cmd.Parameters.AddWithValue("@spouse_bus_name", spouse_business_name);
                    cmd.Parameters.AddWithValue("@spouse_position", spouse_position);
                    cmd.Parameters.AddWithValue("@spouse_income", spouse_income);
                    cmd.Parameters.AddWithValue("@account_num", account_num);

                    cmd.ExecuteNonQuery();
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

        private void insertBusiness()
        {
            MySqlConnection connection = MyConnectionString.mysql_connection();
            try
            {
                connection.Open();

                string query = @"INSERT INTO tbl_business (add_id, bus_name, bus_position, bus_contact_per)
                                 VALUES (@add_id, @bus_name, @bus_position, @bus_contact_per);";
                using (MySqlCommand cmd = new MySqlCommand(query, connection))
                {
                    cmd.Parameters.AddWithValue("@add_id", add_id);
                    cmd.Parameters.AddWithValue("@bus_name", bus_name);
                    cmd.Parameters.AddWithValue("@bus_position", bus_position);
                    cmd.Parameters.AddWithValue("@bus_contact_per", bus_contact_person);

                    cmd.ExecuteNonQuery();
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

        private void updateBusiness()
        {
            MySqlConnection connection = MyConnectionString.mysql_connection();
            try
            {
                connection.Open();

                string query = @"UPDATE tbl_business as business
                                 INNER JOIN tbl_address as address ON business.add_id = address.add_id
                                 INNER JOIN tbl_account as account ON address.add_id = account.add_id
                                 SET 
                                     business.bus_name = @bus_name,
                                     business.bus_position = @bus_position,
                                     business.bus_contact_per = @bus_contact_per
                                 WHERE account.account_num = @account_num;";
                using (MySqlCommand cmd = new MySqlCommand(query, connection))
                {
                    cmd.Parameters.AddWithValue("@bus_name", bus_name);
                    cmd.Parameters.AddWithValue("@bus_position", bus_position);
                    cmd.Parameters.AddWithValue("@bus_contact_per", bus_contact_person);
                    cmd.Parameters.AddWithValue("@account_num", account_num);

                    cmd.ExecuteNonQuery();
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

        private void insertServicePlan(bool temp, bool plan_fiber, string plan_speed, string plan_static_ip, bool plan_phone_service, bool plan_ndd, bool plan_local_only, bool plan_phone_num_conf, int plan_num_line, int plan_num_extension)
        {
            MySqlConnection connection = MyConnectionString.mysql_connection();
            try
            {
                connection.Open();

                string query = null;
                if (temp == true)
                {
                    query = @"INSERT INTO tbl_service_plan_temp (temp_id, plan_type , plan_fiber, plan_speed, plan_static_ip, plan_phone_service, plan_ndd, plan_local_only, plan_phone_num_conf, plan_num_line, plan_num_extension)
                                 VALUES (@temp_id, @plan_type, @plan_fiber, @plan_speed, @plan_static_ip, @plan_phone_service, @plan_ndd, @plan_local_only, @plan_phone_num_conf, @plan_num_line, @plan_num_extension);";
                }
                else
                {
                    query = @"INSERT INTO tbl_service_plan (plan_type , plan_fiber, plan_speed, plan_static_ip, plan_phone_service, plan_ndd, plan_local_only, plan_phone_num_conf, plan_num_line, plan_num_extension)
                                 VALUES (@plan_type, @plan_fiber, @plan_speed, @plan_static_ip, @plan_phone_service, @plan_ndd, @plan_local_only, @plan_phone_num_conf, @plan_num_line, @plan_num_extension);
                                 SELECT LAST_INSERT_ID();";
                }
                using (MySqlCommand cmd = new MySqlCommand(query, connection))
                {
                    cmd.Parameters.AddWithValue("@plan_type", plan_type);
                    cmd.Parameters.AddWithValue("@plan_fiber", plan_fiber);
                    cmd.Parameters.AddWithValue("@plan_speed", plan_speed);
                    cmd.Parameters.AddWithValue("@plan_static_ip", plan_static_ip);
                    cmd.Parameters.AddWithValue("@plan_phone_service", plan_phone_service);
                    cmd.Parameters.AddWithValue("@plan_ndd", plan_ndd);
                    cmd.Parameters.AddWithValue("@plan_local_only", plan_local_only);
                    cmd.Parameters.AddWithValue("@plan_phone_num_conf", plan_phone_num_conf);
                    cmd.Parameters.AddWithValue("@plan_num_line", plan_num_line);
                    cmd.Parameters.AddWithValue("@plan_num_extension", plan_num_extension);

                    if (temp == true)
                    {
                        cmd.Parameters.AddWithValue("@temp_id", temp_id);
                        cmd.ExecuteScalar();
                    }
                    else
                    {
                        plan_id = Convert.ToInt32(cmd.ExecuteScalar());
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

        private void updateServicePlan()
        {
            MySqlConnection connection = MyConnectionString.mysql_connection();
            try
            {
                connection.Open();

                string query = @"UPDATE tbl_service_plan as plan
                                 INNER JOIN tbl_account as account ON plan.plan_id = account.plan_id
                                 SET plan.plan_type = @plan_type,
                                     plan.plan_fiber = @plan_fiber,
                                     plan.plan_speed = @plan_speed,
                                     plan.plan_static_ip = @plan_static_ip,
                                     plan.plan_phone_service = @plan_phone_service,
                                     plan.plan_ndd = @plan_ndd,
                                     plan.plan_local_only = @plan_local_only,
                                     plan.plan_phone_num_conf = @plan_phone_num_conf,
                                     plan.plan_num_line = @plan_num_line,
                                     plan.plan_num_extension = @plan_num_extension
                                 WHERE account.account_num = @account_num;";
                using (MySqlCommand cmd = new MySqlCommand(query, connection))
                {
                    cmd.Parameters.AddWithValue("@plan_type", plan_type);
                    cmd.Parameters.AddWithValue("@plan_fiber", plan_fiber);
                    cmd.Parameters.AddWithValue("@plan_speed", plan_speed);
                    cmd.Parameters.AddWithValue("@plan_static_ip", plan_static_ip);
                    cmd.Parameters.AddWithValue("@plan_phone_service", plan_phone_service);
                    cmd.Parameters.AddWithValue("@plan_ndd", plan_ndd);
                    cmd.Parameters.AddWithValue("@plan_local_only", plan_local_only);
                    cmd.Parameters.AddWithValue("@plan_phone_num_conf", plan_phone_num_conf);
                    cmd.Parameters.AddWithValue("@plan_num_line", plan_num_line);
                    cmd.Parameters.AddWithValue("@plan_num_extension", plan_num_extension);
                    cmd.Parameters.AddWithValue("@account_num", account_num);

                    cmd.ExecuteNonQuery();
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

        private void insertAccount()
        {
            MySqlConnection connection = MyConnectionString.mysql_connection();
            try
            {
                connection.Open();

                string query1 = @"SELECT
                                    price.price_id
                                  FROM tbl_plan_price as price 
                                  WHERE price.plan_type = @plan_type
                                  AND price.plan_fiber = @plan_fiber
                                  AND price.plan_speed = @plan_speed;";
                MySqlCommand cmd1 = new MySqlCommand(query1, connection);
                cmd1.Parameters.AddWithValue("@plan_type", plan_type);
                cmd1.Parameters.AddWithValue("@plan_fiber", plan_fiber);
                cmd1.Parameters.AddWithValue("@plan_speed", plan_speed);
                using (MySqlDataReader reader = cmd1.ExecuteReader())
                {
                    int price_id = 0;
                    if (reader.Read())
                    {
                        price_id = reader.IsDBNull(reader.GetOrdinal("price_id")) ? 0 : reader.GetInt32("price_id");
                    }
                    reader.Close();

                    string query2 = @"INSERT INTO tbl_account (add_id, plan_id, account_date, account_status, account_type, price_id)
                                      VALUES (@add_id, @plan_id, @account_date, @account_status, @account_type, @price_id);";
                    using (MySqlCommand cmd = new MySqlCommand(query2, connection))
                    {
                        dateTimeConforme.Format = DateTimePickerFormat.Custom;
                        dateTimeConforme.CustomFormat = "MM/dd/yyyy";
                        DateTime dateConforme = dateTimeConforme.Value;

                        cmd.Parameters.AddWithValue("@add_id", add_id);
                        cmd.Parameters.AddWithValue("@plan_id", plan_id);
                        cmd.Parameters.AddWithValue("@account_date", dateConforme);
                        cmd.Parameters.AddWithValue("@account_status", 5);
                        cmd.Parameters.AddWithValue("@account_type", account_type);
                        cmd.Parameters.AddWithValue("@price_id", price_id);

                        cmd.ExecuteNonQuery();
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

        private void btnPrevForm_Click(object sender, EventArgs e)
        {
            insertPlanValue();

            frmAppForm appForm = null;
            if (parentForm is frmAdmin)
            {
                appForm = new frmAppForm((frmAdmin)parentForm);
            }
            if (parentForm is frmCS)
            {
                appForm = new frmAppForm((frmCS)parentForm);
            }

            appForm.account_num = account_num;
            appForm.account_date = account_date;
            appForm.previousClick = true;
            appForm.account_type = account_type;

            // tbl_customer
            appForm.cust_lastname = cust_lastname;
            appForm.cust_firstname = cust_firstname;
            appForm.cust_middlename = cust_middlename;
            appForm.cust_email = cust_email;
            appForm.cust_mobile_num = cust_mobile_num;
            appForm.cust_tel_num = cust_tel_num;
            appForm.cust_birthdate = cust_birthdate;
            appForm.cust_sex = cust_sex;
            appForm.cust_citizenship = cust_citizenship;
            appForm.cust_civil_status = cust_civil_status;

            // tbl_address
            appForm.add_type = add_type;
            appForm.add_line = add_line;
            appForm.add_barangay = add_barangay;
            appForm.add_municipality = add_municipality;
            appForm.add_province = add_province;
            appForm.add_yrs_stay = add_yrs_stay;
            appForm.add_home_ownership = add_home_ownership;

            // tbl_business
            appForm.bus_name = bus_name;
            appForm.bus_position = bus_position;
            appForm.bus_contact_person = bus_contact_person;

            // tbl_spouse
            appForm.spouse_lastname = spouse_lastname;
            appForm.spouse_firstname = spouse_firstname;
            appForm.spouse_middlename = spouse_middlename;
            appForm.spouse_birthdate = spouse_birthdate;
            appForm.spouse_contact_number = spouse_contact_number;
            appForm.spouse_income = spouse_income;
            appForm.spouse_business_name = spouse_business_name;
            appForm.spouse_position = spouse_position;

            // tbl_employment
            appForm.emp_status = emp_status;
            appForm.emp_comp_name = emp_comp_name;
            appForm.emp_comp_address = emp_comp_address;
            appForm.emp_comp_yrs_in_comp = emp_comp_yrs_in_comp;
            appForm.emp_monthly_income = emp_monthly_income;
            appForm.emp_comp_number = emp_comp_number;
            appForm.emp_comp_position = emp_comp_position;

            // tbl_service_plan
            appForm.plan_type = plan_type;
            appForm.plan_fiber = plan_fiber;
            appForm.plan_speed = plan_speed;
            appForm.plan_static_ip = plan_static_ip;
            appForm.plan_phone_service = plan_phone_service;
            appForm.plan_ndd = plan_ndd;
            appForm.plan_local_only = plan_local_only;
            appForm.plan_phone_num_conf = plan_phone_num_conf;
            appForm.plan_num_line = plan_num_line;
            appForm.plan_num_extension = plan_num_extension;

            if (parentForm is frmAdmin)
            {
                ((frmAdmin)parentForm).openFormContent(appForm, null);
            }
            if (parentForm is frmCS)
            {
                ((frmCS)parentForm).openFormContent(appForm, null);
            }
        }

        private void btnSubmit_Click(object sender, EventArgs e)
        {
            // added

            if (checkRESI.Checked == true)
            {
                plan_type = "RESIDENTIAL";
            }
            if (checkCOMM.Checked == true)
            {
                plan_type = "COMMERCIAL";
            }
            if (string.IsNullOrEmpty(plan_type))
            {
                MessageBox.Show("Please select plan type.", "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            // added

            if (btnFiber.Enabled == false)
            {
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
            }
            if (btnDSL.Enabled == false)
            {
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
            }
            if (string.IsNullOrEmpty(plan_speed))
            {
                MessageBox.Show("Please enter BANDWIDTH speed.", "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            else
            {
                if (dateTimeConforme.Checked == false)
                {
                    MessageBox.Show("Please select the date of conforme.", "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return;
                }

                insertPlanValue();

                // insert to database
                if (account_type == "new account")
                {
                    MessageBox.Show("new account");
                    insertCustomer(false);
                    insertAddress(false);
                }
                if (account_type == "reconnection" || account_type == "information update")
                {
                    MessageBox.Show("existing account");
                    updateAccountUpdateStatus(5);
                    MessageBox.Show("1");
                    insertCustomer(true);
                    MessageBox.Show("2");
                    insertAddress(true);
                    MessageBox.Show("3");
                }

                if (emp_status == "EMPLOYED")
                {
                    if (account_type == "new account")
                    {
                        insertEmployment();
                    }
                    if (account_type == "reconnection" || account_type == "information update")
                    {

                    }
                }

                if (add_type == "BUSINESS")
                {
                    if (account_type == "new account")
                    {
                        insertBusiness();
                    }
                    if (account_type == "reconnection" || account_type == "information update")
                    {

                    }
                }

                if (!string.IsNullOrEmpty(spouse_lastname) && !string.IsNullOrEmpty(spouse_firstname))
                {
                    if (account_type == "new account")
                    {
                        insertSpouse();
                    }
                    if (account_type == "reconnection" || account_type == "information update")
                    {

                    }
                }

                if (account_type == "new account")
                {
                    insertServicePlan(false, plan_fiber, plan_speed, plan_static_ip, plan_phone_service, plan_ndd, plan_local_only, plan_phone_num_conf, plan_num_line, plan_num_extension);
                    insertAccount();

                    MessageBox.Show($"Application successfully saved and request created for verification.", "", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    // test only //

                    string username = TCPClient.Instance.username;
                    string log_type = null;
                    if (!string.IsNullOrEmpty(cust_middlename))
                    {
                        log_type = $"submit an application form with the name {capitalizedFirstCharacters(cust_firstname)} {capitalizedFirstCharacters(cust_middlename)} {capitalizedFirstCharacters(cust_lastname)}";
                    }
                    else
                    {
                        log_type = $"submit an application form with the name {capitalizedFirstCharacters(cust_firstname)} {capitalizedFirstCharacters(cust_lastname)}";
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
                if (account_type == "reconnection" || account_type == "change plan")
                {
                    insertServicePlan(true, plan_fiber, plan_speed, plan_static_ip, plan_phone_service, plan_ndd, plan_local_only, plan_phone_num_conf, plan_num_line, plan_num_extension);
                }

                frmAppForm appForm = null;
                if (parentForm is frmAdmin)
                {
                    appForm = new frmAppForm((frmAdmin)parentForm);
                    ((frmAdmin)parentForm).openFormContent(appForm, null);
                }
                if (parentForm is frmCS)
                {
                    appForm = new frmAppForm((frmCS)parentForm);
                    ((frmCS)parentForm).openFormContent(appForm, null);
                }
            }   
        }

        private void insertPlanValue()
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

            // added

            plan_static_ip = null;
            if (btnFiber.Enabled == false)
            {
                plan_static_ip = txtFStaticIP.Text;
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
            }
            if (btnDSL.Enabled == false)
            {
                plan_static_ip = txtDStaticIP.Text;
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
            }

            if (btnFiber.Enabled == false)
            {
                if (checkFPhoneYes.Checked == true)
                {
                    plan_phone_service = true;
                }
                if (checkFPhoneNo.Checked == true)
                {
                    plan_phone_service = false;
                }
            }

            if (btnDSL.Enabled == false)
            {
                if (checkDPhoneYes.Checked == true)
                {
                    plan_phone_service = true;
                }
                if (checkDPhoneNo.Checked == true)
                {
                    plan_phone_service = false;
                }
            }

            if (btnFiber.Enabled == false)
            {
                if (checkFNDD.Checked == true)
                {
                    plan_ndd = true;
                }
                if (checkFNDD.Checked == false)
                {
                    plan_ndd = false;
                }
            }
            if (btnDSL.Enabled == false)
            {
                if (checkDNDD.Checked == true)
                {
                    plan_ndd = true;
                }
                if (checkDNDD.Checked == false)
                {
                    plan_ndd = false;
                }
            }

            if (btnFiber.Enabled == false)
            {
                if (checkFLocal.Checked == true)
                {
                    plan_local_only = true;
                }
                if (checkFLocal.Checked == false)
                {
                    plan_local_only = false;
                }
            }
            if (btnDSL.Enabled == false)
            {
                if (checkDLocal.Checked == true)
                {
                    plan_local_only = true;
                }
                if (checkDLocal.Checked == false)
                {
                    plan_local_only = false;
                }
            }
            if (btnFiber.Enabled == false)
            {
                if (checkFConfidential.Checked == true)
                {
                    plan_phone_num_conf = true;
                }
                if (checkFConfidential.Checked == false)
                {
                    plan_phone_num_conf = false;
                }
            }
            if (btnDSL.Enabled == false)
            {
                if (checkDConfidential.Checked == true)
                {
                    plan_phone_num_conf = true;
                }
                if (checkDConfidential.Checked == false)
                {
                    plan_phone_num_conf = false;
                }
            }
            plan_num_line = 0;
            if (checkLines1.Checked == true)
            {
                plan_num_line = 1;
            }
            if (checkLines2.Checked == true)
            {
                plan_num_line = 2;
            }
            plan_num_extension = 0;
            if (checkExtension1.Checked == true)
            {
                plan_num_extension = 1;
            }
            if (checkExtension2.Checked == true)
            {
                plan_num_extension = 2;
            }
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
    }
}
 
 