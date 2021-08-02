namespace SamNHS.NHSUKFrontend.MvcApp.Controllers
{
    using System.Collections.Generic;
    using System.Diagnostics;
    using Microsoft.AspNetCore.Diagnostics;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Logging;
    using SamNHS.NHSUKFrontend.MvcApp.Models;
    using SamNHS.NHSUKFrontend.Razor.Helpers;

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

        public IActionResult Form(User model)
        {
            if (this.Request.Method == "GET")
            {
                model.Biography = "This is line 1\nThis is line 2\t\tThis is tabbed";
                model.FavouriteColour = "Other";
                model.FavouriteColourOther = "Purple";
                model.Nationality = new List<string> { "british", "other" };
            }
            
            if (this.Request.Method == "POST")
            {
                DateInputHelper.UpdateFromFormRequest(model, this.Request);
            }

            return View(model);
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
