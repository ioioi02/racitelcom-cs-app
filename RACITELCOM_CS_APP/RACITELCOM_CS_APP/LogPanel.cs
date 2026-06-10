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
    public partial class LogPanel : UserControl
    {
        public string logMessage
        {
            get { return lblMessage.Text; }
            set { lblMessage.Text = value; }
        }

        public string dateTime
        {
            get { return lblDateTime.Text; }
            set { lblDateTime.Text = value; }
        }

        public LogPanel()
        {
            InitializeComponent();
        }
    }
}
