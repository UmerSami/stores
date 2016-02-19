using System.Threading.Tasks;

namespace OrderDynamics.Stores.Web.Infrastructure.ApiClient
{
    public interface IApiClient {
        Task<TResult> GetAsync<TResult>(string action);
    }
}
