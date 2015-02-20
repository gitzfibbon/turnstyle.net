using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Turnstyle.Client
{
    public class Credentials
    {
        public string user_name;
        public string password;
        public string grant_type = "client_credentials";
    }
}
