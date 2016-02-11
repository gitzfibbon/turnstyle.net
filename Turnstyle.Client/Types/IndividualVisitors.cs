using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Turnstyle.Client.Types
{
    public class IndividualVisitors
    {
        public List<IndividualVisitor> Visitors;

        public void GetIndividualVisitors(string access_token, string venue_id, DateTime startDateTime, DateTime endDateTime)
        {
            int days = (int)(endDateTime - startDateTime).TotalDays + 1;

            if (days <= 0) { return; }

            this.Visitors = new List<IndividualVisitor>();

            dynamic visitorsData = TurnstyleApi.GetDataVisitors(access_token, venue_id, startDateTime.Date, days).Result;
            var data = visitorsData.data["V" + venue_id][Helpers.ConvertDateTimeToUnixTime(startDateTime.Date).ToString()];

            if (data == null) { return; }

            foreach(var v in data)
            {
                IndividualVisitor visitor = new IndividualVisitor();
                visitor.key = v["key"];
                visitor.event_date = v["values"]["event_date"];
                visitor.mac_id = v["values"]["mac_id"].ToString();
                visitor.venue_id = v["values"]["venue_id"];
                visitor.first_seen = DateTime.Parse(v["values"]["first_seen"]);
                visitor.last_seen = DateTime.Parse(v["values"]["last_seen"]);
                visitor.max_rssi = v["values"]["max_rssi"];

                if ((visitor.last_seen > startDateTime && visitor.last_seen < endDateTime)
                 || (visitor.first_seen > startDateTime && visitor.first_seen < endDateTime)
                 || (visitor.first_seen < startDateTime && visitor.last_seen > endDateTime)) { } // within range
                else { continue; } // NOT within range

                this.Visitors.Add(visitor);
            }
        }

        public static void WriteToCsv(List<IndividualVisitor> visitors, string filePath)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("key,event_date,mac_id,venue_id,first_seen,last_seen,max_rssi");

            foreach(IndividualVisitor visitor in visitors)
            {
                sb.AppendFormat("{0},{1},{2},{3},{4},{5},{6}",
                    visitor.key,
                    visitor.event_date,
                    visitor.mac_id,
                    visitor.venue_id,
                    visitor.first_seen,
                    visitor.last_seen,
                    visitor.max_rssi);
                sb.AppendLine();
            }

            File.WriteAllText(filePath, sb.ToString());
        }

    }

    public class IndividualVisitor
    {
        public int key { get; set; }
        public string event_date { get; set; }
        public string mac_id { get; set; }
        public int venue_id { get; set; }
        public DateTime first_seen { get; set; }
        public DateTime last_seen { get; set; }
        public int max_rssi { get; set; }
    }
}
