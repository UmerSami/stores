using System;
using System.Text;
using Microsoft.Extensions.OptionsModel;
using OrderDynamics.Stores.Web.Infrastructure.Configuration;

namespace OrderDynamics.Stores.Web.Infrastructure.ApiClient.Core
{
    internal class BasicRequestCredentialsProvider : IRequestCredentialsProvider
    {
        private readonly IOptions<ConfigurationOptions> _options;

        public BasicRequestCredentialsProvider(IOptions<ConfigurationOptions> options) {
            if (options == null) {
                throw new ArgumentNullException("options");
            }

            _options = options;
        }

        public string GetCredentials() {
            return Convert.ToBase64String(Encoding.ASCII.GetBytes(string.Format("{0}:{1}", _options.Value.StoreId , _options.Value.ApiKey)));
        }
    }
}
