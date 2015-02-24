using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Turnstyle.Client.Tests
{
    [TestClass]
    public class VenuesTests
    {
        [TestMethod]
        public void VenuesTest1()
        {
            string access_token = TurnstyleApi.PostAccess().Result.access_token;
            int venue_id = Int32.Parse(TurnstyleApi.GetVenues(access_token).Result[0].id);
            int venueNodeStatus = Int32.Parse(TurnstyleApi.GetVenueNodeStatus(access_token, venue_id).Result[0].last_checkin);

            dynamic dataVisitors = TurnstyleApi.GetDataVisitors(access_token, venue_id, DateTime.UtcNow.AddDays(-5), 2).Result;
        }
    }
}
