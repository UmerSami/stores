﻿using Microsoft.AspNet.Mvc;

namespace OrderDynamics.Stores.Web.Controllers {
    public class HomeController : Controller {

        public IActionResult Index() {
            return Content("Hello world from a Controller!");
        }

    }
}