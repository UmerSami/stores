namespace OrderDynamics.Stores.Web.Infrastructure.ApiClient
{
    public interface IApiClientFactory {
        IApiClient GetApiClient();
    }
}
