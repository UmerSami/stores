namespace OrderDynamics.Stores.Web.Infrastructure.Configuration
{
    public class ConfigurationOptions
    {
        public string WebApiBaseUrl { get; set; }

        public string StoreId { get; set; }

        public string ApiKey { get; set; }

        public VersionOptions WebApiVersion { get; set; }
    }
}
