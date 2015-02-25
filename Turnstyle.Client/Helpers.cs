using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Turnstyle.Client
{
    public class Helpers
    {
        public static DateTime ConvertUnixTimeToDateTime(double unixTimeStampUtc)
        {
            // Unix timestamp is seconds past epoch
            DateTime result = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
            result = result.AddSeconds(unixTimeStampUtc).ToLocalTime();
            return result;
        }

        public static long ConvertDateTimeToUnixTime(DateTime dateTimeUtc)
        {
            TimeSpan timeSpan = (dateTimeUtc - new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc));
            long result = (long)timeSpan.TotalSeconds;
            return result;
        }
    }
}
