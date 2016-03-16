using System;
using System.Threading.Tasks;

namespace OrderDynamics.Stores.Web.Infrastructure.ApiClient
{
    public interface IApiClient : IDisposable {
        Task<TResult> GetAsync<TResult>(string action);
    }
}
