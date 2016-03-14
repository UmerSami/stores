using System.Net.Http;

namespace OrderDynamics.Stores.Web.Infrastructure.ApiClient.Core
{
    internal interface IRequestBuilder {
        HttpRequestMessage Build(HttpMethod method, string action);
    }
}
