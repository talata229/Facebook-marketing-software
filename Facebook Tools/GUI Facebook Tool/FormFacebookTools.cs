using Facebook_Tools.DAO;
using Facebook_Tools.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Facebook_Tools
{
    public partial class Form1 : Form
    {
        FacebookToolsFunction ftf = new FacebookToolsFunction();
        Dictionary<string, Token> dicCanLayToken = new Dictionary<string, Token>();
        Dictionary<string, Token> dicTokenLayDuoc = new Dictionary<string, Token>();
        int soDongFileLayToken = 0;
        int soDongToken = 0;
        string folder = "";
        bool dangGetToken = false;
        bool dangReactions = false;



        //Tab AutoLikeNewsFee
        public static string UID;
        public static List<string> listUser; //listUser 1 người dùng thôi
        public bool tiepTucThaTim = false;

        Dictionary<string, NewFeed1> dicReaction = new Dictionary<string, NewFeed1>();
        List<string> listAutoLikeNewFeeds = new List<string>();

        bool dangAutoLikeNewsFeed = false;
        List<string> listTypeAutoLikeNewsFeed = new List<string>();

        public Form1()
        {
            InitializeComponent();
        }

        private void btnOpenFile_GetToken_Click(object sender, EventArgs e)
        {
            OpenFileDialog theDialog = new OpenFileDialog();
            theDialog.Title = "Mở danh sách file text chứa Username và Password";
            theDialog.Filter = "TXT files|*.txt";
            theDialog.InitialDirectory = @"Desktop";
            if (theDialog.ShowDialog() == DialogResult.OK)
            {
                string path = theDialog.FileName.ToString();
                StreamReader sr = new StreamReader(path);
                string line = sr.ReadLine();
                int i = 1;
                while (line != null)
                {
                    Token tk = new Token();
                    string[] arr = line.Split('|');
                    tk.username = arr[0];
                    tk.password = arr[1];

                    dicCanLayToken.Add(tk.username, tk);

                    ListViewItem lvi = new ListViewItem(i + "");
                    if (i % 2 == 0) lvi.BackColor = Color.WhiteSmoke;
                    lvi.SubItems.Add(tk.username);
                    lvi.SubItems.Add(tk.password);
                    //lvi.Checked = true;
                    lvToken.Items.Add(lvi);
                    i++;
                    line = sr.ReadLine();
                    soDongFileLayToken++;
                }
                sr.Close();
                txtStatus.Text = theDialog.FileName;
                folder = Path.GetDirectoryName(theDialog.FileName); ;

            }
        }

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            int total = int.Parse(txtKhoangThoiGianGiua2LanLayToken.Text);
            for (int i = 0; i < dicCanLayToken.Count; i++)
            {
                string result = ftf.LayToken(dicCanLayToken.Keys.ElementAt(i), dicCanLayToken.Values.ElementAt(i).password);
                Token tk = new Token();
                tk.username = dicCanLayToken.Keys.ElementAt(i);
                tk.password = dicCanLayToken.Values.ElementAt(i).password;
                tk.sToken = result;

                if (!dicTokenLayDuoc.ContainsKey(dicCanLayToken.Keys.ElementAt(i)))
                {
                    dicTokenLayDuoc.Add(dicCanLayToken.Keys.ElementAt(i), tk);
                }
                for (int q = 1; q <= total; q++)
                {

                    backgroundWorker1.ReportProgress(q * 100 / total, tk);
                    Thread.Sleep(1000);
                    if (backgroundWorker1.CancellationPending)
                    {
                        e.Cancel = true;
                        backgroundWorker1.ReportProgress(0);
                        return;
                    }
                }
            }
        }

        private void backgroundWorker1_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            try
            {
                prBGetToken.Value = e.ProgressPercentage;
                Token tk = e.UserState as Token;
                txtStatus.Text = "Đang get token tài khoản: " + tk.username + "==>" + tk.sToken;
            }
            catch (Exception ex)
            {
                ex.Message.ToString();
                // throw;

            }
        }

        private void backgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            prBGetToken.Value = 0;
            MessageBox.Show("Đã get token xong. Thư mục hiện tại: " + folder, "THÔNG BÁO", MessageBoxButtons.OK, MessageBoxIcon.None);

            lvToken.Items.Clear();
            File.Delete("\\token_result.txt");
            StreamWriter sw = new StreamWriter(folder + "\\token_result.txt");
            for (int i = 0; i < dicTokenLayDuoc.Count; i++)
            {

                ListViewItem lvi = new ListViewItem(i + 1 + "");
                if (i % 2 == 0) lvi.BackColor = Color.WhiteSmoke;
                lvi.SubItems.Add(dicTokenLayDuoc.Values.ElementAt(i).username);
                lvi.SubItems.Add(dicTokenLayDuoc.Values.ElementAt(i).password);
                string ketquatoken = dicTokenLayDuoc.Values.ElementAt(i).sToken == "" || dicTokenLayDuoc.Values.ElementAt(i).sToken == null ? "KHÔNG LẤY ĐƯỢC TOKEN" : dicTokenLayDuoc.Values.ElementAt(i).sToken;
                // lvi.SubItems.Add(dicTokenLayDuoc.Values.ElementAt(i).sToken);
                lvi.SubItems.Add(ketquatoken);
                lvToken.Items.Add(lvi);
                sw.WriteLine(dicTokenLayDuoc.Values.ElementAt(i).username + "|" + dicTokenLayDuoc.Values.ElementAt(i).password + "|" + ketquatoken);
            }
            sw.Close();



        }

        private void button1_Click(object sender, EventArgs e)
        {
            StreamWriter sw = new StreamWriter("\\token_result2.txt");
            sw.WriteLine("Trần Văn Quang");
            sw.Close();
        }

        private void btnGetToken_Click(object sender, EventArgs e)
        {

            if (dangGetToken == false)
            {
                dangGetToken = true;
                btnGetToken.Text = "STOP";
                backgroundWorker1.RunWorkerAsync();
            }
            else if (dangGetToken)
            {
                btnGetToken.Text = "GET TOKEN";
                backgroundWorker1.CancelAsync();
                dangGetToken = false;
            }



        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        List<string> listDSToken = new List<string>();
        List<string> listTypeReactions = new List<string>();
        private void btnOpenFileReactions_Click(object sender, EventArgs e)
        {
            OpenFileDialog theDialog = new OpenFileDialog();
            theDialog.Title = "Mở danh sách file text chứa Token";
            theDialog.Filter = "TXT files|*.txt";
            theDialog.InitialDirectory = @"Desktop";
            if (theDialog.ShowDialog() == DialogResult.OK)
            {
                string path = theDialog.FileName.ToString();
                StreamReader sr = new StreamReader(path);
                string line = sr.ReadLine();
                int i = 1;
                while (line != null)
                {
                    listDSToken.Add(line);
                    ListViewItem lvi = new ListViewItem(i + "");
                    if (i % 2 == 0) lvi.BackColor = Color.WhiteSmoke;
                    lvi.SubItems.Add(line);
                    lvReactions.Items.Add(lvi);
                    i++;
                    line = sr.ReadLine();
                }
                sr.Close();
                txtStatus.Text = theDialog.FileName;
                folder = Path.GetDirectoryName(theDialog.FileName); ;

            }
        }

        private void backgroundWorker2_DoWork(object sender, DoWorkEventArgs e)
        {
            int total = int.Parse(txtThoiGianReactions.Text);
            Random rd = new Random();
            for (int i = 0; i < listDSToken.Count; i++)
            {
                bool result = ftf.FBReactionFromIDFull(listDSToken[i], txtIDPostReactions.Text, listTypeReactions[rd.Next(listTypeReactions.Count)]);
                for (int q = 1; q <= total; q++)
                {

                    backgroundWorker2.ReportProgress(q * 100 / total, result + " ==> " + listDSToken[i]);
                    Thread.Sleep(1000);
                    if (backgroundWorker2.CancellationPending)
                    {
                        e.Cancel = true;
                        backgroundWorker2.ReportProgress(0);
                        return;
                    }
                }
            }
        }

        private void backgroundWorker2_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            try
            {
                prbReactions.Value = e.ProgressPercentage;
                // txtStatus.Font.Size = 8.0;
                txtStatus.Text = e.UserState.ToString();
            }
            catch (Exception ex)
            {
                ex.Message.ToString();
                //throw;
            }

        }

        private void backgroundWorker2_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            prbReactions.Value = 0;
        }

        private void btnStartReactions_Click(object sender, EventArgs e)
        {
            if (chkLike.Checked) listTypeReactions.Add("LIKE");
            if (chkLove.Checked) listTypeReactions.Add("LOVE");
            if (chkWow.Checked) listTypeReactions.Add("WOW");
            if (chkHaha.Checked) listTypeReactions.Add("HAHA");
            if (chkSad.Checked) listTypeReactions.Add("SAD");
            if (chkAngry.Checked) listTypeReactions.Add("ANGRY");

            if (dangReactions == false)
            {
                dangReactions = true;
                btnStartReactions.Text = "STOP";
                backgroundWorker2.RunWorkerAsync();
            }
            else if (dangReactions)
            {
                btnStartReactions.Text = "START";
                backgroundWorker2.CancelAsync();
                dangReactions = false;
            }



        }

        private void btnCheckIDPost_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("http://www.facebook.com/" + txtIDPostReactions.Text);
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            //List<String> listTest = ftf.GetNameAndUIDFromToken(txtTokenAutoLikeNewsFeed.Text);
            //MessageBox.Show(listTest[0]);
            //MessageBox.Show(listTest[1]);
            dicReaction= ftf.FBListNewFeed2(txtTokenAutoLikeNewsFeed.Text, int.Parse(txtSoLuongBaiAutoLikeNewsFeed.Text), true);
            foreach(KeyValuePair< string, NewFeed1 > item in dicReaction)
            {
                MessageBox.Show(item.Key + "||||||" + item.Value);
            }
            //LoadNewsFeeds();
            //foreach (ListViewItem item in lvReactionNewsFeed.Items)
            //{
            //    listAutoLikeNewFeeds.Add(item.SubItems[1].Text);

            //}


        }

        private void btnStartAutoLikeNewsFeed_Click(object sender, EventArgs e)
        {
            HienThiAnhLenGUI();
            // picAutoLikeNewsFeed.Load(ff.getCoverFromUID(objFB[1]));
            LoadNewsFeeds();
            if (chkNoneAutoLikeNewsFeed.Checked) listTypeAutoLikeNewsFeed.Add("NONE");
            if (chkLikeAutoLikeNewsFeed.Checked) listTypeAutoLikeNewsFeed.Add("LIKE");
            if (chkLoveAutoLikeNewsFeed.Checked) listTypeAutoLikeNewsFeed.Add("LOVE");
            if (chkWowAutoLikeNewsFeed.Checked) listTypeAutoLikeNewsFeed.Add("WOW");
            if (chkHahaAutoLikeNewsFeed.Checked) listTypeAutoLikeNewsFeed.Add("HAHA");
            if (chkSadAutoLikeNewsFeed.Checked) listTypeAutoLikeNewsFeed.Add("SAD");
            if (chkAngryAutoLikeNewsFeed.Checked) listTypeAutoLikeNewsFeed.Add("ANGRY");



            listAutoLikeNewFeeds.Clear();
            if (dangAutoLikeNewsFeed == false)
            {
                dangAutoLikeNewsFeed = true;
                btnStartAutoLikeNewsFeed.Text = "Stop";

                foreach (ListViewItem item in lvReactionNewsFeed.Items)
                {
                    listAutoLikeNewFeeds.Add(item.SubItems[1].Text);
                    //MessageBox.Show(listAutoLikeNewFeeds[0]);
                }


                backgroundWorker3.RunWorkerAsync();

            }
            else if (dangAutoLikeNewsFeed)
            {
                btnStartAutoLikeNewsFeed.Text = "Start";

                backgroundWorker3.CancelAsync();
                dangAutoLikeNewsFeed = false;
            }
        }

        private void LoadNewsFeeds()
        {
            //Xóa hết cái cũ
            lvReactionNewsFeed.Items.Clear();
            dicReaction.Clear();
            listAutoLikeNewFeeds.Clear();

            //Hiển thị danh sách các bài cần react lên listview trước
            dicReaction = ftf.FBListNewFeed2(txtTokenAutoLikeNewsFeed.Text, int.Parse(txtSoLuongBaiAutoLikeNewsFeed.Text));
            int i = 1;
            foreach (KeyValuePair<string, NewFeed1> item in dicReaction)
            {
                ListViewItem lvi = new ListViewItem(i + "");
                if (i % 2 == 0) lvi.BackColor = Color.WhiteSmoke;
                NewFeed1 nf1 = item.Value as NewFeed1;

                lvi.Checked = true;
                lvi.SubItems.Add(nf1.id);
                //Thêm vào list
                listAutoLikeNewFeeds.Add(nf1.id);

                //Xử lý hiển thị thời gian
                string time1 = nf1.created_time;
                string time2 = time1.Remove(19);
                string time3 = time2.Replace("T", "===");
                lvi.SubItems.Add(time3);

                lvi.SubItems.Add(nf1.message);
                lvReactionNewsFeed.Items.Add(lvi);

                //  listCanReaction.Add(item.Key);
                i++;
            }

            //foreach (ListViewItem item in lvReactionNewsFeed.Items)
            //{
            //    listAutoLikeNewFeeds.Add(item.SubItems[1].Text);
            //    //MessageBox.Show(listAutoLikeNewFeeds[0]);
            //}
        }

        private void LoadNewsFeedsLanThu2()
        {
            //Xóa hết cái cũ
            lvReactionNewsFeed.Items.Clear();
            dicReaction.Clear();
            listAutoLikeNewFeeds.Clear();

            //Hiển thị danh sách các bài cần react lên listview trước
            dicReaction = ftf.FBListNewFeed2(txtTokenAutoLikeNewsFeed.Text, int.Parse(txtSoLuongBaiAutoLikeNewsFeed.Text),true);
            int i = 1;
            foreach (KeyValuePair<string, NewFeed1> item in dicReaction)
            {
                ListViewItem lvi = new ListViewItem(i + "");
                if (i % 2 == 0) lvi.BackColor = Color.WhiteSmoke;
                NewFeed1 nf1 = item.Value as NewFeed1;

                lvi.Checked = true;
                lvi.SubItems.Add(nf1.id);
                //Thêm vào list
                listAutoLikeNewFeeds.Add(nf1.id);

                //Xử lý hiển thị thời gian
                string time1 = nf1.created_time;
                string time2 = time1.Remove(19);
                string time3 = time2.Replace("T", "===");
                lvi.SubItems.Add(time3);

                lvi.SubItems.Add(nf1.message);
                lvReactionNewsFeed.Items.Add(lvi);

                //  listCanReaction.Add(item.Key);
                i++;
            }

            //foreach (ListViewItem item in lvReactionNewsFeed.Items)
            //{
            //    listAutoLikeNewFeeds.Add(item.SubItems[1].Text);
            //    //MessageBox.Show(listAutoLikeNewFeeds[0]);
            //}
        }

        private void HienThiAnhLenGUI()
        {
            listUser = ftf.GetNameAndUIDFromToken(txtTokenAutoLikeNewsFeed.Text);
            picAutoLikeNewsFeed.Load("https://graph.facebook.com/" + listUser[0] + "/picture?type=normal");
            lbNameAutoLikeNewsFeed.Text = listUser[1];
        }

        private void backgroundWorker3_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
               
                Random rd = new Random();
                int total = rd.Next(int.Parse(txtTuAutoLikeNewsFeed.Text), int.Parse(txtDenAutoLikeNewsFeed.Text));
                //MessageBox.Show("Total la" + total);
                int stt = 0;
                do
                {
                    bool result = ftf.FBReactionFromIDFull(txtTokenAutoLikeNewsFeed.Text, listAutoLikeNewFeeds[stt], listTypeAutoLikeNewsFeed[rd.Next(listTypeAutoLikeNewsFeed.Count)]);
                    for (int q = 1; q <= total; q++)
                    {
                        backgroundWorker3.ReportProgress(q * 100 / total, result + " ==> " + listAutoLikeNewFeeds[stt]);
                        Thread.Sleep(1000);
                        if (backgroundWorker3.CancellationPending)
                        {
                            e.Cancel = true;
                            backgroundWorker3.ReportProgress(0);
                            return;
                        }
                    }
                    stt++;
                    if(stt==listAutoLikeNewFeeds.Count)
                    {
                        tiepTucThaTim = true;
                       // MessageBox.Show("Có chạy vào đây");
                      //  LoadNewsFeedsLanThu2();
                      // backgroundWorker3.RunWorkerAsync();
                    }
                } while (listAutoLikeNewFeeds.Count > 0);
            }
            catch (Exception ex)
            {
                ex.Message.ToString();
            }

        }

        private void backgroundWorker3_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            try
            {
                prBAutoLikeNewsFeed.Value = e.ProgressPercentage;
                // txtStatus.Font.Size = 8.0;
                txtStatus.Text = e.UserState.ToString();
            }
            catch (Exception ex)
            {
                ex.Message.ToString();
                //throw;
            }
        }

        private void backgroundWorker3_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
           // backgroundWorker3.RunWorkerAsync();
            MessageBox.Show("Đã Auto thả tim xong");
            if (tiepTucThaTim)
            {
                tiepTucThaTim = false;
                LoadNewsFeedsLanThu2();
                backgroundWorker3.RunWorkerAsync();
            }
            
            //MessageBox.Show()

        }

        private void button2_Click(object sender, EventArgs e)
        {
            foreach (string temp in listAutoLikeNewFeeds)
            {
                MessageBox.Show(temp);
            }
        }

        private void txtTokenAutoLikeNewsFeed_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control && e.KeyCode == Keys.A)
            {
                if (sender != null)
                    ((TextBox)sender).SelectAll();
            }
        }
    }
}
