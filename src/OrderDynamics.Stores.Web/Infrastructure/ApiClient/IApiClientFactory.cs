namespace OrderDynamics.Stores.Web.Infrastructure.ApiClient
{
    internal interface IApiClientFactory {
        IApiClient GetApiClient();
    }
}
