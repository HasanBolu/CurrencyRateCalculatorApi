using System;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace ExampleApi.Helpers
{
    public class HttpClientHelper : IHttpClientHelper
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly string _clientName;

        public HttpClientHelper(IHttpClientFactory httpClientFactory, string clientName)
        {
            _httpClientFactory = httpClientFactory;
            _clientName = clientName;
        }


        public async Task<string> GetAsync(string url)
        {
            HttpClient client = _httpClientFactory.CreateClient(_clientName);
            
            using (HttpResponseMessage response = await client.GetAsync(url))
            using (HttpContent content = response.Content)
            {
                return await content.ReadAsStringAsync();
            }
        }

        
    }
}