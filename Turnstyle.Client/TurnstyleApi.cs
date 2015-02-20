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
            string username = "";
            string password = "";

            FormUrlEncodedContent content = new FormUrlEncodedContent(new[] 
            {
                new KeyValuePair<string, string>("grant_type", "client_credentials")
            });

            string resultContent;
            using (TurnstyleHttpClient client = new TurnstyleHttpClient())
            {
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", Convert.ToBase64String(System.Text.ASCIIEncoding.ASCII.GetBytes(String.Format("{0}:{1}", username, password))));

                HttpResponseMessage result = client.PostAsync("/access", content).Result;
                resultContent = result.Content.ReadAsStringAsync().Result;
            }

            return resultContent;

        }

        public static string Venues(string access_token)
        {
            // http://api.getturnstyle.com/venues?access_token=abc123

            string requestUri = String.Format("venues?access_token={0}", access_token);

            using (TurnstyleHttpClient client = new TurnstyleHttpClient())
            {
                HttpResponseMessage response = client.GetAsync(requestUri).Result;
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
