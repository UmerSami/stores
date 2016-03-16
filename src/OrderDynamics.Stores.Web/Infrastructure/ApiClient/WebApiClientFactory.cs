using System;
using OrderDynamics.Stores.Web.Infrastructure.ApiClient.Core;

namespace OrderDynamics.Stores.Web.Infrastructure.ApiClient
{
    internal class WebApiClientFactory : IApiClientFactory
    {
        private readonly IHttpClientProvider _httpClientProvider;
        private readonly IRequestBuilder _requestBuilder;

        public WebApiClientFactory(IRequestBuilder requestBuilder, IHttpClientProvider httpClientProvider) {
            if (requestBuilder == null) {
                throw new ArgumentNullException("requestBuilder");
            }
            if (httpClientProvider == null) {
                throw new ArgumentNullException("httpClientProvider");
            }

            _requestBuilder = requestBuilder;
            _httpClientProvider = httpClientProvider;
        }

        public IApiClient GetApiClient() {
            return new WebApiClient(_requestBuilder, _httpClientProvider);
        }
    }
}
