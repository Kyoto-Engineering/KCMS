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
    public partial class frmIndustryCategory : Form
    {
        private SqlConnection con;
        private SqlCommand cmd;
        private SqlDataReader rdr;
        ConnectionString cs=new ConnectionString();
        public frmIndustryCategory()
        {
            InitializeComponent();
        }
        private void Autocomplete()
        {
            try
            {
                con = new SqlConnection(cs.DBConn);
                con.Open();
                SqlCommand cmd = new SqlCommand("SELECT distinct IndustryCategory FROM IndustryCategorys", con);
                DataSet ds = new DataSet();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(ds, "IndustryCategorys");
                AutoCompleteStringCollection col = new AutoCompleteStringCollection();
                int i = 0;
                for (i = 0; i <= ds.Tables[0].Rows.Count - 1; i++)
                {
                    col.Add(ds.Tables[0].Rows[i]["IndustryCategory"].ToString());

                }
                txtIndustryCategory.AutoCompleteSource = AutoCompleteSource.CustomSource;
                txtIndustryCategory.AutoCompleteCustomSource = col;
                txtIndustryCategory.AutoCompleteMode = AutoCompleteMode.Suggest;

                con.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void saveButton_Click(object sender, EventArgs e)
        {
            if (txtIndustryCategory.Text == "")
            {
                MessageBox.Show("Please enter Category name", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtIndustryCategory.Focus();
                return;
            }


            try
            {

                con = new SqlConnection(cs.DBConn);
                con.Open();
                string ct = "select IndustryCategory from IndustryCategorys where IndustryCategory='" + txtIndustryCategory.Text + "'";

                cmd = new SqlCommand(ct);
                cmd.Connection = con;
                rdr = cmd.ExecuteReader();

                if (rdr.Read())
                {
                    MessageBox.Show("Category Name Already Exists", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtIndustryCategory.Text = "";
                    txtIndustryCategory.Focus();


                    if ((rdr != null))
                    {
                        rdr.Close();
                    }
                    return;
                }



                con = new SqlConnection(cs.DBConn);
                con.Open();

                string cb = "insert into IndustryCategorys(IndustryCategory) VALUES (@d1)";

                cmd = new SqlCommand(cb,con);
                cmd.Parameters.AddWithValue("@d1", txtIndustryCategory.Text);
                cmd.ExecuteReader();
                con.Close();
                MessageBox.Show("Successfully saved", "Record", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Autocomplete();
                txtIndustryCategory.Clear();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
