using System;
using System.Collections.Generic;
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

            DateTime date = DateTime.Parse("2016-01-29");

            IndividualVisitors individualVistors = new IndividualVisitors();
            individualVistors.GetIndividualVisitors(access_token, venue_id, date);
            individualVistors.WriteToCsv(String.Format(@"C:\test\Turnstyle\VisitorsData_AllDay_{0}.csv", date.ToString("yyyy-MM-dd")));
            individualVistors.WriteToCsv(String.Format(@"C:\test\Turnstyle\VisitorsData_Range_{0}.csv", date.ToString("yyyy-MM-dd")),
                new DateTime(date.Year, date.Month, date.Day, 20, 0, 0, DateTimeKind.Local),
                new DateTime(date.Year, date.Month, date.Day, 23, 59, 0, DateTimeKind.Local));

        }

    }
}
