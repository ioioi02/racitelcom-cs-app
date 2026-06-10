using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RACITELCOM_CS_APP
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            // Application.Run(new frmLogin());

            frmLogin login = new frmLogin();
            login.Show();

            // frmAdmin admin = new frmAdmin();
            // admin.Show();

            Application.Run();
        }
    }
}
