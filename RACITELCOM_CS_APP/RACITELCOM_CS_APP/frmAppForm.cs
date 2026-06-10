using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace RACITELCOM_CS_APP
{
    public partial class frmAppForm : Form
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

        private Form parentForm;
        public frmAppForm(Form form)
        {
            InitializeComponent();
            setToolTip();
            this.parentForm = form;
        }

        private void frmAppForm_Load(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(account_type))
            {
                checkNewAccount.Checked = true;
                account_type = "new account";
            }

            if (previousClick ==  true || account_type != "new account")
            {
                // filled up the loaded form
                if (account_type == "change plan" || account_type == "reconnection" || account_type == "information update")
                {
                    checkExistingAccount.Checked = true;
                    checkExistingAccount.Text = $"{checkExistingAccount.Tag} ({account_type}[#{10000 + account_num}])";
                }
                else
                {
                    checkNewAccount.Checked = true;
                }

                txtSurname.Text = cust_lastname;
                txtSurname.ForeColor = Color.FromArgb(17, 7, 100);
                txtGivenName.Text = cust_firstname;
                txtGivenName.ForeColor = Color.FromArgb(17, 7, 100);
                txtBirthDate.Text = cust_birthdate?.ToString("MM/dd/yyyy");
                txtBirthDate.ForeColor = Color.FromArgb(17, 7, 100);
                if (!string.IsNullOrEmpty(cust_middlename))
                {
                    txtMiddleName.Text = cust_middlename;
                    txtMiddleName.ForeColor = Color.FromArgb(17, 7, 100);
                }
                txtHomeBussAdd.Text = add_line;
                txtHomeBussAdd.ForeColor = Color.FromArgb(17, 7, 100);
                txtHomeBussBrgy.Text = add_barangay;
                txtHomeBussBrgy.ForeColor = Color.FromArgb(17, 7, 100);
                txtHomeBussMunicipal.Text = add_municipality;
                txtHomeBussMunicipal.ForeColor = Color.FromArgb(17, 7, 100);
                txtHomeBussProvince.Text = add_province;
                txtHomeBussProvince.ForeColor = Color.FromArgb(17, 7, 100);
                if (!string.IsNullOrEmpty(bus_name))
                {
                    txtBussName.Text = bus_name;
                    txtBussName.ForeColor = Color.FromArgb(17, 7, 100);
                }
                txtYrsStay.Text = add_yrs_stay.ToString();
                txtYrsStay.ForeColor = Color.FromArgb(17, 7, 100);
                if (!string.IsNullOrEmpty(bus_contact_person))
                {
                    txtContactName.Text = bus_contact_person;
                    txtGivenName.ForeColor = Color.FromArgb(17, 7, 100);
                }
                txtSex.Text = cust_sex;
                txtSex.ForeColor = Color.FromArgb(17, 7, 100);
                txtCitizenship.Text = cust_citizenship;
                txtCitizenship.ForeColor = Color.FromArgb(17, 7, 100);
                txtCivilStatus.Text = cust_civil_status;
                txtCivilStatus.ForeColor = Color.FromArgb(17, 7, 100);
                if (!string.IsNullOrEmpty(bus_position))
                {
                    txtPosition.Text = bus_position;
                    txtPosition.ForeColor = Color.FromArgb(17, 7, 100);
                }
                if (!string.IsNullOrEmpty(cust_email))
                {
                    txtEmailAdd.Text = cust_email;
                    txtEmailAdd.ForeColor = Color.FromArgb(17, 7, 100);
                }
                if (!string.IsNullOrEmpty(cust_mobile_num))
                {
                    txtMobileNum.Text = cust_mobile_num;
                    txtMobileNum.ForeColor = Color.FromArgb(17, 7, 100);
                }
                if (!string.IsNullOrEmpty(cust_tel_num))
                {
                    txtTelNum.Text = cust_tel_num;
                    txtTelNum.ForeColor = Color.FromArgb(17, 7, 100);
                }
                if (!string.IsNullOrEmpty(spouse_lastname))
                {
                    txtSpouseSurname.Text = spouse_lastname;
                    txtSpouseSurname.ForeColor = Color.FromArgb(17, 7, 100);
                    txtSpouseGivenName.Text = spouse_firstname;
                    txtSpouseGivenName.ForeColor = Color.FromArgb(17, 7, 100);
                    txtSpouseBirthDate.Text = cust_birthdate?.ToString("MM/dd/yyyy");
                    txtSpouseBirthDate.ForeColor = Color.FromArgb(17, 7, 100);
                }
                if (!string.IsNullOrEmpty(spouse_middlename))
                {
                    txtSpouseMiddleName.Text = spouse_middlename;
                    txtSpouseMiddleName.ForeColor = Color.FromArgb(17, 7, 100);
                }
                if (!string.IsNullOrEmpty(spouse_contact_number))
                {
                    txtSpouseContactNum.Text = spouse_contact_number;
                    txtSpouseContactNum.ForeColor = Color.FromArgb(17, 7, 100);
                }
                if (!string.IsNullOrEmpty(spouse_business_name))
                {
                    txtSpouseIncome.Text = spouse_income;
                    txtSpouseIncome.ForeColor = Color.FromArgb(17, 7, 100);
                    txtSpouseBussName.Text = spouse_business_name;
                    txtSpouseBussName.ForeColor = Color.FromArgb(17, 7, 100);
                    txtSpousePosition.Text = spouse_position;
                    txtSpousePosition.ForeColor = Color.FromArgb(17, 7, 100);
                }
                if (emp_status == "EMPLOYED")
                {
                    txtSourceCompName.Text = emp_comp_name;
                    txtSourceCompName.ForeColor = Color.FromArgb(17, 7, 100);
                    txtSourceMonthIncome.Text = emp_monthly_income;
                    txtSourceMonthIncome.ForeColor = Color.FromArgb(17, 7, 100);
                    txtSourceCompAdd.Text = emp_comp_address;
                    txtSourceCompAdd.ForeColor = Color.FromArgb(17, 7, 100);
                    txtSourceYrsInComp.Text = emp_comp_yrs_in_comp.ToString();
                    txtSourceYrsInComp.ForeColor = Color.FromArgb(17, 7, 100);
                    txtSourceCompNum.Text = emp_comp_number;
                    txtSourceCompNum.ForeColor = Color.FromArgb(17, 7, 100);
                    txtSourcePosition.Text = emp_comp_position;
                    txtSourcePosition.ForeColor = Color.FromArgb(17, 7, 100);
                    checkEmployed.Checked = true;
                }
                else if (emp_status == "UNEMPLOYEDw/INCOME")
                {
                    checkUnemployedWiI.Checked = true;
                }
                else if (emp_status == "UNEMPLOYEDw/oINCOME")
                {
                    checkUnemployedWoI.Checked = true;
                }
                else
                {
                    checkPensioner.Checked = true;
                }

                if (add_type == "RESIDENTIAL")
                {
                    checkResi.Checked = true;
                }
                else
                {
                    checkBuss.Checked = true;
                }

                if (add_home_ownership == "OWNED")
                {
                    checkOwned.Checked = true;
                }
                else if (add_home_ownership == "RENTED")
                {
                    checkRented.Checked = true;
                }
                else
                {
                    checkLiveWP.Checked = true;
                }
            }

            // test

            if(account_type == "information update" || account_type == "reconnection")
            {
                setControls(this, false, true);
            }
            if(account_type == "change plan")
            {
                setControls(this, true, false);
            }

            // test
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
                    if (checkbox != checkNewAccount && checkbox != checkExistingAccount)
                    {
                        checkbox.Enabled = bool2;
                    }
                }
                else if (ctrl.HasChildren)
                {
                    setControls(ctrl, bool1, bool2);
                }
            }
        }

        private void setToolTip()
        {
            ToolTip toolTip = new ToolTip();

            toolTip.SetToolTip(txtSurname, txtSurname.Tag.ToString());
            toolTip.SetToolTip(txtGivenName, txtGivenName.Tag.ToString());
            toolTip.SetToolTip(txtMiddleName, txtMiddleName.Tag.ToString());
            toolTip.SetToolTip(txtBussName, txtBussName.Tag.ToString());
            toolTip.SetToolTip(txtHomeBussAdd, txtHomeBussAdd.Tag.ToString());
            toolTip.SetToolTip(txtHomeBussBrgy, txtHomeBussBrgy.Tag.ToString());
            toolTip.SetToolTip(txtHomeBussMunicipal, txtHomeBussMunicipal.Tag.ToString());
            toolTip.SetToolTip(txtHomeBussProvince, txtHomeBussProvince.Tag.ToString());
            toolTip.SetToolTip(txtBillAdd, txtBillAdd.Tag.ToString());
            toolTip.SetToolTip(txtBillBrgy, txtBillBrgy.Tag.ToString());
            toolTip.SetToolTip(txtBillMunicipal, txtBillMunicipal.Tag.ToString());
            toolTip.SetToolTip(txtBillProvince, txtBillProvince.Tag.ToString());
            toolTip.SetToolTip(txtBirthDate, txtBirthDate.Tag.ToString());
            toolTip.SetToolTip(txtSex, txtSex.Tag.ToString());
            toolTip.SetToolTip(txtCitizenship, txtCitizenship.Tag.ToString());
            toolTip.SetToolTip(txtCivilStatus, txtCivilStatus.Tag.ToString());
            toolTip.SetToolTip(txtPosition, txtPosition.Tag.ToString());
            toolTip.SetToolTip(txtEmailAdd, txtEmailAdd.Tag.ToString());
            toolTip.SetToolTip(txtMobileNum, txtMobileNum.Tag.ToString());
            toolTip.SetToolTip(txtTelNum, txtTelNum.Tag.ToString());
            toolTip.SetToolTip(txtSpouseSurname, txtSpouseSurname.Tag.ToString());
            toolTip.SetToolTip(txtSpouseGivenName, txtSpouseGivenName.Tag.ToString());
            toolTip.SetToolTip(txtSpouseMiddleName, txtSpouseMiddleName.Tag.ToString());
            toolTip.SetToolTip(txtSpouseBirthDate, txtSpouseBirthDate.Tag.ToString());
            toolTip.SetToolTip(txtSpouseContactNum, txtSpouseContactNum.Tag.ToString());
            toolTip.SetToolTip(txtSpouseIncome, txtSpouseIncome.Tag.ToString());
            toolTip.SetToolTip(txtSpouseBussName, txtSpouseBussName.Tag.ToString());
            toolTip.SetToolTip(txtSpousePosition, txtSpousePosition.Tag.ToString());
            toolTip.SetToolTip(txtSourceCompName, txtSourceCompName.Tag.ToString());
            toolTip.SetToolTip(txtSourceYrsInComp, txtSourceYrsInComp.Tag.ToString());
            toolTip.SetToolTip(txtSourceCompAdd, txtSourceCompAdd.Tag.ToString());
            toolTip.SetToolTip(txtSourceMonthIncome, txtSourceMonthIncome.Tag.ToString());
            toolTip.SetToolTip(txtSourceCompNum, txtSourceCompNum.Tag.ToString());
            toolTip.SetToolTip(txtSourcePosition, txtSourcePosition.Tag.ToString());
        }

        private void loadForm(int? account_num)
        {
            if (account_num == null)
            {
                MessageBox.Show("null");
                return;
            }

            MySqlConnection connection = MyConnectionString.mysql_connection();
            try
            {
                connection.Open();

                string query = $@"SELECT
                                    account.*,
                                    address.*,
                                    customer.*,
                                    plan.*,
                                    employment.*,
                                    business.*,
                                    spouse.*
                                  FROM
                                    tbl_account as account
                                    INNER JOIN tbl_address as address ON account.add_id = address.add_id
                                    INNER JOIN tbl_customer as customer ON address.cust_id = customer.cust_id
                                    INNER JOIN tbl_service_plan as plan ON account.plan_id = plan.plan_id
                                    LEFT JOIN tbl_employment as employment ON customer.cust_id = employment.cust_id
                                    LEFT JOIN tbl_business as business ON address.add_id = business.add_id
                                    LEFT JOIN tbl_spouse as spouse ON customer.cust_id = spouse.cust_id
                                  WHERE
                                    account.account_num = @account_num;";
                MySqlCommand cmd = new MySqlCommand(query, connection);
                cmd.Parameters.AddWithValue("@account_num", account_num);

                using (MySqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        // account_num = Convert.ToInt32(reader["account_num"]);
                        // add_id = Convert.ToInt32(reader["add_id"]);
                        // plan_id = Convert.ToInt32(reader["plan_id"]);
                        account_date = Convert.ToDateTime(reader["account_date"]);
                        // account_type = reader["account_type"].ToString();

                        // cust_id = Convert.ToInt32(reader["cust_id"]);
                        add_type = reader["add_type"].ToString();
                        add_line = reader["add_line"].ToString();
                        add_barangay = reader["add_barangay"].ToString();
                        add_municipality = reader["add_municipality"].ToString();
                        add_province = reader["add_province"].ToString();
                        add_yrs_stay = reader["add_yrs_stay"] == DBNull.Value ? 0 : Convert.ToInt32(reader["add_yrs_stay"]);
                        add_home_ownership = reader["add_home_ownership"].ToString();

                        cust_lastname = reader["cust_lastname"].ToString();
                        cust_firstname = reader["cust_firstname"].ToString();
                        cust_middlename = reader["cust_middlename"].ToString();
                        cust_email = reader["cust_email"].ToString();
                        cust_mobile_num = reader["cust_mobile_num"].ToString();
                        cust_tel_num = reader["cust_tel_num"].ToString();
                        cust_birthdate = reader["cust_birthdate"] != DBNull.Value ? (DateTime?)Convert.ToDateTime(reader["cust_birthdate"]) : null;
                        cust_sex = reader["cust_sex"].ToString();
                        cust_citizenship = reader["cust_citizenship"].ToString();
                        cust_civil_status = reader["cust_civil_status"].ToString();
                        emp_status = reader["emp_status"].ToString();

                        plan_type = reader["plan_type"].ToString();
                        plan_fiber = Convert.ToBoolean(reader["plan_fiber"]);
                        plan_speed = reader["plan_speed"].ToString();
                        plan_static_ip = reader["plan_static_ip"].ToString();
                        plan_phone_service = Convert.ToBoolean(reader["plan_phone_service"]);
                        plan_ndd = Convert.ToBoolean(reader["plan_ndd"]);
                        plan_local_only = Convert.ToBoolean(reader["plan_local_only"]);
                        plan_phone_num_conf = Convert.ToBoolean(reader["plan_phone_num_conf"]);
                        plan_num_line = reader["plan_num_line"] == DBNull.Value ? 0 : Convert.ToInt32(reader["plan_num_line"]);
                        plan_num_extension = reader["plan_num_extension"] == DBNull.Value ? 0 : Convert.ToInt32(reader["plan_num_extension"]);

                        emp_comp_name = reader["emp_comp_name"].ToString();
                        emp_comp_address = reader["emp_comp_add"].ToString();
                        emp_comp_number = reader["emp_comp_num"].ToString();
                        emp_comp_yrs_in_comp = reader["emp_comp_yrs_in"] == DBNull.Value ? 0 : Convert.ToInt32(reader["emp_comp_yrs_in"]);
                        emp_comp_position = reader["emp_comp_position"].ToString();
                        emp_monthly_income = reader["emp_monthly_income"].ToString();

                        bus_name = reader["bus_name"].ToString();
                        bus_position = reader["bus_position"].ToString();
                        bus_contact_person = reader["bus_contact_per"].ToString();

                        spouse_lastname = reader["spouse_lastname"].ToString();
                        spouse_firstname = reader["spouse_firstname"].ToString();
                        spouse_middlename = reader["spouse_middlename"].ToString();
                        spouse_birthdate = reader["spouse_birthdate"] != DBNull.Value ? (DateTime?)Convert.ToDateTime(reader["spouse_birthdate"]) : null;
                        spouse_contact_number = reader["spouse_contact_num"].ToString();
                        spouse_business_name = reader["spouse_bus_name"].ToString();
                        spouse_position = reader["spouse_position"].ToString();
                        spouse_income = reader["spouse_income"].ToString();
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

        private void btnNextForm_Click(object sender, EventArgs e)
        {
            string add_home_ownership = null;
            if (checkRented.Checked == true)
            {
                add_home_ownership = "RENTED";
            }
            if (checkOwned.Checked == true)
            {
                add_home_ownership = "OWNED";
            }
            if (checkLiveWP.Checked == true)
            {
                add_home_ownership = "LIVEWP";
            }
            string add_type = null;
            if (checkResi.Checked == true)
            {
                add_type = "RESIDENTIAL";
            }
            if (checkBuss.Checked == true)
            {
                add_type = "BUSINESS";
            }
            string emp_status = null;
            if (checkEmployed.Checked == true)
            {
                emp_status = "EMPLOYED";
            }
            if (checkUnemployedWiI.Checked == true)
            {
                emp_status = "UNEMPLOYEDw/INCOME";
            }
            if (checkUnemployedWoI.Checked == true)
            {
                emp_status = "UNEMPLOYEDw/oINCOME";
            }
            if (checkPensioner.Checked == true)
            {
                emp_status = "PENSIONER";
            }

            if (txtSurname.ForeColor == Color.Gray || txtGivenName.ForeColor == Color.Gray || txtHomeBussAdd.ForeColor == Color.Gray || txtHomeBussBrgy.ForeColor == Color.Gray || txtHomeBussMunicipal.ForeColor == Color.Gray || txtHomeBussProvince.ForeColor == Color.Gray ||
                txtBirthDate.ForeColor == Color.Gray || add_home_ownership == null || add_type == null || emp_status == null || 
               (txtMobileNum.ForeColor == Color.Gray && txtTelNum.ForeColor == Color.Gray) || (checkNewAccount.Checked == false && checkExistingAccount.Checked == false))
            {
                MessageBox.Show("Please ensure all required fields are filled out, and that either the mobile or telephone field is completed.", "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            else
            {
                DateTime? cust_birthdate = parseDateString(txtBirthDate.Text);
                DateTime? spouse_birthdate = parseDateString(txtSpouseBirthDate.Text);
                if (cust_birthdate == null)
                {
                    MessageBox.Show("Please ensure the BirthDate format is correct. (MM/DD/YYYY)", "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
                else
                {
                    if (txtSpouseBirthDate.ForeColor != Color.Gray && spouse_birthdate == null)
                    {
                        MessageBox.Show("Please ensure the BirthDate format is correct. (MM/DD/YYYY)", "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        return;
                    }
                    if ((!string.IsNullOrEmpty(txtYrsStay.Text) && !txtYrsStay.Text.All(char.IsDigit)) || (!string.IsNullOrEmpty(txtSourceYrsInComp.Text) && !txtSourceYrsInComp.Text.All(char.IsDigit) && txtSourceYrsInComp.ForeColor != Color.Gray))
                    {
                        MessageBox.Show("Please enter a valid number for the Year fields.", "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        return;
                    }
                    // tbl_customer
                    string cust_lastname = txtSurname.Text;
                    string cust_firstname = txtGivenName.Text;
                    string cust_middlename = txtMiddleName.ForeColor != Color.Gray ? txtMiddleName.Text : null;
                    string cust_email = txtEmailAdd.ForeColor != Color.Gray ? txtEmailAdd.Text : null;
                    string cust_mobile_num = txtMobileNum.ForeColor != Color.Gray ? txtMobileNum.Text : null;
                    string cust_tel_num = txtTelNum.ForeColor != Color.Gray ? txtTelNum.Text : null;
                    string cust_sex = txtSex.ForeColor != Color.Gray ? txtSex.Text : null;
                    string cust_citizenship = txtCitizenship.ForeColor != Color.Gray ? txtCitizenship.Text : null;
                    string cust_civil_status = txtCivilStatus.ForeColor != Color.Gray ? txtCivilStatus.Text : null;

                    // tbl_address
                    string add_line = txtHomeBussAdd.Text;
                    string add_barangay = txtHomeBussBrgy.Text;
                    string add_municipality = txtHomeBussMunicipal.Text;
                    string add_province = txtHomeBussProvince.Text;
                    string add_yrs_stay = txtYrsStay.Text;

                    // tbl_business
                    string bus_name = txtBussName.ForeColor != Color.Gray ? txtBussName.Text : null;
                    string bus_position = txtPosition.ForeColor != Color.Gray ? txtPosition.Text : null;
                    string bus_contact_person = txtContactName.ForeColor != Color.Gray ? txtContactName.Text : null;

                    // tbl_spouse
                    string spouse_lastname = txtSpouseSurname.ForeColor != Color.Gray ? txtSpouseSurname.Text : null;
                    string spouse_firstname = txtSpouseGivenName.ForeColor != Color.Gray ? txtSpouseGivenName.Text : null;
                    string spouse_middlename = txtSpouseMiddleName.ForeColor != Color.Gray ? txtSpouseMiddleName.Text : null;
                    string spouse_contact_number = txtSpouseContactNum.ForeColor != Color.Gray ? txtSpouseContactNum.Text : null;
                    string spouse_income = txtSpouseIncome.ForeColor != Color.Gray ? txtSpouseIncome.Text : null;
                    string spouse_business_name = txtSpouseBussName.ForeColor != Color.Gray ? txtSpouseBussName.Text : null;
                    string spouse_position = txtSpousePosition.ForeColor != Color.Gray ? txtSpousePosition.Text : null;

                    // tbl_employment
                    string emp_comp_name = txtSourceCompName.ForeColor != Color.Gray ? txtSourceCompName.Text : null;
                    string emp_comp_address = txtSourceCompAdd.ForeColor != Color.Gray ? txtSourceCompAdd.Text : null;
                    string emp_comp_yrs_in_comp = txtSourceYrsInComp.ForeColor != Color.Gray ? txtSourceYrsInComp.Text : null;
                    string emp_monthly_income = txtSourceMonthIncome.ForeColor != Color.Gray ? txtSourceMonthIncome.Text : null;
                    string emp_comp_number = txtSourceCompNum.ForeColor != Color.Gray ? txtSourceCompNum.Text : null;
                    string emp_comp_position = txtSourcePosition.ForeColor != Color.Gray ? txtSourcePosition.Text : null;

                    frmAppForm1 appForm1 = null;
                    if (parentForm is frmAdmin)
                    {
                        appForm1 = new frmAppForm1((frmAdmin)parentForm);
                    }
                    if (parentForm is frmCS)
                    {
                        appForm1 = new frmAppForm1((frmCS)parentForm);
                    }

                    appForm1.account_num = account_num;
                    appForm1.account_date = account_date;
                    appForm1.previousClick = previousClick;
                    appForm1.account_type = account_type;

                    // tbl_customer
                    appForm1.cust_lastname = cust_lastname;
                    appForm1.cust_firstname = cust_firstname;
                    appForm1.cust_middlename = cust_middlename;
                    appForm1.cust_email = cust_email;
                    appForm1.cust_mobile_num = cust_mobile_num;
                    appForm1.cust_tel_num = cust_tel_num;
                    appForm1.cust_birthdate = cust_birthdate;
                    appForm1.cust_sex = cust_sex;
                    appForm1.cust_citizenship = cust_citizenship;
                    appForm1.cust_civil_status = cust_civil_status;

                    // tbl_address
                    appForm1.add_type = add_type;
                    appForm1.add_line = add_line;
                    appForm1.add_barangay = add_barangay;
                    appForm1.add_municipality = add_municipality;
                    appForm1.add_province = add_province;
                    if (!string.IsNullOrEmpty(add_yrs_stay))
                    {
                        appForm1.add_yrs_stay = int.Parse(add_yrs_stay);
                    }
                    else
                    {
                        appForm1.add_yrs_stay = 0;
                    }
                    appForm1.add_home_ownership = add_home_ownership;

                    // tbl_bussiness
                    appForm1.bus_name = bus_name;
                    appForm1.bus_position = bus_position;
                    appForm1.bus_contact_person = bus_contact_person;

                    // tbl_spouse
                    appForm1.spouse_lastname = spouse_lastname;
                    appForm1.spouse_firstname = spouse_firstname;
                    appForm1.spouse_middlename = spouse_middlename;
                    appForm1.spouse_birthdate = spouse_birthdate;
                    appForm1.spouse_contact_number = spouse_contact_number;
                    appForm1.spouse_income = spouse_income;
                    appForm1.spouse_business_name = spouse_business_name;
                    appForm1.spouse_position = spouse_position;

                    // tbl_employment
                    appForm1.emp_status = emp_status;
                    appForm1.emp_comp_name = emp_comp_name;
                    appForm1.emp_comp_address = emp_comp_address;
                    if (!string.IsNullOrEmpty(emp_comp_yrs_in_comp))
                    {
                        appForm1.emp_comp_yrs_in_comp = int.Parse(emp_comp_yrs_in_comp);
                    }
                    else
                    {
                        appForm1.emp_comp_yrs_in_comp = 0;
                    }
                    appForm1.emp_monthly_income = emp_monthly_income;
                    appForm1.emp_comp_number = emp_comp_number;
                    appForm1.emp_comp_position = emp_comp_position;

                    // tbl_service_plan
                    appForm1.plan_type = plan_type;
                    appForm1.plan_fiber = plan_fiber;
                    appForm1.plan_speed = plan_speed;
                    appForm1.plan_static_ip = plan_static_ip;
                    appForm1.plan_phone_service = plan_phone_service;
                    appForm1.plan_ndd = plan_ndd;
                    appForm1.plan_local_only = plan_local_only;
                    appForm1.plan_phone_num_conf = plan_phone_num_conf;
                    appForm1.plan_num_line = plan_num_line;
                    appForm1.plan_num_extension = plan_num_extension;

                    if (parentForm is frmAdmin)
                    {
                        ((frmAdmin)parentForm).openFormContent(appForm1, null);
                    }
                    if (parentForm is frmCS)
                    {
                        ((frmCS)parentForm).openFormContent(appForm1, null);
                    }
                }
            }
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

        private void checkResi_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBuss.Checked == true)
            {
                checkBuss.Checked = false;
            }
        }

        private void checkBuss_CheckedChanged(object sender, EventArgs e)
        {
            if (checkResi.Checked == true)
            {
                checkResi.Checked = false;
            }
        }

        private void checkRented_CheckedChanged(object sender, EventArgs e)
        {
            if (checkOwned.Checked == true)
            {
                checkOwned.Checked = false;
            }
            else if (checkLiveWP.Checked == true)
            {
                checkLiveWP.Checked = false;
            }
        }

        private void checkOwned_CheckedChanged(object sender, EventArgs e)
        {
            if (checkRented.Checked == true)
            {
                checkRented.Checked = false;
            }
            else if (checkLiveWP.Checked == true)
            {
                checkLiveWP.Checked = false;
            }
        }

        private void checkLiveWP_CheckedChanged(object sender, EventArgs e)
        {
            if (checkRented.Checked == true)
            {
                checkRented.Checked = false;
            }
            else if (checkOwned.Checked == true)
            {
                checkOwned.Checked = false;
            }
        }

        private void checkEmployed_CheckedChanged(object sender, EventArgs e)
        {
            if (checkUnemployedWiI.Checked == true)
            {
                checkUnemployedWiI.Checked = false;
            }
            else if (checkUnemployedWoI.Checked == true)
            {
                checkUnemployedWoI.Checked = false;
            }
            else if (checkPensioner.Checked == true)
            {
                checkPensioner.Checked = false;
            }
        }

        private void checkUnemployedWiI_CheckedChanged(object sender, EventArgs e)
        {
            if (checkEmployed.Checked == true)
            {
                checkEmployed.Checked = false;
            }
            else if (checkUnemployedWoI.Checked == true)
            {
                checkUnemployedWoI.Checked = false;
            }
            else if (checkPensioner.Checked == true)
            {
                checkPensioner.Checked = false;
            }
        }

        private void checkUnemployedWoI_CheckedChanged(object sender, EventArgs e)
        {
            if (checkEmployed.Checked == true)
            {
                checkEmployed.Checked = false;
            }
            else if (checkUnemployedWiI.Checked == true)
            {
                checkUnemployedWiI.Checked = false;
            }
            else if (checkPensioner.Checked == true)
            {
                checkPensioner.Checked = false;
            }
        }

        private void checkPensioner_CheckedChanged(object sender, EventArgs e)
        {
            if (checkEmployed.Checked == true)
            {
                checkEmployed.Checked = false;
            }
            else if (checkUnemployedWiI.Checked == true)
            {
                checkUnemployedWiI.Checked = false;
            }
            else if (checkUnemployedWoI.Checked == true)
            {
                checkUnemployedWoI.Checked = false;
            }
        }

        private void checkNewAccount_Click(object sender, EventArgs e)
        {
            if (checkExistingAccount.Checked == true)
            {
                checkExistingAccount.Checked = false;
                checkExistingAccount.Text = checkExistingAccount.Tag.ToString();
                unCheckExistingAccount();
                checkNewAccount.Checked = true;
            }
            account_type = "new account";
        }

        private void checkExistingAccount_Click(object sender, EventArgs e)
        {
            if (checkNewAccount.Checked == true)
            {
                checkNewAccount.Checked = false;
            }
            if (checkExistingAccount.Checked == true)
            {
                account_type = null;
                frmExistingAccount existingAccount = new frmExistingAccount(this);
                existingAccount.ShowDialog();
                if (!string.IsNullOrEmpty(account_type))
                {
                    checkExistingAccount.Text = $"{checkExistingAccount.Tag} ({account_type})";
                    MessageBox.Show(account_num.ToString());
                    loadForm(account_num);
                    frmAppForm_Load(null, null);
                }
            }
            else
            {
                checkExistingAccount.Text = checkExistingAccount.Tag.ToString();
                unCheckExistingAccount();
            }
        }

        private void resetAllVariables()
        {
            account_num = null;
            account_date = null;
            previousClick = false;
            account_type = "new account";

            // tbl_customer
            cust_lastname = null;
            cust_firstname = null;
            cust_middlename = null;
            cust_email = null;
            cust_mobile_num = null;
            cust_tel_num = null;
            cust_birthdate = null;
            cust_sex = null;
            cust_citizenship = null;
            cust_civil_status = null;

            // tbl_address
            add_type = null;
            add_line = null;
            add_barangay = null;
            add_municipality = null;
            add_province = null;
            add_yrs_stay = 0;
            add_home_ownership = null;

            // tbl_business
            bus_name = null;
            bus_position = null;
            bus_contact_person = null;

            // tbl_spouse
            spouse_lastname = null;
            spouse_firstname = null;
            spouse_middlename = null;
            spouse_birthdate = null;
            spouse_contact_number = null;
            spouse_income = null;
            spouse_business_name = null;
            spouse_position = null;

            // tbl_employment
            emp_status = null;
            emp_comp_name = null;
            emp_comp_address = null;
            emp_comp_yrs_in_comp = 0;
            emp_monthly_income = null;
            emp_comp_number = null;
            emp_comp_position = null;

            // tbl_service_plan
            plan_type = null;
            plan_fiber = false;
            plan_speed = null;
            plan_static_ip = null;
            plan_phone_service = false;
            plan_ndd = false;
            plan_local_only = false;
            plan_phone_num_conf = false;
            plan_num_line = 0;
            plan_num_extension = 0;
        }

        private void resetAllControls(Control control)
        {
            foreach (Control ctrl in control.Controls)
            {
                if (ctrl is TextBox)
                {
                    TextBox textBox = (TextBox)ctrl;
                    textBox.ForeColor = Color.Gray;
                    textBox.ReadOnly = false;  // add
                    if (textBox.Tag != null)
                    {
                        textBox.Text = textBox.Tag.ToString();
                    }
                }
                else if (ctrl is CheckBox)
                {
                    CheckBox checkbox = (CheckBox)ctrl;
                    checkbox.Checked = false;
                    checkbox.Enabled = true;  // add
                }
                else if (ctrl.HasChildren)
                {
                    resetAllControls(ctrl);
                }
            }
        }

        public void unCheckExistingAccount()
        {
            checkExistingAccount.Checked = false;
            resetAllVariables();
            resetAllControls(this);
        }
    }
}
