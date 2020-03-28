using FacebookMarketing.Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using xNet;
using System.Web;
using FacebookMarketing.Model.Json;
using System.Threading;

namespace FacebookMarketing.Functions
{
    public class FullFunction
    {
        string type = "application/x-www-form-urlencoded";
        List<String> objFb = new List<string>();
        HttpRequest http = new HttpRequest();

        public static void _Test()
        {
            MessageBox.Show("Day la functions test");
        }

        //private void btnLogin_Click(object sender, EventArgs e)
        //{
        //    // DangNhapFacebook("tranquanghero%40gmail.com", "emcoyeuanhkhong");
        //    //objFb = DangNhapFacebook("tranquanghero@gmail.com", "emcoyeuanhkhong");
        //    objFb = DangNhapFacebook("muathudep150@gmail.com", "tranquanghero");

        //    // objFb = DangNhapFacebook(txtUsername.Text, txtPassword.Text);
        //    //MessageBox.Show(objFb[0]); //Cookifinal
        //    // MessageBox.Show(objFb[1]);//id
        //    //MessageBox.Show(objFb[2]);//fb_dtsg
        //    MessageBox.Show(objFb[3]);//token

        //    FB_ListGroup(objFb);
        //    // FB_m(objFb ,"100001578994326", "Anh nhow em222");
        //    // FB_LikeAPostViaToken("1821076337954982", objFb[3]);
        //    //MessageBox.Show(FB_Post(objFb, "t1", objFb[1]));
        //    // MessageBox.Show(FB_Post_WithToken(objFb, "post lêntường cá nhân này", objFb[1]));
        //    // MessageBox.Show(FB_Post(objFb, "post lêntường cá nhân này lần 2", objFb[1]));


        //}
        public List<String> DangNhapFacebook(string username, string password)
        {
            try
            {
                List<String> RET = new List<string>();
               http.Cookies = new CookieDictionary();

                http.UserAgent = Http.ChromeUserAgent();
                string html = http.Get("https://www.facebook.com/").ToString();
                string urlLogin = "https://www.facebook.com/login.php?login_attempt=1&lwv=110";
                string data = "&email=" + username + "&pass=" + password;
                html = http.Post(urlLogin, data, type).ToString();

                string cookiFinal = http.Cookies.ToString();
                string id = Regex.Match(html, "\"ACCOUNT_ID\":\"(.*?)\"").Groups[1].Value;
                string fb_dtsg = Regex.Match(html, "name=\"fb_dtsg\" value=\"(.*?)\"").Groups[1].Value;
                string urlToken = "https://www.facebook.com/" + id;
                string source = http.Get(urlToken).ToString();
                string token = Regex.Match(source, "access_token:\"(.*?)\",").Groups[1].Value;
                string tokenFullQuyenLayTuAPp = LayToken(username, password);
                RET.Add(cookiFinal);  //0
                RET.Add(id);//1
                RET.Add(fb_dtsg);//2
                RET.Add(token);//3
                RET.Add(tokenFullQuyenLayTuAPp);//4

                return RET;
            }
            catch (Exception ex)
            {
                ex.Message.ToString();
                return null;
            }


        }
        public void Html_Test(string html)
        {
            File.WriteAllText("b.html", html);
            Process.Start("b.html");
            // MessageBox.Show("Da xong");
        }

        public string FB_Comment(List<string> RET, string PostID, string content = "Xin chào các bạn", int Sticker = 0)
        {
            string[] Stickers = new string[33] { "", "126361874215276", "126362187548578", "126361967548600", "126362100881920", "126362137548583", "126361920881938", "126362064215257", "126361974215266", "126361910881939", "126361987548598", "126361994215264", "126362007548596", "126362027548594", "126362044215259", "126362074215256", "126362080881922", "126362087548588", "126362107548586", "126362117548585", "126362124215251", "126362130881917", "126362160881914", "126362167548580", "126362180881912", "126362197548577", "126362207548576", "126361900881940", "126361884215275", "126361957548601", "126361890881941", "126362034215260", "126362230881907" };
            string data = "ft_ent_identifier=" + PostID + "&comment_text=" + content + "&attached_sticker_fbid=" + Stickers[Sticker] + "&source=1&client_id=1&session_id=1&fb_dtsg=" + RET[2];
            string s = http.Post("https://www.facebook.com/ufi/add/comment/", data, type).ToString();
            return s;
        }
        public void FB_m(List<string> RET, string id, string content = "wew332")
        {
            string urlSendM = "https://m.facebook.com/messages/send/?icm=1";
            //content = "Chào bạn nhé ahihi thử thành công1";
            //fb_dtsg = AQFJiHGtLaZJ % 3AAQFnRlUvXWMs & body = Chao + em1 & send = G % E1 % BB % ADi & tids = cid.c.100001578994326 % 3A100012897726970 & wwwupp = C3 & ids % 5B100012897726970 % 5D = 100012897726970 & referrer = &ctype = &cver = legacy & csid = 194b7608 - 4564 - 90cb - 0946 - 1092e09a399c
            //string dataSendM = "fb_dtsg=" + RET[2] + "&body=" + content + "&send=G%E1%BB%ADi&tids=cid.c.100001578994326%3A100012897726970&wwwupp=C3&ids%5B100012897726970%5D=100012897726970&referrer=&ctype=&cver=legacy&csid=194b7608-4564-90cb-0946-1092e09a399c";
            string dataSendM = "Send=G%E1%BB%ADi&ids%5B" + id + "%5D=" + id + "&body=" + content + "&fb_dtsg=" + RET[2];
            string result = http.Post(urlSendM, dataSendM, type).ToString();
            //Html_Test(result);

        }
        public string FB_Share(List<string> RET, string IDPost, string content = "Noi dung chia se", string IDTartget = "")
        {
            string url = "https://graph.facebook.com/" + IDPost + "/sharedposts?to=" + IDTartget + "&locale=en_US&method=post&return_structure=true&message=" + content + "&access_token=" + RET[3];
            string s = http.Get(url).ToString();

            string result = Regex.Match(s, "\"id\": \"(.*?)\"").Groups[1].Value;
            return result;
        }

        public void FB_Tag(List<string> RET, string IDPost, string content = "Nội dung tag", @List<string> ListUID = default(List<string>), int SoLuongTag1Lan = 3, int ThoiGianGiua2LanTag = 15000)
        {
            if (ListUID == default(List<string>)) ListUID = new List<string>();
            string sTag = "";
            for (int i = 0; i < SoLuongTag1Lan; i++)
            {
                if (ListUID.Count >= SoLuongTag1Lan)
                {
                    if (ListUID[i] != null)
                    {
                        sTag += "+@[" + ListUID[i] + ":0]";
                    }
                    else
                    {
                        sTag += "";
                    }
                }
                else
                {
                    sTag += "";
                }

            }
            for (int i = 0; i < SoLuongTag1Lan; i++)
            {

                if (ListUID.Count >= SoLuongTag1Lan)
                {
                    if (ListUID[i] != null)
                    {
                        ListUID.RemoveAt(0);
                    }
                    else
                    {
                        break;
                    }
                }
                else
                {
                    break;
                }

            }

            while (ListUID.Count > 0)
            {

                string url = "https://graph.facebook.com/" + IDPost + "/comments?method=post&access_token=" + RET[3] + "&message=" + content + sTag;
                string s = http.Get(url).ToString();
                string result = Regex.Match(s, "\"id\": \"(.*?)\"").Groups[1].Value;
                MessageBox.Show(result);
                Thread.Sleep(ThoiGianGiua2LanTag);
                FB_Tag(RET, IDPost, content, ListUID, SoLuongTag1Lan);

            }



            // return result;
        }

        public void FB_TagVersion2(List<string> RET, string IDPost, string content = "Nội dung tag", List<string> ListUID = default(List<string>), int SoLuongTag1Lan = 3, int ThoiGianGiua2LanTag = 15000)
        {
            if (ListUID == default(List<string>)) ListUID = new List<string>();
            int dem = 0;
            string sTag = "";
            int i = 0;
            int chiSoHienTai = 1;
            do
            {
                if (chiSoHienTai + SoLuongTag1Lan <= ListUID.Count - 1)
                {
                    for (int j = 0; j < SoLuongTag1Lan - 1; j++)
                    {
                        sTag += "+@[" + ListUID[chiSoHienTai] + ":0]";
                        chiSoHienTai++;
                    }


                    string url = "https://graph.facebook.com/" + IDPost + "/comments?method=post&access_token=" + RET[3] + "&message=" + content + sTag;
                    string s = http.Get(url).ToString();
                    string result = Regex.Match(s, "\"id\": \"(.*?)\"").Groups[1].Value;
                    MessageBox.Show("Van o trong iffLan lap thu = " + chiSoHienTai);
                    Thread.Sleep(ThoiGianGiua2LanTag);
                    FB_TagVersion2(RET, IDPost, content, ListUID, SoLuongTag1Lan);
                    sTag = "";
                }
                else
                {
                    for (int j = chiSoHienTai; j < ListUID.Count - 1; j++)
                    {
                        sTag += "+@[" + ListUID[chiSoHienTai] + ":0]";
                        chiSoHienTai++;
                    }
                    string url = "https://graph.facebook.com/" + IDPost + "/comments?method=post&access_token=" + RET[3] + "&message=" + content + sTag;
                    string s = http.Get(url).ToString();
                    string result = Regex.Match(s, "\"id\": \"(.*?)\"").Groups[1].Value;
                    MessageBox.Show("O trong else lap thu = " + chiSoHienTai);
                    Thread.Sleep(ThoiGianGiua2LanTag);
                    FB_TagVersion2(RET, IDPost, content, ListUID, SoLuongTag1Lan);
                    sTag = "";
                    // sTag = "";
                }
                i++;


                //             If $chiSoHienTai + $soLuong <= UBound($arrUID) - 1 Then
                //         For $j = 0 To $soLuong - 1
                //	$tempNoiDung &= '@[' & $arrUID[$chiSoHienTai] & ':0] '
                //	$chiSoHienTai = $chiSoHienTai + 1

                //         Next
                //$noiDungBinhLuan = $tempNoiDung & @CRLF & $noiDungCmt

                //         FB_Comment($obj, $idPost, $noiDungBinhLuan)
                //$tempNoiDung = ''

                //     Else
                //         For $j = $chiSoHienTai To UBound($arrUID) - 1
                //	$tempNoiDung &= '@[' & $arrUID[$chiSoHienTai] & ':0] '
                //	$chiSoHienTai = $chiSoHienTai + 1

                //         Next
                //$noiDungBinhLuan = $tempNoiDung & @CRLF & $noiDungCmt

                //         FB_Comment($obj, $idPost, $noiDungBinhLuan)

                //         ExitLoop
                //$tempNoiDung = ''


                //     EndIf
            } while (i >= ListUID.Count);
        }


        public void FB_Share2(List<string> RET, string IDPost, string content = "Noi dung chia se", string IDTartget = "")
        {
            //Hàm này chưa thành công
            string url = "https://www.facebook.com/share/dialog/submit/?app_id=25554907596&audience_type=fiend&audience_targets[0]=" + IDTartget;


            url += "&message=" + content;
            url += "&post_id=" + IDPost;
            url += "&privacy=";
            url += "&share_type=22";
            url += "&is_forced_reshare_of_post=true";
            url += "&source=1";
            string html = http.Post(url, "fb_dtsg=" + RET[2]).ToString();
            //MessageBox.Show("Đã chia sẻ xong,a hihih");


        }
        public void FB_LikeAPostViaToken(string idPost, string token)
        {
            //  string idPost = "";
            string urlLike = "https://graph.facebook.com/v2.12/" + idPost + "/likes?method=post&access_token=" + token;

            string result = http.Get(urlLike).ToString();
            Html_Test(result);
        }


        public string FB_Post(List<string> Handle, string noiDung, string FB_ID)
        {
            //Không hiểu sao cứ chạy là đơ máy, post đc ở chế độ công khai
            string result = "Nội dung bài post mặc định";
            string data = "privacyx=300645083384735&xhpc_targetid=" + FB_ID + "&xhpc_message=" + noiDung + "&fb_dtsg=" + Handle[2];
            //	data  "privacyx=300645083384735&xhpc_targetid=100024544302593&xhpc_message=To chao moi ng&fb_dtsg=AQF0cEesVOoX:AQFGBj_cUCp9"	string

            // http.Referer = "https://www.facebook.com/profile.php?id="+ FB_ID;
            result = http.Post("https://www.facebook.com/ajax/updatestatus.php", data, type).ToString();
            // Clipboard.SetText(result);
            //MessageBox.Show(result);
            // Html_Test(result);
            return result;
        }
        public string FB_Post_WithToken(List<string> Handle, string noiDung, string FB_ID)
        {
            string result = "";
            //string data = "privacyx=300645083384735&xhpc_targetid=" + FB_ID + "&xhpc_message=" + noiDung + "&fb_dtsg=" + Handle[2];
            //	data  "privacyx=300645083384735&xhpc_targetid=100024544302593&xhpc_message=To chao moi ng&fb_dtsg=AQF0cEesVOoX:AQFGBj_cUCp9"	string
            try
            {
                result = http.Get("https://graph.facebook.com/v3.0/" + FB_ID + "/feed?method=post&message=" + noiDung + "&access_token=" + Handle[3]).ToString();
                Clipboard.SetText(result);
            }
            catch (Exception ex)
            {
                FB_Post(Handle, noiDung, FB_ID);
                ex.Message.ToString();
            }
            // http.Referer = "https://www.facebook.com/profile.php?id="+ FB_ID;


            // MessageBox.Show(result);
            // Html_Test(result);
            return result;
        }

        public string FB_Post_WithToken2(List<string> Handle, string noiDung, string FB_ID)
        {
            string result = "";
            //string data = "privacyx=300645083384735&xhpc_targetid=" + FB_ID + "&xhpc_message=" + noiDung + "&fb_dtsg=" + Handle[2];
            //	data  "privacyx=300645083384735&xhpc_targetid=100024544302593&xhpc_message=To chao moi ng&fb_dtsg=AQF0cEesVOoX:AQFGBj_cUCp9"	string
            try
            {
                result = http.Get("https://graph.facebook.com/v3.0/" + FB_ID + "/feed?method=post&message=" + noiDung + "&access_token=" + Handle[4]).ToString();
                Clipboard.SetText(result);
            }

            catch (Exception ex)
            {
                ex.Message.ToString();
            }

            // http.Referer = "https://www.facebook.com/profile.php?id="+ FB_ID;


            // MessageBox.Show(result);
            // Html_Test(result);
            return result;
        }

        private void Facebook_Load(object sender, EventArgs e)
        {
            //objFb = DangNhapFacebook("muathudep150@gmail.com", "tranquanghero");
            //FB_ListGroup(objFb);
            string text = "One car red car blue car";
            string pat = @"(\w+)\s+(car)";

            // Instantiate the regular expression object.
            Regex r = new Regex(pat, RegexOptions.IgnoreCase);

            // Match the regular expression pattern against a text string.
            Match m = r.Match(text);
            int matchCount = 0;
            while (m.Success)
            {
                Console.WriteLine("Match" + (++matchCount));
                for (int i = 1; i <= 2; i++)
                {
                    Group g = m.Groups[i];
                    Console.WriteLine("Group" + i + "='" + g + "'");
                    CaptureCollection cc = g.Captures;
                    for (int j = 0; j < cc.Count; j++)
                    {
                        Capture c = cc[j];
                        System.Console.WriteLine("Capture" + j + "='" + c + "', Position=" + c.Index);
                    }
                }
                m = m.NextMatch();
            }
        }

        public void FB_ListGroup(List<string> Handle)
        {
            string url = "https://www.facebook.com/bookmarks/groups/";
            string html = http.Get(url).ToString();
            string ListFilter = Regex.Match(html, "BookmarkSeeAllEntsSectionController(.*?)</script>").Groups[1].Value;
            //Clipboard.SetText(ListFilter);
            string value = "{id:\"(\\d+?)\",name:\"(.*?)\",count:";
            MatchCollection matches = Regex.Matches(ListFilter, value);

            foreach (Match match in matches)
            {
                // txtTest.Text += match.Groups[1].Value;
                // txtTest.Text += match.Groups[2].Value;
                //ListViewItem lvi = new ListViewItem((lvGroup.Items.Count + 1) + "");
                // lvi.SubItems.Add(match.Groups[1].Value);
                // lvi.SubItems.Add(match.Groups[2].Value);
                // lvGroup.Items.Add(lvi);
            }

            Console.ReadLine();

        }


        public string getCoverFromUID(string id)
        {
            string result = "";
            try
            {
                result = "https://scontent.fhan2-3.fna.fbcdn.net/v/t1.0-9/32745701_185070642317166_8489549919118426112_o.jpg?_nc_cat=0&oh=667a1c63038f25b1df047b4cb718eb1b&oe=5B9A1956";
                string url = "https://graph.facebook.com/" + id + "?fields=cover&access_token=EAAAAUaZA8jlABADwyrLfvkvXd2yDc6D2WUtGpHcuiPHchQrlqfa42J8F0ZCSF9JRbfFZCqnZCnry0KrgZAkifBorlroe9YOdzoO7kRZBJWuRddytzag9RKHJAtYMAdP3KqVWXuMabMNVNHhZCo4AiKvOLxbLNJ3UaLjHJAo7LZBVEtns5NNVALEu";
                string html = http.Get(url).ToString();
                Clipboard.SetText(html);
                CoverWallpaper cw = new CoverWallpaper();
                //Newtonsoft.Json.JsonConvert.PopulateObject(html, cw);
                JsonConvert.PopulateObject(html, cw);
                return result = cw.cover["source"];
            }
            catch (Exception ex)
            {
                ex.Message.ToString();
            }

            // MessageBox.Show(result);
            return result;
        }

        public Dictionary<string, BaiPost> getListFileFromPath(string path)
        {
            Dictionary<string, BaiPost> result = new Dictionary<string, BaiPost>();
            DirectoryInfo d = new DirectoryInfo(path);//Assuming Test is your Folder
            FileInfo[] Files = d.GetFiles("*.txt"); //Getting Text files
            string[] textFiles = Directory.GetFiles(path, "*.txt").Select(Path.GetFileName).ToArray();
            List<string> myListFileName = textFiles.ToList();
            List<string> myListContent = new List<string>();
            foreach (string file in Directory.EnumerateFiles(path))
            {
                //  string contents = ;

                myListContent.Add(File.ReadAllText(file));
                // MessageBox.Show(contents);
                //      BaiPost bp = new BaiPost();


            }
            for (int i = 0; i < myListFileName.Count; i++)
            {
                BaiPost bp = new BaiPost();
                bp.Ma = myListFileName[i];
                bp.NoiDung = myListContent[i];
                bp.Loai = true;
                result.Add(myListFileName[i], bp);
            }


            return result;
        }
        public Dictionary<string, string> getListFileContentCommentFromPath(string path)
        {
            Dictionary<string, string> result = new Dictionary<string, string>();
            DirectoryInfo d = new DirectoryInfo(path);//Assuming Test is your Folder
            FileInfo[] Files = d.GetFiles("*.txt"); //Getting Text files
            string[] textFiles = Directory.GetFiles(path, "*.txt").Select(Path.GetFileName).ToArray();
            List<string> myListFileName = textFiles.ToList();
            List<string> myListContent = new List<string>();
            foreach (string file in Directory.EnumerateFiles(path))
            {
                //  string contents = ;

                myListContent.Add(File.ReadAllText(file));
                // MessageBox.Show(contents);
                //      BaiPost bp = new BaiPost();


            }
            for (int i = 0; i < myListFileName.Count; i++)
            {

                result.Add(myListFileName[i], myListContent[i]);
            }


            return result;
        }

        public Dictionary<string, string> FBListGroupFromUID(List<string> Handle, string UID)
        {
            try
            {
                Dictionary<string, string> result = new Dictionary<string, string>();
                string html = http.Get("https://graph.facebook.com/v3.0/" + UID + "?fields=groups.limit(1000)&access_token=" + Handle[3]).ToString();//lấy dạng json
                Clipboard.SetText(html);//gửi vào clipboard để xem
                Nhom nh = JsonConvert.DeserializeObject<Nhom>(html);
                Nhom2 nh2 = nh.groups;
                List<Nhom3> listNhom3 = nh2.data.ToList();
                for (int i = 0; i < listNhom3.Count; i++)
                {
                    Nhom3 n3 = listNhom3[i];
                    result.Add(n3.id, n3.name);
                    //MessageBox.Show("Ten = " + n3.name);
                }

                return result;
            }
            catch (Exception ex)
            {
                ex.Message.ToString();
                return null;
            }

        }

        public Dictionary<string, string> FBListFriendFromUID(List<string> Handle, string UID)
        {
            Dictionary<string, string> result = new Dictionary<string, string>();
            string html = http.Get("https://graph.facebook.com/" + UID + "?fields=friends.fields(id,name)&access_token=" + Handle[3]).ToString();
            Clipboard.SetText(html);
            Friend fr = JsonConvert.DeserializeObject<Friend>(html);
            Friend2 fr2 = fr.friends;
            List<Friend3> listFriend3 = fr2.data.ToList();
            for (int i = 0; i < listFriend3.Count; i++)
            {
                Friend3 fr3 = listFriend3[i];
                result.Add(fr3.id, fr3.name);
                //    MessageBox.Show("Ten = " + fr3.name);
            }


            return result;
        }



        public Dictionary<string, string> FBListUIDComentFromPost(List<string> Handle, string IDPost)
        {
            try
            {
                Dictionary<string, string> result = new Dictionary<string, string>();
                //ahihi
                string s = http.Get("https://graph.facebook.com/v3.0/" + KiemTraLink(IDPost) + "?fields=comments&access_token=" + Handle[3]).ToString();
                //  MessageBox.Show(s);
                Tag1 tag1 = JsonConvert.DeserializeObject<Tag1>(s);
                Tag2 tag2 = tag1.comments;
                List<Tag3> ltag3 = tag2.data.ToList();
                for (int i = 0; i < ltag3.Count; i++)
                {
                    Tag4 tag4 = ltag3[i].from;
                    //result.Add(n3.id, n3.name);
                    if (!result.ContainsKey(tag4.id))
                        result.Add(tag4.id, tag4.name);
                    //MessageBox.Show("Ten = " + tag4.id);
                }
                return result;
            }
            catch (Exception ex)
            {
                ex.Message.ToString();
                return null;
            }

        }
        public Dictionary<string, string> FBListNewFeed(List<string> Handle)
        {
            Dictionary<string, string> result = new Dictionary<string, string>();
            string s = http.Get("https://graph.facebook.com/me/home?fields=id,created_time,message&access_token=" + Handle[4]).ToString();
            //MessageBox.Show(s);
            NewFeed nf = JsonConvert.DeserializeObject<NewFeed>(s);
            List<NewFeed1> lnf = nf.data.ToList();
            for (int i = 0; i < lnf.Count; i++)
            {
                //NewFeed1 nf1 = lnf[i].id;
                //result.Add(n3.id, n3.name);
                if (!result.ContainsKey(lnf[i].id))
                    result.Add(lnf[i].id, lnf[i].message);
                //  MessageBox.Show(lnf[i].id + "|" + lnf[i].message);
            }

            return result;
        }

        public Dictionary<string, NewFeed1> FBListNewFeed2(List<string> Handle, int limit = 25)
        {
            //Dùng tốt
            Dictionary<string, NewFeed1> result = new Dictionary<string, NewFeed1>();
            // Clipboard.SetText(Handle[4]);
            string s = http.Get("https://graph.facebook.com/me/home?fields=id,created_time,message&limit=" + limit + "&access_token=" + Handle[4]).ToString();
            //MessageBox.Show(s);
            NewFeed nf = JsonConvert.DeserializeObject<NewFeed>(s);
            List<NewFeed1> lnf = nf.data.ToList();
            for (int i = 0; i < lnf.Count; i++)
            {
                NewFeed1 nf1 = lnf[i];
                nf1.id = lnf[i].id;
                nf1.created_time = lnf[i].created_time;
                nf1.message = lnf[i].message;
                if (!result.ContainsKey(lnf[i].id))
                    result.Add(lnf[i].id, nf1);
                // MessageBox.Show(nf1.ToString());
            }

            return result;
        }

        public bool FBReactionFromIDFull(List<string> Handle, string IDFull, int type = 1)
        {
            // 0     1     2      3   4     5    6       
            //NONE, LIKE, LOVE, WOW, HAHA, SAD, ANGRY, THANKFUL, PRIDE
            string stype = "";
            if (type == 0) stype = "NONE";
            if (type == 1) stype = "LIKE";
            if (type == 2) stype = "LOVE";
            if (type == 3) stype = "WOW";
            if (type == 4) stype = "HAHA";
            if (type == 5) stype = "SAD";
            if (type == 6) stype = "ANGRY";
            if (type < 0 || type > 6) stype = "LIKE";
            string s = http.Get("https://graph.facebook.com/" + IDFull + "/reactions?type=" + stype + "&method=post&access_token=" + Handle[4]).ToString();

            //bool ketQua = Regex.Match(s, "\"success\": (.*?)\").Groups[1].Value;
            return s.Contains("success");
            //return result;
        }
        public string RandomChuoi(string input)
        {
            //    { Tôi | Tớ | T} { hiện là | đang là | là | hiện tại đang là} { sv | sinh viên | 1 sv } { tại | ở | trong | của } { Bách Khoa | bách khoa | bk | trường bk | ĐH bk}
            if (input.IndexOf('|') != -1)
            {
                string result = "";
                int soLuongCumChuoi = input.Count(x => x == '}');
                string[] CumChuoiNho = input.Split('}');
                string[] CumChuoiNhoNew = new string[CumChuoiNho.Length];
                CumChuoiNho.CopyTo(CumChuoiNhoNew, 0);


                Random rd = new Random();
                List<string> list = new List<string>();
                for (int i = 0; i < CumChuoiNho.Length; i++)
                {
                    //  Để xóa { đầu CumChuoiNho
                    CumChuoiNhoNew[i] = CumChuoiNho[i].Replace("{", "");
                }
                for (int i = 0; i < soLuongCumChuoi; i++)
                {
                    string[] tuTrongCumChuoiNho = CumChuoiNhoNew[i].Split('|');
                    int index = rd.Next(tuTrongCumChuoiNho.Length);
                    list.Add(tuTrongCumChuoiNho[index]);

                }
                foreach (string s in list)
                {
                    result += s;
                }
                // MessageBox.Show(result);
                return result;
            }
            else return input;

        }
        public string KiemTraLink(string input)
        {
            string result = "";
            if (input.Contains("groups") && input.Contains("permalink"))
            {
                //httkps://www.facebook.com/groups/1785277221722270/permalink/2119777688272220/
                //Trường hợp cho link trong group
                int vitri = input.IndexOf("k/");
                string temp = input.Substring(vitri + 2);
                result = temp.Replace("/", "");
                //Console.WriteLine(result);
                return result;
            }
            else if (input.Contains("permalink") && input.Contains("story_fbid") && input.Contains("id"))
            {
                //https://www.facebook.com/permalink.php?story_fbid=179683402862218&id=100024616790966
                //Trường hợp trên tường của mình
                int vitri = input.IndexOf("d=");
                char[] mangChar = input.ToCharArray();
                string temp = input.Substring(vitri + 2);
                if (input.IndexOf("&id") != -1)
                {

                    for (int i = vitri + 2; i < input.IndexOf("&i"); i++)
                    {
                        result += mangChar[i];
                    }
                    return result;
                }
                else
                {
                    result = temp.Replace("/", "");
                    Console.WriteLine(result);
                    return result;
                }
            }
            else if (input.Contains("post"))
            {
                //https://www.facebook.com/giang.tra.3572/posts/147636226068605
                //Trường hợp trên tường của bạn bè
                int vitri = input.IndexOf("ts/");
                string temp = input.Substring(vitri + 3);
                result = temp.Replace("/", "");
                // Console.WriteLine(result);
                return result;
            }
            else if (input.Contains("videos"))
            {
                //https://www.facebook.com/NeuConfessions.VN/videos/1612179665565972/
                //Trường hợp cho video trên Fanpage
                int vitri = input.IndexOf("os/");
                string temp = input.Substring(vitri + 3);
                result = temp.Replace("/", "");
                // Console.WriteLine(result);
                return result;
            }
            else if (input.Contains("photo") && input.Contains("set"))
            {
                //https://www.facebook.com/photo.php?fbid=224172305020428&set=p.224172305020428&type=3&theater
                //Trường hợp bạn bè up ảnh của họ
                int vitri = input.IndexOf("id=");
                char[] mangChar = input.ToCharArray();
                string temp = input.Substring(vitri + 3);
                if (input.IndexOf("fbid") != -1)
                {
                    for (int i = vitri + 3; i < input.IndexOf("&set"); i++)
                    {
                        result += mangChar[i];
                    }
                    return result;
                }
                else
                {
                    result = temp.Replace("/", "");
                    return result;
                }
            }
            return input;
        }

        public string _Md5Encode(string stringInput)
        {
            //string type = "application/x-www-form-urlencoded";
            //List<String> objFb = new List<string>();
            //HttpRequest http = new HttpRequest();

            // stringInput = "api_key=882a8490361da98702bf97a021ddc14demail=nghiahsgsformat=JSONlocale=vi_vnmethod=auth.loginpassword=cogaitoiyeureturn_ssl_resources=0v=1.062f8ce9f74b12f84c123cc23437a4a32";
            string data = http.Post("http://www.md5.cz/getmd5.php", "what=" + stringInput, type).ToString();

            //Html_Test(data);
            string[] arr = data.Split('|');
            Console.WriteLine(arr[0]);
            return arr[0];

        }

        public static string CreateMD5(string input)
        {
            // Use input string to calculate MD5 hash
            using (System.Security.Cryptography.MD5 md5 = System.Security.Cryptography.MD5.Create())
            {
                byte[] inputBytes = System.Text.Encoding.ASCII.GetBytes(input);
                byte[] hashBytes = md5.ComputeHash(inputBytes);

                // Convert the byte array to hexadecimal string
                StringBuilder sb = new StringBuilder();
                for (int i = 0; i < hashBytes.Length; i++)
                {
                    sb.Append(hashBytes[i].ToString("X2"));
                }
                return sb.ToString();
            }
        }


        public string LayToken(string username, string password)
        {

            //string type = "application/x-www-form-urlencoded";
            //List<String> objFb = new List<string>();
            //HttpRequest http = new HttpRequest();

            string api_key = "882a8490361da98702bf97a021ddc14d";
            //; ~882a8490361da98702bf97a021ddc14d
            string email = username;
            string format = "JSON";
            string locale = "vi_vn";
            string method = "auth.login";
            //string password =password
            string return_ssl_resources = "0";
            string v = "1.0";

            string sig = "api_key=882a8490361da98702bf97a021ddc14demail=" + username + "format=JSONlocale=vi_vnmethod=auth.loginpassword=" + password + "return_ssl_resources=0v=1.062f8ce9f74b12f84c123cc23437a4a32";

            //sig = _Md5Encode(sig);
            sig = CreateMD5(sig).ToLower();
            Console.WriteLine(sig);

            string data2Post = "api_key=" + api_key + "&email=" + username + "&format=" + format + "&locale=" + locale + "&method=" + method + "&password=" + password + "&return_ssl_resources=" + return_ssl_resources + "&v=" + v + "&sig=" + sig;
            //Console.WriteLine(data2Post);

            string data = http.Post("https://api.facebook.com/restserver.php", data2Post, type).ToString();
            //Html_Test(data);
            string tokenFull = Regex.Match(data, "\"access_token\":\"(.*?)\"").Groups[1].Value;
            return tokenFull;
        }

    }
}
