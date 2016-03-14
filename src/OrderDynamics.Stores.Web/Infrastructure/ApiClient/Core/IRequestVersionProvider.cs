namespace OrderDynamics.Stores.Web.Infrastructure.ApiClient.Core
{
    internal interface IRequestVersionProvider {
        string GetVersion(string action);
    }
}
