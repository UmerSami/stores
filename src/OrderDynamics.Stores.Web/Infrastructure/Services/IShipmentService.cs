using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OrderDynamics.Stores.Web.Models;

namespace OrderDynamics.Stores.Web.Infrastructure.Services
{
    public interface IShipmentService {
        Task<IEnumerable<ShipmentModel>> GetShipmentsAsync();
        Task<ShipmentModel> GetShipmentAsync(int id);
    }
}
