using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Common.ModelsDTO.Requests.Simsimi;
using Common.ModelsDTO.Responses.Simsimi;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace Common.Helpers
{
    public class SimsimiHelper
    {
        public static List<string> listAPIKey = new List<string>
        {
            "YnaQrO3/OY8eN3rlYZQejKv8sPsFaa8OC9q5i3nb",
            "6Z71R8yzBuhCui8X4W8tABqW+GlrfObJA4enwVs+",
            "YZh8vKVwsL4L0bB/STI46UDY/QOBeYPLoI1aPWJa"
        };

        public static string RandomAPIKey(List<string> list)
        {
            Random rd = new Random();
            return list[rd.Next(list.Count)];
        }

        public static async Task<string> SendSimsimi(string question)
        {
            int countTried = 0;
            try
            {
                do
                {
                    var http = new HttpClient();
                    http.DefaultRequestHeaders.Clear();
                    http.DefaultRequestHeaders.Add("x-api-key", RandomAPIKey(listAPIKey));
                    string url = "https://wsapi.simsimi.com/190410/talk";
                    SimsimiRequest request = new SimsimiRequest
                    {
                        Utext = question,
                        Lang = "vn"
                    };
                    string json = JsonConvert.SerializeObject(request);
                    HttpContent httpContent = new StringContent(json, Encoding.UTF8, "application/json");
                    var response = await http.PostAsync(url, httpContent);
                    var result = await response.Content.ReadAsStringAsync();
                    SimsimiResponse simsimi = JsonConvert.DeserializeObject<SimsimiResponse>(result);
                    if (simsimi.Status == 200)
                    {
                        return simsimi.Atext;
                    }
                    else
                    {
                        countTried++;
                    }
                } while (countTried > 3);

            }
            catch (Exception e)
            {
            }
            return "";
        }
    }


}
