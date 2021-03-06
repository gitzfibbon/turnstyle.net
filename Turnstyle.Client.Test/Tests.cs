﻿using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Turnstyle.Client.Tests
{
    [TestClass]
    public class Tests
    {
        [TestMethod]
        public void TestVenues()
        {
            string access_token = TurnstyleApi.PostAccess().Result.access_token;
            string venue_id = TurnstyleApi.GetVenues(access_token).Result[0].id;
            string venueNodeStatus = TurnstyleApi.GetVenueNodeStatus(access_token, venue_id).Result[0].last_checkin;
        }

        [TestMethod]
        public void TestVisitors1()
        {
            string access_token = TurnstyleApi.PostAccess().Result.access_token;
            string venue_id = TurnstyleApi.GetVenues(access_token).Result[0].id;

            DateTime start_date = DateTime.Parse("2016-01-08");

            dynamic visitorsData = TurnstyleApi.GetDataVisitors(access_token, venue_id, start_date, 2);

            dynamic visitorsCountNoComparison = TurnstyleApi.GetCount(access_token, venue_id, start_date).Result;

            List<DateTime> comparison_dates = new List<DateTime> { DateTime.UtcNow.AddDays(-2), DateTime.UtcNow.AddDays(-1) };
            dynamic visitorsCountWithComparison = TurnstyleApi.GetCount(access_token, venue_id, start_date, 2, comparison_dates).Result;
        }

        [TestMethod]
        public void TestVisitors2()
        {
            string access_token = TurnstyleApi.PostAccess().Result.access_token;
            string venue_id = TurnstyleApi.GetVenues(access_token).Result[0].id;

            DateTime start_date = DateTime.UtcNow.AddDays(-5);

            dynamic lengthDistroWithComparison = TurnstyleApi.GetLengthDistro(access_token, venue_id, start_date).Result;

            List<DateTime> comparison_dates = new List<DateTime> { DateTime.UtcNow.AddDays(-2), DateTime.UtcNow.AddDays(-1) };
            dynamic lengthDistroWithoutComparison = TurnstyleApi.GetLengthDistro(access_token, venue_id, start_date, 2, comparison_dates).Result;
        }


        [Ignore]
        [TestMethod]
        public void TestLocationTrigger()
        {
            string access_token = TurnstyleApi.PostAccess().Result.access_token;
            string venue_id = TurnstyleApi.GetVenues(access_token).Result[0].id;

            string mac_address = "";
            string rss_threshold = "";
            string webservice_url = "";
            dynamic locationTriggerResult = TurnstyleApi.GetLocationTrigger(access_token, venue_id, mac_address, rss_threshold, webservice_url);
        }
    }
}
