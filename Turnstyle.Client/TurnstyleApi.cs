using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace Turnstyle.Client
{
    public class TurnstyleApi
    {
        public const string baseAddress = "http://api.getturnstyle.com";

        public static string Access()
        {
            return "";
        }

        public static async Task<string> Venues(string access_token)
        {
            // http://api.getturnstyle.com/venues?access_token=abc123

            string requestUri = String.Format("venues?access_token={0}", access_token);

            using (TurnstyleHttpClient client = new TurnstyleHttpClient())
            {
                HttpResponseMessage response = await client.GetAsync(requestUri);
                return response.Content.ReadAsAsync<string>().Result;

                //if (response.IsSuccessStatusCode)
                //{
                //    message = await response.Content.ReadAsAsync<string>();
                //}
                //else
                //{
                //    message = await response.Content.ReadAsAsync<string>();
                //}

            }

        }


    }
}
