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
    public partial class NatureOfClient : Form
    {
        private SqlConnection con;
        private SqlCommand cmd;
        private SqlDataReader rdr;
        ConnectionString cs = new ConnectionString();
        public NatureOfClient()
        {
            InitializeComponent();
        }

        private void saveButton_Click(object sender, EventArgs e)
        {
            if (txtNatureOfClient.Text == "")
            {
                MessageBox.Show("Please enter Client Nature", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtNatureOfClient.Focus();
                return;
            }


            try
            {
                con = new SqlConnection(cs.DBConn);
                con.Open();
                string ct = "select ClientNature from NatureOfClients where ClientNature='" + txtNatureOfClient.Text + "'";

                cmd = new SqlCommand(ct);
                cmd.Connection = con;
                rdr = cmd.ExecuteReader();

                if (rdr.Read())
                {
                    MessageBox.Show("NatureOfClients  Already Exists", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtNatureOfClient.Text = "";
                    txtNatureOfClient.Focus();


                    if ((rdr != null))
                    {
                        rdr.Close();
                    }
                    return;
                }
                con = new SqlConnection(cs.DBConn);
                con.Open();
                string cb = "insert into NatureOfClients(ClientNature) VALUES (@d1)";
                cmd = new SqlCommand(cb, con);
                cmd.Parameters.AddWithValue("@d1", txtNatureOfClient.Text);
                cmd.ExecuteReader();
                con.Close();
                MessageBox.Show("Successfully saved", "Record", MessageBoxButtons.OK, MessageBoxIcon.Information);
                // Autocomplete();
                txtNatureOfClient.Clear();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void NatureOfClient_Load(object sender, EventArgs e)
        {

        }
    }
}
