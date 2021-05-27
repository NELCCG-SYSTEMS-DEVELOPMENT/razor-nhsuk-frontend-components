namespace SamNHS.NHSUKFrontend.RazorComponents.ViewComponents.Navigation
{
    using Microsoft.AspNetCore.Mvc;
    using SamNHS.NHSUKFrontend.RazorComponents.Models;
    using System.Threading.Tasks;

    public class HeaderViewComponent: ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync(HeaderProperties properties)
        {
            if (properties == null)
            {
                properties = new HeaderProperties();
            }
            return View(properties);
        }
    }
}