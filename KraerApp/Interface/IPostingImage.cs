using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace KraerApp.Interface
{
    public interface IPostingImage
    {
        void Post(HttpClient client, DateTime machineUtcDateTime, TimeSpan timeSkewAdjustSeconds);
    }
}
