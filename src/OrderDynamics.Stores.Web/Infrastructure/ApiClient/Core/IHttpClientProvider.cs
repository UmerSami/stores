using System.Net.Http;

namespace OrderDynamics.Stores.Web.Infrastructure.ApiClient.Core
{
    internal interface IHttpClientProvider {
        HttpClient GetHttpClient();
    }
}
