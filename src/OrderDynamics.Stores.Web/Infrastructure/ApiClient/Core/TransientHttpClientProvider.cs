using System;
using System.Net.Http;
using System.Net.Http.Headers;
using Microsoft.Extensions.OptionsModel;
using OrderDynamics.Stores.Web.Infrastructure.Configuration;

namespace OrderDynamics.Stores.Web.Infrastructure.ApiClient.Core
{
    internal class TransientHttpClientProvider : IHttpClientProvider
    {
        private readonly IOptions<ConfigurationOptions> _options;

        public TransientHttpClientProvider( IOptions<ConfigurationOptions> options)
        {
            if (options == null) {
                throw new ArgumentNullException("options");
            }

            _options = options;
        }

        public HttpClient GetHttpClient() {
            var client = new HttpClient();

            client.BaseAddress = new Uri(_options.Value.WebApiBaseUrl);
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            return client;
        }
    }
}
