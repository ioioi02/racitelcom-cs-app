using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RACITELCOM_CS_APP
{
    public partial class userTicketList : UserControl
    {
        public event EventHandler OnUserTicketListClick;

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

        public string ticketNum
        {
            get { return lblTicketNum.Text; }
            set { lblTicketNum.Text = $"Ticket Number: {value}"; }
        }

        public string accountNum
        {
            get { return lblAccountNum.Text; }
            set { lblAccountNum.Text = $"Account Number: {value}"; }
        }

        public string customerName
        {
            get { return lblCustomerName.Text; }
            set { lblCustomerName.Text = $"Customer Name: {value}"; }
        }

        public string ticketIssue
        {
            get { return lblIssue.Text; }
            set { lblIssue.Text = $"Issue: {value}"; }
        }

        public string createdBy
        {
            get { return lblCreatedBy.Text; }
            set { lblCreatedBy.Text = $"Created by: {value}"; }
        }

        public string createdByDate
        {
            get { return lblCreatedByDate.Text; }
            set { lblCreatedByDate.Text = value; }
        }

        public string ticketType
        {
            get { return lblType.Text; }
            set
            {
                lblType.Text = value;
                if (value == "OPEN")
                {
                    lblType.ForeColor = Color.Lime;
                }
                if (value == "CLOSED")
                {
                    lblType.ForeColor = Color.Maroon;
                }
                lblType.Text = value;
            }
        }

        public string ticketPriority
        {
            get { return lblPriority.Text; }
            set
            {
                lblPriority.Text = value;
                if (value == "Priority Low")
                {
                    lblPriority.ForeColor = Color.FromArgb(192, 192, 0);
                }
                if (value == "Priority High")
                {
                    lblPriority.ForeColor = Color.Maroon;
                }
            }
        }

        public userTicketList()
        {
            InitializeComponent();
            AttachClickEvent(this);
        }

        private void AttachClickEvent(Control control)
        {
            control.DoubleClick += UserTicketList_Click;
            foreach (Control childControl in control.Controls)
            {
                AttachClickEvent(childControl);
            }
        }

        private void UserTicketList_Click(object sender, EventArgs e)
        {
            OnUserTicketListClick?.Invoke(this, EventArgs.Empty);
        }
    }
}
