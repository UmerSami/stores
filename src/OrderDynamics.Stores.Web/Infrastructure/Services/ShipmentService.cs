using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OrderDynamics.Stores.Web.Infrastructure.ApiClient;
using OrderDynamics.Stores.Web.Models;

namespace OrderDynamics.Stores.Web.Infrastructure.Services
{
    internal class ShipmentService : IShipmentService
    {
        private readonly IApiClient _apiClient;

        public ShipmentService(IApiClient apiClient) {
            if (apiClient == null) {
                throw new ArgumentNullException("apiClient");
            }

            _apiClient = apiClient;
        }

        public Task<IEnumerable<ShipmentModel>> GetShipmentsAsync() {
            return _apiClient.GetAsync<IEnumerable<ShipmentModel>>("Shipments");
        }

        public Task<ShipmentModel> GetShipmentAsync(int id) {
            return _apiClient.GetAsync<ShipmentModel>(string.Format("Shipments/{0}", id));
        }
    }
}
