using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Mvc;
using OrderDynamics.Stores.Web.Infrastructure.Services;

namespace OrderDynamics.Stores.Web.Controllers {
    public class HomeController : Controller {

        private readonly IShipmentService _shipmentService;

        public HomeController(IShipmentService shipmentService) {
            if (shipmentService == null) {
                throw new ArgumentNullException("shipmentService");
            }

            _shipmentService = shipmentService;
        }

        public async Task<IActionResult> Index() {
            var shipments = await _shipmentService.GetShipmentsAsync();

            return View(shipments);
        }

    }
}