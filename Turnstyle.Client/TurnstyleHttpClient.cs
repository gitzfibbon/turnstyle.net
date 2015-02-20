using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace Turnstyle.Client
{
    public class TurnstyleHttpClient : HttpClient
    {
        public TurnstyleHttpClient()
        {
            this.BaseAddress = new Uri("http://api.getturnstyle.com");
            this.DefaultRequestHeaders.Accept.Clear();
            this.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }
    }
}
