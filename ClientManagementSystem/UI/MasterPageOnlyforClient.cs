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
    public partial class MasterPageOnlyforClient : Form
    {
        public MasterPageOnlyforClient()
        {
            InitializeComponent();
        }

        private void btnClient_Click(object sender, EventArgs e)
        {
            dynamic ww = new MainUIInquieryClient();
            this.Visible = false;
            ww.ShowDialog();
            this.Visible = true;
        }

        private void logOutButton_Click(object sender, EventArgs e)
        {
            this.Dispose();
            LoginForm inForm=new LoginForm();
            inForm.Show();
        }

        private void changePasswordButton_Click(object sender, EventArgs e)
        {
            
            ChangePass2 cp = new ChangePass2();
            this.Visible = false;
            cp.ShowDialog();
            this.Visible = true;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            dynamic sg = new ForSalseClientMP();
            this.Visible = false;
            //dynamic sg = new ForSalseClientMP();
            sg.ShowDialog();
            this.Visible = true;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            dynamic f2 = new FeedBack();
            this.Visible = false;
            f2.ShowDialog();
            //MainUI frm=new MainUI();  
            this.Visible = true; 
        }

        private void button3_Click(object sender, EventArgs e)
        {
            frmAbout fg = new frmAbout();
            this.Visible = false;
            fg.ShowDialog();
            this.Visible = true;
        }
        }
    }

