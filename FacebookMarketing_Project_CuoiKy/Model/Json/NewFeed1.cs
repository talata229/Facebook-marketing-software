using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FacebookMarketing_Project_CuoiKy.Model.Json
{
    public class NewFeed1
    {
        public string id { get; set; }
        public string created_time { get; set; }
        public string message { get; set; }
        public override string ToString()
        {
          return  this.id + "|" + this.created_time + "|" + this.message;
        }

    }
}
