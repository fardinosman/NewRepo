using KraerApp.Interface;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Web;
using System.Web.Helpers;

namespace KraerApp
{
    public class InsertProperty
    {
        public DateTime machineUtcDateTime;
        public TimeSpan timeSkewAdjustSeconds;
        IPostToApi interpost;
        IHttpClientWrapper clientWrapper;
        HttpClient client = new HttpClient();
        IPostingImage _postingImage;
        IActivateProperties _activateProperties;


        public InsertProperty(IActivateProperties activateProperties,IPostToApi Postprop, IHttpClientWrapper clientWrapper, IPostingImage postingImage)
        {
            this.interpost = Postprop;
            this.clientWrapper = clientWrapper;
            this._postingImage = postingImage;
            this._activateProperties = activateProperties;
    }

        public void Execute()
        {
            WarmUpRequest(clientWrapper);

            interpost.CsvReadMapPropertyModel(@"C:\Users\Fard\Documents\FTP\export.csv",client, machineUtcDateTime, timeSkewAdjustSeconds);
            Thread.Sleep(5000);
            _postingImage.Post(client, machineUtcDateTime, timeSkewAdjustSeconds);
            Thread.Sleep(5000);
            _activateProperties.Post(client, machineUtcDateTime, timeSkewAdjustSeconds);



        }
        public void WarmUpRequest(IHttpClientWrapper client)
        {
            machineUtcDateTime = DateTime.UtcNow;

            // Pretend the machine time is 30 minutes behind
            //machineUtcDateTime = machineUtcDateTime.AddMinutes(-30);

            // Make a warm-up request only the first time
            var warmupRequest = new HttpRequestMessage(HttpMethod.Get, "https://api.oline.dk/v1/system/status");
            var warmupResponse = client.Client.SendAsync(warmupRequest).Result;

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