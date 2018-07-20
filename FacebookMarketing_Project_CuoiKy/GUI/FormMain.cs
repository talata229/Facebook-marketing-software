using FacebookMarketing_Project_CuoiKy.Functions;
using FacebookMarketing_Project_CuoiKy.GUI;
using FacebookMarketing_Project_CuoiKy.Model;
using FacebookMarketing_Project_CuoiKy.Model.Json;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using xNet;

namespace FacebookMarketing_Project_CuoiKy
{
    public partial class FormMain : Form
    {

        public static bool dangPost = false;
        public static bool dangShare = false;
        public static bool dangComment = false;
        public static bool dangInbox = false;
        public static bool dangTag = false;
        public static bool dangReaction = false;

        FullFunction ff = new FullFunction();

        // public List<string> objFB = new List<string>();
        public List<string> objFB;
        public static string username = "";
        public static string password = "";

        public static string noiDungBaiPost = "";
        public static ListView lvNoiDungBaiPost = new ListView();


        Dictionary<string, BaiPost> dicBaiPost;
        Dictionary<string, string> dicGroupUID;//dùng để hiển thị all group của user lên list
        Dictionary<string, string> dicFriendUID;//dùng để hiển thị all friend của user lên list

        Dictionary<string, string> dicContentComment;
        static string[] arrContentCanComment;
        static List<string> ListIDCanComment = new List<string>();
        static List<string> ListContentComment = new List<string>();

        bool danhDauTatCaGroup = true;
        bool danhDauTatCaNoiDungBaiViet = true;
        static bool isLast = false; //Nếu là cuối thì mới hiển thị lên listKetQuaPostUpVaComment;
        static bool DangLenNhomDauTien = true; //để đăng lên nhóm đầu tiên
        static int SoLuongBaiCanDangCho1Lan;
        static int SoPhutNghi;

        /// <summary>
        /// trong tab Tag Người dùng
        /// </summary>
        HttpRequest http = new HttpRequest();
        string type = "application/x-www-form-urlencoded";
        public Dictionary<string, string> dicCanTag = new Dictionary<string, string>();
        public static List<string> listCanTag = new List<string>();
        int chiSoHienTai = 0;



        /// <summary>
        /// Trong tabReaction
        /// </summary>


        Dictionary<string, NewFeed1> dicReaction = new Dictionary<string, NewFeed1>();
        List<string> listCanReaction = new List<string>();


        public FormMain()
        {
            InitializeComponent();

        }



        private void pictureBox2_Click(object sender, EventArgs e)
        {

            FormLoginFacebook flf = new FormLoginFacebook();
            if (flf.ShowDialog() == DialogResult.OK)
            {
                username = flf.txtUsername_FormLoginFacebook.Text;
                password = flf.txtPassword_FromLoginFacebook.Text;
                objFB = new List<string>();

                objFB = ff.DangNhapFacebook(username, password);
                if (objFB == null)
                {
                    MessageBox.Show("Sai username hoặc password");
                    this.Close();
                }
                else
                {
                    pictureBox1.Load(ff.getCoverFromUID(objFB[1]));
                    pictureBox2.Load("https://graph.facebook.com/" + objFB[1] + "/picture?type=normal");
                    txtTokenFull.Text = objFB[4];
                }

            }

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            FormNoiDungPost fnd = new FormNoiDungPost();
            if (fnd.ShowDialog() == DialogResult.OK)
            {
                dicBaiPost = ff.getListFileFromPath(Directory.GetCurrentDirectory() + "\\DanhSachNoiDung");
                Directory.CreateDirectory(Directory.GetCurrentDirectory() + "\\DanhSachNoiDung");
                for (int i = 0; i < 1000; i++)
                {
                    if (!File.Exists(Directory.GetCurrentDirectory() + "\\DanhSachNoiDung" + "\\" + i + ".txt"))
                    {
                        StreamWriter sw = new StreamWriter(Directory.GetCurrentDirectory() + "\\DanhSachNoiDung" + "\\" + i + ".txt");
                        sw.Write(fnd.txtNoiDungBaiPost.Text);
                        sw.Close();

                        BaiPost bp = new BaiPost();
                        bp.Ma = i + "";
                        bp.NoiDung = fnd.txtNoiDungBaiPost.Text;
                        bp.Loai = true;
                        dicBaiPost.Add(i.ToString(), bp);
                        dicBaiPost = ff.getListFileFromPath(Directory.GetCurrentDirectory() + "\\DanhSachNoiDung");
                        break;
                    }
                }

                hienThiAllFileLenList();
            }
        }

        private void hienThiAllFileLenList()
        {
            dicBaiPost = ff.getListFileFromPath(Directory.GetCurrentDirectory() + "\\DanhSachNoiDung");
            lvDanhSachNoiDungBaiViet.Items.Clear(); //Xóa toàn bộ listview cũ đi

            foreach (KeyValuePair<string, BaiPost> item in dicBaiPost)
            {
                ListViewItem lvi = new ListViewItem(item.Key);
                lvi.Checked = true;//set check all
                lvi.SubItems.Add(item.Value.NoiDung);
                lvi.SubItems.Add(item.Value.Loai.ToString());
                lvDanhSachNoiDungBaiViet.Items.Add(lvi);
            }
            rdDangGroup.Checked = true;
        }

        private void button7_Click(object sender, EventArgs e)
        {

            dicBaiPost = ff.getListFileFromPath(Directory.GetCurrentDirectory() + "\\DanhSachNoiDung");
            foreach (KeyValuePair<string, BaiPost> item in dicBaiPost)
            {
                MessageBox.Show("Ten= " + item.Key + "\tNoidung = " + item.Value.NoiDung);

            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (lvDanhSachNoiDungBaiViet.SelectedItems.Count > 0)
            {
                ListViewItem lvi = lvDanhSachNoiDungBaiViet.SelectedItems[0];
                FormNoiDungPost fndp = new FormNoiDungPost();
                dicBaiPost = ff.getListFileFromPath(Directory.GetCurrentDirectory() + "\\DanhSachNoiDung");
                //string noiDung = 
                fndp.txtNoiDungBaiPost.Text = lvi.SubItems[1].Text;
                fndp.ShowDialog();



                StreamWriter sw = new StreamWriter(Directory.GetCurrentDirectory() + "\\DanhSachNoiDung" + "\\" + lvi.SubItems[0].Text);
                sw.Write(fndp.txtNoiDungBaiPost.Text);
                sw.Close();

                BaiPost bp = new BaiPost();
                bp.Ma = lvi.SubItems[0].Text;
                bp.NoiDung = fndp.txtNoiDungBaiPost.Text;
                bp.Loai = true;
                // dicBaiPost.Add(lvi.SubItems[0].Text.ToString(), bp);
                //  dicBaiPost = ff.getListFileFromPath(Directory.GetCurrentDirectory() + "\\DanhSachNoiDung");
                //  break;
                hienThiAllFileLenList();
            }

        }

        private void lvDanhSachNoiDungBaiViet_SelectedIndexChanged(object sender, EventArgs e)
        {
            //if (lvDanhSachNoiDungBaiViet.SelectedItems.Count > 0)
            //{
            //    ListViewItem lvi = lvDanhSachNoiDungBaiViet.SelectedItems[0];
            //    MessageBox.Show("Nội dung =" + lvi.SubItems[0] + " Thể loại = " + lvi.SubItems[1] + " Name = " + lvi.SubItems[2]);
            //}
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (lvDanhSachNoiDungBaiViet.SelectedItems.Count > 0)
            {
                ListViewItem lvi = lvDanhSachNoiDungBaiViet.SelectedItems[0];
                string name = lvi.SubItems[0].Text;
                File.Delete(Directory.GetCurrentDirectory() + "\\DanhSachNoiDung\\" + name);
                hienThiAllFileLenList();
            }

        }

        private void btnTest_Click(object sender, EventArgs e)
        {
            // ff.FB_Comment(objFB, "1910414092582362", "chaof cacs ban 3333333");
            //MessageBox.Show("Đã comemnt xong");
            // FB_Comment($obj, '1910414092582362', 'chaof cacs ban 2')
            MessageBox.Show(objFB[1]);
            ff.FB_Post_WithToken(objFB, "AHIHI ", objFB[1]);
        }

        private void btnLoadDanhSachGroup_Click(object sender, EventArgs e)
        {
            if (objFB == null) return;
            hienThiAllGroupUserLenList();
        }

        private void hienThiAllGroupUserLenList()
        {
            //throw new NotImplementedException();
            if (objFB[1] == null || objFB[1] == "")
            {
                MessageBox.Show("Bạn chưa đăng nhập Facebook", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            lvGroupUser.Items.Clear();
            dicGroupUID = ff.FBListGroupFromUID(objFB, objFB[1]);
            int i = 1;
            foreach (KeyValuePair<string, string> item in dicGroupUID)
            {
                ListViewItem lvi = new ListViewItem(i + "");
                if (i % 2 == 0) lvi.BackColor = Color.WhiteSmoke;
                lvi.SubItems.Add(item.Key);
                lvi.SubItems.Add(item.Value);
                lvGroupUser.Items.Add(lvi);
                lvi.Checked = true;
                i++;
            }
            //lvGroupUser.ColumnClick+=new ColumnClickEventHandler
        }

        private void lvGroupUser_ColumnClick(object sender, ColumnClickEventArgs e)
        {


            if (danhDauTatCaGroup)
            {
                foreach (ListViewItem item in lvGroupUser.Items)
                {
                    item.Checked = false;
                    danhDauTatCaGroup = false;
                }
            }
            else if (!danhDauTatCaGroup)
            {
                foreach (ListViewItem item in lvGroupUser.Items)
                {
                    item.Checked = true;
                    danhDauTatCaGroup = true;
                }
            }
        }

        private void lvDanhSachNoiDungBaiViet_ColumnClick(object sender, ColumnClickEventArgs e)
        {
            if (danhDauTatCaNoiDungBaiViet)
            {
                foreach (ListViewItem item in lvDanhSachNoiDungBaiViet.Items)
                {
                    item.Checked = false;
                    danhDauTatCaNoiDungBaiViet = !danhDauTatCaNoiDungBaiViet;
                }
            }
            else if (!danhDauTatCaNoiDungBaiViet)
            {
                foreach (ListViewItem item in lvGroupUser.Items)
                {
                    item.Checked = true;
                    danhDauTatCaNoiDungBaiViet = !danhDauTatCaNoiDungBaiViet;
                }
            }
        }
        static List<string> IDCacNhomCanPost = new List<string>();
        static List<string> TenCacNhomCanPost = new List<string>();

        static List<string> NoiDungCanPost = new List<string>();
        private void btnStartPost_Click(object sender, EventArgs e)
        {
            if (dangPost == false)
            {

                dangPost = true;
                btnStartPost.Text = "Stop";
                IDCacNhomCanPost.Clear();
                TenCacNhomCanPost.Clear();
                foreach (ListViewItem item in lvGroupUser.CheckedItems)
                {
                    IDCacNhomCanPost.Add(item.SubItems[1].Text);
                    TenCacNhomCanPost.Add(item.SubItems[2].Text);
                }
                foreach (ListViewItem item in lvDanhSachNoiDungBaiViet.CheckedItems)
                {
                    NoiDungCanPost.Add(item.SubItems[1].Text);
                }


                if (rdDangGroup.Checked)
                    backgroundWorker1.RunWorkerAsync();
                else if (rdDangLenTuong.Checked)
                    backgroundWorker2.RunWorkerAsync();
            }
            else if (dangPost)
            {
                btnStartPost.Text = "Start";


                if (rdDangGroup.Checked)
                    backgroundWorker1.CancelAsync();
                else if (rdDangLenTuong.Checked)
                    backgroundWorker2.CancelAsync();


                //backgroundWorker1.CancelAsync();
                //btnStartPost.Visible = true;
                //btnStopPost.Visible = false;
                dangPost = false;
            }


        }
        private void FormMain_Load(object sender, EventArgs e)
        {
            taoThuMuc();
            hienThiAllFileLenList();
            taoFilesContent();
            hienThiFileCommentLenList();
            khoiTaoBanDau();
            //CapNhatLaiNoiDungInbox();
            //  themDanhSachBanDau();
            // hienThiThongTinTacGia();
        }

        private void taoThuMuc()
        {
            try
            {
                Directory.CreateDirectory(Directory.GetCurrentDirectory() + "\\DanhSachContenComment");
                Directory.CreateDirectory(Directory.GetCurrentDirectory() + "\\DanhSachNoiDung");
                Directory.CreateDirectory(Directory.GetCurrentDirectory() + "\\NoiDungInbox");
            }
            catch (Exception ex)
            {
                ex.Message.ToString();
            }

        }

        private void taoFilesContent()
        {

            arrContentCanComment = new string[] { txtNoiDungCmt0.Text, txtNoiDungCmt1.Text, txtNoiDungCmt2.Text, txtNoiDungCmt3.Text, txtNoiDungCmt4.Text, txtNoiDungCmt5.Text, txtNoiDungCmt6.Text, txtNoiDungCmt6.Text };
            for (int i = 0; i < 8; i++)
            {
                if (!File.Exists(Directory.GetCurrentDirectory() + "\\DanhSachContenComment" + "\\" + i + ".txt"))
                {
                    StreamWriter sw = new StreamWriter(Directory.GetCurrentDirectory() + "\\DanhSachContenComment" + "\\" + i + ".txt");
                    sw.Write(arrContentCanComment[i]);
                    sw.Close();

                }
            }
        }

        private void CapNhatLaiFilesContent()
        {
            //Directory.CreateDirectory(Directory.GetCurrentDirectory() + "\\DanhSachContenComment");
            arrContentCanComment = new string[] { txtNoiDungCmt0.Text, txtNoiDungCmt1.Text, txtNoiDungCmt2.Text, txtNoiDungCmt3.Text, txtNoiDungCmt4.Text, txtNoiDungCmt5.Text, txtNoiDungCmt6.Text, txtNoiDungCmt7.Text };
            for (int i = 0; i < 8; i++)
            {
                //  if (!File.Exists(Directory.GetCurrentDirectory() + "\\DanhSachContenComment" + "\\" + i + ".txt"))
                // {
                StreamWriter sw = new StreamWriter(Directory.GetCurrentDirectory() + "\\DanhSachContenComment" + "\\" + i + ".txt");
                sw.Write(arrContentCanComment[i]);
                sw.Close();

                //  }
            }
        }
        private void hienThiFileCommentLenList()
        {
            TextBox[] listTextBoxContent = new TextBox[] { txtNoiDungCmt0, txtNoiDungCmt1, txtNoiDungCmt2, txtNoiDungCmt3, txtNoiDungCmt4, txtNoiDungCmt5, txtNoiDungCmt6, txtNoiDungCmt7 };
            for (int i = 0; i < 8; i++)
            {
                StreamReader sr = new StreamReader(Directory.GetCurrentDirectory() + "\\DanhSachContenComment" + "\\" + i + ".txt");
                listTextBoxContent[i].Text = sr.ReadToEnd();
                sr.Close();
            }
        }

        public void khoiTaoBanDau()
        {
            // throw new NotImplementedException();
            //bool isLast = false
            SoLuongBaiCanDangCho1Lan = int.Parse(txtSauKhiPost.Text);
            SoPhutNghi = int.Parse(txtSeDungNghi.Text);
            // btnStopPost.Enabled = false;
        }

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            Random rd = new Random();
            //int total = int.Parse(txtTuGiay.Text);
            int total = rd.Next(int.Parse(txtTuGiay.Text), int.Parse(txtDenGiay.Text));

            if (rdDangGroup.Checked)
            {

                for (int a = 0; a < SoLuongBaiCanDangCho1Lan; a++)
                {
                    for (int i = 0; i < IDCacNhomCanPost.Count; i++)
                    {
                        int indexNoiDung = rd.Next(0, NoiDungCanPost.Count);
                        string s = ff.FB_Post_WithToken2(objFB, NoiDungCanPost[indexNoiDung], IDCacNhomCanPost[i]);

                        BaiDaPost bdp = new BaiDaPost();

                        bdp.GroupName = TenCacNhomCanPost[i];
                        bdp.ID = s;
                        for (int q = 0; q < total; q++)
                        {
                            backgroundWorker1.ReportProgress(q * 100 / total, bdp);
                            Thread.Sleep(1000);
                            if (q == total - 1) isLast = true;
                            if (backgroundWorker1.CancellationPending)
                            {
                                e.Cancel = true;
                                backgroundWorker1.ReportProgress(0);
                                return;
                            }
                        }

                    }
                    Thread.Sleep(SoPhutNghi * 60 * 1000);
                }
            }
            else
            {
                MessageBox.Show("Không làm gì cả");
            }


        }

        private void backgroundWorker1_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            try
            {
                prBTimeLeftPost.Value = e.ProgressPercentage;
                //circularProgressBar1.Value = e.ProgressPercentage;
                if (isLast || DangLenNhomDauTien)
                {
                    BaiDaPost bdp = e.UserState as BaiDaPost;
                    txtStatus1.Text = "Post thành công vào group " + bdp.GroupName;

                    ListViewItem lvi = new ListViewItem(bdp.GroupName);
                    DangLenNhomDauTien = false;

                    string idFinal = Regex.Match(bdp.ID, "\"id\": \"(.*?)\"").Groups[1].Value;
                    lvi.SubItems.Add("https://www.facebook.com/" + idFinal);
                    Clipboard.SetText("https://www.facebook.com/" + idFinal);
                    lvi.Checked = true;
                    lvKetQuaPostUpVaComment.Items.Add(lvi);
                    isLast = false;
                }
            }
            catch (Exception ex)
            {
                ex.Message.ToString();
            }



        }

        private void backgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            MessageBox.Show("Đã post xong!");
        }



        private void backgroundWorker2_DoWork(object sender, DoWorkEventArgs e)
        {
            Random rd = new Random();
            int total = rd.Next(int.Parse(txtTuGiay.Text), int.Parse(txtDenGiay.Text));
            // MessageBox.Show("Total la" + total);
            if (rdDangLenTuong.Checked)
            {
                for (int a = 0; a < 50; a++)
                {

                    int indexNoiDung = rd.Next(0, NoiDungCanPost.Count);
                    string s = ff.FB_Post_WithToken2(objFB, NoiDungCanPost[indexNoiDung], objFB[1]);
                    //Clipboard.SetText(s);
                    BaiPostTrenTuongCaNhan bpttcn = new BaiPostTrenTuongCaNhan();
                    bpttcn.ID = s;
                    bpttcn.NoiDung = NoiDungCanPost[indexNoiDung];

                    BaiDaPostTrenTuongCaNhan bdpttcn = new BaiDaPostTrenTuongCaNhan();
                    // CoverWallpaper cw = new CoverWallpaper();
                    //Newtonsoft.Json.JsonConvert.PopulateObject(html, cw);
                    JsonConvert.PopulateObject(s, bdpttcn);

                    string temp = bdpttcn.id;


                    for (int q = 0; q < total; q++)
                    {
                        backgroundWorker2.ReportProgress(q * 100 / total, bdpttcn);
                        Thread.Sleep(1000);
                        if (q == total - 1) isLast = true;
                    }
                    if (backgroundWorker2.CancellationPending)
                    {
                        e.Cancel = true;
                        backgroundWorker2.ReportProgress(0);
                        return;
                    }
                }
                Thread.Sleep(SoPhutNghi * 60 * 1000);
            }
        }

        private void backgroundWorker2_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            try
            {
                prBTimeLeftPost.Value = e.ProgressPercentage;

                if (isLast || DangLenNhomDauTien)
                {
                    BaiDaPostTrenTuongCaNhan bdpttcn = e.UserState as BaiDaPostTrenTuongCaNhan;
                    txtStatus1.Text = "ID cua bai viet la " + bdpttcn.id;

                    ListViewItem lvi = new ListViewItem("Đăng lên tường");
                    DangLenNhomDauTien = false;

                    // string idFinal = Regex.Match(bdp.ID, "\"id\": \"(.*?)\"").Groups[1].Value;
                    lvi.SubItems.Add("https://www.facebook.com/" + bdpttcn.id);
                    Clipboard.SetText("https://www.facebook.com/" + bdpttcn.id);
                    lvi.Checked = true;
                    lvKetQuaPostUpVaComment.Items.Add(lvi);
                    isLast = false;
                }
            }
            catch (Exception ex)
            {
                ex.Message.ToString();
            }


        }

        private void backgroundWorker2_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            MessageBox.Show("Đã post xong");
        }

        private void btnLuuRaFilePostUpVaComment_Click(object sender, EventArgs e)
        {
            string filename = "";
            SaveFileDialog sfd = new SaveFileDialog();

            sfd.Title = "Lưu danh sách các bài viết đã đăng";
            sfd.Filter = "Text File (.txt) | *.txt";

            if (sfd.ShowDialog() == DialogResult.OK)
            {
                filename = sfd.FileName.ToString();
                if (filename != "")
                {
                    using (StreamWriter sw = new StreamWriter(filename))
                    {
                        foreach (ListViewItem item in lvKetQuaPostUpVaComment.Items)
                        {
                            sw.WriteLine(item.Text + "|" + item.SubItems[1].Text);
                        }
                    }
                }
            }
        }

        private void btnNhapThemTuFilePostUpVaComment_Click(object sender, EventArgs e)
        {
            OpenFileDialog theDialog = new OpenFileDialog();
            theDialog.Title = "Open Text File";
            theDialog.Filter = "TXT files|*.txt";
            theDialog.InitialDirectory = @"Desktop";
            if (theDialog.ShowDialog() == DialogResult.OK)
            {
                //MessageBox.Show(theDialog.FileName.ToString());
                string path = theDialog.FileName.ToString();
                StreamReader sr = new StreamReader(path);
                string line = sr.ReadLine();
                while (line != null)
                {
                    BaiDaPost bdp = new BaiDaPost();
                    string[] arr = line.Split('|');
                    bdp.GroupName = arr[0];
                    bdp.ID = arr[1];

                    ListViewItem lvi = new ListViewItem(arr[0]);
                    lvi.SubItems.Add("https://www.facebook.com/" + arr[1]);
                    lvi.Checked = true;
                    lvKetQuaPostUpVaComment.Items.Add(lvi);

                    line = sr.ReadLine();
                }
                sr.Close();
            }
        }

        private void rdDangLenTuong_Click(object sender, EventArgs e)
        {
            //MessageBox.Show("vua click");
            lvGroupUser.Visible = false;
            btnLoadDanhSachGroup.Visible = false;
        }

        private void rdDangGroup_CheckedChanged(object sender, EventArgs e)
        {
            lvGroupUser.Visible = true;
            btnLoadDanhSachGroup.Visible = true;
        }



        private void backgroundWorker3_DoWork(object sender, DoWorkEventArgs e)
        {
            int min = int.Parse(txtKhoangCachGiua2LanCmtTu.Text);
            int max = int.Parse(txtKhoangCachGiua2lanCmtDen.Text);

            //MessageBox.Show(ListIDCanComment.Count + "");
            Random rd = new Random();
            int total = rd.Next(min, max + 1);
            for (int i = 0; i < ListIDCanComment.Count; i++)
            {
                int index = rd.Next(8);
                string s = ListIDCanComment[i];
                string result = ff.FB_Comment(objFB, s, arrContentCanComment[index]);
                for (int q = 0; q < total; q++)
                {

                    backgroundWorker3.ReportProgress(q * 100 / total, result);
                    Thread.Sleep(1000);
                    if (backgroundWorker3.CancellationPending)
                    {
                        e.Cancel = true;
                        backgroundWorker3.ReportProgress(0);
                        return;
                    }
                }
            }
        }

        private void backgroundWorker3_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            prBComment.Value = e.ProgressPercentage;
            try
            {
                string s = e.UserState.ToString();
                txtStatus1.Text = "Vừa mới comment vào bài viết " + s;
            }
            catch (Exception ex)
            {
                ex.Message.ToString();
            }

        }

        private void backgroundWorker3_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {

        }



        private void btnStartComent_Click(object sender, EventArgs e)
        {
            if (dangComment == false)
            {
                CapNhatLaiFilesContent();
                dangComment = true;
                btnStartComent.Text = "Stop";


                dicContentComment = ff.getListFileContentCommentFromPath(Directory.GetCurrentDirectory() + "\\DanhSachContenComment");

                foreach (ListViewItem item in lvKetQuaPostUpVaComment.CheckedItems)
                {
                    string line = item.SubItems[1].Text;
                    string[] arr = line.Split('_');
                    ListIDCanComment.Add(arr[1]);
                    //MessageBox.Show(line);
                }

                //foreach (string s in ListIDCanComment)
                //{
                //    MessageBox.Show(s);
                //}
                backgroundWorker3.RunWorkerAsync();
            }
            else if (dangComment)
            {
                btnStartComent.Text = "Start";
                if (backgroundWorker3.IsBusy) backgroundWorker3.CancelAsync();
                dangComment = false;

            }





        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            // Dictionary<string,string> temp=ff.getListFileContentCommentFromPath("";
            CapNhatLaiFilesContent();
            dicContentComment = ff.getListFileContentCommentFromPath(Directory.GetCurrentDirectory() + "\\DanhSachContenComment");
            foreach (KeyValuePair<string, string> item in dicContentComment)
            {
                MessageBox.Show("Ten file =" + item.Key + "\tNoidung = " + item.Value);
            }
        }

        private void btnXoaTatCaPostUpVaComment_Click(object sender, EventArgs e)
        {
            lvKetQuaPostUpVaComment.Items.Clear();
            // MessageBox.Show("da chay vao day");
        }

        private void btnTuDanhSachBanBe_GuiInbox_Click(object sender, EventArgs e)
        {
            HienThiAllFriendUserLenList();
        }

        private void btnNapTuFile_GuiInbox_Click(object sender, EventArgs e)
        {
            CapNhatLaiListViewFriend();
            OpenFileDialog theDialog = new OpenFileDialog();
            theDialog.Title = "Open DS UID from File";
            theDialog.Filter = "TXT files|*.txt";
            theDialog.InitialDirectory = @"Desktop";
            if (theDialog.ShowDialog() == DialogResult.OK)
            {
                //MessageBox.Show(theDialog.FileName.ToString());
                string path = theDialog.FileName.ToString();
                StreamReader sr = new StreamReader(path);
                string line = sr.ReadLine();
                while (line != null)
                {
                    Friend3 fr3 = new Friend3();
                    string[] arr = line.Split('|');
                    fr3.id = arr[0];
                    fr3.name = arr[1];


                    int soLuongHienTai = lvFriendUser.Items.Count;
                    int i = soLuongHienTai + 1;
                    ListViewItem lvi = new ListViewItem(i + "");
                    lvi.SubItems.Add(fr3.id);
                    lvi.SubItems.Add(fr3.name);
                    //lvi.Checked = true;
                    lvFriendUser.Items.Add(lvi);
                    i++;
                    line = sr.ReadLine();
                }
                sr.Close();
            }

        }

        private void HienThiAllFriendUserLenList()
        {

            if (objFB[1] == null || objFB[1] == "")
            {
                MessageBox.Show("Bạn chưa đăng nhập Facebook", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            lvFriendUser.Items.Clear();
            dicFriendUID = ff.FBListFriendFromUID(objFB, objFB[1]);

            int i = 1;
            foreach (KeyValuePair<string, string> item in dicFriendUID)
            {

                ListViewItem lvi = new ListViewItem(i + "");
                lvi.BackColor = Color.WhiteSmoke;
                lvi.SubItems.Add(item.Key);
                lvi.SubItems.Add(item.Value);
                lvFriendUser.Items.Add(lvi);
                i++;
                //  lvi.Checked = true;
            }
        }

        private void btnTestInbox_Click(object sender, EventArgs e)
        {
            //dicFriendUID = ff.FBListFriendFromUID(objFB, "100001578994326");
            //MessageBox.Show(dicFriendUID[""])
            //CapNhatLaiListViewFriend();
            ff.RandomChuoi(txtNoiDungTinNhan.Text);
        }

        private void btnLuuDanhSachBanBeRaFile_Click(object sender, EventArgs e)
        {
            CapNhatLaiListViewFriend();
            string filename = "";
            SaveFileDialog sfd = new SaveFileDialog();

            sfd.Title = "Lưu danh sách bạn bè ra file text";
            sfd.Filter = "Text File (.txt) | *.txt";

            if (sfd.ShowDialog() == DialogResult.OK)
            {
                filename = sfd.FileName.ToString();
                if (filename != "")
                {
                    using (StreamWriter sw = new StreamWriter(filename))
                    {
                        foreach (ListViewItem item in lvFriendUser.Items)
                        {
                            sw.WriteLine(item.SubItems[1].Text + "|" + item.SubItems[2].Text);
                        }
                    }
                }
            }

        }

        private void btnXoa_GuiInbox_Click(object sender, EventArgs e)
        {
            if (lvFriendUser.SelectedItems.Count > 0)
            {
                var confirmation = MessageBox.Show("Bạn có chắc muốn xóa dòng này chứ?", "Hỏi xóa", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (confirmation == DialogResult.Yes)
                {
                    for (int i = lvFriendUser.SelectedItems.Count - 1; i >= 0; i--)
                    {
                        ListViewItem itm = lvFriendUser.SelectedItems[i];
                        lvFriendUser.Items[itm.Index].Remove();
                    }
                }
                CapNhatLaiListViewFriend();
            }

            else
                MessageBox.Show("Bạn chưa chọn dòng nào cả");
        }
        private void CapNhatLaiListViewFriend()
        {
            Dictionary<String, String> Dic = lvFriendUser.Items.Cast<ListViewItem>().ToDictionary(x => x.SubItems[1].Text, x => x.SubItems[2].Text);
            lvFriendUser.Items.Clear();
            int i = 1;
            foreach (KeyValuePair<string, string> item in Dic)
            {
                ListViewItem lvi = new ListViewItem(i + "");
                lvi.SubItems.Add(item.Key);
                lvi.SubItems.Add(item.Value);
                lvFriendUser.Items.Add(lvi);
                i++;
            }
        }
        private List<string> dsUIDCanInbox = new List<string>();
        private void btnStartInbox_Click(object sender, EventArgs e)
        {
            if (dangInbox == false)
            {
                dangInbox = true;
                btnStartInbox.Text = "Stop";

                CapNhatLaiNoiDungInbox();
                foreach (ListViewItem item in lvFriendUser.Items)
                {
                    dsUIDCanInbox.Add(item.SubItems[1].Text);
                }
                backgroundWorker4.RunWorkerAsync();
            }
            else if (dangInbox)
            {
                btnStartInbox.Text = "Start";
                backgroundWorker4.CancelAsync();

                dangInbox = false;

            }

        }

        private void CapNhatLaiNoiDungInbox()
        {
            Directory.CreateDirectory(Directory.GetCurrentDirectory() + "\\NoiDungInbox");
            //arrContentCanComment = new string[] { txtNoiDungCmt0.Text, txtNoiDungCmt1.Text, txtNoiDungCmt2.Text, txtNoiDungCmt3.Text, txtNoiDungCmt4.Text, txtNoiDungCmt5.Text, txtNoiDungCmt6.Text, txtNoiDungCmt7.Text };

            StreamWriter sw = new StreamWriter(Directory.GetCurrentDirectory() + "\\NoiDungInbox" + "\\Inbox.txt");
            sw.Write(txtNoiDungTinNhan.Text);
            sw.Close();
            //  MessageBox.Show(txtNoiDungTinNhan.Text);

        }

        private void backgroundWorker4_DoWork(object sender, DoWorkEventArgs e)
        {
            int min = int.Parse(txtDoTreTu.Text);
            int max = int.Parse(txtDoTreDen.Text);

            //MessageBox.Show(ListIDCanComment.Count + "");
            Random rd = new Random();
            int total = rd.Next(min, max + 1);


            for (int i = 0; i < dsUIDCanInbox.Count; i++)
            {
                ff.FB_m(objFB, dsUIDCanInbox[i], ff.RandomChuoi(txtNoiDungTinNhan.Text));
                for (int q = 0; q < total; q++)
                {
                    backgroundWorker4.ReportProgress(q * 100 / total, dsUIDCanInbox[i]);
                    Thread.Sleep(1000);
                    if (backgroundWorker4.CancellationPending)
                    {
                        e.Cancel = true;
                        backgroundWorker4.ReportProgress(0);
                        return;
                    }
                }
            }

        }

        private void backgroundWorker4_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            prBGuiInbox.Value = e.ProgressPercentage;
            try
            {
                txtStatus1.Text = "Vừa mới inbox cho " + e.UserState.ToString();
            }
            catch (Exception ex)
            {
                ex.Message.ToString();
            }


        }

        private void backgroundWorker4_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {

        }

        private void btnNapNhomTuFile_ChiaSeBaiViet_Click(object sender, EventArgs e)
        {
            OpenFileDialog theDialog = new OpenFileDialog();
            theDialog.Title = "Nạp nhóm từ file";
            theDialog.Filter = "TXT files|*.txt";
            theDialog.InitialDirectory = @"Desktop";
            if (theDialog.ShowDialog() == DialogResult.OK)
            {
                //MessageBox.Show(theDialog.FileName.ToString());
                string path = theDialog.FileName.ToString();
                StreamReader sr = new StreamReader(path);
                string line = sr.ReadLine();
                while (line != null)
                {
                    GroupUID grUID = new GroupUID();
                    string[] arr = line.Split('|');

                    ListViewItem lvi = new ListViewItem(arr[0]);
                    lvi.SubItems.Add(arr[1]);
                    lvi.SubItems.Add(arr[2]);
                    // lvi.SubItems.Add(fr3.name);
                    lvi.Checked = true;
                    lvNhomChiaSeBaiViet.Items.Add(lvi);

                    line = sr.ReadLine();
                }
                sr.Close();
            }
        }

        private void btnLoadDSNhom_ChiaSeBaiViet_Click(object sender, EventArgs e)
        {
            try
            {
                lvNhomChiaSeBaiViet.Items.Clear();
                dicGroupUID = ff.FBListGroupFromUID(objFB, objFB[1]);
                int i = 1;
                foreach (KeyValuePair<string, string> item in dicGroupUID)
                {
                    ListViewItem lvi = new ListViewItem(i.ToString());
                    if (i % 2 == 0) lvi.BackColor = Color.WhiteSmoke;
                    lvi.SubItems.Add(item.Key);
                    lvi.SubItems.Add(item.Value);
                    lvNhomChiaSeBaiViet.Items.Add(lvi);
                    lvi.Checked = true;
                    i++;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Có lỗi xảy ra");
                Console.Write(ex.Message);
            }
            //lvGroupUser.ColumnClick+=new ColumnClickEventHandler
        }

        private void btnLuuNhomRaFile_ChiaSeBaiViet_Click(object sender, EventArgs e)
        {
            string filename = "";
            SaveFileDialog sfd = new SaveFileDialog();

            sfd.Title = "Lưu danh sách nhóm ra file";
            sfd.Filter = "Text File (.txt) | *.txt";

            if (sfd.ShowDialog() == DialogResult.OK)
            {
                filename = sfd.FileName.ToString();
                if (filename != "")
                {
                    using (StreamWriter sw = new StreamWriter(filename))
                    {
                        foreach (ListViewItem item in lvNhomChiaSeBaiViet.Items)
                        {
                            sw.WriteLine(item.Text + "|" + item.SubItems[1].Text + "|" + item.SubItems[2].Text);
                        }
                    }
                }
            }
        }

        private void btnXoa_ChiaSeBaiViet_Click(object sender, EventArgs e)
        {
            if (lvNhomChiaSeBaiViet.SelectedItems.Count > 0)
            {
                var confirmation = MessageBox.Show("Bạn có chắc muốn xóa dòng này chứ?", "Hỏi xóa", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (confirmation == DialogResult.Yes)
                {
                    for (int i = lvNhomChiaSeBaiViet.SelectedItems.Count - 1; i >= 0; i--)
                    {
                        ListViewItem itm = lvNhomChiaSeBaiViet.SelectedItems[i];
                        lvNhomChiaSeBaiViet.Items[itm.Index].Remove();
                    }
                }
                // CapNhatLaiListViewFriend();
            }

            else
                MessageBox.Show("Bạn chưa chọn dòng nào cả");
        }

        private void backgroundWorker5_DoWork(object sender, DoWorkEventArgs e)
        {
            //Dùng để chia sẻ bài viết;
            Random rd = new Random();
            int min = int.Parse(txtKhoangCachGiua2LanShareTu.Text);
            int max = int.Parse(txtKhoangCachGiua2LanShareDen.Text);
            int total = rd.Next(min, max + 1);
            for (int q = 0; q < dsGroupCanChiaSe.Count; q++)
            {
                string s = ff.FB_Share(objFB, ff.KiemTraLink(txtLinkBaiVietCanChiaSe.Text), ff.RandomChuoi(txtNoiDungChiaSe.Text), dsGroupCanChiaSe[q]);

                for (int i = 0; i < total; i++)
                {
                    backgroundWorker5.ReportProgress(i * 100 / total, s);
                    Thread.Sleep(1000);
                    if (backgroundWorker5.CancellationPending)
                    {
                        e.Cancel = true;
                        backgroundWorker5.ReportProgress(0);
                        return;
                    }


                }

                // if (q == total - 1) isLast = true;
            }
        }

        private void backgroundWorker5_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            prBChiaSe.Value = e.ProgressPercentage;
            try
            {
                txtStatus1.Text = "Vừa mới chia sẻ bài viết " + e.UserState.ToString();
            }
            catch (Exception ex)
            {
                ex.Message.ToString();
            }

        }

        private void backgroundWorker5_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {

        }
        List<string> dsGroupCanChiaSe = new List<string>();
        private void btnStartShare_Click(object sender, EventArgs e)
        {
            dsGroupCanChiaSe.Clear();
            if (dangShare == false)
            {
                dangShare = true;
                btnStartShare.Text = "Stop";

                foreach (ListViewItem item in lvNhomChiaSeBaiViet.CheckedItems)
                {
                    dsGroupCanChiaSe.Add(item.SubItems[1].Text);
                }
                backgroundWorker5.RunWorkerAsync();

            }
            else if (dangShare)
            {
                btnStartShare.Text = "Start";

                backgroundWorker5.CancelAsync();
                dangShare = false;
            }

        }

        private void button9_Click(object sender, EventArgs e)
        {
            MessageBox.Show(ff.FB_Share(objFB, "180687729426036", "Tập share lên group lần thứ 3", "100024544302593"));
        }

        private void btnLoadDSBanBe_TagNguoiDung_Click(object sender, EventArgs e)
        {
            lvDSBanBe_TagNguoiDung.Items.Clear();
            dicCanTag = ff.FBListFriendFromUID(objFB, objFB[1]);
            int i = 1;
            foreach (KeyValuePair<string, string> item in dicCanTag)
            {
                listCanTag.Add(item.Key);
                ListViewItem lvi = new ListViewItem(i + "");
                lvi.SubItems.Add(item.Key);
                lvi.SubItems.Add(item.Value);
                lvDSBanBe_TagNguoiDung.Items.Add(lvi);
                i++;
            }
            txtStatus1.Text = "Đã load xong danh sách bạn bè của Tab Tag Người Dùng";

        }

        private void backgroundWorker6_DoWork(object sender, DoWorkEventArgs e)
        {
            if (rdTagBanBe_TagNguoiDung.Checked)
            {
                int soLuongTag1Lan = int.Parse(txtSoLuong_TagNguoiDung.Text);
                int soLuongVongLap = listCanTag.Count / soLuongTag1Lan + 1;
                int soLuongTagTrongVongLapCuoi = listCanTag.Count - soLuongTag1Lan * (soLuongVongLap - 1);
                int total = int.Parse(txtKhoangThoiGianGiua2LanTag_TagNguoiDung.Text);
                //MessageBox.Show("Số lượng tag trong vòng lặp cuối là: " + soLuongTagTrongVongLapCuoi);
                string sTag = "";
                for (int i = 0; i < soLuongVongLap; i++)
                {
                    if (i <= soLuongVongLap - 2)
                    {
                        // MessageBox.Show("Chưa phải vòng lặp cuối");
                        // MessageBox.Show("Toatal =" + total);
                        for (int j = 0; j < soLuongTag1Lan; j++)
                        {
                            sTag += "+@[" + listCanTag[chiSoHienTai] + ":0]";
                            chiSoHienTai++;
                        }
                        string url = "https://graph.facebook.com/" + ff.KiemTraLink(txtIDPost_TagNguoiDung.Text) + "/comments?method=post&access_token=" + objFB[3] + "&message=" + txtNoiDung_TagNguoiDung.Text + sTag;
                        string s = http.Get(url).ToString();
                        for (int k = 0; k < total; k++)
                        {
                            TagUID tuid = new TagUID();
                            tuid.ID = s;
                            backgroundWorker6.ReportProgress(k * 100 / total, tuid);
                            Thread.Sleep(1000);
                            if (backgroundWorker6.CancellationPending)
                            {
                                e.Cancel = true;
                                backgroundWorker6.ReportProgress(0);
                                return;
                            }
                        }


                        // Thread.Sleep(15000);
                        // MessageBox.Show(s);
                        sTag = "";

                    }
                    else if (i == soLuongVongLap - 1)
                    {
                        //MessageBox.Show("Là vòng lặp cuối");
                        // MessageBox.Show("Toatal =" + total);
                        for (int j = 0; j < soLuongTagTrongVongLapCuoi; j++)
                        {
                            sTag += "+@[" + listCanTag[chiSoHienTai] + ":0]";
                            chiSoHienTai++;
                        }
                        string url = "https://graph.facebook.com/" + ff.KiemTraLink(txtIDPost_TagNguoiDung.Text) + "/comments?method=post&access_token=" + objFB[3] + "&message=" + txtNoiDung_TagNguoiDung.Text + sTag;
                        string s = http.Get(url).ToString();
                        for (int k = 0; k < total; k++)
                        {
                            TagUID tuid = new TagUID();
                            tuid.ID = s;
                            backgroundWorker6.ReportProgress(k * 100 / total, tuid);
                            Thread.Sleep(1000);
                            if (backgroundWorker6.CancellationPending)
                            {
                                e.Cancel = true;
                                backgroundWorker6.ReportProgress(0);
                                return;
                            }
                        }
                        //Thread.Sleep(15000);
                        //MessageBox.Show(s);

                        break;

                    }
                }
            }
            else if (rdTagTu1ListUID_TagNguoiDung.Checked)
            {
                listCanTag.Clear();
                for (int i = 0; i < txtListTag.Lines.Length; i++)
                {
                    //MessageBox.Show();
                    if (txtListTag.Lines[i] + "\r\n" != null && txtListTag.Lines[i] + "\r\n" != "")
                        listCanTag.Add(txtListTag.Lines[i] + "\r\n");
                }

                int soLuongTag1Lan = int.Parse(txtSoLuong_TagNguoiDung.Text);
                int soLuongVongLap = listCanTag.Count / soLuongTag1Lan + 1;
                int soLuongTagTrongVongLapCuoi = listCanTag.Count - soLuongTag1Lan * (soLuongVongLap - 1);
                int total = int.Parse(txtKhoangThoiGianGiua2LanTag_TagNguoiDung.Text);
                //MessageBox.Show("Số lượng tag trong vòng lặp cuối là: " + soLuongTagTrongVongLapCuoi);
                string sTag = "";
                for (int i = 0; i < soLuongVongLap; i++)
                {
                    if (i <= soLuongVongLap - 2)
                    {
                        // MessageBox.Show("Chưa phải vòng lặp cuối");
                        //MessageBox.Show("Toatal =" + total);
                        for (int j = 0; j < soLuongTag1Lan; j++)
                        {
                            sTag += "+@[" + listCanTag[chiSoHienTai] + ":0]";
                            chiSoHienTai++;
                        }
                        string url = "https://graph.facebook.com/" + ff.KiemTraLink(txtIDPost_TagNguoiDung.Text) + "/comments?method=post&access_token=" + objFB[3] + "&message=" + txtNoiDung_TagNguoiDung.Text + sTag;
                        string s = http.Get(url).ToString();
                        for (int k = 0; k < total; k++)
                        {
                            TagUID tuid = new TagUID();
                            tuid.ID = s;
                            backgroundWorker6.ReportProgress(k * 100 / total, tuid);
                            Thread.Sleep(1000);
                            if (backgroundWorker6.CancellationPending)
                            {
                                e.Cancel = true;
                                backgroundWorker6.ReportProgress(0);
                                return;
                            }
                        }
                        // Thread.Sleep(15000);
                        // MessageBox.Show(s);
                        sTag = "";

                    }
                    else if (i == soLuongVongLap - 1)
                    {
                        //MessageBox.Show("Là vòng lặp cuối");
                        // MessageBox.Show("Toatal =" + total);
                        for (int j = 0; j < soLuongTagTrongVongLapCuoi; j++)
                        {
                            sTag += "+@[" + listCanTag[chiSoHienTai] + ":0]";
                            chiSoHienTai++;
                        }
                        string url = "https://graph.facebook.com/" + ff.KiemTraLink(txtIDPost_TagNguoiDung.Text) + "/comments?method=post&access_token=" + objFB[3] + "&message=" + txtNoiDung_TagNguoiDung.Text + sTag;
                        string s = http.Get(url).ToString();
                        for (int k = 0; k < total; k++)
                        {
                            TagUID tuid = new TagUID();
                            tuid.ID = s;
                            backgroundWorker6.ReportProgress(k * 100 / total, tuid);
                            Thread.Sleep(1000);
                            if (backgroundWorker6.CancellationPending)
                            {
                                e.Cancel = true;
                                backgroundWorker6.ReportProgress(0);
                                return;
                            }
                        }
                        //Thread.Sleep(15000);
                        //MessageBox.Show(s);

                        break;

                    }
                }
            }
            else if (rdTagNhungNguoiDaComment_TagBanBe.Checked)
            {
                listCanTag.Clear();
                dicCanTag.Clear();
                dicCanTag = ff.FBListUIDComentFromPost(objFB, txtIDPost_TagNguoiDung.Text);
                //for (int i = 0; i < txtListTag.Lines.Length; i++)
                //{
                //    //MessageBox.Show();
                //    if (txtListTag.Lines[i] + "\r\n" != null && txtListTag.Lines[i] + "\r\n" != "")
                //        listCanTag.Add(txtListTag.Lines[i] + "\r\n");
                //}
                foreach (KeyValuePair<string, string> item in dicCanTag)
                {
                    listCanTag.Add(item.Key);
                }
                int soLuongTag1Lan = int.Parse(txtSoLuong_TagNguoiDung.Text);
                int soLuongVongLap = listCanTag.Count / soLuongTag1Lan + 1;
                int soLuongTagTrongVongLapCuoi = listCanTag.Count - soLuongTag1Lan * (soLuongVongLap - 1);
                int total = int.Parse(txtKhoangThoiGianGiua2LanTag_TagNguoiDung.Text);
                //MessageBox.Show("Số lượng tag trong vòng lặp cuối là: " + soLuongTagTrongVongLapCuoi);
                string sTag = "";
                for (int i = 0; i < soLuongVongLap; i++)
                {
                    if (i <= soLuongVongLap - 2)
                    {
                        // MessageBox.Show("Chưa phải vòng lặp cuối");
                        //MessageBox.Show("Toatal =" + total);
                        for (int j = 0; j < soLuongTag1Lan; j++)
                        {
                            sTag += "+@[" + listCanTag[chiSoHienTai] + ":0]";
                            chiSoHienTai++;
                        }
                        string url = "https://graph.facebook.com/" + ff.KiemTraLink(txtIDPost_TagNguoiDung.Text) + "/comments?method=post&access_token=" + objFB[3] + "&message=" + txtNoiDung_TagNguoiDung.Text + sTag;
                        string s = http.Get(url).ToString();
                        for (int k = 0; k < total; k++)
                        {
                            TagUID tuid = new TagUID();
                            tuid.ID = s;
                            backgroundWorker6.ReportProgress(k * 100 / total, tuid);
                            Thread.Sleep(1000);
                            if (backgroundWorker6.CancellationPending)
                            {
                                e.Cancel = true;
                                backgroundWorker6.ReportProgress(0);
                                return;
                            }
                        }
                        // Thread.Sleep(15000);
                        // MessageBox.Show(s);
                        sTag = "";

                    }
                    else if (i == soLuongVongLap - 1)
                    {
                        //MessageBox.Show("Là vòng lặp cuối");
                        // MessageBox.Show("Toatal =" + total);
                        for (int j = 0; j < soLuongTagTrongVongLapCuoi; j++)
                        {
                            sTag += "+@[" + listCanTag[chiSoHienTai] + ":0]";
                            chiSoHienTai++;
                        }
                        string url = "https://graph.facebook.com/" + ff.KiemTraLink(txtIDPost_TagNguoiDung.Text) + "/comments?method=post&access_token=" + objFB[3] + "&message=" + txtNoiDung_TagNguoiDung.Text + sTag;
                        string s = http.Get(url).ToString();
                        for (int k = 0; k < total; k++)
                        {
                            TagUID tuid = new TagUID();
                            tuid.ID = s;
                            backgroundWorker6.ReportProgress(k * 100 / total, tuid);
                            Thread.Sleep(1000);
                            if (backgroundWorker6.CancellationPending)
                            {
                                e.Cancel = true;
                                backgroundWorker6.ReportProgress(0);
                                return;
                            }
                        }
                        //Thread.Sleep(15000);
                        //MessageBox.Show(s);

                        break;

                    }
                }
            }
        }

        private void backgroundWorker6_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            prBTag.Value = e.ProgressPercentage;

            try
            {
                TagUID tuid = e.UserState as TagUID;
                txtStatus1.Text = tuid.ID;
            }
            catch (Exception ex)
            {
                ex.Message.ToString();
            }

        }

        private void backgroundWorker6_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            MessageBox.Show("Đã tag xong!");
        }

        private void btnStartTag_TagNguoiDung_Click(object sender, EventArgs e)
        {
            if (dangTag == false)
            {

                dangTag = true;
                btnStartTag_TagNguoiDung.Text = "Stop";

                backgroundWorker6.RunWorkerAsync();
            }
            else if (dangTag)
            {
                btnStartTag_TagNguoiDung.Text = "Start";

                dangTag = false;
                backgroundWorker6.CancelAsync();
            }

        }

        private void btnMoTuFile_TagNguoiDung_Click(object sender, EventArgs e)
        {
            OpenFileDialog theDialog = new OpenFileDialog();
            theDialog.Title = "Open Text File";
            theDialog.Filter = "TXT files|*.txt";
            theDialog.InitialDirectory = @"Desktop";
            if (theDialog.ShowDialog() == DialogResult.OK)
            {
                //MessageBox.Show(theDialog.FileName.ToString());
                string path = theDialog.FileName.ToString();
                StreamReader sr = new StreamReader(path);
                string line = sr.ReadLine();
                while (line != null)
                {
                    // txtListTag.Text += line;
                    txtListTag.Text += line + "\r\n";

                    line = sr.ReadLine();
                }
                sr.Close();
            }
        }

        private void btnLuuRaFile_TagNguoiDung_Click(object sender, EventArgs e)
        {
            dicCanTag = ff.FBListFriendFromUID(objFB, objFB[1]);


            string filename = "";
            SaveFileDialog sfd = new SaveFileDialog();

            sfd.Title = "Lưu danh sách các bài viết đã đăng";
            sfd.Filter = "Text File (.txt) | *.txt";

            if (sfd.ShowDialog() == DialogResult.OK)
            {
                filename = sfd.FileName.ToString();
                if (filename != "")
                {
                    using (StreamWriter sw = new StreamWriter(filename))
                    {
                        foreach (KeyValuePair<string, string> item in dicCanTag)
                        {
                            if (item.Key != null && item.Key != "")
                                sw.WriteLine(item.Key);
                        }
                    }
                }
            }
        }

        private void btnTest_TagNguoiDung_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < txtListTag.Lines.Length; i++)
            {
                MessageBox.Show(txtListTag.Lines[i] + "\r\n");
            }
        }

        private void btnLoadNhungNguoiDaCommentBaiViet_TagNguoiDung_Click(object sender, EventArgs e)
        {
            lvNhungNguoiDaCommentBaiViet.Items.Clear();
            dicCanTag = ff.FBListUIDComentFromPost(objFB, ff.KiemTraLink(txtIDPost_TagNguoiDung.Text));
            int i = 1;
            foreach (KeyValuePair<string, string> item in dicCanTag)
            {
                //MessageBox.Show(item.Key + "|" + item.Value);
                ListViewItem lvi = new ListViewItem(i + "");
                lvi.SubItems.Add(item.Key);
                lvi.SubItems.Add(item.Value);
                lvNhungNguoiDaCommentBaiViet.Items.Add(lvi);
                i++;
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            //ff.FB_Post(objFB, "Con đường mua gió", objFB[1]);
            MessageBox.Show(ff.LayToken("muathudep152@gmail.com", "tranequanghero"));
        }

        private void btnLoadReaction_Click(object sender, EventArgs e)
        {
            //Xóa hết cái cũ
            lvReaction.Items.Clear();
            dicReaction.Clear();
            listCanReaction.Clear();

            //Hiển thị danh sách các bài cần react lên listview trước
            dicReaction = ff.FBListNewFeed2(objFB, int.Parse(txtSoLuongBaiReaction.Text));
            int i = 1;
            foreach (KeyValuePair<string, NewFeed1> item in dicReaction)
            {
                ListViewItem lvi = new ListViewItem(i + "");
                if (i % 2 == 0) lvi.BackColor = Color.WhiteSmoke;
                NewFeed1 nf1 = item.Value as NewFeed1;

                lvi.Checked = true;
                lvi.SubItems.Add(nf1.id);

                //Xử lý hiển thị thời gian
                string time1 = nf1.created_time;
                string time2 = time1.Remove(19);
                string time3 = time2.Replace("T", "===");
                lvi.SubItems.Add(time3);

                lvi.SubItems.Add(nf1.message);
                lvReaction.Items.Add(lvi);

                //  listCanReaction.Add(item.Key);
                i++;
            }

        }

        private void backgroundWorker7_DoWork(object sender, DoWorkEventArgs e)
        {
            //Dùng để Reaction vào bài post
            Random rd = new Random();
            int min = int.Parse(txtReactionTu.Text);
            int max = int.Parse(txtReactionDen.Text);
            int total = rd.Next(min, max + 1);
            //int soLuong
            for (int q = 0; q < listCanReaction.Count; q++)
            {
                bool s = ff.FBReactionFromIDFull(objFB, listCanReaction[q], rd.Next(7));
                for (int i = 0; i < total; i++)
                {
                    backgroundWorker7.ReportProgress(i * 100 / total, s);
                    Thread.Sleep(1000);
                    if (backgroundWorker7.CancellationPending)
                    {
                        e.Cancel = true;
                        backgroundWorker7.ReportProgress(0);
                        return;
                    }


                }

                // if (q == total - 1) isLast = true;
            }
        }

        private void backgroundWorker7_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            prBReaction.Value = e.ProgressPercentage;
            try
            {
                txtStatus1.Text = "Vừa mới Reaction cho bài viết " + e.UserState.ToString();
            }
            catch (Exception ex)
            {
                ex.Message.ToString();
            }
        }

        private void backgroundWorker7_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            MessageBox.Show("Đã Reaction xong tất cả các bài");
        }

        private void btnStartReaction_Click(object sender, EventArgs e)
        {
            listCanReaction.Clear();
            if (dangReaction == false)
            {
                dangReaction = true;
                btnStartReaction.Text = "Stop";

                foreach (ListViewItem item in lvReaction.CheckedItems)
                {
                    listCanReaction.Add(item.SubItems[1].Text);
                }


                backgroundWorker7.RunWorkerAsync();

            }
            else if (dangReaction)
            {
                btnStartReaction.Text = "Start";

                backgroundWorker7.CancelAsync();
                dangReaction = false;
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (ff.FBReactionFromIDFull(objFB, "1460919090645616_1955515217852665", 5))
                MessageBox.Show("Thành công");
            else
                MessageBox.Show("Thất bại");
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("http://www.facebook.com/tranquanghero229");
        }

        private void label31_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("https://m.facebook.com/messages/read/?tid=100001578994326");

        }

        private void label29_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("https://mail.google.com/mail/u/0/");

        }
        bool danhDauTatCaGroup_TabChiaSeBaiViet = true;
        private void lvNhomChiaSeBaiViet_ColumnClick(object sender, ColumnClickEventArgs e)
        {
            if (danhDauTatCaGroup_TabChiaSeBaiViet)
            {
                foreach (ListViewItem item in lvNhomChiaSeBaiViet.Items)
                {
                    item.Checked = false;
                    danhDauTatCaGroup_TabChiaSeBaiViet = false;
                }
            }
            else if (!danhDauTatCaGroup_TabChiaSeBaiViet)
            {
                foreach (ListViewItem item in lvNhomChiaSeBaiViet.Items)
                {
                    item.Checked = true;
                    danhDauTatCaGroup_TabChiaSeBaiViet = true;
                }
            }
        }
    }
}
