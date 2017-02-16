using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ClientManagementSystem.DBGateway;
using ClientManagementSystem.UI;

namespace ClientManagementSystem.LoginUI
{
    public partial class UserManagementUI : Form
    {
        private SqlConnection con;
        private SqlCommand cmd;
        private SqlDataAdapter sda;
        private SqlDataReader rdr;
        public string userTypeU;
        
        public UserManagementUI()
        {
            InitializeComponent();
        }

        private void buttonCreateNewUser_Click(object sender, EventArgs e)
        {
            if (userTypeU == "Admin")
            {
                this.Visible = false;
                dynamic frm = new UserRegistration();
                frm.ShowDialog();
                this.Visible = true;
            }
            if (userTypeU == "User")
            {
                this.Visible = false;
                dynamic frm = new UserCreationByuser();
                frm.ShowDialog();
                this.Visible = true;
            }

        }

        private void buttonResetPassword_Click(object sender, EventArgs e)
        {
            this.Visible = false;
            dynamic frm = new ResetPassword();
            frm.ShowDialog();
            this.Visible = true;
        }

        private void buttonChangePassword_Click(object sender, EventArgs e)
        {
            this.Visible = false;
            dynamic frm = new ChangePassword();
            frm.ShowDialog();
            this.Visible = true;
        }

        private void changeStatusButton_Click(object sender, EventArgs e)
        {
            this.Visible = false;
            dynamic frm = new ChangeStatus();
            frm.ShowDialog();
            this.Visible = true;
        }

        private void UserManagementUI_Load(object sender, EventArgs e)
        {
            userTypeU = LoginForm.userType;
            if (userTypeU == "User")
            {
                buttonResetPassword.Visible = false;
                changeStatusButton.Visible = false;
            }
        }

        private void UserManagementUI_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (userTypeU == "Admin")
            {
                this.Hide();
                MainUI frm = new MainUI();
                frm.Show();
            }
            else if (userTypeU == "User")
            {
                this.Hide();
                MainUIForUser frm=new MainUIForUser();
                frm.Show();
            }
        }

        private void updateUserButton_Click(object sender, EventArgs e)
        {
            if (userTypeU == "Admin")
            {
                this.Hide();
                UpdateUserForAdmin frm = new UpdateUserForAdmin();
                frm.Show();
            }
            else if (userTypeU == "User")
            {
                this.Hide();
                UpdateUserInfo frm = new UpdateUserInfo();
                frm.Show();
            }
        }
    }
}
