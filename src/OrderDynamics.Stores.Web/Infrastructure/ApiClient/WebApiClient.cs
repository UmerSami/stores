using System;
using System.Net.Http;
using System.Runtime.Serialization.Json;
using System.Threading.Tasks;
using OrderDynamics.Stores.Web.Infrastructure.ApiClient.Core;

namespace OrderDynamics.Stores.Web.Infrastructure.ApiClient
{
    internal sealed class WebApiClient : IApiClient
    {
        private readonly IHttpClientProvider _httpClientProvider;
        private readonly IRequestBuilder _requestBuilder;

        private HttpClient _httpClient;

        private bool _isDisposed;
        
        public WebApiClient(IRequestBuilder requestBuilder, IHttpClientProvider httpClientProvider) {
            if (requestBuilder == null) {
                throw new ArgumentNullException("requestBuilder");
            }
            if (httpClientProvider == null) {
                throw new ArgumentNullException("httpClientProvider");
            }

            _requestBuilder = requestBuilder;
            _httpClientProvider = httpClientProvider;
        }


        public async Task<TResult> GetAsync<TResult>(string action) {
            EnsureAlive();

            var result = default(TResult);

            var requestMessage = _requestBuilder.Build(HttpMethod.Get, action);

            try {
                var response = await GetHttpClient().SendAsync(requestMessage);
                if (response.IsSuccessStatusCode) {
                    var stream = await response.Content.ReadAsStreamAsync();
                    result = (TResult) new DataContractJsonSerializer(typeof (TResult)).ReadObject(stream);
                }
            }
            catch (Exception ex) {
                //TODO: log an exception
            }

            return result;
        }

        public void Dispose() {
            Dispose(true);
            // ReSharper disable once GCSuppressFinalizeForTypeWithoutDestructor
            GC.SuppressFinalize(this);
        }

        private void Dispose(bool disposing) {
            if (disposing) {
                if (_httpClient != null) {
                    _httpClient.Dispose();
                }

                _isDisposed = true;
            }
        }

        private HttpClient GetHttpClient() {
            EnsureAlive();

            if (_httpClient == null) {
                _httpClient = _httpClientProvider.GetHttpClient();
            }

            return _httpClient;
        }

        private void EnsureAlive() {
            if (_isDisposed) {
                throw new InvalidOperationException("The instance is already disposed.");
            }
        }
    }
}
