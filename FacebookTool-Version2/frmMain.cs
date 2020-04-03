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
        string token = "EAAAAUaZA8jlABABZCfxHbH6nJI6aFhkLODH6yDLZCBkjqJdf258F1t6HvWZC5idPBFdSrKIrCufIpmKeAlBbw9uBSdcai4S3unqdmjAw88ZCi8wiHPKDfQKUT7dD0nTJz7LV4LUjuPoQW178m9Jk3hsH1itIahSn9ZBGaiHwAeFwZDZD";
        public frmMain()
        {
            InitializeComponent();
        }

        private async void button1_Click(object sender, EventArgs e)
        {
            //FacebookHelper.CheckAllMessageWithFbDtsgAsync("AQG91tirzkvM:AQHqn1aQJvJ-");
            FacebookHelper.GetHome(token);

        }
    }
}
