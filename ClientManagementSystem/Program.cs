using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using ClientManagementSystem.LoginUI;
using ClientManagementSystem.UI;

namespace ClientManagementSystem
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
         //Application.Run(new MainUI());
         Application.Run(new LoginForm());
           //Application.Run(new MasterPageOnlyforClient());
        // Application.Run(new Form13());
         // Application.Run(new MailSendingOption());
         //Application.Run(new InqueiryClientFeedbackDairy());
        }
    }
}
