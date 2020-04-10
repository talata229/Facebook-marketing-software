using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FireSharp;
using FireSharp.Config;
using FireSharp.Interfaces;

namespace Common.Helpers
{
    public class FirebaseHelper
    {

        public static FirebaseClient SetFirebaseClientForChat()
        {
            IFirebaseConfig config = new FirebaseConfig()
            {
                AuthSecret = "GpXZu8NinDy8OYG3nDP8ktY60aEzMLnGWW3vGHeE",
                BasePath = "https://facebook-chat-cdb5c.firebaseio.com/"
            };
            return new FirebaseClient(config);
        }
    }
}
