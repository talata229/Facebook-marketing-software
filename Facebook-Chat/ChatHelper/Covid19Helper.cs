using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Facebook_Chat.Models.Response.CovidVN;
using Newtonsoft.Json;

namespace Facebook_Chat.ChatHelper
{
    public class Covid19Helper
    {
        public static async Task<string> GetDetail()
        {
            var http = new HttpClient();
            string urlGetCovid = "https://gw.vnexpress.net/cr/?name=tracker_coronavirus";
            var response = await http.GetAsync(urlGetCovid);
            var result = await response.Content.ReadAsStringAsync();
            CovidRoot covidRoot = JsonConvert.DeserializeObject<CovidRoot>(result);

            string temp = @"Tại Việt Nam, tổng số có: " + covidRoot.Data.DataData[0].TrackerTotalByDay.Cases + " ca nhiễm."
                          + " Số ca tử vong là: " + covidRoot.Data.DataData[0].TrackerTotalByDay.Deaths + " ca."
                          + " Số ca chữa khỏi là: " + covidRoot.Data.DataData[0].TrackerTotalByDay.Recovered + " ca.\n";
            var listProvince = covidRoot.Data.DataData[0].TrackerByProvince;
            temp += "========Thống kê theo tỉnh thành=======:\n";
            foreach (var provice in listProvince)
            {
                temp += @"-" + provice.Name + ": " + provice.Cases + " ca nhiễm. " + provice.Deaths + " ca tử vong. " + provice.Recovered + " ca chữa khỏi \n";
            }
            return temp;
        }
    }
}
