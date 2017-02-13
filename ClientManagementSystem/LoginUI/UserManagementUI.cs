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

namespace ClientManagementSystem.LoginUI
{
    public partial class UserManagementUI : Form
    {
        private SqlConnection con;
        private SqlCommand cmd;
        private SqlDataAdapter sda;
        private SqlDataReader rdr;
        
        public UserManagementUI()
        {
            InitializeComponent();
        }

        private void buttonCreateNewUser_Click(object sender, EventArgs e)
        {
            this.Visible = false;
            dynamic frm=new UserRegistration();
            frm.ShowDialog();
            this.Visible = true;

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
    }
}
