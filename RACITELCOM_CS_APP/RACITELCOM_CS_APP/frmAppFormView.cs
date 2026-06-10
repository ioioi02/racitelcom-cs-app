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
    public partial class frmAppFormView : Form
    {
        public bool previousClick { get; set; }

        // Account
        public int? account_num { get; set; }
        public int add_id { get; set; }
        public int plan_id { get; set; }
        public DateTime? account_date { get; set; }
        public string account_type { get; set; }

        // Address
        public int cust_id { get; set; }
        public string add_type { get; set; }
        public string add_line { get; set; }
        public string add_barangay { get; set; }
        public string add_municipality { get; set; }
        public string add_province { get; set; }
        public int add_yrs_stay { get; set; }
        public string add_home_ownership { get; set; }
        // old
        public string add_type_old { get; set; }
        public string add_line_old { get; set; }
        public string add_barangay_old { get; set; }
        public string add_municipality_old { get; set; }
        public string add_province_old { get; set; }
        public int add_yrs_stay_old { get; set; }
        public string add_home_ownership_old { get; set; }

        // Customer
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
        public string emp_status { get; set; }
        // old
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

        // Plan
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
        // old
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

        // Employment
        public string emp_comp_name { get; set; }
        public string emp_comp_add { get; set; }
        public string emp_comp_num { get; set; }
        public int emp_comp_yrs_in { get; set; }
        public string emp_comp_position { get; set; }
        public string emp_monthly_income { get; set; }
        // old
        public string emp_comp_name_old { get; set; }
        public string emp_comp_add_old { get; set; }
        public string emp_comp_num_old { get; set; }
        public int emp_comp_yrs_in_old { get; set; }
        public string emp_comp_position_old { get; set; }
        public string emp_monthly_income_old { get; set; }

        // Business
        public string bus_name { get; set; }
        public string bus_position { get; set; }
        public string bus_contact_per { get; set; }
        // old
        public string bus_name_old { get; set; }
        public string bus_position_old { get; set; }
        public string bus_contact_per_old { get; set; }

        // Spouse
        public string spouse_lastname { get; set; }
        public string spouse_firstname { get; set; }
        public string spouse_middlename { get; set; }
        public DateTime? spouse_birthdate { get; set; }
        public string spouse_contact_num { get; set; }
        public string spouse_bus_name { get; set; }
        public string spouse_position { get; set; }
        public string spouse_income { get; set; }
        // old
        public string spouse_lastname_old { get; set; }
        public string spouse_firstname_old { get; set; }
        public string spouse_middlename_old { get; set; }
        public DateTime? spouse_birthdate_old { get; set; }
        public string spouse_contact_num_old { get; set; }
        public string spouse_bus_name_old { get; set; }
        public string spouse_position_old { get; set; }
        public string spouse_income_old { get; set; }

        public string account_num_string { get; set; }

        public Form parent_form { get; set; }

        public frmAppFormView()
        {
            InitializeComponent();
            setToolTip();
        }

        private void frmAppFormView_Load(object sender, EventArgs e)
        {
            if (previousClick == true)
            {
                prevFillUp();
            }
            else
            {
                if (!string.IsNullOrEmpty(account_num_string))
                {
                    string account_num_trim = account_num_string.TrimStart('#');
                    int account_num_ = int.Parse(account_num_trim);
                    account_num = account_num_ - 10000;
                    loadForm(account_num);
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

        private void loadForm(int? account_num)
        {
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
                                   (account.account_status = 5 OR account.account_update_status = 5 ) AND
                                    account.account_num = @account_num;";
                MySqlCommand cmd = new MySqlCommand(query, connection);
                cmd.Parameters.AddWithValue("@account_num", account_num);

                using (MySqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        // account_num = Convert.ToInt32(reader["account_num"]);
                        add_id = Convert.ToInt32(reader["add_id"]);
                        plan_id = Convert.ToInt32(reader["plan_id"]);
                        account_date = Convert.ToDateTime(reader["account_date"]);
                        account_type = reader["account_type"].ToString();

                        cust_id = Convert.ToInt32(reader["cust_id"]);
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
                        emp_comp_add = reader["emp_comp_add"].ToString();
                        emp_comp_num = reader["emp_comp_num"].ToString();
                        emp_comp_yrs_in = reader["emp_comp_yrs_in"] == DBNull.Value ? 0 : Convert.ToInt32(reader["emp_comp_yrs_in"]);
                        emp_comp_position = reader["emp_comp_position"].ToString();
                        emp_monthly_income = reader["emp_monthly_income"].ToString();

                        bus_name = reader["bus_name"].ToString();
                        bus_position = reader["bus_position"].ToString();
                        bus_contact_per = reader["bus_contact_per"].ToString();

                        spouse_lastname = reader["spouse_lastname"].ToString();
                        spouse_firstname = reader["spouse_firstname"].ToString();
                        spouse_middlename = reader["spouse_middlename"].ToString();
                        spouse_birthdate = reader["spouse_birthdate"] != DBNull.Value ? (DateTime?)Convert.ToDateTime(reader["spouse_birthdate"]) : null;
                        spouse_contact_num = reader["spouse_contact_num"].ToString();
                        spouse_bus_name = reader["spouse_bus_name"].ToString();
                        spouse_position = reader["spouse_position"].ToString();
                        spouse_income = reader["spouse_income"].ToString();
                    }
                    reader.Close();

                    if (account_type == "reconnection" || account_type == "information update" || account_type == "change plan")
                    {
                        string query0 = $@"SELECT
                                    address.*,
                                    customer.*,
                                    plan.*,
                                    employment.*,
                                    business.*,
                                    spouse.*
                                  FROM
                                    tbl_account as account
                                    INNER JOIN tbl_temp as temporary ON account.account_num = temporary.account_num
                                    LEFT JOIN tbl_address_temp as address ON temporary.temp_id = address.temp_id
                                    LEFT JOIN tbl_customer_temp as customer ON temporary.temp_id = customer.temp_id
                                    LEFT JOIN tbl_service_plan_temp as plan ON temporary.temp_id = plan.temp_id
                                    LEFT JOIN tbl_employment_temp as employment ON temporary.temp_id = employment.temp_id
                                    LEFT JOIN tbl_business_temp as business ON temporary.temp_id = business.temp_id
                                    LEFT JOIN tbl_spouse_temp as spouse ON temporary.temp_id = spouse.temp_id
                                  WHERE
                                    account.account_num = @account_num AND
                                    account.account_update_status = 5;";
                        MySqlCommand cmd0 = new MySqlCommand(query0, connection);
                        cmd0.Parameters.AddWithValue("@account_num", account_num);

                        using (MySqlDataReader reader0 = cmd0.ExecuteReader())
                        {
                            while (reader0.Read())
                            {
                                if (account_type == "reconnection" || account_type == "information update")
                                {
                                    // account_num = Convert.ToInt32(reader["account_num"]);
                                    add_type_old = add_type;
                                    add_type = reader0["add_type"].ToString();
                                    add_line_old = add_line;
                                    add_line = reader0["add_line"].ToString();
                                    add_barangay_old = add_barangay;
                                    add_barangay = reader0["add_barangay"].ToString();
                                    add_municipality_old = add_municipality;
                                    add_municipality = reader0["add_municipality"].ToString();
                                    add_province_old = add_province;
                                    add_province = reader0["add_province"].ToString();
                                    add_yrs_stay_old = add_yrs_stay;
                                    add_yrs_stay = reader0["add_yrs_stay"] == DBNull.Value ? 0 : Convert.ToInt32(reader0["add_yrs_stay"]);
                                    add_home_ownership_old = add_home_ownership;
                                    add_home_ownership = reader0["add_home_ownership"].ToString();

                                    cust_lastname_old = cust_lastname;
                                    cust_lastname = reader0["cust_lastname"].ToString();
                                    cust_firstname_old = cust_firstname;
                                    cust_firstname = reader0["cust_firstname"].ToString();
                                    cust_middlename_old = cust_middlename;
                                    cust_middlename = reader0["cust_middlename"].ToString();
                                    cust_email_old = cust_email;
                                    cust_email = reader0["cust_email"].ToString();
                                    cust_mobile_num_old = cust_mobile_num;
                                    cust_mobile_num = reader0["cust_mobile_num"].ToString();
                                    cust_tel_num_old = cust_tel_num;
                                    cust_tel_num = reader0["cust_tel_num"].ToString();
                                    cust_birthdate_old = cust_birthdate;
                                    cust_birthdate = reader0["cust_birthdate"] != DBNull.Value ? (DateTime?)Convert.ToDateTime(reader0["cust_birthdate"]) : null;
                                    cust_sex_old = cust_sex;
                                    cust_sex = reader0["cust_sex"].ToString();
                                    cust_citizenship_old = cust_citizenship;
                                    cust_citizenship = reader0["cust_citizenship"].ToString();
                                    cust_civil_status_old = cust_civil_status;
                                    cust_civil_status = reader0["cust_civil_status"].ToString();
                                    emp_status_old = emp_status;
                                    emp_status = reader0["emp_status"].ToString();

                                    emp_comp_name_old = emp_comp_name;
                                    emp_comp_name = reader0["emp_comp_name"].ToString();
                                    emp_comp_add_old = emp_comp_add;
                                    emp_comp_add = reader0["emp_comp_add"].ToString();
                                    emp_comp_num_old = emp_comp_num;
                                    emp_comp_num = reader0["emp_comp_num"].ToString();
                                    emp_comp_yrs_in_old = emp_comp_yrs_in;
                                    emp_comp_yrs_in = reader0["emp_comp_yrs_in"] == DBNull.Value ? 0 : Convert.ToInt32(reader0["emp_comp_yrs_in"]);
                                    emp_comp_position_old = emp_comp_position;
                                    emp_comp_position = reader0["emp_comp_position"].ToString();
                                    emp_monthly_income_old = emp_monthly_income;
                                    emp_monthly_income = reader0["emp_monthly_income"].ToString();

                                    bus_name_old = bus_name;
                                    bus_name = reader0["bus_name"].ToString();
                                    bus_position_old = bus_position;
                                    bus_position = reader0["bus_position"].ToString();
                                    bus_contact_per_old = bus_contact_per;
                                    bus_contact_per = reader0["bus_contact_per"].ToString();

                                    spouse_lastname_old = spouse_lastname;
                                    spouse_lastname = reader0["spouse_lastname"].ToString();
                                    spouse_firstname_old = spouse_firstname;
                                    spouse_firstname = reader0["spouse_firstname"].ToString();
                                    spouse_middlename_old = spouse_middlename;
                                    spouse_middlename = reader0["spouse_middlename"].ToString();
                                    spouse_birthdate_old = spouse_birthdate;
                                    spouse_birthdate = reader0["spouse_birthdate"] != DBNull.Value ? (DateTime?)Convert.ToDateTime(reader0["spouse_birthdate"]) : null;
                                    spouse_contact_num_old = spouse_contact_num;
                                    spouse_contact_num = reader0["spouse_contact_num"].ToString();
                                    spouse_bus_name_old = spouse_bus_name;
                                    spouse_bus_name = reader0["spouse_bus_name"].ToString();
                                    spouse_position_old = spouse_position;
                                    spouse_position = reader0["spouse_position"].ToString();
                                    spouse_income_old = spouse_income;
                                    spouse_income = reader0["spouse_income"].ToString();
                                }
                                if (account_type == "reconnection" || account_type == "change plan")
                                {
                                    plan_type_old = plan_type;
                                    plan_type = reader0["plan_type"].ToString();
                                    plan_fiber_old = plan_fiber;
                                    plan_fiber = Convert.ToBoolean(reader0["plan_fiber"]);
                                    plan_speed_old = plan_speed;
                                    plan_speed = reader0["plan_speed"].ToString();
                                    plan_static_ip_old = plan_static_ip;
                                    plan_static_ip = reader0["plan_static_ip"].ToString();
                                    plan_phone_service_old = plan_phone_service;
                                    plan_phone_service = Convert.ToBoolean(reader0["plan_phone_service"]);
                                    plan_ndd_old = plan_ndd;
                                    plan_ndd = Convert.ToBoolean(reader0["plan_ndd"]);
                                    plan_local_only_old = plan_local_only;
                                    plan_local_only = Convert.ToBoolean(reader0["plan_local_only"]);
                                    plan_phone_num_conf_old = plan_phone_num_conf;
                                    plan_phone_num_conf = Convert.ToBoolean(reader0["plan_phone_num_conf"]);
                                    plan_num_line_old = plan_num_line;
                                    plan_num_line = reader0["plan_num_line"] == DBNull.Value ? 0 : Convert.ToInt32(reader0["plan_num_line"]);
                                    plan_num_extension_old = plan_num_extension;
                                    plan_num_extension = reader0["plan_num_extension"] == DBNull.Value ? 0 : Convert.ToInt32(reader0["plan_num_extension"]);
                                }
                            }
                            reader0.Close();
                        }
                    }
                    prevFillUp();
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

        private void prevFillUp()
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
            if (!string.IsNullOrEmpty(bus_contact_per))
            {
                txtContactName.Text = bus_contact_per;
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
            if (!string.IsNullOrEmpty(spouse_contact_num))
            {
                txtSpouseContactNum.Text = spouse_contact_num;
                txtSpouseContactNum.ForeColor = Color.FromArgb(17, 7, 100);
            }
            if (!string.IsNullOrEmpty(spouse_bus_name))
            {
                txtSpouseIncome.Text = spouse_income;
                txtSpouseIncome.ForeColor = Color.FromArgb(17, 7, 100);
                txtSpouseBussName.Text = spouse_bus_name;
                txtSpouseBussName.ForeColor = Color.FromArgb(17, 7, 100);
                txtSpousePosition.Text = spouse_position;
                txtSpousePosition.ForeColor = Color.FromArgb(17, 7, 100);
            }
            if (emp_status == "EMPLOYED")
            {
                txtSourceMonthIncome.Text = emp_monthly_income;
                txtSourceMonthIncome.ForeColor = Color.FromArgb(17, 7, 100);
                txtSourceCompAdd.Text = emp_comp_add;
                txtSourceCompAdd.ForeColor = Color.FromArgb(17, 7, 100);
                txtSourceYrsInComp.Text = emp_comp_yrs_in.ToString();
                txtSourceYrsInComp.ForeColor = Color.FromArgb(17, 7, 100);
                txtSourceCompNum.Text = emp_comp_num;
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

            if (account_type == "reconnection" || account_type == "information update")
            {
                if (cust_lastname != cust_lastname_old)
                {
                    txtSurname.ForeColor = Color.Maroon;
                }
                if (cust_firstname != cust_firstname_old)
                {
                    txtGivenName.ForeColor = Color.Maroon;
                }
                if (cust_middlename != cust_middlename_old)
                {
                    txtMiddleName.ForeColor = Color.Maroon;
                }
                if (cust_email != cust_email_old)
                {
                    txtEmailAdd.ForeColor = Color.Maroon;
                }
                if (cust_mobile_num != cust_mobile_num_old)
                {
                    txtMobileNum.ForeColor = Color.Maroon;
                }
                if (cust_tel_num != cust_tel_num_old)
                {
                    txtTelNum.ForeColor = Color.Maroon;
                }
                if (cust_birthdate != cust_birthdate_old)
                {
                    txtBirthDate.ForeColor = Color.Maroon;
                }
                if (cust_sex != cust_sex_old)
                {
                    txtSex.ForeColor = Color.Maroon;
                }
                if (cust_citizenship != cust_citizenship_old)
                {
                    txtCitizenship.ForeColor = Color.Maroon;
                }
                if (cust_civil_status != cust_civil_status_old)
                {
                    txtCivilStatus.ForeColor = Color.Maroon;
                }
                if (emp_status != emp_status_old)
                {
                    if (checkEmployed.Checked == true)
                    {
                        checkEmployed.ForeColor = Color.Maroon;
                    }
                    if (checkUnemployedWiI.Checked == true)
                    {
                        checkUnemployedWiI.ForeColor = Color.Maroon;
                    }
                    if (checkUnemployedWoI.Checked == true)
                    {
                        checkUnemployedWoI.ForeColor = Color.Maroon;
                    }
                    if (checkPensioner.Checked == true)
                    {
                        checkPensioner.ForeColor = Color.Maroon;
                    }
                }

                if (add_type != add_type_old)
                {
                    if(checkResi.Checked == true)
                    {
                        checkResi.ForeColor = Color.Maroon;
                    }
                    if (checkBuss.Checked == true)
                    {
                        checkBuss.ForeColor = Color.Maroon;
                    }
                }
                if (add_line != add_line_old)
                {
                    txtHomeBussAdd.ForeColor = Color.Maroon;
                }
                if (add_barangay != add_barangay_old)
                {
                    txtHomeBussBrgy.ForeColor = Color.Maroon;
                }
                if (add_municipality != add_municipality_old)
                {
                    txtHomeBussMunicipal.ForeColor = Color.Maroon;
                }
                if (add_province != add_province_old)
                {
                    txtHomeBussProvince.ForeColor = Color.Maroon;
                }
                if (add_yrs_stay != add_yrs_stay_old)
                {
                    txtYrsStay.ForeColor = Color.Maroon;
                }
                if (add_home_ownership != add_home_ownership_old)
                {
                    if (checkRented.Checked == true)
                    {
                        checkRented.ForeColor = Color.Maroon;
                    }
                    if (checkOwned.Checked == true)
                    {
                        checkOwned.ForeColor = Color.Maroon;
                    }
                    if (checkLiveWP.Checked == true)
                    {
                        checkLiveWP.ForeColor = Color.Maroon;
                    }
                }
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

        private void btnNextForm_Click(object sender, EventArgs e)
        {
            // string add_home_ownership = null;
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
            // string add_type = null;
            if (checkResi.Checked == true)
            {
                add_type = "RESIDENTIAL";
            }
            if (checkBuss.Checked == true)
            {
                add_type = "BUSINESS";
            }
            // string emp_status = null;
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
                    string cust_middlename = txtMiddleName.ForeColor != Color.Gray ? txtMiddleName.Text : "";
                    string cust_email = txtEmailAdd.ForeColor != Color.Gray ? txtEmailAdd.Text : "";
                    string cust_mobile_num = txtMobileNum.ForeColor != Color.Gray ? txtMobileNum.Text : "";
                    string cust_tel_num = txtTelNum.ForeColor != Color.Gray ? txtTelNum.Text : "";
                    string cust_sex = txtSex.Text;
                    string cust_citizenship = txtCitizenship.Text;
                    string cust_civil_status = txtCivilStatus.Text;

                    // tbl_address
                    string add_line = txtHomeBussAdd.Text;
                    string add_barangay = txtHomeBussBrgy.Text;
                    string add_municipality = txtHomeBussMunicipal.Text;
                    string add_province = txtHomeBussProvince.Text;
                    string add_yrs_stay = txtYrsStay.Text;

                    // tbl_business
                    string bus_name = txtBussName.ForeColor != Color.Gray ? txtBussName.Text : "";
                    string bus_position = txtPosition.ForeColor != Color.Gray ? txtPosition.Text : "";
                    string bus_contact_person = txtContactName.ForeColor != Color.Gray ? txtContactName.Text : "";

                    // tbl_spouse
                    string spouse_lastname = txtSpouseSurname.ForeColor != Color.Gray ? txtSpouseSurname.Text : "";
                    string spouse_firstname = txtSpouseGivenName.ForeColor != Color.Gray ? txtSpouseGivenName.Text : "";
                    string spouse_middlename = txtSpouseMiddleName.ForeColor != Color.Gray ? txtSpouseMiddleName.Text : "";
                    string spouse_contact_number = txtSpouseContactNum.ForeColor != Color.Gray ? txtSpouseContactNum.Text : "";
                    string spouse_income = txtSpouseIncome.ForeColor != Color.Gray ? txtSpouseIncome.Text : "";
                    string spouse_business_name = txtSpouseBussName.ForeColor != Color.Gray ? txtSpouseBussName.Text : "";
                    string spouse_position = txtSpousePosition.ForeColor != Color.Gray ? txtSpousePosition.Text : "";

                    // tbl_employment
                    string emp_comp_name = txtSourceCompName.ForeColor != Color.Gray ? txtSourceCompName.Text : "";
                    string emp_comp_address = txtSourceCompAdd.ForeColor != Color.Gray ? txtSourceCompAdd.Text : "";
                    string emp_comp_yrs_in_comp = txtSourceYrsInComp.ForeColor != Color.Gray ? txtSourceYrsInComp.Text : "";
                    string emp_monthly_income = txtSourceMonthIncome.ForeColor != Color.Gray ? txtSourceMonthIncome.Text : "";
                    string emp_comp_number = txtSourceCompNum.ForeColor != Color.Gray ? txtSourceCompNum.Text : "";
                    string emp_comp_position = txtSourcePosition.ForeColor != Color.Gray ? txtSourcePosition.Text : "";

                    frmAppForm1View appForm1View = new frmAppForm1View();

                    appForm1View.parent_form = parent_form;

                    // tbl_account
                    appForm1View.previousClick = previousClick;
                    appForm1View.account_num = account_num;
                    appForm1View.account_date = account_date;
                    appForm1View.account_type = account_type;

                    // tbl_address
                    appForm1View.add_type = add_type;
                    appForm1View.add_line = add_line;
                    appForm1View.add_barangay = add_barangay;
                    appForm1View.add_municipality = add_municipality;
                    appForm1View.add_province = add_province;
                    if (!string.IsNullOrEmpty(add_yrs_stay))
                    {
                        appForm1View.add_yrs_stay = int.Parse(add_yrs_stay);
                    }
                    else
                    {
                        appForm1View.add_yrs_stay = 0;
                    }
                    appForm1View.add_home_ownership = add_home_ownership;

                    // tbl_customer
                    appForm1View.cust_lastname = cust_lastname;
                    appForm1View.cust_firstname = cust_firstname;
                    appForm1View.cust_middlename = cust_middlename;
                    appForm1View.cust_email = cust_email;
                    appForm1View.cust_mobile_num = cust_mobile_num;
                    appForm1View.cust_tel_num = cust_tel_num;
                    appForm1View.cust_birthdate = cust_birthdate;
                    appForm1View.cust_sex = cust_sex;
                    appForm1View.cust_citizenship = cust_citizenship;
                    appForm1View.cust_civil_status = cust_civil_status;

                    // tbl_service_plan
                    appForm1View.plan_type = plan_type;
                    appForm1View.plan_fiber = plan_fiber;
                    appForm1View.plan_speed = plan_speed;
                    appForm1View.plan_static_ip = plan_static_ip;
                    appForm1View.plan_phone_service = plan_phone_service;
                    appForm1View.plan_ndd = plan_ndd;
                    appForm1View.plan_local_only = plan_local_only;
                    appForm1View.plan_phone_num_conf = plan_phone_num_conf;
                    appForm1View.plan_num_line = plan_num_line;
                    appForm1View.plan_num_extension = plan_num_extension;

                    // tbl_employment
                    appForm1View.emp_status = emp_status;
                    appForm1View.emp_comp_name = emp_comp_name;
                    appForm1View.emp_comp_address = emp_comp_address;
                    if (!string.IsNullOrEmpty(emp_comp_yrs_in_comp))
                    {
                        appForm1View.emp_comp_yrs_in_comp = int.Parse(emp_comp_yrs_in_comp);
                    }
                    else
                    {
                        appForm1View.emp_comp_yrs_in_comp = 0;
                    }
                    appForm1View.emp_monthly_income = emp_monthly_income;
                    appForm1View.emp_comp_number = emp_comp_number;
                    appForm1View.emp_comp_position = emp_comp_position;

                    // tbl_business
                    appForm1View.bus_name = bus_name;
                    appForm1View.bus_position = bus_position;
                    appForm1View.bus_contact_person = bus_contact_person;

                    // tbl_spouse
                    appForm1View.spouse_lastname = spouse_lastname;
                    appForm1View.spouse_firstname = spouse_firstname;
                    appForm1View.spouse_middlename = spouse_middlename;
                    appForm1View.spouse_birthdate = spouse_birthdate;
                    appForm1View.spouse_contact_number = spouse_contact_number;
                    appForm1View.spouse_income = spouse_income;
                    appForm1View.spouse_business_name = spouse_business_name;
                    appForm1View.spouse_position = spouse_position;

                    if (account_type == "reconnection" || account_type == "information update")
                    {
                        // tbl_address
                        appForm1View.add_type_old = add_type_old;
                        appForm1View.add_line_old = add_line_old;
                        appForm1View.add_barangay_old = add_barangay_old;
                        appForm1View.add_municipality_old = add_municipality_old;
                        appForm1View.add_province_old = add_province_old;
                        appForm1View.add_yrs_stay_old = add_yrs_stay_old;
                        appForm1View.add_home_ownership_old = add_home_ownership_old;

                        // tbl_customer
                        appForm1View.cust_lastname_old = cust_lastname_old;
                        appForm1View.cust_firstname_old = cust_firstname_old;
                        appForm1View.cust_middlename_old = cust_middlename_old;
                        appForm1View.cust_email_old = cust_email_old;
                        appForm1View.cust_mobile_num_old = cust_mobile_num_old;
                        appForm1View.cust_tel_num_old = cust_tel_num_old;
                        appForm1View.cust_birthdate_old = cust_birthdate_old;
                        appForm1View.cust_sex_old = cust_sex_old;
                        appForm1View.cust_citizenship_old = cust_citizenship_old;
                        appForm1View.cust_civil_status_old = cust_civil_status_old;

                        // tbl_employment
                        appForm1View.emp_status_old = emp_status_old;
                        appForm1View.emp_comp_name_old = emp_comp_name_old;
                        appForm1View.emp_comp_add_old = emp_comp_add_old;
                        appForm1View.emp_comp_yrs_in_old = emp_comp_yrs_in_old;
                        appForm1View.emp_monthly_income_old = emp_monthly_income_old;
                        appForm1View.emp_comp_num_old = emp_comp_num_old;
                        appForm1View.emp_comp_position_old = emp_comp_position_old;

                        // tbl_business
                        appForm1View.bus_name_old = bus_name_old;
                        appForm1View.bus_position_old = bus_position_old;
                        appForm1View.bus_contact_per_old = bus_contact_per_old;

                        // tbl_spouse
                        appForm1View.spouse_lastname_old = spouse_lastname_old;
                        appForm1View.spouse_firstname_old = spouse_firstname_old;
                        appForm1View.spouse_middlename_old = spouse_middlename_old;
                        appForm1View.spouse_birthdate_old = spouse_birthdate_old;
                        appForm1View.spouse_contact_num_old = spouse_contact_num_old;
                        appForm1View.spouse_income_old = spouse_income_old;
                        appForm1View.spouse_bus_name_old = spouse_bus_name_old;
                        appForm1View.spouse_position_old = spouse_position_old;
                    }
                    if (account_type == "reconnection" || account_type == "change plan")
                    {
                        // tbl_service_plan
                        appForm1View.plan_type_old = plan_type_old;
                        appForm1View.plan_fiber_old = plan_fiber_old;
                        appForm1View.plan_speed_old = plan_speed_old;
                        appForm1View.plan_static_ip_old = plan_static_ip_old;
                        appForm1View.plan_phone_service_old = plan_phone_service_old;
                        appForm1View.plan_ndd_old = plan_ndd_old;
                        appForm1View.plan_local_only_old = plan_local_only_old;
                        appForm1View.plan_phone_num_conf_old = plan_phone_num_conf_old;
                        appForm1View.plan_num_line_old = plan_num_line_old;
                        appForm1View.plan_num_extension_old = plan_num_extension_old;
                    }

                    appForm1View.ShowDialog();
                    // this.Dispose();
                    this.Close();
                }
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            // this.Dispose();
            this.Close();
        }

        private void checkNewAccount_Click(object sender, EventArgs e)
        {
            if (checkExistingAccount.Checked == true)
            {
                checkExistingAccount.Checked = false;
                checkExistingAccount.Text = checkExistingAccount.Tag.ToString();
            }
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
                }
            }
            else
            {
                checkExistingAccount.Text = checkExistingAccount.Tag.ToString();
            }
        }

        public void unCheckExistingAccount()
        {
            checkExistingAccount.Checked = false;
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
                    if (checkbox != checkNewAccount && checkbox != checkExistingAccount)
                    {

                    }
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
            if (account_type != "change plan")
            {
                setEnableAllControls(this);
            }
            else
            {
                MessageBox.Show("You can't edit this section of form.", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void txt_DoubleClick(object sender, EventArgs e)
        {
            TextBox txt = sender as TextBox;
            if (txt.ForeColor == Color.Maroon)
            {
                if(txt == txtSurname)
                {
                    MessageBox.Show($"{cust_lastname_old}  -->  {cust_lastname}");
                }
                if (txt == txtGivenName)
                {
                    MessageBox.Show($"{cust_firstname_old}  -->  {cust_firstname}");
                }
                if (txt == txtMiddleName)
                {
                    MessageBox.Show($"{cust_middlename_old}  -->  {cust_middlename}");
                }
                if (txt == txtEmailAdd)
                {
                    MessageBox.Show($"{cust_email_old}  -->  {cust_email}");
                }
                if (txt == txtMobileNum)
                {
                    MessageBox.Show($"{cust_mobile_num_old}  -->  {cust_mobile_num}");
                }
                if (txt == txtTelNum)
                {
                    MessageBox.Show($"{cust_tel_num_old}  -->  {cust_tel_num}");
                }
                if (txt == txtBirthDate)
                {
                    MessageBox.Show($"{cust_birthdate_old?.ToString("MMMM dd, yyyy")}  -->  {cust_birthdate?.ToString("MMMM dd, yyyy")}");
                }
                if (txt ==txtSex)
                {
                    MessageBox.Show($"{cust_sex_old}  -->  {cust_sex}");
                }
                if (txt == txtCitizenship)
                {
                    MessageBox.Show($"{cust_citizenship_old}  -->  {cust_citizenship}");
                }
                if (txt == txtCivilStatus)
                {
                    MessageBox.Show($"{cust_civil_status_old}  -->  {cust_civil_status}");
                }


                if (txt == txtHomeBussAdd)
                {
                    MessageBox.Show($"{add_line_old}  -->  {add_line}");
                }
                if (txt == txtHomeBussBrgy)
                {
                    MessageBox.Show($"{add_barangay_old}  -->  {add_barangay}");
                }
                if (txt == txtHomeBussMunicipal)
                {
                    MessageBox.Show($"{add_municipality_old}  -->  {add_municipality}");
                }
                if (txt == txtHomeBussProvince)
                {
                    MessageBox.Show($"{add_province_old}  -->  {add_province}");
                }
                if (txt == txtYrsStay)
                {
                    MessageBox.Show($"{add_yrs_stay_old}  -->  {add_yrs_stay}");
                }
            }
        }

        private void lbl_DoubleClick(object sender, EventArgs e)
        {
            Label lbl = sender as Label;
            if (lbl.Text == "      Employment Status")
            {
                if (checkEmployed.ForeColor == Color.Maroon || checkUnemployedWiI.ForeColor == Color.Maroon || checkUnemployedWoI.ForeColor == Color.Maroon || checkPensioner.ForeColor == Color.Maroon)
                {
                    MessageBox.Show($"{emp_status_old}  -->  {emp_status}");
                }
            }
            if (lbl.Text == "      Address Type")
            {
                if (checkResi.ForeColor == Color.Maroon || checkBuss.ForeColor == Color.Maroon)
                {
                    MessageBox.Show($"{add_type_old}  -->  {add_type}");
                }
            }
            if (lbl.Text == "      Home Ownership")
            {
                if (checkRented.ForeColor == Color.Maroon || checkRented.ForeColor == Color.Maroon || checkLiveWP.ForeColor == Color.Maroon)
                {
                    MessageBox.Show($"{add_home_ownership_old}  -->  {add_home_ownership}");
                }
            }
        }
    }
}
