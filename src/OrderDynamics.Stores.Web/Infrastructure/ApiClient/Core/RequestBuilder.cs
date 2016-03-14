using System;
using System.Net.Http;
using System.Net.Http.Headers;

namespace OrderDynamics.Stores.Web.Infrastructure.ApiClient.Core
{
    internal class RequestBuilder : IRequestBuilder
    {
        private const string ApiVersionHeaderName = "od-api-version";
        private const string AuthenticationScheme = "Basic";

        private readonly IRequestVersionProvider _requestVersionProvider;
        private readonly IRequestCredentialsProvider _requestCredentialsProvider;

        public RequestBuilder(IRequestCredentialsProvider requestCredentialsProvider, IRequestVersionProvider requestVersionProvider) {
            if (requestCredentialsProvider == null) {
                throw new ArgumentNullException("requestCredentialsProvider");
            }
            if (requestVersionProvider == null) {
                throw new ArgumentNullException("requestVersionProvider");
            }

            _requestCredentialsProvider = requestCredentialsProvider;
            _requestVersionProvider = requestVersionProvider;
        }

        public HttpRequestMessage Build(HttpMethod method, string action) {

            var requestMessage = new HttpRequestMessage(method, action);
            
            requestMessage.Headers.Authorization = new AuthenticationHeaderValue(AuthenticationScheme, _requestCredentialsProvider.GetCredentials());

            requestMessage.Headers.Add(ApiVersionHeaderName, _requestVersionProvider.GetVersion(action));

            return requestMessage;
        }
    }
}
