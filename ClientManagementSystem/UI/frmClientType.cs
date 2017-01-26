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
    public partial class frmClientType : Form
    {
        private SqlConnection con;
        private SqlCommand cmd;
        private SqlDataReader rdr;
        ConnectionString cs=new ConnectionString();
        public frmClientType()
        {
            InitializeComponent();
        }

        private void saveButton_Click(object sender, EventArgs e)
        {
            if (txtClientType.Text == "")
            {
                MessageBox.Show("Please enter Client Type", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtClientType.Focus();
                return;
            }


            try
            {

                con = new SqlConnection(cs.DBConn);
                con.Open();
                string ct = "select ClientType from ClientTypes where ClientType='" + txtClientType.Text + "'";

                cmd = new SqlCommand(ct);
                cmd.Connection = con;
                rdr = cmd.ExecuteReader();

                if (rdr.Read())
                {
                    MessageBox.Show("ClientType  Already Exists", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtClientType.Text = "";
                    txtClientType.Focus();


                    if ((rdr != null))
                    {
                        rdr.Close();
                    }
                    return;
                }



                con = new SqlConnection(cs.DBConn);
                con.Open();

                string cb = "insert into ClientTypes(ClientType) VALUES (@d1)";

                cmd = new SqlCommand(cb, con);
                cmd.Parameters.AddWithValue("@d1", txtClientType.Text);
                cmd.ExecuteReader();
                con.Close();
                MessageBox.Show("Successfully saved", "Record", MessageBoxButtons.OK, MessageBoxIcon.Information);
               // Autocomplete();
                txtClientType.Clear();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }
    }
}
