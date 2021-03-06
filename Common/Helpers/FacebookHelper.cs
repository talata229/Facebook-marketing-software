﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;

using System.Text;
using System.Threading.Tasks;
using Common.ModelsDTO.Responses.Message;
using System.Net.Http;
using System.Net.Http.Headers;
using Common.ModelsDTO.Responses.Home;

namespace Common.Helpers
{
    public class FacebookHelper
    {
        public static async void CheckAllMessageWithFbDtsgAsync(string fbdtsg)
        {
            try
            {
                HttpClient http = new HttpClient();
                http.DefaultRequestHeaders.UserAgent.ParseAdd("Mozilla/5.0 (Windows NT 10.0; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/78.0.3904.108 Safari/537.36");
                var url = "https://www.facebook.com/api/graphql/";
                var fb_dtsg = "AQG91tirzkvM:AQHqn1aQJvJ-";
                var q = "viewer(){message_threads{nodes{thread_key{thread_fbid,other_user_id}}}}";
                var formContent = new FormUrlEncodedContent(new[]
                {
                    new KeyValuePair<string, string>("fb_dtsg", fb_dtsg),
                    new KeyValuePair<string, string>("q", q)
                });
                //formContent.Headers.Clear();
                formContent.Headers.ContentType = new MediaTypeHeaderValue("application/x-www-form-urlencoded");
                //formContent.Headers.Add("Content-Type", "application/x-www-form-urlencoded");

                var response = await http.PostAsync(url, formContent);
                using (var sr = new StreamReader(await response.Content.ReadAsStreamAsync(), Encoding.GetEncoding("iso-8859-1")))
                {
                    string s = await sr.ReadLineAsync();
                    Html_Test(s);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }

        }

        public static async void GetHome(string token)
        {
            try
            {
                string url = $"https://graph.facebook.com/me/home?access_token={token}";
                HttpClient http = new HttpClient();
                var response = await http.GetAsync(url);
                var result = await response.Content.ReadAsStringAsync();
                HomeRoot homeRoot = JsonConvert.DeserializeObject<HomeRoot>(result);
            }
            catch (Exception e)
            {
                var test = true;
            }
        }

        public static void Html_Test(string html)
        {
            File.WriteAllText("b.html", html);
            Process.Start("b.html");
        }
    }
}
