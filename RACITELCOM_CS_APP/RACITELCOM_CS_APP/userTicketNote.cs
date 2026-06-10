using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace RACITELCOM_CS_APP
{
    public partial class userTicketNote : UserControl
    {
        public DateTime? dateTime = null;

        public userTicketNote()
        {
            InitializeComponent();
        }

        public bool checkTxtNote()
        {
            if (string.IsNullOrEmpty(txtNote.Text))
            {
                return true;
            }
            return false;
        }

        public void loadNote(string author, string date, string note)
        {
            lblAuthor.Text = author;
            lblDate.Text = date;
            txtNote.Text = note;
            txtNote.ReadOnly = true;
        }

        public void savedNote(string user_lastname, string user_firstname, string ticket_lineman)
        {
            lblAuthor.Text = $"{user_firstname.ToUpper()} {user_lastname.ToUpper()}";
            dateTime = DateTime.Now;
            lblDate.Text = dateTime?.ToString("MM/dd/yyyy HH:mm:ss");
            if (!string.IsNullOrEmpty(ticket_lineman))
            {
                txtNote.Text = $"({ticket_lineman.ToUpper()}) {txtNote.Text}";
            }
            txtNote.ReadOnly = true;
        }

        public void savedNoteToDatabase(int ticket_num)
        {
            MySqlConnection connection = MyConnectionString.mysql_connection();
            try
            {
                connection.Open();

                string query = $@"INSERT INTO tbl_ticket_note(ticket_num, note_created_by, note_created_by_date, note_text)
                                 VALUES(@ticket_num, @note_created_by, @note_created_by_date, @note_text);";
                using (MySqlCommand cmd = new MySqlCommand(query, connection))
                {
                    cmd.Parameters.AddWithValue("@ticket_num", ticket_num);
                    cmd.Parameters.AddWithValue("@note_created_by", lblAuthor.Text);
                    cmd.Parameters.AddWithValue("@note_created_by_date", dateTime);
                    cmd.Parameters.AddWithValue("@note_text", txtNote.Text);

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
