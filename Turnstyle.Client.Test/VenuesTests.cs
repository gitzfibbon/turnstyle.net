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
            dynamic venue1 = TurnstyleApi.GetVenues(access_token).Result[0].name;
        }
    }
}
