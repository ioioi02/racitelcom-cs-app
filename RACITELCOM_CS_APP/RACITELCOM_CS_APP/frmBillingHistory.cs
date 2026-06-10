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
    public partial class frmBillingHistory : Form
    {
        public string default_query = @"SELECT
                                            bill_transac_type,
                                            CONCAT('OR-', or_num + 80000) as or_num,
                                            bill_payment_type,
                                            CONCAT('₱', FORMAT(bill_amount_paid, 0)) as amount_paid,
                                            DATE_FORMAT(bill_sales_agent_date, '%M %d, %Y %H:%i') as sales_agent_date
                                         FROM
                                            tbl_billing
                                         WHERE
                                            account_num = @account_num
                                         ORDER BY
                                            bill_sales_agent_date DESC;";

        public int account_num { get; set; }
        public string cust_lastname { get; set; }
        public string cust_firstname { get; set; }
        public string cust_middlename { get; set; }

        public frmBillingHistory()
        {
            InitializeComponent();
        }

        private void frmBillingHistory_Load(object sender, EventArgs e)
        {
            displayDatabase(default_query);

            lblAccountNum.Text = $"#{10000 + account_num}";
            if (string.IsNullOrEmpty(cust_middlename))
            {
                lblCustomerName.Text = $"{capitalizedFirstCharacters(cust_firstname)} {capitalizedFirstCharacters(cust_lastname)}";
            }
            else
            {
                lblCustomerName.Text = $"{capitalizedFirstCharacters(cust_firstname)} {capitalizedFirstCharacters(cust_middlename)} {capitalizedFirstCharacters(cust_lastname)}";
            }

            string[] customHeaderText = { "Transaction Type", "OR Number", "Payment Type", "Amount Paid", "Date" };
            for (int i = 0; i < dataBaseGridView.Columns.Count && i < customHeaderText.Length; i++)
            {
                dataBaseGridView.Columns[i].HeaderText = customHeaderText[i];
            }

            if (dataBaseGridView.Columns["btnView"] == null)
            {
                var buttonColumn = new DataGridViewButtonColumn
                {
                    Text = "View",
                    UseColumnTextForButtonValue = true,
                    Name = "btnView",
                    HeaderText = "",
                    FlatStyle = FlatStyle.Flat
                };
                dataBaseGridView.Columns.Add(buttonColumn);
            }
        }

        private void displayDatabase(string query)
        {
            MySqlConnection connection = MyConnectionString.mysql_connection();
            MySqlCommand cmd = new MySqlCommand(query, connection);
            cmd.Parameters.AddWithValue("@account_num", account_num);
            MySqlDataAdapter da = new MySqlDataAdapter(cmd);
            DataTable dt = new DataTable();

            try
            {
                connection.Open();

                da.Fill(dt);
                dataBaseGridView.DataSource = dt;

                dataBaseGridView.ClearSelection();

                string query0 = @"SELECT
                                    customer.cust_lastname,
                                    customer.cust_firstname,
                                    customer.cust_middlename
                                  FROM
                                    tbl_billing as bill
                                    INNER JOIN tbl_account as account ON bill.account_num = account.account_num
                                    INNER JOIN tbl_address as address ON account.add_id = address.add_id
                                    INNER JOIN tbl_customer as customer ON address.cust_id = customer.cust_id
                                  WHERE
                                    account.account_num = @account_num;";
                MySqlCommand cmd0 = new MySqlCommand(query0, connection);
                cmd0.Parameters.AddWithValue("@account_num", account_num);
                using (MySqlDataReader reader = cmd0.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        cust_lastname = reader["cust_lastname"].ToString();
                        cust_firstname = reader["cust_firstname"].ToString();
                        cust_middlename = reader["cust_middlename"].ToString();
                    }
                    reader.Close();
                }
            }
            catch (Exception ex)
            {
                // MessageBox.Show($"Error: {ex}", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                MessageBox.Show("An error has occured.", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                connection.Close();
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            // this.Dispose();
            this.Close();
        }

        private void dataBaseGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex == dataBaseGridView.Columns["btnView"].Index)
            {
                string orNumber = dataBaseGridView.Rows[e.RowIndex].Cells["or_num"].Value.ToString();

                if (!string.IsNullOrEmpty(orNumber))
                {
                    MessageBox.Show($"View OR number: {orNumber}");
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
    }
}
