using System.Net.Http;
using System.Threading.Tasks;

namespace ExampleApi.Helpers
{
    public interface IHttpClientHelper
    {
        Task<string> GetAsync(string url);
    }
}