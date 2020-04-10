using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using fbchart_csharp;
using fbchart_csharp.API;

namespace Facebook_Chat
{
    public class Basic_Usage_Custom
    {
        private static readonly AutoResetEvent _closing = new AutoResetEvent(false);

        public static async Task Run()
        {
            // Instantiate FBClient
            MessengerClient client = new FBClientCustom();

            try
            {
                // Try logging in from saved session
                await client.TryLogin();
            }
            catch
            {
                // Read email and pw from console
                Console.WriteLine("Insert Facebook email:");
                var email = Console.ReadLine();
                Console.WriteLine("Insert Facebook password:");
                var password = Console.ReadLine();

                // Login with username and password
                await client.DoLogin(email, password);
            }

            // Start listening for new messages
            await client.StartListening();

            // Stop listening Ctrl+C
            Console.WriteLine("Listening... Press Ctrl+C to exit.");
            Console.CancelKeyPress += new ConsoleCancelEventHandler((s, e) => { e.Cancel = true; _closing.Set(); });
            _closing.WaitOne();
            client.StopListening();

            // Logging out is not required
            // await client.DoLogout();
        }
    }
}
