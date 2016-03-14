using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Runtime.Serialization.Json;
using System.Threading.Tasks;
using Microsoft.Extensions.OptionsModel;
using OrderDynamics.Stores.Web.Infrastructure.ApiClient.Core;
using OrderDynamics.Stores.Web.Infrastructure.Configuration;

namespace OrderDynamics.Stores.Web.Infrastructure.ApiClient
{
    internal class WebApiClient : IApiClient
    {

        private readonly IOptions<ConfigurationOptions> _options;
        private readonly IRequestBuilder _requestBuilder;

        public WebApiClient(IRequestBuilder requestBuilder, IOptions<ConfigurationOptions> options) {
            if (requestBuilder == null) {
                throw new ArgumentNullException("requestBuilder");
            }
            if (options == null) {
                throw new ArgumentNullException("options");
            }

            _requestBuilder = requestBuilder;
            _options = options;
        }

        public async Task<TResult> GetAsync<TResult>(string action)
        {
            var result = default(TResult);

            using (var client = new HttpClient()) {

                client.BaseAddress = new Uri(_options.Value.WebApiBaseUrl);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                var requestMessage = _requestBuilder.Build(HttpMethod.Get, action);

                var response = await client.SendAsync(requestMessage);
                if (response.IsSuccessStatusCode) {
                    var stream = await response.Content.ReadAsStreamAsync();
                    result = (TResult)(new DataContractJsonSerializer(typeof(TResult)).ReadObject(stream));
                }
            }

            return result;
        }
    }
}
