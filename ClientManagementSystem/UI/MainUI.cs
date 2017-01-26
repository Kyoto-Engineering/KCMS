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
    public partial class MainUI : Form
    {
        public static  int uId2 ;
        public string TheValue
        {
            get { return forUserTextBox.Text; }
        }
        public MainUI()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Hide();
            ForSalseClientMP frm = new ForSalseClientMP();
            frm.Show();
           // dynamic sg = new ForSalseClientMP();
            //this.Visible = false;
            //dynamic sg = new ForSalseClientMP();
            //sg.ShowDialog();
            //this.Visible = true; 
        }

        private void button2_Click(object sender, EventArgs e)
        {
            //this.Visible = false;
            //dynamic ww = new MainUIInquieryClient();
            //ww.ShowDialog();
            //this.Visible = true;

            this.Hide();
            MainUIInquieryClient frm = new MainUIInquieryClient();
            frm.Show();
                        
           
        }

        private void userButton_Click(object sender, EventArgs e)
        {
            this.Visible = false;
            UserManagementUI ug=new UserManagementUI();
            ug.ShowDialog();
            this.Visible = true;
        }

        private void instantClientButton_Click(object sender, EventArgs e)
        {
            
            this.Visible = false;            
            dynamic f2 = new FeedBack();
            f2.ShowDialog();
            this.Visible = true;            
        }

        private void logOutButton_Click(object sender, EventArgs e)
        {
            LoginForm f3 = new LoginForm();
            //if (MessageBox.Show("Are you sure you want to exit?", "Confirm exit", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
            //{
            //    e.Cancel = true;
            //}
            //else  { 
                this.Dispose();
         //   LoginForm f3=new LoginForm();
            f3.Show();
            //}
        }

        private void aboutButton_Click(object sender, EventArgs e)
        {
            frmAbout fg = new frmAbout();
            fg.Show();
        }

        private void MainUI_Load(object sender, EventArgs e)
        {
            textBox3.Text = LoginForm.uId.ToString();
        }

        private void followUpButton_Click(object sender, EventArgs e)
        {
            dynamic ww = new ActionFollowUpProceedForm();
            this.Visible = false;
            ww.ShowDialog();
            this.Visible = true;
        }

        private void feedBackButton2_Click(object sender, EventArgs e)
        {
           
        }

        private void btnIndustryCategory_Click(object sender, EventArgs e)
        {
            this.Visible = false;
            dynamic fg4 = new frmIndustryCategory();
            fg4.ShowDialog();
            this.Visible = true;

        }

        private void buttonClientType_Click(object sender, EventArgs e)
        {
            this.Visible = false;
            dynamic fg4 = new frmClientType();
            fg4.ShowDialog();
            this.Visible = true;

        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.Visible = false;
            dynamic fg4 = new frmAddressType();
            fg4.ShowDialog();
            this.Visible = true;
        }

        private void buttonNatureOfClient_Click(object sender, EventArgs e)
        {
            this.Visible = false;
            dynamic fg4 = new NatureOfClient();
            fg4.ShowDialog();
            this.Visible = true;
        }
    }
}
