namespace NELCCG.NHSUKFrontend.MvcApp.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using NELCCG.NHSUKFrontend.MvcApp.Models;

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

        public IActionResult DoDontLists()
        {
            return View();
        }

        public IActionResult Hero(HeroExample model)
        {
            if (this.Request.Method == "GET")
            {
                model.HeroTitle = "Hero";
            }
            
            return View(model);
        }

        public IActionResult Image()
        {
            return View();
        }

    }
}
