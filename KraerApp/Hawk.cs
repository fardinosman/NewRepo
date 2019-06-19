using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Helpers;

namespace KraerApp
{
    public class Hawk
    {
        const string myId = "9"; // SupplierID
        const string mySecret = "kIJUiosdeK870324"; // APiKey;

        public static DateTime machineUtcDateTime;
        public static TimeSpan timeSkewAdjustSeconds;
        public static HttpClient client = new HttpClient();
        public static string csvpath = @"C:\Users\Fard\Documents\FTP\export.csv";
        public static string imagepath = @"C:\Users\Fard\Documents\FTP";

        public static string CreateRequestHeader(DateTime machineUtcDateTime, ref TimeSpan timeSkewAdjustSeconds, Uri requestUri, string httpMethod)
        {
            var requestHeader = HawkSampleHelper.GetAuthorizationHeader(
              httpMethod,
              requestUri,
              myId,
              mySecret,
              machineUtcDateTime,
              timeSkewAdjustSeconds);
            return requestHeader;
        }

        public static void WarmUpRequest(HttpClient client)
        {
            machineUtcDateTime = DateTime.UtcNow;

            // Pretend the machine time is 30 minutes behind
            //machineUtcDateTime = machineUtcDateTime.AddMinutes(-30);

            // Make a warm-up request only the first time
            var warmupRequest = new HttpRequestMessage(HttpMethod.Get, "https://api.oline.dk/v1/system/status");
            var warmupResponse = client.SendAsync(warmupRequest).Result;

            // Take the returned warm-up response
            dynamic data = Json.Decode(warmupResponse.Content.ReadAsStringAsync().Result);

            if (data.Offline)
            {
                Console.WriteLine("The service is offline with the following message: ");
                Console.WriteLine(data.OffLineMessage);
            }

            // The server responded, now use the returned ServerUtcTime to adjust the local machine time
            DateTime serverUtcDateTime;
            DateTime.TryParseExact(data.ServerUtcTime, "O", CultureInfo.InvariantCulture, DateTimeStyles.RoundtripKind, out serverUtcDateTime);

            // Calculate the difference between the current machine time and the server time
            timeSkewAdjustSeconds = serverUtcDateTime - machineUtcDateTime;
        }
    }
}