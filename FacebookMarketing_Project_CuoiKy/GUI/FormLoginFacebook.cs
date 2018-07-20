using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FacebookMarketing_Project_CuoiKy.GUI
{
    public partial class FormLoginFacebook : Form
    {
        public FormLoginFacebook()
        {
            InitializeComponent();
        }

        private void btnLogin_FormLoginFacebook_Click(object sender, EventArgs e)
        {
            //FormMain.username = txtUsername_FormLoginFacebook.Text;
            //FormMain.password = txtPassword_FromLoginFacebook.Text;
            DialogResult = DialogResult.OK;
        }

        private void FormLoginFacebook_Load(object sender, EventArgs e)
        {

        }

        private void btnCancel_FormLoginFacebook_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
