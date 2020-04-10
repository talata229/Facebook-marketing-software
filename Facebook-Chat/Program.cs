using System;
using System.Threading.Tasks;
using Common.Helpers;
using Facebook_Chat.ChatHelper;

namespace Facebook_Chat
{
    class Program
    {
        static void Main(string[] args)
        {
            // Run example
            try
            {
                Basic_Usage_Custom.Run().GetAwaiter().GetResult();
                //TestFunction();

            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }

            // Wait for keypress
            Console.ReadKey();
        }

        static async void TestFunction()
        {
            //string result = await Covid19Helper.GetDetail();
            string result = await SimsimiHelper.SendSimsimi("Xin chào bạn, Làm quen nhé");
        }
    }
}
