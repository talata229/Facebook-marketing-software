using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FacebookMarketing.Model
{
    public class GroupUID
    {
        string ID { get; set; }
        string Ten { get; set; }
        
        public GroupUID(string ID,string Ten)
        {
            this.ID = ID;
            this.Ten = Ten;
        }

        public GroupUID()
        {

        }
    }
}
