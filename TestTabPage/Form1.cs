using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TestTabPage
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            TextBox tmpLog = new TextBox(); // create new control of textbox type
            tmpLog.Text = "some text here";

            TabPage tb = new TabPage("my brand new tab"); //create tab
            tabControl1.TabPages.Add(tb); // add tab to existed TabControl
            tb.Controls.Add(tmpLog); // add textBox to new tab

            tabControl1.SelectedTab = tb;     // activate tab
        }

        private void button2_Click(object sender, EventArgs e)
        {
            tabControl1.TabPages.Remove(tabControl1.SelectedTab);
        }
    }
}
