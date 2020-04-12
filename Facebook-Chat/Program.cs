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
                //Basic_Usage_Custom.Run().GetAwaiter().GetResult();
                //TestFunction();
                var source = @"  
                <!DOCTYPE html>  
                    <html>  
                        <head>  
                            <style>  
                                table {  
                                  font-family: arial, sans-serif;  
                                  border-collapse: collapse;  
                                  width: 100%;  
                                }  
                                  
                                td, th {  
                                  border: 1px solid #dddddd;  
                                  text-align: left;  
                                  padding: 8px;  
                                }  
                                  
                                tr:nth-child(even) {  
                                  background-color: #dddddd;  
                                }  
                          </style>  
                         </head>  
                    <body>  
                      
                        <h2>HTML Table</h2>  
                          
                        <table>  
                          <tr>  
                            <th>Contact</th>  
                            <th>Country</th>  
                          </tr>  
                          <tr>  
                            <td>Kaushik</td>  
                            <td>India</td>  
                          </tr>  
                          <tr>  
                            <td>Bhavdip</td>  
                            <td>America</td>  
                          </tr>  
                          <tr>  
                            <td>Faisal</td>  
                            <td>Australia</td>  
                          </tr>  
                        </table>  
                     </body>  
                    </html> ";
                HtmlHelper.GenderImage(source);
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
