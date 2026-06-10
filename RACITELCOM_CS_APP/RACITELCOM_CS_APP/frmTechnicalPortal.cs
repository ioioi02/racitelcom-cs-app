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
    public partial class frmTechnicalPortal : Form
    {
        public string defualt_query = @"SELECT
                                            CONCAT('#', account.account_num + 10000) as account_num,
                                            customer.cust_lastname,
                                            customer.cust_firstname,
                                            customer.cust_middlename,
                                            customer.cust_email,
                                            customer.cust_mobile_num,
                                            customer.cust_tel_num,
                                            customer.cust_sex,
                                            LOWER(address.add_type) as add_type,
                                            address.add_line,
                                            address.add_barangay,
                                            address.add_municipality,
                                            address.add_province,
                                            LOWER(plan.plan_type) as plan_type,
                                            CASE 
                                                WHEN plan.plan_fiber = 1 THEN 'true'
                                                ELSE 'false'
                                            END as plan_fiber,
                                            plan.plan_speed,
                                            CASE 
                                                WHEN plan.plan_phone_service = 1 THEN 'true'
                                                ELSE 'false'
                                            END as plan_phone_service,
                                            CASE 
                                                WHEN plan.plan_ndd = 1 THEN 'true'
                                                ELSE 'false'
                                            END as plan_ndd,
                                            CASE 
                                                WHEN plan.plan_local_only = 1 THEN 'true'
                                                ELSE 'false'
                                            END as plan_local_only,
                                            CASE 
                                                WHEN plan.plan_phone_num_conf = 1 THEN 'true'
                                                ELSE 'false'
                                            END as plan_phone_num_conf,
                                            DATE_FORMAT(account.account_due_date, '%M %d, %Y') as account_due_date,
                                            CONCAT('₱', FORMAT(price.plan_price, 0)) as plan_price,
                                            CASE 
                                                WHEN account.account_status = 1 THEN 'ACTIVE'
                                            END as account_status
                                         FROM
                                            tbl_account as account
                                            INNER JOIN tbl_address as address ON account.add_id = address.add_id
                                            INNER JOIN tbl_customer as customer ON address.cust_id = customer.cust_id
                                            INNER JOIN tbl_service_plan as plan ON account.plan_id = plan.plan_id
                                            LEFT JOIN tbl_plan_price as price ON account.price_id = price.price_id
                                         WHERE
                                            account.account_status = 1
                                         ORDER BY
                                            customer.cust_lastname ASC;";

        private frmCustomerInfo customerInfo;

        public frmTechnicalPortal()
        {
            InitializeComponent();
        }

        private void frmTechnicalPortal_Load(object sender, EventArgs e)
        {
            comboSearchOption.Items.AddRange(new string[] { "Account Number", "Lastname", "Barangay", "Municipality", "Province" });
            comboSearchOption.SelectedItem = "Account Number";

            displayDatabase(defualt_query, null);

            string[] customHeaderText = { "Account Number", "Lastname", "Firstname", "Middlename", "Email", "Mobile Number", "Telephone Number", "Sex", "Address Type", "Address Line", "Barangay", "Municipality", "Province", "Plan Type", "Fiber", "Plan Speed", "Phone Service", "NDD", "Local Only", "Confidential", "Account Due Date", "Monthly Bill", "Account Status" };
            for (int i = 0; i < dataBaseGridView.Columns.Count && i < customHeaderText.Length; i++)
            {
                dataBaseGridView.Columns[i].HeaderText = customHeaderText[i];
            }

            dataBaseGridView.DataBindingComplete += DataBaseGridView_DataBindingComplete;
        }

        private void DataBaseGridView_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            dataBaseGridView.ClearSelection();
        }

        private void displayDatabase(string query, object value)
        {
            MySqlConnection connection = MyConnectionString.mysql_connection();
            MySqlCommand cmd = new MySqlCommand(query, connection);
            cmd.Parameters.AddWithValue("@searchText", value);
            MySqlDataAdapter da = new MySqlDataAdapter(cmd);
            DataTable dt = new DataTable();

            try
            {
                connection.Open();
                da.Fill(dt);
                dataBaseGridView.DataSource = dt;

                txtSearchBox.Text = "";
                txtAccountNum.Text = "";
                txtUserFullName.Text = "";
                txtAccountDue.Text = "";
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex}", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                MessageBox.Show("An error has occured.", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                connection.Close();
            }
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            string searchText = txtSearchBox.Text;
            object searchText_ = null;
            if (searchText != "")
            {
                string column = null;
                string table_alias = null;
                if (comboSearchOption.SelectedItem.ToString() == "Account Number")
                {
                    column = "account_num";
                    table_alias = "account";
                    searchText = searchText.TrimStart('#');
                    int accountNumber;
                    if (int.TryParse(searchText, out accountNumber))
                    {
                        accountNumber -= 10000;
                        searchText_ = accountNumber;
                    }
                }
                else if (comboSearchOption.SelectedItem.ToString() == "Lastname")
                {
                    column = "cust_lastname";
                    table_alias = "customer";
                    searchText_ = searchText.ToLower();
                }
                else if (comboSearchOption.SelectedItem.ToString() == "Barangay")
                {
                    column = "add_barangay";
                    table_alias = "address";
                    searchText_ = searchText.ToLower();
                }

                else if (comboSearchOption.SelectedItem.ToString() == "Municipality")
                {
                    column = "add_municipality";
                    table_alias = "address";
                    searchText_ = searchText.ToLower();
                }

                else if (comboSearchOption.SelectedItem.ToString() == "Province")
                {
                    column = "add_province";
                    table_alias = "address";
                    searchText_ = searchText.ToLower();
                }

                string query = $@"SELECT
                                    CONCAT('#', account.account_num + 10000) as account_num,
                                    customer.cust_lastname,
                                    customer.cust_firstname,
                                    customer.cust_middlename,
                                    customer.cust_email,
                                    customer.cust_mobile_num,
                                    customer.cust_tel_num,
                                    customer.cust_sex,
                                    LOWER(address.add_type) as add_type,
                                    address.add_line,
                                    address.add_barangay,
                                    address.add_municipality,
                                    address.add_province,
                                    LOWER(plan.plan_type) as plan_type,
                                    CASE 
                                        WHEN plan.plan_fiber = 1 THEN 'true'
                                        ELSE 'false'
                                    END as plan_fiber,
                                    plan.plan_speed,
                                    CASE 
                                        WHEN plan.plan_phone_service = 1 THEN 'true'
                                        ELSE 'false'
                                    END as plan_phone_service,
                                    CASE 
                                        WHEN plan.plan_ndd = 1 THEN 'true'
                                        ELSE 'false'
                                    END as plan_ndd,
                                    CASE 
                                        WHEN plan.plan_local_only = 1 THEN 'true'
                                        ELSE 'false'
                                    END as plan_local_only,
                                    CASE 
                                        WHEN plan.plan_phone_num_conf = 1 THEN 'true'
                                        ELSE 'false'
                                    END as plan_phone_num_conf,
                                    DATE_FORMAT(account.account_due_date, '%M %d, %Y') as account_due_date,
                                    CONCAT('₱', FORMAT(price.plan_price, 0)) as plan_price,
                                    CASE 
                                        WHEN account.account_status = 1 THEN 'ACTIVE'
                                    END as account_status
                                    FROM
                                    tbl_account as account
                                    INNER JOIN tbl_address as address ON account.add_id = address.add_id
                                    INNER JOIN tbl_customer as customer ON address.cust_id = customer.cust_id
                                    INNER JOIN tbl_service_plan as plan ON account.plan_id = plan.plan_id
                                    LEFT JOIN tbl_plan_price as price ON account.price_id = price.price_id
                                    WHERE
                                    account.account_status = 1 AND
                                    {table_alias}.{column} = @searchText
                                    ORDER BY
                                    customer.cust_lastname ASC;";
                displayDatabase(query, searchText_);
                txtSearchBox.Text = "";
            }
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            displayDatabase(defualt_query, null);
            txtSearchBox.Text = "";
        }

        private void dataBaseGridView_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dataBaseGridView.Rows[e.RowIndex];

                string accountNumValue = row.Cells["account_num"].Value.ToString();
                int? accountNum = null;
                if (!string.IsNullOrEmpty(accountNumValue))
                {
                    accountNumValue = accountNumValue.TrimStart('#');
                    accountNum = Convert.ToInt32(accountNumValue);
                    accountNum -= 10000;
                }
                string lastName = row.Cells["cust_lastname"].Value.ToString();
                string firstName = row.Cells["cust_firstname"].Value.ToString();
                string middlename = row.Cells["cust_middlename"].Value.ToString();
                DateTime _;
                DateTime? accountDueDate = DateTime.TryParse(row.Cells["account_due_date"].Value?.ToString(), out _) ? DateTime.Parse(row.Cells["account_due_date"].Value.ToString()) : (DateTime?)null;

                if (string.IsNullOrEmpty(lastName) && string.IsNullOrEmpty(firstName) && string.IsNullOrEmpty(middlename) && accountNum == null)
                {
                    txtAccountNum.Text = "";
                    txtUserFullName.Text = "";
                    txtAccountDue.Text = "";
                }
                else
                {
                    txtAccountNum.Text = $"#{10000 + accountNum}";
                    txtAccountDue.Text = accountDueDate.HasValue ? accountDueDate.Value.ToString("MMMM dd, yyyy") : "";
                    if (!string.IsNullOrEmpty(middlename))
                    {
                        txtUserFullName.Text = $"{capitalizedFirstCharacters(firstName)} {capitalizedFirstCharacters(middlename)} {capitalizedFirstCharacters(lastName)}";
                    }
                    else
                    {
                        txtUserFullName.Text = $"{capitalizedFirstCharacters(firstName)} {capitalizedFirstCharacters(lastName)}";
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

        private void btnDetails_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtAccountNum.Text))
            {
                if (customerInfo == null || customerInfo.IsDisposed)
                {
                    customerInfo = new frmCustomerInfo();
                    customerInfo.FormClosed += frmCustomerInfo_FormClosed;
                    customerInfo.Owner = this;
                    customerInfo.TopMost = true;
                    string accountNumString = txtAccountNum.Text.TrimStart('#');
                    int account_num = Convert.ToInt32(accountNumString);
                    account_num -= 10000;
                    customerInfo.account_num = account_num;
                    customerInfo.isPortal = true;
                    btnDetails.Enabled = false;
                    customerInfo.Show();
                }
            }
            else
            {
                MessageBox.Show("Please select user first.", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnHistory_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtAccountNum.Text))
            {
                string accountNumString = txtAccountNum.Text.TrimStart('#');
                int account_num = Convert.ToInt32(accountNumString);
                account_num -= 10000;
                frmBillingHistory billingHistory = new frmBillingHistory();
                billingHistory.account_num = account_num;
                billingHistory.ShowDialog();
            }
            else
            {
                MessageBox.Show("Please select user first.", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnDisconnect_Click(object sender, EventArgs e)
        {

        }

        private void frmCustomerInfo_FormClosed(object sender, EventArgs e)
        {
            btnDetails.Enabled = true;
        }

        private void frmTechnicalPortal_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (customerInfo != null)
            {
                customerInfo.Close();
            }
        }

        private void dataBaseGridView_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            frmCustTicket custTicket = new frmCustTicket();
            string accountNumValue = txtAccountNum.Text;
            int? accountNum = null;
            if (!string.IsNullOrEmpty(accountNumValue))
            {
                accountNumValue = accountNumValue.TrimStart('#');
                accountNum = Convert.ToInt32(accountNumValue);
                accountNum -= 10000;

                custTicket.account_num = accountNum;
                custTicket.ShowDialog();
            }
        }
    }
}
