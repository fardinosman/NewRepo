using KraerApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace KraerApp.Interface
{
    public interface IPostToApi
    {
        List<PropertyModel> Post(IHttpClientWrapper client, DateTime MachineUtcDateTime, TimeSpan timeSkewAdjustSeconds);
        void CsvReadMapPropertyModel(string csvpath, HttpClient client, DateTime machineUtcDateTime, TimeSpan timeSkewAdjustSeconds);
    }
}
