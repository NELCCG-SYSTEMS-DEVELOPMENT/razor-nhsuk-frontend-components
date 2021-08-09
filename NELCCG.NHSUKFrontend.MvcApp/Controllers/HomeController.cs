namespace NELCCG.NHSUKFrontend.MvcApp.Controllers
{
    using System.Collections.Generic;
    using System.Diagnostics;
    using Microsoft.AspNetCore.Diagnostics;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Logging;
    using NELCCG.NHSUKFrontend.MvcApp.Models;
    using NELCCG.NHSUKFrontend.Razor.Helpers;

    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            this._logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult SvgIcons()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            var feature = this.HttpContext.Features.Get<IExceptionHandlerFeature>();
            this._logger.LogError(feature?.Error, feature?.Error.Message);
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier, Error = feature?.Error });
        }
    }
}
