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
    public partial class frmCustomerInfo : Form
    {
        public int? account_num { get; set; }
        public bool isPortal { get; set; }

        private frmCustomerInfo1 customerInfo1;

        public frmCustomerInfo()
        {
            InitializeComponent();
        }

        private void frmCustomerInfo_Load(object sender, EventArgs e)
        {
            if (isPortal)
            {
                btnOthers.Visible = true;
            }

            MySqlConnection connection = MyConnectionString.mysql_connection();
            try
            {
                connection.Open();

                string query = @"SELECT account.account_date, 
                                    customer.cust_lastname, 
                                    customer.cust_firstname, 
                                    customer.cust_middlename, 
                                    customer.cust_sex, 
                                    customer.cust_email, 
                                    customer.cust_mobile_num, 
                                    customer.cust_tel_num, 
                                    customer.emp_status, 
                                    address.add_line, 
                                    address.add_barangay, 
                                    address.add_municipality, 
                                    address.add_province, 
                                    address.add_type, 
                                    address.add_home_ownership, 
                                    plan.plan_type, 
                                    plan.plan_fiber, 
                                    plan.plan_speed, 
                                    plan.plan_static_ip, 
                                    plan.plan_phone_service, 
                                    plan.plan_ndd, 
                                    plan.plan_local_only, 
                                    plan.plan_phone_num_conf, 
                                    plan.plan_num_line, 
                                    plan.plan_num_extension, 
                                    business.bus_name, 
                                    business.bus_position, 
                                    business.bus_contact_per, 
                                    price.plan_price
                                 FROM tbl_account as account 
                                 INNER JOIN tbl_address as address ON account.add_id = address.add_id 
                                 INNER JOIN tbl_service_plan as plan ON account.plan_id = plan.plan_id 
                                 INNER JOIN tbl_customer as customer ON address.cust_id = customer.cust_id 
                                 LEFT JOIN tbl_plan_price as price ON account.price_id = price.price_id
                                 LEFT JOIN tbl_business as business ON address.add_id = business.add_id 
                                 WHERE account.account_num = @account_num;";
                MySqlCommand cmd = new MySqlCommand(query, connection);
                cmd.Parameters.AddWithValue("@account_num", account_num);
                using (MySqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        DateTime accountDate = reader.IsDBNull(reader.GetOrdinal("account_date")) ? default(DateTime) : reader.GetDateTime("account_date");
                        string custLastName = reader.IsDBNull(reader.GetOrdinal("cust_lastname")) ? null : reader.GetString("cust_lastname");
                        string custFirstName = reader.IsDBNull(reader.GetOrdinal("cust_firstname")) ? null : reader.GetString("cust_firstname");
                        string custMiddleName = reader.IsDBNull(reader.GetOrdinal("cust_middlename")) ? null : reader.GetString("cust_middlename");
                        string custSex = reader.IsDBNull(reader.GetOrdinal("cust_sex")) ? null : reader.GetString("cust_sex");
                        string custEmail = reader.IsDBNull(reader.GetOrdinal("cust_email")) ? null : reader.GetString("cust_email");
                        string custMobileNum = reader.IsDBNull(reader.GetOrdinal("cust_mobile_num")) ? null : reader.GetString("cust_mobile_num");
                        string custTelNum = reader.IsDBNull(reader.GetOrdinal("cust_tel_num")) ? null : reader.GetString("cust_tel_num");
                        string empStatus = reader.IsDBNull(reader.GetOrdinal("emp_status")) ? null : reader.GetString("emp_status");
                        string addLine = reader.IsDBNull(reader.GetOrdinal("add_line")) ? null : reader.GetString("add_line");
                        string addBarangay = reader.IsDBNull(reader.GetOrdinal("add_barangay")) ? null : reader.GetString("add_barangay");
                        string addMunicipality = reader.IsDBNull(reader.GetOrdinal("add_municipality")) ? null : reader.GetString("add_municipality");
                        string addProvince = reader.IsDBNull(reader.GetOrdinal("add_province")) ? null : reader.GetString("add_province");
                        string addType = reader.IsDBNull(reader.GetOrdinal("add_type")) ? null : reader.GetString("add_type");
                        string addHomeOwnership = reader.IsDBNull(reader.GetOrdinal("add_home_ownership")) ? null : reader.GetString("add_home_ownership");
                        string planType = reader.IsDBNull(reader.GetOrdinal("plan_type")) ? null : reader.GetString("plan_type");
                        bool planFiber = reader.IsDBNull(reader.GetOrdinal("plan_fiber")) ? false : reader.GetBoolean("plan_fiber");
                        string planSpeed = reader.IsDBNull(reader.GetOrdinal("plan_speed")) ? null : reader.GetString("plan_speed");
                        string planStaticIP = reader.IsDBNull(reader.GetOrdinal("plan_static_ip")) ? null : reader.GetString("plan_static_ip");
                        bool planPhoneService = reader.IsDBNull(reader.GetOrdinal("plan_phone_service")) ? false : reader.GetBoolean("plan_phone_service");
                        bool planNDD = reader.IsDBNull(reader.GetOrdinal("plan_ndd")) ? false : reader.GetBoolean("plan_ndd");
                        bool planLocalOnly = reader.IsDBNull(reader.GetOrdinal("plan_local_only")) ? false : reader.GetBoolean("plan_local_only");
                        bool planPhoneNumConf = reader.IsDBNull(reader.GetOrdinal("plan_phone_num_conf")) ? false : reader.GetBoolean("plan_phone_num_conf");
                        int planNumLine = reader.IsDBNull(reader.GetOrdinal("plan_num_line")) ? 0 : reader.GetInt32("plan_num_line");
                        int planNumExtension = reader.IsDBNull(reader.GetOrdinal("plan_num_extension")) ? 0 : reader.GetInt32("plan_num_extension");
                        string busName = reader.IsDBNull(reader.GetOrdinal("bus_name")) ? null : reader.GetString("bus_name");
                        string busPosition = reader.IsDBNull(reader.GetOrdinal("bus_position")) ? null : reader.GetString("bus_position");
                        string busContactPer = reader.IsDBNull(reader.GetOrdinal("bus_contact_per")) ? null : reader.GetString("bus_contact_per");
                        int planPrice = reader.IsDBNull(reader.GetOrdinal("plan_price")) ? 0 : reader.GetInt32("plan_price");

                        lblAccountNum.Text = $"#{10000 + account_num}";
                        lblAccountDate.Text = accountDate.ToString("MMMM dd, yyyy");
                        lblCustLastname.Text = capitalizedFirstCharacters(custLastName.ToLower());
                        lblCustFirstname.Text = capitalizedFirstCharacters(custFirstName.ToLower());
                        lblCustMiddlename.Text = !string.IsNullOrEmpty(custMiddleName) ? capitalizedFirstCharacters(custMiddleName.ToLower()) : "";
                        lblCustSex.Text = !string.IsNullOrEmpty(custSex) ? custSex.ToLower() : "";
                        lblCustEmail.Text = !string.IsNullOrEmpty(custEmail) ? custEmail.ToLower() : "";
                        lblCustmobile.Text = custMobileNum;
                        lblCustTelephone.Text = custTelNum;
                        lblEmpStatus.Text = empStatus.ToLower();
                        lblAddType.Text = addType.ToLower();
                        lblAddFull.Text = $"{addLine.ToUpper()}, {addBarangay.ToUpper()}, {addMunicipality.ToUpper()}, {addProvince.ToUpper()}";
                        if (addHomeOwnership == "LIVEWP")
                        {
                            lblHomeOwnership.Text = "living with parents";
                        }
                        else
                        {
                            lblHomeOwnership.Text = addHomeOwnership.ToLower();
                        }
                        lblPlanType.Text = planType.ToString().ToLower();
                        lblPlanFiber.Text = planFiber.ToString().ToLower();
                        lblPlanSpeed.Text = planSpeed;
                        lblPlanStatic.Text = planStaticIP;
                        lblPlanPhone.Text = planPhoneService.ToString().ToLower();
                        lblPlanNDD.Text = planNDD.ToString().ToLower();
                        lblPlanLocal.Text = planLocalOnly.ToString().ToLower();
                        lblPlanLine.Text = planNumLine != 0 ? planNumLine.ToString() : "";
                        lblPlanExtension.Text = planNumExtension != 0 ? planNumExtension.ToString() : "";
                        lblMonthlyBill.Text = planPrice != 0 ? $"₱{planPrice:N0}" : "";
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.ToString()}", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                // MessageBox.Show("An error has occured.", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                connection.Close();
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

        private void btnOthers_Click(object sender, EventArgs e)
        {
            if (customerInfo1 == null || customerInfo1.IsDisposed)
            {
                customerInfo1 = new frmCustomerInfo1();
                customerInfo1.FormClosed += frmCustomerInfo1_FormClosed;
                customerInfo1.Owner = this;
                customerInfo1.TopMost = true;
                customerInfo1.account_num = account_num;
                btnOthers.Visible = false;
                customerInfo1.Show();
            }
        }

        private void frmCustomerInfo1_FormClosed(object sender, EventArgs e)
        {
            btnOthers.Visible = true;
        }

        private void frmCustomerInfo_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (customerInfo1 != null)
            {
                customerInfo1.Close();
            }
        }
    }
}
