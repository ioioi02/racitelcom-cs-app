using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RACITELCOM_CS_APP
{
    public partial class frmLinemanTicket : Form
    {
        public string ticket_lineman { get; set; }
        public Form ticket_form { get; set; }

        public frmLinemanTicket()
        {
            InitializeComponent();
        }

        private void frmLinemanTicket_Load(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(ticket_lineman))
            {
                txtLinemanName.Text = ticket_lineman.ToLower();

                txtLinemanName.SelectionStart = txtLinemanName.Text.Length;
                txtLinemanName.SelectionLength = 0;
            }
        }

        private void btnConfirm_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtLinemanName.Text))
            {
                if (ticket_form is frmTicketForm)
                {
                    ((frmTicketForm)ticket_form).ticket_lineman = txtLinemanName.Text.ToUpper();
                }
                this.DialogResult = DialogResult.OK;
                // this.Dispose();
                this.Close();
            }
        }
    }
}
