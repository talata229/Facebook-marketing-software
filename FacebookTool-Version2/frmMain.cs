using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Common.Helpers;

namespace FacebookTool_Version2
{
    public partial class frmMain : Form
    {
        public frmMain()
        {
            InitializeComponent();
        }

        private async void button1_Click(object sender, EventArgs e)
        {
            FacebookHelper.CheckAllMessageWithFbDtsgAsync("AQG91tirzkvM:AQHqn1aQJvJ-");
        }
    }
}
