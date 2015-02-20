using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Turnstyle.Client
{
    public class AccessToken
    {
        // {"access_token":"abc123def","expires_in":3600,"token_type":"bearer","scope":"navizon-hub navizon-jack"}

        public string access_token;
        public long expires_in;
        public string token_type;
        public string scope;
    }
}
