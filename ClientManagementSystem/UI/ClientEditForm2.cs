using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ClientManagementSystem.DAO;
using ClientManagementSystem.Gateway;

namespace ClientManagementSystem.UI
{
    public partial class ClientEditForm2 : Form
    {
        private ClientGateway clientGateway;
       
        private InqueryClient client;
        public ClientEditForm2()
        {
            InitializeComponent();
        }

        private void editButton_Click(object sender, EventArgs e)
        {
            clientGateway = new ClientGateway();
            decimal clientId = Convert.ToInt64(clientIdTextBox.Text);
            client = clientGateway.SearchClient2(clientId);
            if (client.IClientId != Convert.ToInt64(clientIdTextBox.Text))
            {
                MessageBox.Show("Client Id is not found.Please enter correct Id");
            }
            else
            {
                clientIdTextBox.Text = Convert.ToString(client.IClientId);
                clientNameEditTextBox.Text = client.ClientName;
                txtClientEditComboBox.Text = client.ClientType;
                txtNatureOfClientEditComboBox.Text = client.NatureOfClient;
                emailAddressEditTextBox.Text = client.EmailAddress;
                txtIndustryCatagoryComboBox.Text = client.IndustryCatagory;

                cFlatNoTextBox.Text = client.CFlatNo;
                cHouseNoTextBox.Text = client.CHouseNo;
                cRoadNoTextBox.Text = client.CRoadNo;
                cBlockTextBox.Text = client.CBlock;
                cAreaTextBox.Text = client.CARea;
                cPostTextBox.Text = client.CPost;
                cPostCodeTextBox.Text = client.CPostCode;
                cDistCombo.Text = client.CDistrict;
                cContactNoTextBox.Text = client.CContactNo;

                tFlatNoTextBox.Text = client.TFlatNo;
                tHouseNoTextBox.Text = client.THouseNo;
                tRoadNoTextBox.Text = client.TRoadNo;
                tBlockTextBox.Text = client.TBlock;
                tAreaTextBox.Text = client.TARea;
                tPostTextBox.Text = client.TPost;
                tPostCodeTextBox.Text = client.TPostCode;
                tDistComboBox.Text = client.TDistrict;
                tContactNoTextBox.Text = client.TContactNo;





                contactPersonNameEditTextBox.Text = client.ContactPersonName;
                designationTextBox.Text = client.Designation;
                cellNumberTextBox.Text = client.CellNumber;
                endUserTextBox.Text = client.EndUser;

            }
        }

        private void updateButton_Click(object sender, EventArgs e)
        {
            ClientGateway aClientGateway = new ClientGateway();
            try
            {
                InqueryClient aClient = new InqueryClient
                {
                    IClientId = Convert.ToInt64(clientIdTextBox.Text),
                    ClientName = clientNameEditTextBox.Text,
                    ClientType = txtClientEditComboBox.Text,
                    NatureOfClient = txtNatureOfClientEditComboBox.Text,
                    EmailAddress = emailAddressEditTextBox.Text,
                    IndustryCatagory = txtIndustryCatagoryComboBox.Text,


                    CFlatNo = cFlatNoTextBox.Text,
                    CHouseNo = cHouseNoTextBox.Text,
                    CRoadNo = cRoadNoTextBox.Text,
                    CBlock = cBlockTextBox.Text,
                    CARea = cAreaTextBox.Text,
                    CPost = cPostTextBox.Text,
                    CPostCode = cPostCodeTextBox.Text,
                    CDistrict = cDistCombo.Text,
                    CContactNo = cContactNoTextBox.Text,

                    TFlatNo = tFlatNoTextBox.Text,
                    THouseNo = tHouseNoTextBox.Text,
                    TRoadNo = tRoadNoTextBox.Text,
                    TBlock = tBlockTextBox.Text,
                    TARea = tAreaTextBox.Text,
                    TPost = tPostTextBox.Text,
                    TPostCode = tPostCodeTextBox.Text,
                    TDistrict = tDistComboBox.Text,
                    TContactNo = tContactNoTextBox.Text,





                    ContactPersonName = contactPersonNameEditTextBox.Text,
                    Designation = designationTextBox.Text,
                    CellNumber = cellNumberTextBox.Text,
                    EndUser = endUserTextBox.Text
                };
                aClientGateway.UpdateClient2(aClient);
                Reset();


            }
            catch (FormatException formatException)
            {
                MessageBox.Show("Please Enter Input in Correct Format", formatException.Message);
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message);
            }
        }
        private void Reset()
        {
            clientNameEditTextBox.Text = "";
            txtClientEditComboBox.Text = "";
            txtNatureOfClientEditComboBox.Text = "";
            emailAddressEditTextBox.Text = "";
            txtIndustryCatagoryComboBox.Text = "";

            cFlatNoTextBox.Text = "";
            cHouseNoTextBox.Text = "";
            cRoadNoTextBox.Text = "";
            cBlockTextBox.Text = "";
            cAreaTextBox.Text = "";
            cPostTextBox.Text = "";
            cPostCodeTextBox.Text = "";
            cDistCombo.Text = "";
            cContactNoTextBox.Text = "";

            tFlatNoTextBox.Text = "";
            tHouseNoTextBox.Text = "";
            tRoadNoTextBox.Text = "";
            tAreaTextBox.Text = "";
            tBlockTextBox.Text = "";
            tPostTextBox.Text = "";
            tPostCodeTextBox.Text = "";
            tDistComboBox.Text = "";
            tContactNoTextBox.Text = "";



            contactPersonNameEditTextBox.Text = "";
            designationTextBox.Text = "";
            cellNumberTextBox.Text = "";
            endUserTextBox.Text = "";

            updateButton.Enabled = true;

        }
        private void closeButton_Click(object sender, EventArgs e)
        {
            this.Hide();
            GridForClientDetails cg=new GridForClientDetails();
                cg.Show();
        }

        private void clientIdTextBox_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
