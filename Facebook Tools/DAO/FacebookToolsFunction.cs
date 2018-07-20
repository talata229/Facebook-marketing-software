using Facebook_Tools.Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using xNet;

namespace Facebook_Tools.DAO
{
  
    class FacebookToolsFunction
    {
        string type = "application/x-www-form-urlencoded";
        List<String> objFb = new List<string>();
        HttpRequest http = new HttpRequest();
        public List<String> DangNhapFacebook(string username, string password)
        {
            try
            {
                List<String> RET = new List<string>();
                http.Cookies = http.Cookies = new CookieDictionary();

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
        public List<String>GetNameAndUIDFromToken(string tk)
        {
            try
            {
                List<String> Result = new List<string>();
                http.Cookies = http.Cookies = new CookieDictionary();

                http.UserAgent = Http.ChromeUserAgent();

                string html = http.Get("https://graph.facebook.com/v3.0/me?fields=id%2Cname&access_token="+ tk).ToString();
                User user = JsonConvert.DeserializeObject<User>(html);
                Result.Add(user.id);  //0
                Result.Add(user.name);//1
               
                return Result;
            }
            catch (Exception ex)
            {
                ex.Message.ToString();
                return null;
            }



            

          //  pictureBox2.Load("https://graph.facebook.com/" + objFB[1] + "/picture?type=normal");

        }
        public string getCoverFromUID(string id)
        {
            string result = "https://scontent.fhan2-3.fna.fbcdn.net/v/t1.0-9/32745701_185070642317166_8489549919118426112_o.jpg?_nc_cat=0&oh=667a1c63038f25b1df047b4cb718eb1b&oe=5B9A1956";
            string url = "https://graph.facebook.com/" + id + "?fields=cover&access_token=EAAAAUaZA8jlABAEqjLKxaG4NCHDM9biCFxwP7EkmGesZA1LQlrZCaKcB2ELMGBOGRDvIQ1IGyUCtIQESUhZC1sdusUuIGINVrRW00rrXmSDlNi805T9X4AcaFtvbbqUcOu9DgZCs1QydE7ODeLTq2kgZBUiiyim8ZCq063dtW7pCwZDZD";
            string html = http.Get(url).ToString();
            Clipboard.SetText(html);
            CoverWallpaper cw = new CoverWallpaper();
            //Newtonsoft.Json.JsonConvert.PopulateObject(html, cw);
            JsonConvert.PopulateObject(html, cw);
            try
            {
                return result = cw.cover["source"];
            }
            catch (Exception ex)
            {
                ex.Message.ToString();
            }

            // MessageBox.Show(result);
            return result;
        }


        public void Html_Test(string html)
        {
            File.WriteAllText("b.html", html);
            Process.Start("b.html");
            // MessageBox.Show("Da xong");
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

        public bool FBReactionFromIDFull(string token, string IDFull, string type)
        {
            try
            {
                string s = http.Get("https://graph.facebook.com/" + IDFull + "/reactions?type=" + type + "&method=post&access_token=" + token).ToString();
                return s.Contains("success");

            }
            catch (Exception)
            {
                MessageBox.Show("Không đúng định dạng ID POST");
                throw;
                //return;
            }
            // 0     1     2      3   4     5    6       
            //NONE, LIKE, LOVE, WOW, HAHA, SAD, ANGRY, THANKFUL, PRIDE
            //string stype = "";
            //if (type == 0) stype = "NONE";
            //if (type == 1) stype = "LIKE";
            //if (type == 2) stype = "LOVE";
            //if (type == 3) stype = "WOW";
            //if (type == 4) stype = "HAHA";
            //if (type == 5) stype = "SAD";
            //if (type == 6) stype = "ANGRY";
            //if (type < 0 || type > 6) stype = "LIKE";
          
        }

        public Dictionary<string, NewFeed1> FBListNewFeed2(string tk, int limit = 25, bool next = false)
        {
            //Dùng tốt
            Dictionary<string, NewFeed1> result = new Dictionary<string, NewFeed1>();
            // Clipboard.SetText(Handle[4]);
            string s = http.Get("https://graph.facebook.com/me/home?fields=id,created_time,message&limit=" + limit + "&access_token=" + tk).ToString();
            //MessageBox.Show(s);
            if (next == false)
            {
                result.Clear();
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
              
            }
            else if (next == true)
            {
                result.Clear();
                NewFeed nf = JsonConvert.DeserializeObject<NewFeed>(s);
                string tempNext = nf.paging.next;
                //MessageBox.Show(tempNext);
                s = http.Get(tempNext).ToString();
              //  MessageBox.Show(s);

                nf = JsonConvert.DeserializeObject<NewFeed>(s);
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
            }
            //foreach(KeYva)
            return result;
        }

    }
}
