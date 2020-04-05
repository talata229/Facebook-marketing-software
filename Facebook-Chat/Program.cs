using System;

namespace Facebook_Chat
{
    class Program
    {
        static void Main(string[] args)
        {
            // Run example
            try
            {
                Basic_Usage.Run().GetAwaiter().GetResult();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }

            // Wait for keypress
            Console.ReadKey();
        }
    }
}
