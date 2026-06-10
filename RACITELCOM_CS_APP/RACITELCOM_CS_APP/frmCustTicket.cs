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
    public partial class frmCustTicket : Form
    {
        public int? account_num { get; set; }
        public int ticket_num { get; set; }

        // ticket information
        public string ticket_priority { get; set; }
        public string ticket_type { get; set; }
        public string ticket_called_in { get; set; }
        public string ticket_call_type { get; set; }
        public string ticket_issue { get; set; }
        public DateTime ticket_created_by_date { get; set; }
        public string ticket_call_num { get; set; }
        public string ticket_lineman { get; set; }
        public string ticket_assigned_to { get; set; }

        // customer information to store
        public string cust_lastname { get; set; }
        public string cust_firstname { get; set; }
        public string cust_middlename { get; set; }
        public string add_line { get; set; }
        public string add_barangay { get; set; }
        public string add_municipality { get; set; }
        public string add_province { get; set; }

        public string created_by_name { get; set; }

        // username information to store
        public string user_lastname { get; set; }
        public string user_firstname { get; set; }
        public string user_middlename { get; set; }

        public string ticket_type_ = "Open";

        public frmCustTicket()
        {
            InitializeComponent();
            TCPClient.Instance.OnMessageReceived += Client_On_MessageReceived;
        }

        private void Client_On_MessageReceived(string message)
        {
            if (InvokeRequired)
            {
                Invoke(new Action(() => {
                    if (message.IndexOf("ticket", StringComparison.OrdinalIgnoreCase) >= 0)
                    {
                        loadCustTicket();
                    }
                }));
            }
            else
            {
                if (message.IndexOf("ticket", StringComparison.OrdinalIgnoreCase) >= 0)
                {
                    loadCustTicket();
                }
            }
        }

        private void frmCustTicket_Load(object sender, EventArgs e)
        {
            loadCustTicket();
        }

        private void loadCustTicket()
        {
            MySqlConnection connection = MyConnectionString.mysql_connection();
            try
            {
                connection.Open();

                string query = $@"SELECT 
                                    ticket.ticket_num, 
                                    ticket.ticket_priority, 
                                    ticket.ticket_type, 
                                    ticket.ticket_called_in, 
                                    ticket.ticket_call_type, 
                                    ticket.ticket_issue, 
                                    ticket.ticket_created_by_date, 
                                    ticket.ticket_call_num, 
                                    ticket.ticket_lineman, 
                                    ticket.ticket_assigned_to, 
                                    user.user_lastname, 
                                    user.user_firstname, 
                                    user.user_middlename, 
                                    customer.cust_lastname, 
                                    customer.cust_firstname, 
                                    customer.cust_middlename, 
                                    address.add_line, 
                                    address.add_barangay, 
                                    address.add_municipality, 
                                    address.add_province
                                  FROM
                                    tbl_account as account
                                    INNER JOIN tbl_ticket as ticket ON account.account_num = ticket.account_num
                                    INNER JOIN tbl_user as user ON ticket.ticket_created_by = user.user_name
                                    INNER JOIN tbl_address as address ON account.add_id = address.add_id
                                    INNER JOIN tbl_customer as customer ON address.cust_id = customer.cust_id
                                  WHERE 
                                    account.account_num = @account_num AND
                                    ticket_type = @ticket_type
                                  ORDER BY 
                                    ticket_created_by_date DESC
                                  LIMIT 10;";
                MySqlCommand cmd = new MySqlCommand(query, connection);
                cmd.Parameters.AddWithValue("@account_num", account_num);
                cmd.Parameters.AddWithValue("@ticket_type", ticket_type_);
                using (MySqlDataReader reader = cmd.ExecuteReader())
                {
                    flowTicketPanel.Controls.Clear();
                    if (reader.HasRows)
                    {
                        int count = 0;
                        while (reader.Read())
                        {
                            ticket_num = Convert.ToInt32(reader["ticket_num"]);
                            ticket_priority = reader["ticket_priority"].ToString();
                            ticket_type = reader["ticket_type"].ToString();
                            ticket_called_in = reader["ticket_called_in"].ToString();
                            ticket_call_type = reader["ticket_call_type"].ToString();
                            ticket_issue = reader["ticket_issue"].ToString();
                            ticket_created_by_date = Convert.ToDateTime(reader["ticket_created_by_date"]);
                            ticket_call_num = reader["ticket_call_num"].ToString();
                            ticket_lineman = reader["ticket_lineman"].ToString();
                            ticket_assigned_to = reader["ticket_assigned_to"].ToString();
                            user_lastname = reader["user_lastname"].ToString();
                            user_firstname = reader["user_firstname"].ToString();
                            user_middlename = reader["user_middlename"].ToString();

                            cust_lastname = reader["cust_lastname"].ToString();
                            cust_firstname = reader["cust_firstname"].ToString();
                            cust_middlename = reader["cust_middlename"].ToString();
                            add_line = reader["add_line"].ToString();
                            add_barangay = reader["add_barangay"].ToString();
                            add_municipality = reader["add_municipality"].ToString();
                            add_province = reader["add_province"].ToString();

                            userTicketList ticketList = new userTicketList();
                            ticketList.account_num = account_num;
                            ticketList.ticket_num = ticket_num;
                            ticketList.ticket_priority = ticket_priority;
                            ticketList.ticket_type = ticket_type;
                            ticketList.ticket_called_in = ticket_called_in;
                            ticketList.ticket_call_type = ticket_call_type;
                            ticketList.ticket_issue = ticket_issue;
                            ticketList.ticket_created_by_date = ticket_created_by_date;
                            ticketList.ticket_call_num = ticket_call_num;
                            ticketList.ticket_lineman = ticket_lineman;
                            ticketList.ticket_assigned_to = ticket_assigned_to;
                            ticketList.cust_lastname = cust_lastname;
                            ticketList.cust_firstname = cust_firstname;
                            ticketList.cust_middlename = cust_middlename;
                            ticketList.add_line = add_line;
                            ticketList.add_barangay = add_barangay;
                            ticketList.add_municipality = add_municipality;
                            ticketList.add_province = add_province;
                            if (string.IsNullOrEmpty(user_middlename))
                            {
                                ticketList.createdBy = $"{user_firstname.ToUpper()} {user_lastname.ToUpper()}";
                                ticketList.created_by_name = $"{user_firstname} {user_lastname}";
                            }
                            else
                            {
                                ticketList.createdBy = $"{user_firstname.ToUpper()} {user_middlename.ToUpper()} {user_lastname.ToUpper()}";
                                ticketList.created_by_name = $"{user_firstname} {user_middlename} {user_lastname}";
                            }
                            ticketList.accountNum = $"#{10000 + account_num}";
                            if (string.IsNullOrEmpty(cust_middlename))
                            {
                                ticketList.customerName = $"{cust_firstname.ToUpper()} {cust_lastname.ToUpper()}";
                            }
                            else
                            {
                                ticketList.customerName = $"{cust_firstname.ToUpper()} {cust_middlename.ToUpper()} {cust_lastname.ToUpper()}";
                            }
                            ticketList.ticketNum = $"TK-{30000 + ticket_num}";
                            ticketList.ticketIssue = ticket_issue;
                            ticketList.createdByDate = ticket_created_by_date.ToString("MMMM dd, yyyy HH:mm:ss");
                            ticketList.ticketType = ticket_type.ToUpper();
                            ticketList.ticketPriority = $"Priority {ticket_priority}";
                            ticketList.OnUserTicketListClick += UserTicketList_Click;
                            flowTicketPanel.Controls.Add(ticketList);

                            count += 1;
                            lblCount.Text = count.ToString();
                        }
                        reader.Close();
                    }
                    else
                    {
                        lblCount.Text = "";
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error has occured.", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                connection.Close();
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnCreate_Click(object sender, EventArgs e)
        {
            frmTicketForm ticketForm = new frmTicketForm();
            ticketForm.parent_form = this;
            ticketForm.account_num = account_num;
            ticketForm.is_new = true;
            ticketForm.ShowDialog();
        }

        private void Ticket_Click()
        {
            frmTicketForm ticketForm = new frmTicketForm();
            ticketForm.parent_form = this;
            ticketForm.account_num = account_num;
            ticketForm.ticket_num = ticket_num;
            ticketForm.ticket_priority = ticket_priority;
            ticketForm.ticket_type = ticket_type;
            ticketForm.ticket_called_in = ticket_called_in;
            ticketForm.ticket_call_type = ticket_call_type;
            ticketForm.ticket_issue = ticket_issue;
            ticketForm.ticket_created_by_date = ticket_created_by_date;
            ticketForm.ticket_call_num = ticket_call_num;
            ticketForm.ticket_lineman = ticket_lineman;
            ticketForm.ticket_assigned_to = ticket_assigned_to;
            ticketForm.cust_lastname = cust_lastname;
            ticketForm.cust_firstname = cust_firstname;
            ticketForm.cust_middlename = cust_middlename;
            ticketForm.add_line = add_line;
            ticketForm.add_barangay = add_barangay;
            ticketForm.add_municipality = add_municipality;
            ticketForm.add_province = add_province;
            ticketForm.created_by_name = created_by_name;
            ticketForm.ShowDialog();
        }

        private void UserTicketList_Click(object sender, EventArgs e)
        {
            userTicketList UserTicketList = sender as userTicketList;
            if (UserTicketList != null)
            {
                account_num = UserTicketList.account_num;
                ticket_num = UserTicketList.ticket_num;
                ticket_priority = UserTicketList.ticket_priority;
                ticket_type = UserTicketList.ticket_type;
                ticket_called_in = UserTicketList.ticket_called_in;
                ticket_call_type = UserTicketList.ticket_call_type;
                ticket_issue = UserTicketList.ticket_issue;
                ticket_created_by_date = UserTicketList.ticket_created_by_date;
                ticket_call_num = UserTicketList.ticket_call_num;
                ticket_lineman = UserTicketList.ticket_lineman;
                ticket_assigned_to = UserTicketList.ticket_assigned_to;
                cust_lastname = UserTicketList.cust_lastname;
                cust_firstname = UserTicketList.cust_firstname;
                cust_middlename = UserTicketList.cust_middlename;
                add_line = UserTicketList.add_line;
                add_barangay = UserTicketList.add_barangay;
                add_municipality = UserTicketList.add_municipality;
                add_province = UserTicketList.add_province;
                created_by_name = UserTicketList.created_by_name;

                Ticket_Click();
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            if (btnClose.Text == "EDIT CLOSED TICKET")
            {
                btnClose.Text = "CANCEL";
                btnClose.BackColor = Color.Maroon;

                ticket_type_ = "Closed";
                loadCustTicket();
            }
            else if (btnClose.Text == "CANCEL")
            {
                btnClose.Text = "EDIT CLOSED TICKET";
                btnClose.BackColor = Color.FromArgb(17, 7, 100);

                ticket_type_ = "Open";
                loadCustTicket();
            }
        }
    }
}
