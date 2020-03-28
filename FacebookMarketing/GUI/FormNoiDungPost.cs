using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FacebookMarketing.GUI
{
    public partial class FormNoiDungPost : Form
    {
        public FormNoiDungPost()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
            
        }

        private void hienThiNoiDungLenDanhSachBaiPost()
        {
            ListViewItem lvi = new ListViewItem("1");
            lvi.SubItems.Add("Quang");
            lvi.SubItems.Add("Teof");
            //lvDanhSachNoiDungBaiviet
            FormMain fm = new FormMain();
            fm.lvDanhSachNoiDungBaiViet.Items.Add(lvi);
            MessageBox.Show("ahihihih vao day");//throw new NotImplementedException();


           // ListViewItem lvi = new ListViewItem("1");
           // lvi.SubItems.Add("eawliejalj");
           // FormMain.lvNoiDungBaiPost.Items.Add(lvi);
        }

        private void radText_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void radTextImage_CheckedChanged(object sender, EventArgs e)
        {
            if(radText.Checked)
            {
                groupBox3.Visible = false;
              //  MessageBox.Show("rad text dc an");
            } else if (radTextImage.Checked)
            {
                groupBox3.Visible = true;
                //MessageBox.Show("rad text +_Image dc an");
            }
        }

        private void FormNoiDungPost_Load(object sender, EventArgs e)
        {
            radText.Checked = true;
            groupBox3.Visible = false;
        }
    }
}
