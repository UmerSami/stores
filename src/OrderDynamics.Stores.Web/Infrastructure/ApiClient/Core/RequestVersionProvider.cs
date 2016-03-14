using System;
using System.Linq;
using Microsoft.Extensions.OptionsModel;
using OrderDynamics.Stores.Web.Infrastructure.Configuration;

namespace OrderDynamics.Stores.Web.Infrastructure.ApiClient.Core
{
    internal class RequestVersionProvider : IRequestVersionProvider
    {
        private readonly IOptions<ConfigurationOptions> _options;

        public RequestVersionProvider(IOptions<ConfigurationOptions> options) {
            if (options == null) {
                throw new ArgumentNullException("options");
            }

            _options = options;
        }

        public string GetVersion(string action) {
            //the version is not configured.
            if (_options.Value.WebApiVersion == null) {
                return string.Empty;
            }

            var defaultVersion = _options.Value.WebApiVersion.Version;

            var versionOverrides = _options.Value.WebApiVersion.VersionOverrides;
            if (versionOverrides == null) {
                return defaultVersion;
            }

            var overridenVersion =
                versionOverrides.FirstOrDefault(
                    v => string.Equals(action, v.ApiControllerName, StringComparison.OrdinalIgnoreCase));

            if (overridenVersion != null) {
                return overridenVersion.Version;
            }

            return defaultVersion;
        }
    }
}
