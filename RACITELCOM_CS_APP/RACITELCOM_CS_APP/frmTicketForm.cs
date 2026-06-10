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
    public partial class frmTicketForm : Form
    {
        public int? account_num { get; set; }
        public int ticket_num { get; set; }
        public int next_ticket_num { get; set; }
        public string user_name { get; set; }
        public string created_by_name { get; set; }
        public bool is_new { get; set; }

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

        // username information to store
        public string user_lastname { get; set; }
        public string user_firstname { get; set; }
        public string user_middlename { get; set; }

        public Form parent_form { get; set; }

        userTicketNote ticketNote;

        private List<userTicketNote> userTicketNoteList = new List<userTicketNote>();
        private DateTime? dateTime = null;
        private bool unfinishedNote = false;
        private string ticket_lineman_ = null;

        public frmTicketForm()
        {
            InitializeComponent();
        }

        private void frmTicketForm_Load(object sender, EventArgs e)
        {
            // combolist of PRIORITY
            comboPriority.Items.AddRange(new string[] { "Low", "High" });
            comboPriority.SelectedItem = "Low";

            // combolist of TICKET TYPE
            comboType.Items.AddRange(new string[] { "Open", "Closed" });
            comboType.SelectedItem = "Open";

            // combolist of CALLED IN
            comboCalledIn.Items.AddRange(new string[] { "Phone Smart", "Phone Globe", "Telephone 1", "Telephone 2", "Messenger", "Telegram" });
            comboCalledIn.SelectedItem = "Phone Smart";

            // combolist of CALL TYPE
            comboCallType.Items.AddRange(new string[] { "Call", "Message" });
            comboCallType.SelectedItem = "Call";

            // combolist of ASSIGNED TO
            comboAssignedTo.Items.AddRange(new string[] { "Technician", "Lineman" });
            comboAssignedTo.SelectedItem = "Technician";

            // get the username
            user_name = TCPClient.Instance.username;
            loadUserFullName();

            if (is_new == true)
            {
                loadDatabase();
            }
            else
            {
                loadTicket();
                loadTicketNote();
            }
        }

        private void loadUserFullName()
        {
            MySqlConnection connection = MyConnectionString.mysql_connection();
            try
            {
                connection.Open();

                // query for retrieving username information
                string query = $@"SELECT 
                                        user_lastname, 
                                        user_firstname, 
                                        user_middlename
                                      FROM
                                        tbl_user
                                      WHERE 
                                        user_name = @user_name;";
                MySqlCommand cmd = new MySqlCommand(query, connection);
                cmd.Parameters.AddWithValue("@user_name", user_name);
                using (MySqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        user_lastname = reader["user_lastname"].ToString();
                        user_firstname = reader["user_firstname"].ToString();
                        user_middlename = reader["user_middlename"].ToString();
                    }
                    reader.Close();
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

        private void loadDatabase()
        {
            MySqlConnection connection = MyConnectionString.mysql_connection();
            try
            {
                connection.Open();

                // query1 for retrieving customer information
                string query1 = $@"SELECT 
                                    customer.cust_lastname, 
                                    customer.cust_firstname, 
                                    customer.cust_middlename, 
                                    address.add_line, 
                                    address.add_barangay, 
                                    address.add_municipality, 
                                    address.add_province
                                  FROM
                                    tbl_account as account
                                    INNER JOIN tbl_address as address ON account.add_id = address.add_id
                                    INNER JOIN tbl_customer as customer ON address.cust_id = customer.cust_id
                                  WHERE 
                                    account.account_num = @account_num;";
                MySqlCommand cmd1 = new MySqlCommand(query1, connection);
                cmd1.Parameters.AddWithValue("@account_num", account_num);
                using (MySqlDataReader reader1 = cmd1.ExecuteReader())
                {
                    while (reader1.Read())
                    {
                        cust_lastname = reader1["cust_lastname"].ToString();
                        cust_firstname = reader1["cust_firstname"].ToString();
                        cust_middlename = reader1["cust_middlename"].ToString();
                        add_line = reader1["add_line"].ToString();
                        add_barangay = reader1["add_barangay"].ToString();
                        add_municipality = reader1["add_municipality"].ToString();
                        add_province = reader1["add_province"].ToString();
                    }
                    reader1.Close();

                    // get the next available ticket number
                    string query3 = @"SELECT AUTO_INCREMENT
                                        FROM information_schema.TABLES
                                        WHERE TABLE_SCHEMA = @databaseName
                                        AND TABLE_NAME = @tableName;";
                    MySqlCommand cmd3 = new MySqlCommand(query3, connection);
                    cmd3.Parameters.AddWithValue("@databaseName", "db_cs_app");
                    cmd3.Parameters.AddWithValue("@tableName", "tbl_ticket");
                    next_ticket_num = Convert.ToInt32(cmd3.ExecuteScalar());

                    // fill-up the coresponding textbox
                    txtAccountNumber.Text = $"#{10000 + account_num}";
                    if (string.IsNullOrEmpty(cust_middlename))
                    {
                        txtCustomerName.Text = $"{cust_firstname.ToUpper()} {cust_lastname.ToUpper()}";
                    }
                    else
                    {
                        txtCustomerName.Text = $"{cust_firstname.ToUpper()} {cust_middlename.ToUpper()} {cust_lastname.ToUpper()}";
                    }
                    txtLocation.Text = $"{add_line.ToUpper()} {add_barangay.ToUpper()}, {add_municipality.ToUpper()}, {add_province.ToUpper()}";
                    if (string.IsNullOrEmpty(user_middlename))
                    {
                        txtCreatedBy.Text = $"{user_firstname.ToUpper()} {user_lastname.ToUpper()}";
                    }
                    else
                    {
                        txtCreatedBy.Text = $"{user_firstname.ToUpper()} {user_middlename.ToUpper()} {user_lastname.ToUpper()}";
                    }
                    txtTicketNum.Text = $"TK-{30000 + next_ticket_num}";
                    dateTime = DateTime.Now;
                    txtDate.Text = dateTime?.ToString("MMMM dd, yyyy HH:mm:ss");
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

        private void createTicket()
        {
            MySqlConnection connection = MyConnectionString.mysql_connection();
            try
            {
                connection.Open();

                string query = $@"INSERT INTO tbl_ticket(account_num, ticket_priority, ticket_type, ticket_called_in, ticket_call_type, ticket_issue, ticket_created_by, ticket_created_by_date, ticket_call_num, ticket_lineman, ticket_assigned_to)
                                VALUES(@account_num, @ticket_priority, @ticket_type, @ticket_called_in, @ticket_call_type, @ticket_issue, @ticket_created_by, @ticket_created_by_date, @ticket_call_num, @ticket_lineman, @ticket_assigned_to);
                                SELECT LAST_INSERT_ID();";
                using (MySqlCommand cmd = new MySqlCommand(query, connection))
                {
                    var ticket_priority = comboPriority.SelectedItem;
                    var ticket_type = comboType.SelectedItem;
                    var ticket_called_in = comboCalledIn.SelectedItem;
                    var ticket_call_type = comboCallType.SelectedItem;
                    var ticket_assigned_to = comboAssignedTo.SelectedItem;

                    cmd.Parameters.AddWithValue("@account_num", account_num);
                    cmd.Parameters.AddWithValue("@ticket_priority", ticket_priority.ToString());
                    cmd.Parameters.AddWithValue("@ticket_type", ticket_type.ToString());
                    cmd.Parameters.AddWithValue("@ticket_called_in", ticket_called_in.ToString());
                    cmd.Parameters.AddWithValue("@ticket_call_type", ticket_call_type.ToString());
                    cmd.Parameters.AddWithValue("@ticket_issue", txtIssue.Text);
                    cmd.Parameters.AddWithValue("@ticket_created_by", user_name);
                    cmd.Parameters.AddWithValue("@ticket_created_by_date", dateTime);
                    cmd.Parameters.AddWithValue("@ticket_call_num", txtCallNumber.Text);
                    cmd.Parameters.AddWithValue("@ticket_lineman", ticket_lineman);
                    cmd.Parameters.AddWithValue("@ticket_assigned_to", ticket_assigned_to);

                    ticket_num = Convert.ToInt32(cmd.ExecuteScalar());
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

        private void loadTicket()
        {
            // fill-up the coresponding textbox
            txtAccountNumber.Text = $"#{10000 + account_num}";
            if (string.IsNullOrEmpty(cust_middlename))
            {
                txtCustomerName.Text = $"{cust_firstname.ToUpper()} {cust_lastname.ToUpper()}";
            }
            else
            {
                txtCustomerName.Text = $"{cust_firstname.ToUpper()} {cust_middlename.ToUpper()} {cust_lastname.ToUpper()}";
            }
            txtLocation.Text = $"{add_line.ToUpper()} {add_barangay.ToUpper()}, {add_municipality.ToUpper()}, {add_province.ToUpper()}";
            txtCreatedBy.Text = created_by_name.ToUpper();
            txtTicketNum.Text = $"TK-{30000 + ticket_num}";
            txtDate.Text = ticket_created_by_date.ToString("MMMM dd, yyyy HH:mm:ss");
            txtCallNumber.Text = ticket_call_num;
            comboPriority.SelectedItem = ticket_priority;
            comboType.SelectedItem = ticket_type;
            comboCalledIn.SelectedItem = ticket_called_in;
            comboCallType.SelectedItem = ticket_call_type;
            comboAssignedTo.SelectedItem = ticket_assigned_to;
            txtIssue.Text = ticket_issue;

            // read-only and disabled
            txtCallNumber.ReadOnly = true;
            txtIssue.ReadOnly = true;
            comboCalledIn.Enabled = false;
            comboCallType.Enabled = false;
        }

        private void loadTicketNote()
        {
            MySqlConnection connection = MyConnectionString.mysql_connection();
            try
            {
                connection.Open();

                string query = $@"SELECT * FROM tbl_ticket_note WHERE ticket_num = @ticket_num ORDER BY note_created_by_date DESC;";
                MySqlCommand cmd = new MySqlCommand(query, connection);
                cmd.Parameters.AddWithValue("@ticket_num", ticket_num);
                using (MySqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        string note_created_by = reader["note_created_by"].ToString();
                        DateTime note_created_by_date = Convert.ToDateTime(reader["note_created_by_date"]);
                        string note_date = note_created_by_date.ToString("MM/dd/yyyy HH:mm:ss");
                        string note_text = reader["note_text"].ToString();

                        ticketNote = new userTicketNote();
                        ticketNote.Height = ticketNote.ClientSize.Height;
                        ticketNote.loadNote(note_created_by, note_date, note_text);
                        flowNotePanel.Controls.Add(ticketNote);
                    }
                    reader.Close();
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

        private void updateTicket()
        {
            MySqlConnection connection = MyConnectionString.mysql_connection();
            try
            {
                connection.Open();

                string query = $@"UPDATE tbl_ticket SET ticket_priority = @ticket_priority, ticket_type = @ticket_type, ticket_assigned_to = @ticket_assigned_to WHERE ticket_num = @ticket_num;";
                using (MySqlCommand cmd = new MySqlCommand(query, connection))
                {
                    var ticket_priority = comboPriority.SelectedItem;
                    var ticket_type = comboType.SelectedItem;
                    var ticket_assigned_to = comboAssignedTo.SelectedItem;

                    cmd.Parameters.AddWithValue("@ticket_priority", ticket_priority);
                    cmd.Parameters.AddWithValue("@ticket_type", ticket_type);
                    cmd.Parameters.AddWithValue("@ticket_num", ticket_num);
                    cmd.Parameters.AddWithValue("@ticket_assigned_to", ticket_assigned_to);

                    cmd.ExecuteNonQuery();
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
            DialogResult dialogResult = MessageBox.Show("Do you want to save the ticket before exiting?", "", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation);
            if (dialogResult == DialogResult.Yes)
            {
                btnSave_Click(null, null);
            }
            else
            {
                this.Close();
            }
        }

        private void btnAddNote_Click(object sender, EventArgs e)
        {
            // add new note
            unfinishedNote = true;

            ticketNote = new userTicketNote();
            ticketNote.Height = ticketNote.ClientSize.Height;

            var savedControls = flowNotePanel.Controls.Cast<Control>().ToList();
            flowNotePanel.Controls.Clear();
            flowNotePanel.Controls.Add(ticketNote);
            foreach (var control in savedControls)
            {
                flowNotePanel.Controls.Add(control);
            }

            btnAddNote.Enabled = false;
            btnSaveNote.Enabled = true;
            btnCancelNote.Enabled = true;
        }

        private void btnSaveNote_Click(object sender, EventArgs e)
        {
            unfinishedNote = false;
            bool isTxtNoteEmpty = ticketNote.checkTxtNote();
            if (isTxtNoteEmpty)
            {
                MessageBox.Show("You can't save with an empty note.", "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            if (comboAssignedTo.SelectedItem.ToString() == "Lineman")
            {
                frmLinemanTicket linemanTicket = new frmLinemanTicket();
                linemanTicket.ticket_lineman = ticket_lineman;
                linemanTicket.ticket_form = this;
                ticket_lineman_ = ticket_lineman;
                DialogResult linemanResult = linemanTicket.ShowDialog();
                if (linemanResult == DialogResult.Cancel)
                {
                    return;
                }
                else
                {
                    if (ticket_lineman_ != ticket_lineman)
                    {
                        MySqlConnection connection = MyConnectionString.mysql_connection();
                        try
                        {
                            connection.Open();

                            string query = $@"UPDATE tbl_ticket SET ticket_lineman = @ticket_lineman WHERE ticket_num = @ticket_num;";
                            using (MySqlCommand cmd = new MySqlCommand(query, connection))
                            {
                                cmd.Parameters.AddWithValue("@ticket_lineman", ticket_lineman.ToUpper());
                                cmd.Parameters.AddWithValue("@ticket_num", ticket_num);

                                cmd.ExecuteNonQuery();
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
                }
            }

            ticketNote.savedNote(user_lastname, user_firstname, ticket_lineman);
            userTicketNoteList.Add(ticketNote);

            btnAddNote.Enabled = true;
            btnSaveNote.Enabled = false;
            btnCancelNote.Enabled = false;
        }

        private void btnCancelNote_Click(object sender, EventArgs e)
        {
            Control firstTicketNote = flowNotePanel.Controls[0];
            if (firstTicketNote is userTicketNote)
            {
                flowNotePanel.Controls.Remove(firstTicketNote);
                firstTicketNote.Dispose();
            }

            btnAddNote.Enabled = true;
            btnSaveNote.Enabled = false;
            btnCancelNote.Enabled = false;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtIssue.Text))
            {
                MessageBox.Show("You can't save with an empty issue.", "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            if (comboType.SelectedItem.ToString() == "Closed")
            {
                DialogResult dialogResult = MessageBox.Show("Proceed in saving this as closed ticket?", "", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation);
                if (dialogResult == DialogResult.No)
                {
                    return;
                }
            }

            if (unfinishedNote == true)
            {
                DialogResult dialogResult = MessageBox.Show("You have an unfinished note, do you to save the ticket without saving the unfinished note?", "", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation);
                if (dialogResult == DialogResult.No)
                {
                    return;
                }
            }

            if (is_new == true)
            {
                createTicket();
            }
            else
            {
                updateTicket();
            }

            foreach (Control control in userTicketNoteList)
            {
                if (control is userTicketNote)
                {
                    userTicketNote noteControl = (userTicketNote)control;
                    noteControl.savedNoteToDatabase(ticket_num);
                }
            }

            if (parent_form is frmCustTicket)
            {
                parent_form.Close();
            }
            else
            {
                ((frmPendingTicket)parent_form).frmPendingTicket_Load(null, null);
            }
            this.Close();

            // test only //

            string username = TCPClient.Instance.username;
            string ticketType = comboType.SelectedItem.ToString().ToLower();
            string logType = $"save a {ticketType} ticket with ticket number {txtTicketNum.Text} and account number {txtAccountNumber.Text}.";

            InsertLogs inserLogs = new InsertLogs();
            string log_result = inserLogs.insertLogs(username, logType);
            if (log_result != "0")
            {
                // MessageBox.Show($"Error: {log_result}", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                MessageBox.Show("An error has occured.", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            TCPClient.Instance.SendMessage($"{logType}.");

            // test only //
        }

        private void txtCustomerName_DoubleClick(object sender, EventArgs e)
        {
            MessageBox.Show(txtCustomerName.Text);
        }

        private void txtLocation_DoubleClick(object sender, EventArgs e)
        {
            MessageBox.Show(txtLocation.Text);
        }
    }
}
