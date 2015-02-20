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
            var x = TurnstyleApi.Venues(TurnstyleApi.Access()).Result;
        }
    }
}
