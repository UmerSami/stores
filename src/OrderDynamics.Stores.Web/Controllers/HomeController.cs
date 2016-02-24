using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Mvc;
using OrderDynamics.Stores.Web.Infrastructure.Services;

namespace OrderDynamics.Stores.Web.Controllers {
    public class HomeController : Controller {

        private readonly IFakeShipmentService _fakeShipmentService;

        public HomeController(IFakeShipmentService fakeShipmentService) {
            if (fakeShipmentService == null) {
                throw new ArgumentNullException("fakeShipmentService");
            }

            _fakeShipmentService = fakeShipmentService;
        }

        public async Task<IActionResult> Index() {
            var shipments = await _fakeShipmentService.GetShipmentsAsync();

            return View(shipments);
        }

    }
}