using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FacebookMarketing_Project_CuoiKy.Model
{
    public class BaiPost
    {
        public string Ma { get; set; }
        public string NoiDung { get; set; }
        public bool Loai { get; set; }
        BaiPost(string Ma, string NoiDung, bool Loai)
        {
            this.Ma = Ma;
            this.NoiDung = NoiDung;
            this.Loai = Loai;
        }
        BaiPost(string Ma, string NoiDung)
        {
            this.Ma = Ma;
            this.NoiDung = NoiDung;
        }

        public BaiPost()
        {

        }
    }
}
