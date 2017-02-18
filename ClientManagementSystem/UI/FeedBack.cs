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
using ClientManagementSystem.Reports;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;

namespace ClientManagementSystem.UI
{
    public partial class FeedBack : Form
    {
        public FeedBack()
        {
            InitializeComponent();
        }

        public string uType;
        private void button1_Click(object sender, EventArgs e)
        {
           
            this.Visible = false;
            dynamic ww=new FirstStepOfIClientFeedbackDairy();
            ww.ShowDialog();
            this.Visible = true;
            //ww.nUserTextBox.Text = LoginForm.uId.ToString();  
        }

        private void button2_Click(object sender, EventArgs e)
        {
            
            this.Visible = false;
            dynamic ww = new ActionFollowUpProceedForm();
            ww.ShowDialog();
            this.Visible = true;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Visible = false;
            dynamic fg4 = new ActionByFollowUp();
            fg4.ShowDialog();
            this.Visible = true;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            //creating an object of ParameterField class
            ParameterField paramField = new ParameterField();

            //creating an object of ParameterFields class
            ParameterFields paramFields = new ParameterFields();

            //creating an object of ParameterDiscreteValue class
            ParameterDiscreteValue paramDiscreteValue = new ParameterDiscreteValue();

            //set the parameter field name
            paramField.Name = "User Id ";

            //set the parameter value
            paramDiscreteValue.Value = LoginForm.uId;

            //add the parameter value in the ParameterField object
            paramField.CurrentValues.Add(paramDiscreteValue);

            //add the parameter in the ParameterFields object
            paramFields.Add(paramField);

            //set the parameterfield information in the crystal report



            ReportViewer f2 = new ReportViewer();
            TableLogOnInfos reportLogonInfos = new TableLogOnInfos();
            TableLogOnInfo reportLogonInfo = new TableLogOnInfo();
            ConnectionInfo reportConInfo = new ConnectionInfo();
            Tables tables = default(Tables);
            //	Table table = default(Table);
            var with1 = reportConInfo;
            with1.ServerName = "tcp:KyotoServer,49172";
            with1.DatabaseName = "NewProductList";
            with1.UserID = "sa";
            with1.Password = "SystemAdministrator";
            FeedbackDiary cr = new FeedbackDiary();
            tables = cr.Database.Tables;
            foreach (Table table in tables)
            {
                reportLogonInfo = table.LogOnInfo;
                reportLogonInfo.ConnectionInfo = reportConInfo;
                table.ApplyLogOnInfo(reportLogonInfo);
            }
            f2.crystalReportViewer1.ParameterFieldInfo = paramFields;
            //set the parameterfield information in the crystal report
            f2.crystalReportViewer1.ReportSource = cr;
            this.Visible = false;

            f2.ShowDialog();
            this.Visible = true;
        }

        private void salesClientButton_Click(object sender, EventArgs e)
        {
            this.Hide();
            FirstStepOfSClientFeedBackDairy frm = new FirstStepOfSClientFeedBackDairy();
            frm.Show();
            
        }

        private void button5_Click(object sender, EventArgs e)
        {
            this.Visible = false;
            dynamic frm = new SActionByFollowUp();
            frm.ShowDialog();
            this.Visible = true;
        }

        private void button6_Click(object sender, EventArgs e)
        {
            this.Visible = false;
            dynamic frm = new SActionFollowUpProceedForm();
            frm.ShowDialog();
            this.Visible = true;
        }

        private void FeedBack_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (uType == "Admin")
            {
                this.Hide();
                MainUI frm = new MainUI();
                frm.Show();
            }
            else if (uType == "User")
            {
                this.Hide();
                MainUIForUser frm = new MainUIForUser();
                frm.Show();
            }
        }

        private void FeedBack_Load(object sender, EventArgs e)
        {
            uType = LoginForm.userType;
        }
    }
}
