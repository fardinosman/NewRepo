using KraerApp.Interface;
using System.Net.Http;

namespace KraerApp.Controllers
{
    public class HttpClientWrapper : IHttpClientWrapper
    {
        private readonly HttpClient _client;

        public HttpClient Client { get { return _client; } }

        public HttpClientWrapper()
        {
            _client = new HttpClient();
        }

    }
}