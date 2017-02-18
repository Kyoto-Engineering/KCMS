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
using ClientManagementSystem.LoginUI;

namespace ClientManagementSystem.UI
{
    public partial class NatureOfClient : Form
    {
        private SqlConnection con;
        private SqlCommand cmd;
        private SqlDataReader rdr;
        ConnectionString cs = new ConnectionString();
        public string userId;
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
                string cb = "insert into NatureOfClients(ClientNature,CreatedByUId,CreatedDTime) VALUES (@d1)";
                cmd = new SqlCommand(cb, con);
                cmd.Parameters.AddWithValue("@d1", txtNatureOfClient.Text);
                cmd.Parameters.AddWithValue("@d1", userId);
                cmd.Parameters.AddWithValue("@d1", DateTime.UtcNow.ToLocalTime());
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
        public void LoadNatureOfClientGrid()
        {
            try
            {
                con = new SqlConnection(cs.DBConn);
                con.Open();
                cmd = new SqlCommand("SELECT RTRIM(NatureOfClients.ClientNature) from NatureOfClients order by NatureOfClients.NatureOfClientId desc", con);
                rdr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                dataGridView1.Rows.Clear();
                while (rdr.Read() == true)
                {
                    dataGridView1.Rows.Add(rdr[0]);
                }
                con.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void NatureOfClient_Load(object sender, EventArgs e)
        {
            LoadNatureOfClientGrid();
            userId = LoginForm.uId.ToString();
        }
    }
}
