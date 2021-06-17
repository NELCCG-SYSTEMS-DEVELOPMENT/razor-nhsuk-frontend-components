namespace SamNHS.NHSUKFrontend.Razor.ViewComponents.Navigation
{
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using SamNHS.NHSUKFrontend.Razor.Models;

    public class FooterViewComponent : ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync(FooterProperties properties)
        {
            if (properties == null)
            {
                properties = new FooterProperties();
            }
            return View(properties);
        }
    }
}