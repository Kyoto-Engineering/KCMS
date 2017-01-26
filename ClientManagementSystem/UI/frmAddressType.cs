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

namespace ClientManagementSystem.UI
{
    public partial class frmAddressType : Form
    {
        private SqlConnection con;
        private SqlCommand cmd;
        private SqlDataReader rdr;
        ConnectionString cs=new ConnectionString();
        public frmAddressType()
        {
            InitializeComponent();
        }

        private void saveButton_Click(object sender, EventArgs e)
        {
            if (txtAddressType.Text == "")
            {
                MessageBox.Show("Please enter Address Type", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtAddressType.Focus();
                return;
            }


            try
            {

                con = new SqlConnection(cs.DBConn);
                con.Open();
                string ct = "select AdressType from AddressTypes where AdressType='" + txtAddressType.Text + "'";

                cmd = new SqlCommand(ct);
                cmd.Connection = con;
                rdr = cmd.ExecuteReader();

                if (rdr.Read())
                {
                    MessageBox.Show(" This AddressType  Already Exists", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtAddressType.Text = "";
                    txtAddressType.Focus();


                    if ((rdr != null))
                    {
                        rdr.Close();
                    }
                    return;
                }



                con = new SqlConnection(cs.DBConn);
                con.Open();

                string cb = "insert into AddressTypes(AdressType) VALUES (@d1)";

                cmd = new SqlCommand(cb, con);
                cmd.Parameters.AddWithValue("@d1", txtAddressType.Text);
                cmd.ExecuteReader();
                con.Close();
                MessageBox.Show("Successfully saved", "Record", MessageBoxButtons.OK, MessageBoxIcon.Information);               
                txtAddressType.Clear();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
