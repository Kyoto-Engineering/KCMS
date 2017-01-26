using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ClientManagementSystem.UI
{
    public partial class ClientFeedbackGrid : Form
    {
        public ClientFeedbackGrid()
        {
            InitializeComponent();
        }

        private void newFeedbackButon_Click(object sender, EventArgs e)
        {
            dynamic ww = new InqueiryClientFeedbackDairy();
            this.Visible = false;
            ww.ShowDialog();
            this.Visible = true;

        }
    }
}
