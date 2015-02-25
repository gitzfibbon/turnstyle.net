using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web.Helpers;

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

        public static async Task<dynamic> Get(string requestUri)
        {
            using (TurnstyleHttpClient client = new TurnstyleHttpClient())
            {
                HttpResponseMessage response = await client.GetAsync(requestUri);
                string json = response.Content.ReadAsStringAsync().Result;
                return Json.Decode(json);
            }
        }

        public static async Task<dynamic> Post(string requestUri, FormUrlEncodedContent content)
        {
            using (TurnstyleHttpClient client = new TurnstyleHttpClient())
            {
                HttpResponseMessage result = await client.PostAsync(requestUri, content);
                string resultContent = result.Content.ReadAsStringAsync().Result;
                return Json.Decode(resultContent);
            }
        }
    }
}
