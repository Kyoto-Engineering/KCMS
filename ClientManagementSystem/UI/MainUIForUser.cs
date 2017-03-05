using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ClientManagementSystem.LoginUI;

namespace ClientManagementSystem.UI
{
    public partial class MainUIForUser : Form
    {
        public MainUIForUser()
        {
            InitializeComponent();
        }

        private void logOutButton_Click(object sender, EventArgs e)
        {
            this.Dispose();
            LoginForm f3 = new LoginForm();
            f3.Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Hide();
            MainUIInquieryClient frm = new MainUIInquieryClient();
            frm.Show();
        }

        private void feedbackButton_Click(object sender, EventArgs e)
        {
            this.Visible = false;
            dynamic f2 = new FeedBack();
            f2.ShowDialog();
            this.Visible = true;      
        }

        private void buttonClientType_Click(object sender, EventArgs e)
        {
            this.Visible = false;
            dynamic fg4 = new frmClientType();
            fg4.ShowDialog();
            this.Visible = true;
        }

        private void emailBankButton_Click(object sender, EventArgs e)
        {
            this.Hide();
            EmailBank frm = new EmailBank();
            frm.Show();
        }

        private void userButton_Click(object sender, EventArgs e)
        {
           this.Hide();
            UserManagementUI frm=new UserManagementUI();
            frm.Show();
        }

        private void buttonSalesClient_Click(object sender, EventArgs e)
        {
            this.Hide();
            ForSalseClientMP frm = new ForSalseClientMP();
            frm.Show();
        }
    }
}
