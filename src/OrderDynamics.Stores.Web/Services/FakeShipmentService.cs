using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using OrderDynamics.Stores.Web.Infrastructure.ApiClient;
using OrderDynamics.Stores.Web.Models;

namespace OrderDynamics.Stores.Web.Services
{
    public class FakeShipmentService : IFakeShipmentService
    {
        private readonly IApiClientFactory _apiClientFactory;

        public FakeShipmentService(IApiClientFactory apiClientFactory) {
            if (apiClientFactory == null) {
                throw new ArgumentNullException("apiClientFactory");
            }

            _apiClientFactory = apiClientFactory;
        }

        //the async await part of this method is crucial, if you would remove it -> 
        //client will be disposed BEFORE you will actually execute the Api Call and will fail
        public async Task<IEnumerable<ShipmentModel>> GetShipmentsAsync() {
            using (var client = _apiClientFactory.GetApiClient()) {
                return await client.GetAsync<IEnumerable<ShipmentModel>>("FakeShipments");
            } 
        }

        //the async await part of this method is crucial, if you would remove it -> 
        //client will be disposed BEFORE you will actually execute the Api Call and will fail
        public async Task<ShipmentModel> GetShipmentAsync(int id) {
            using (var client = _apiClientFactory.GetApiClient()) {
                return await client.GetAsync<ShipmentModel>(string.Format("FakeShipments/{0}", id));
            }
        }
    }
}
