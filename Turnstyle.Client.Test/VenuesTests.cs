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
            int venue1 = Int32.Parse(TurnstyleApi.GetVenues(access_token).Result[0].id);
            int venue1NodeStatus = Int32.Parse(TurnstyleApi.GetVenueNodeStatus(access_token, venue1).Result[0].last_checkin);

        }
    }
}
