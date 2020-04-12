using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using Common.Helpers;
using Facebook_Chat.ChatHelper;
using Facebook_Chat.Models;
using fbchart_csharp;
using fbchart_csharp.API;
using FireSharp.Interfaces;
using FireSharp.Response;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Facebook_Chat
{
    public class FBClientCustom : MessengerClient
    {

        private static readonly string appName = "FBChat-Sharp";
        private static readonly string sessionFile = "SESSION_COOKIES_core.dat";

        public FBClientCustom()
        {
            this.Set2FACallback(get2FACode);
        }

        private async Task<string> get2FACode()
        {
            await Task.Yield();
            Console.WriteLine("Insert 2FA code:");
            return Console.ReadLine();
        }

        protected override async Task onMessage(string mid = null, string author_id = null, string message = null, FB_Message message_object = null, string thread_id = null, ThreadType? thread_type = null, long ts = 0, JToken metadata = null, JToken msg = null)
        {
            //authorId: Nguoi Gui
            //threaId: Nguoi Nhan
            try
            {
                if (thread_type == ThreadType.ROOM || thread_type == ThreadType.GROUP)
                    return;
                IFirebaseClient client = FirebaseHelper.SetFirebaseClientForChat();
                //Check Remove blockall
                if (message.ToLower() == "removestopall")
                {
                    await client.DeleteAsync("ListBlockUser/" + thread_id);
                    return;
                }

                if (author_id == GetUserUid())
                {
                    await client.SetAsync("ListBlockUser/" + GetUserUid(), new MessageFirsebase
                    {
                        Id = GetUserUid(),
                        BlockAll = true
                    });
                    return;
                }

                //Check ListBlockUser
                FirebaseResponse firebaseResponse = await client.GetAsync("ListBlockUser");
                Dictionary<string, MessageFirsebase> listBlockUser = JsonConvert.DeserializeObject<Dictionary<string, MessageFirsebase>>(firebaseResponse.Body);
                var isBlockedAll = listBlockUser != null && listBlockUser.Values.Any(x => x.BlockAll == true && x.Id == thread_id);
                if (isBlockedAll)
                    return;


                var firebaseGet = await client.GetAsync("ListUser/" + author_id);
                if (firebaseGet == null)
                {
                    if (!await CheckUserInListSimsimi(client, thread_id))
                    {
                        await Add5MinuteUser(client, author_id, author_id, thread_id, message, false);
                    }
                }
                else
                {
                    //Thơi gian hien tai < Thoi gian Block
                    var response = firebaseGet.ResultAs<MessageFirsebase>();
                    if (DateTime.UtcNow < response?.BlockUntil)
                    {
                        if (message.ToLower() == "stopall")
                        {
                            await client.SetAsync("ListBlockUser/" + thread_id, new MessageFirsebase
                            {
                                Id = thread_id,
                                BlockAll = true
                            });
                            return;
                        }
                        else if (message.ToLower() == "stophour")
                        {
                            await client.SetAsync("ListUser/" + thread_id, new MessageFirsebase
                            {
                                BlockUntil = DateTime.UtcNow.AddHours(1)
                            });
                            return;
                        }
                        else if (message.ToLower() == "trolyao")
                        {
                            await client.SetAsync("ListSimsimiUser/" + thread_id, new MessageFirsebase
                            {
                                Id = thread_id
                            });
                            await send(new FB_Message { text = "Chào bạn. Mình là Trợ lý ảo của Quang.\nNhững tin nhắn này được trả lời tự động. Mục đích vui là chính :D" }, author_id, ThreadType.USER);

                        }
                        if (await CheckUserInListSimsimi(client, thread_id))
                        {
                            string answer = await SimsimiHelper.SendSimsimi(message);
                            string text = "Trợ lý ảo:\n" + answer;
                            await send(new FB_Message { text = text }, author_id, ThreadType.USER);
                        }
                    }
                    else
                    {
                        if (message.ToLower() == "stopall")
                        {
                            await client.SetAsync("ListBlockUser/" + thread_id, new MessageFirsebase
                            {
                                Id = thread_id,
                                BlockAll = true
                            });
                            return;
                        }
                        else if (message.ToLower() == "stophour")
                        {
                            await client.SetAsync("ListUser/" + thread_id, new MessageFirsebase
                            {
                                Id = thread_id,
                                BlockUntil = DateTime.UtcNow.AddHours(1)
                            });
                            return;
                        }
                        else if (message.ToLower() == "trolyao")
                        {
                            await client.SetAsync("ListSimsimiUser/" + thread_id, new MessageFirsebase
                            {
                                Id = thread_id
                            });
                            await send(new FB_Message { text = "Chào bạn. Mình là Trợ lý ảo của Quang.\nNhững tin nhắn này được trả lời tự động. Mục đích vui là chính :D" }, author_id, ThreadType.USER);
                        }


                        //Co the send message o day
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine(string.Format($"{DateTime.Now}: Got new 1 message from author_id = {author_id}: {message}. Thread_type = {thread_type.ToString()} \nthread_id={thread_id}"));
                        if (author_id != GetUserUid())
                        {
                            Console.ForegroundColor = ConsoleColor.Blue;
                            if (await CheckUserInListSimsimi(client, thread_id))
                            {
                                string answer = await SimsimiHelper.SendSimsimi(message);
                                string text = "Trợ lý ảo:\n" + answer;
                                await send(new FB_Message { text = text }, author_id, ThreadType.USER);
                            }
                            else
                            {
                                string text = "Đây là trợ lý ảo của Quang.\nHiện tại Quang không đang online nên không thể trả lời bạn ngay được.\nTrong khi chờ đợi, các bạn có thể gõ tin nhắn \"trolyao\" để trao đổi với trợ lý ảo";

                                await send(new FB_Message { text = text }, author_id, ThreadType.USER);
                            }



                            #region Ít dùng
                            //using (FileStream stream = File.OpenRead(@"the girl with katana.jpg"))
                            //{
                            //    await sendLocalFiles(
                            //        file_paths: new Dictionary<string, Stream>() { { @"the girl with katana.jpg", stream } },
                            //        message: null,
                            //        thread_id: author_id,
                            //        thread_type: ThreadType.USER);
                            //}
                            //await send(new FB_Message { text = "Đáp lại tin nhắn----" }, author_id, ThreadType.USER);
                            //await send(new FB_Message { text = await Covid19Helper.GetDetail() }, author_id, ThreadType.USER);

                            #endregion

                        }

                        ///////////////////
                        if (!await CheckUserInListSimsimi(client, thread_id))
                        {
                            await Add5MinuteUser(client, author_id, author_id, thread_id, message, false);
                        }
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine($"{DateTime.Now}: Có lỗi xảy ra. Exception = {e.Message}");
            }

        }

        private async Task<bool> CheckUserInListSimsimi(IFirebaseClient client, string threadId)
        {
            FirebaseResponse firebaseResponse = await client.GetAsync("ListSimsimiUser");
            Dictionary<string, MessageFirsebase> listSimsimiUser = JsonConvert.DeserializeObject<Dictionary<string, MessageFirsebase>>(firebaseResponse.Body);
            var isInSimsimiUser = listSimsimiUser != null && listSimsimiUser.Values.Any(x => x.Id == threadId);
            return isInSimsimiUser;
        }


        private async Task Add5MinuteUser(IFirebaseClient client, string id, string author_id, string thread_id, string message, bool blockAll)
        {
            await client.SetAsync("ListUser/" + author_id, new MessageFirsebase()
            {
                Id = id,
                From = author_id,
                To = thread_id,
                Content = message,
                TimeReceived = DateTime.UtcNow,
                BlockUntil = DateTime.UtcNow.AddMinutes(5),
                BlockAll = blockAll
            });
        }








        protected override async Task DeleteCookiesAsync()
        {
            try
            {
                await Task.Yield();
                var file = Path.Combine(UserDataFolder, sessionFile);
                File.Delete(file);
            }
            catch (Exception ex)
            {
                this.Log(ex.ToString());
            }
        }

        protected override async Task<Dictionary<string, List<Cookie>>> ReadCookiesFromDiskAsync()
        {
            try
            {
                var file = Path.Combine(UserDataFolder, sessionFile);
                using (var fileStream = File.OpenRead(file))
                {
                    await Task.Yield();
                    using (var jsonTextReader = new JsonTextReader(new StreamReader(fileStream)))
                    {
                        JsonSerializer serializer = new JsonSerializer();
                        return serializer.Deserialize<Dictionary<string, List<Cookie>>>(jsonTextReader);
                    }
                }
            }
            catch (Exception ex)
            {
                this.Log(string.Format("Problem reading cookies from disk: {0}", ex.ToString()));
                return null;
            }
        }

        protected override async Task WriteCookiesToDiskAsync(Dictionary<string, List<Cookie>> cookieJar)
        {
            var file = Path.Combine(UserDataFolder, sessionFile);

            using (var fileStream = File.Create(file))
            {
                try
                {
                    using (var jsonWriter = new JsonTextWriter(new StreamWriter(fileStream)))
                    {
                        JsonSerializer serializer = new JsonSerializer();
                        serializer.Serialize(jsonWriter, cookieJar);
                        await jsonWriter.FlushAsync();
                    }
                }
                catch (Exception ex)
                {
                    this.Log(string.Format("Problem writing cookies to disk: {0}", ex.ToString()));
                }
            }
        }

        /// <summary>
        /// Get the current user data folder
        /// </summary>
        private static string UserDataFolder
        {
            get
            {
                string folderBase = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
                string dir = Path.Combine(folderBase, appName.ToUpper());
                return CheckDir(dir);
            }
        }

        /// <summary>
        /// Check the specified folder, and create if it doesn't exist.
        /// </summary>
        /// <param name="dir"></param>
        /// <returns></returns>
        private static string CheckDir(string dir)
        {
            if (!Directory.Exists(dir))
            {
                Directory.CreateDirectory(dir);
            }
            return dir;
        }
    }
}
