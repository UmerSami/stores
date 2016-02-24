using System;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Runtime.Serialization.Json;
using System.Threading.Tasks;
using Microsoft.Extensions.OptionsModel;
using OrderDynamics.Stores.Web.Infrastructure.Configuration;

namespace OrderDynamics.Stores.Web.Infrastructure.ApiClient
{
    internal class WebApiClient : IApiClient
    {
        private const string ApiVersionHeaderName = "od-api-version";

        private readonly IOptions<ConfigurationOptions> _options;

        public WebApiClient(IOptions<ConfigurationOptions> options) {
            if (options == null) {
                throw new ArgumentNullException("options");
            }

            _options = options;
        }

        public async Task<TResult> GetAsync<TResult>(string action)
        {
            var result = default(TResult);

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(_options.Value.WebApiBaseUrl);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                var requestMessage = new HttpRequestMessage(HttpMethod.Get, action);
                requestMessage.Headers.Add(ApiVersionHeaderName, GetVersion(action));

                var response = await client.SendAsync(requestMessage);
                if (response.IsSuccessStatusCode) {
                    var stream = await response.Content.ReadAsStreamAsync();
                    result = (TResult)(new DataContractJsonSerializer(typeof(TResult)).ReadObject(stream));
                }
            }

            return result;
        }

        private string GetVersion(string action) {
            //the version is not configured.
            if (_options.Value.WebApiVersion == null) {
                return string.Empty;
            }

            var defaultVersion = _options.Value.WebApiVersion.Version;

            var versionOverrides = _options.Value.WebApiVersion.VersionOverrides;
            if (versionOverrides == null) {
                return defaultVersion;
            }

            var overridenVersion = versionOverrides.FirstOrDefault(v => string.Equals(action, v.ApiControllerName, StringComparison.OrdinalIgnoreCase));

            if (overridenVersion != null) {
                return overridenVersion.Version;
            }

            return defaultVersion;
        }
    }
}
