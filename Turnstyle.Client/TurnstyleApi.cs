using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Web.Helpers;

namespace Turnstyle.Client
{
    public class TurnstyleApi
    {
        public const string baseAddress = "http://api.getturnstyle.com";

        /// <summary>
        /// Makes a Basic Authorization http request to get an access_token
        /// </summary>
        public static async Task<dynamic> PostAccess()
        {
            string username = "";
            string password = "";

            FormUrlEncodedContent content = new FormUrlEncodedContent(new[] 
            {
                new KeyValuePair<string, string>("grant_type", "client_credentials")
            });

            using (TurnstyleHttpClient client = new TurnstyleHttpClient())
            {
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", Convert.ToBase64String(System.Text.ASCIIEncoding.ASCII.GetBytes(String.Format("{0}:{1}", username, password))));

                HttpResponseMessage result = await client.PostAsync("/access", content);
                string resultContent = result.Content.ReadAsStringAsync().Result;

                return Json.Decode(resultContent);
            }
        }

        /// <summary>
        /// Eg. http://api.getturnstyle.com/venues?access_token=abc123
        /// </summary>
        public static async Task<dynamic> GetVenues(string access_token)
        {
            string requestUri = String.Format("venues?access_token={0}", access_token);

            using (TurnstyleHttpClient client = new TurnstyleHttpClient())
            {
                HttpResponseMessage response = await client.GetAsync(requestUri);
                string json = response.Content.ReadAsStringAsync().Result;
                return Json.Decode(json);
            }
        }

    }
}
