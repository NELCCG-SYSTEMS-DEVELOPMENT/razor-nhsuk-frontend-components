namespace NELCCG.NHSUKFrontend.MvcApp.Controllers
{
    using Microsoft.AspNetCore.Mvc;

    public class ContentController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult CareCard()
        {
            return View();
        }

        public IActionResult Details()
        {
            return View();
        }

    }
}
