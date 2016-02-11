using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using Turnstyle.Client.Types;

namespace Turnstyle.Client.Tests
{
    [TestClass]
    public class VisitorsData
    {
        [TestMethod]
        public void GetVisitorsData()
        {
            string access_token = TurnstyleApi.PostAccess().Result.access_token;
            string venue_id = TurnstyleApi.GetVenues(access_token).Result[0].id.ToString();

            DateTime startDate = new DateTime(2016, 1, 7, 0, 0, 0,DateTimeKind.Local);
            DateTime endDate = new DateTime(2016, 1, 31, 23, 59, 59, DateTimeKind.Local);

            IndividualVisitors individualVistors = new IndividualVisitors();
            individualVistors.GetIndividualVisitors(access_token, venue_id, startDate, endDate);

            // Get unique visitors in the list
            Dictionary<string, IndividualVisitor> uniqueVisitors = new Dictionary<string, IndividualVisitor>();
            foreach (IndividualVisitor v in individualVistors.Visitors)
            {
                if (!uniqueVisitors.ContainsKey(v.mac_id))
                {
                    uniqueVisitors.Add(v.mac_id, v);
                }
            }

            IndividualVisitors.WriteToCsv(individualVistors.Visitors, String.Format(@"C:\test\Turnstyle\VisitorsData_{0}_to_{1}.csv", startDate.ToString("yyyy-MM-dd"), endDate.ToString("yyyy-MM-dd")));
            IndividualVisitors.WriteToCsv(uniqueVisitors.Values.ToList(), String.Format(@"C:\test\Turnstyle\UniqueVisitorsData_{0}_to_{1}.csv", startDate.ToString("yyyy-MM-dd"), endDate.ToString("yyyy-MM-dd")));

        }

    }
}
