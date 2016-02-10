using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Web.Helpers;

using Turnstyle.Client.Types;

namespace Turnstyle.Client
{
    /// <summary>
    /// Provides methods to use the Turnstyle API
    /// </summary>
    /// <remarks>
    /// Design Notes:
    /// 
    /// 1.  Methods are asynchronous. Use the Task.Result property to wait for async methods to complete.
    /// 
    /// 2.  Dynamic objects are returned instead of strongly types objects. This reduces the need for
    ///     updating the client library if the Turnstyle API return types are modified.
    /// 
    /// </remarks>
    public class TurnstyleApi
    {

        /// <summary>
        /// Makes a Basic Authorization http request to get an access_token
        /// </summary>
        public static async Task<dynamic> PostAccess()
        {
            Credentials credentials = new Credentials(@"C:\test\Turnstyle\APICredentials.xml");

            string username = credentials.Login;
            string password = credentials.Secret;

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
        /// http://api.getturnstyle.com/data/venue/{venue_id}/count?access_token={access_token}&start={start_date}&length={length_in_days}&cmp={cmp_string}
        /// </summary>
        /// <remarks>
        /// Might need to be updated. Still trying to understand the API parameters.
        /// </remarks>
        public static async Task<dynamic> GetCount(string access_token, string venue_id, DateTime start_date, int? length_in_days = null, List<DateTime> cmp_dates = null)
        {
            // Optional
            string length = String.Empty;
            if (null != length_in_days)
            {
                length = String.Format("&length={0}", length_in_days);
            }

            // Optional
            string cmp = String.Empty;
            if (null != cmp_dates)
            {
                string dates_string = String.Empty;
                foreach (DateTime cmp_date in cmp_dates)
                {
                    dates_string = String.Concat(dates_string, ",", Helpers.ConvertDateTimeToUnixTime(cmp_date).ToString());
                }

                // Remove the prepended comma
                if (dates_string.StartsWith(","))
                {
                    dates_string = dates_string.Substring(1);
                }

                cmp = String.Format("&cmp={0}", dates_string);
            }

            string requestUri = String.Format("data/venue/{0}/count?access_token={1}&start={2}{3}{4}",
                venue_id, access_token, Helpers.ConvertDateTimeToUnixTime(start_date), length, cmp);

            using (TurnstyleHttpClient client = new TurnstyleHttpClient())
            {
                HttpResponseMessage response = await client.GetAsync(requestUri);
                string json = response.Content.ReadAsStringAsync().Result;
                return Json.Decode(json);
            }
        }


        /// <summary>
        /// Individual Visitors -- By Entry and Exit Time
        /// </summary>
        public static async Task<dynamic> GetDataVisitors(string access_token, string venue_id, DateTime start_date, int length, string type = "V")
        {
            string requestUri = String.Format("data/visitor-data-condensed?id={0}&start={1}&length={2}&type={3}&access_token={4}",
                venue_id, Helpers.ConvertDateTimeToUnixTime(start_date), length, type, access_token);

            using (TurnstyleHttpClient client = new TurnstyleHttpClient())
            {
                HttpResponseMessage response = await client.GetAsync(requestUri);
                string json = response.Content.ReadAsStringAsync().Result;
                return Json.Decode(json);
            }
        }

        /// <summary>
        /// http://api.getturnstyle.com/data/venue/402/lengthdistro?access_token=abc123&start=1383278400&length=1&cmp=1383019200,1383105600
        /// </summary>
        /// <remarks>
        /// Might need to be updated. Still trying to understand the API parameters.
        /// </remarks>
        public static async Task<dynamic> GetLengthDistro(string access_token, string venue_id, DateTime start_date, int? length_in_days = null, List<DateTime> cmp_dates = null)
        {
            // Optional
            string length = String.Empty;
            if (null != length_in_days)
            {
                length = String.Format("&length={0}", length_in_days);
            }

            // Optional
            string cmp = String.Empty;
            if (null != cmp_dates)
            {
                string dates_string = String.Empty;
                foreach (DateTime cmp_date in cmp_dates)
                {
                    dates_string = String.Concat(dates_string, ",", Helpers.ConvertDateTimeToUnixTime(cmp_date).ToString());
                }

                // Remove the prepended comma
                if (dates_string.StartsWith(","))
                {
                    dates_string = dates_string.Substring(1);
                }

                cmp = String.Format("&cmp={0}", dates_string);
            }

            string requestUri = String.Format("data/venue/{0}/length-distro?access_token={1}&start={2}{3}{4}",
                venue_id, access_token, Helpers.ConvertDateTimeToUnixTime(start_date), length, cmp);

            using (TurnstyleHttpClient client = new TurnstyleHttpClient())
            {
                HttpResponseMessage response = await client.GetAsync(requestUri);
                string json = response.Content.ReadAsStringAsync().Result;
                return Json.Decode(json);
            }
        }

        /// <summary>
        /// http://api.getturnstyle.com/loctrigger?access_token={access_token}&venueID={venueID}&clientMAC={client_mac_address}&rssThreshold={rss_threshold}&webserviceURL={webservice_url}
        /// </summary>
        public static async Task<dynamic> GetLocationTrigger(string access_token, string venue_id, string client_mac_address, string rss_threshold, string webservice_url)
        {
            string requestUri = String.Format("loctrigger?access_token={0}venueID={1}&clientMAC={2}&rssThreshold={3}&webserviceURL={4}",
                access_token, venue_id, client_mac_address, rss_threshold, webservice_url);

            return null; // until we can manage (view, edit, delete) location triggers
            // return await TurnstyleHttpClient.Get(requestUri);
        }


        /// <summary>
        /// http://api.getturnstyle.com/venues?access_token=abc123
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


        /// <summary>
        /// http://api.getturnstyle.com/venue/456/nodes/status?access_token=abc123def
        /// </summary>
        /// <remarks>
        /// The API doc doesn't show any example of how node_mac_address is used. It's use isn't tested in this method.
        /// </remarks>
        public static async Task<dynamic> GetVenueNodeStatus(string access_token, string venue_id, string node_mac_address = null)
        {
            string mac_address = String.Empty;
            if (!String.IsNullOrWhiteSpace(node_mac_address))
            {
                mac_address = String.Format("?mac_address={0}", node_mac_address);
            }

            string requestUri = String.Format("venue/{0}/nodes/status?access_token={1}{2}", venue_id, access_token, mac_address);

            using (TurnstyleHttpClient client = new TurnstyleHttpClient())
            {
                HttpResponseMessage response = await client.GetAsync(requestUri);
                string json = response.Content.ReadAsStringAsync().Result;
                return Json.Decode(json);
            }
        }



    }
}
