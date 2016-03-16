using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OrderDynamics.Stores.Web.Infrastructure.ApiClient;
using OrderDynamics.Stores.Web.Models;

namespace OrderDynamics.Stores.Web.Infrastructure.Services
{
    internal class FakeShipmentService : IFakeShipmentService
    {
        private readonly IApiClientFactory _apiClientFactory;

        public FakeShipmentService(IApiClientFactory apiClientFactory) {
            if (apiClientFactory == null) {
                throw new ArgumentNullException("apiClientFactory");
            }

            _apiClientFactory = apiClientFactory;
        }

        public Task<IEnumerable<ShipmentModel>> GetShipmentsAsync() {
            using (var client = _apiClientFactory.GetApiClient()) {
                return client.GetAsync<IEnumerable<ShipmentModel>>("FakeShipments");
            } 
        }

        public Task<ShipmentModel> GetShipmentAsync(int id) {
            using (var client = _apiClientFactory.GetApiClient()) {
                return client.GetAsync<ShipmentModel>(string.Format("FakeShipments/{0}", id));
            }
        }
    }
}
