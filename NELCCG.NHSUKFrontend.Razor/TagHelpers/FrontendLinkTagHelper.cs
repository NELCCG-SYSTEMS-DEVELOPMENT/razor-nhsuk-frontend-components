namespace NELCCG.NHSUKFrontend.Razor.TagHelpers
{
    using Microsoft.AspNetCore.Mvc.Razor.TagHelpers;
    using Microsoft.AspNetCore.Mvc.Routing;
    using Microsoft.AspNetCore.Razor.TagHelpers;
    using NELCCG.NHSUKFrontend.Razor.Models;
    using System.Text.Encodings.Web;

    [HtmlTargetElement("link", Attributes = "nhsuk-frontend-version")]
    public class FrontendLinkTagHelper : UrlResolutionTagHelper
    {
        public FrontendLinkTagHelper(IUrlHelperFactory urlHelperFactory, HtmlEncoder htmlEncoder) : base(urlHelperFactory, htmlEncoder)
        {
        }

        [HtmlAttributeName("nhsuk-frontend-version")]
        public FrontendOptions.FrontendVersion Version { get; set; }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            var ns = System.IO.Path.GetFileNameWithoutExtension(this.GetType().Module.Name);
            var href = this.Version switch
            {
                FrontendOptions.FrontendVersion.Version__4_1_0 => $"~/_content/{ns}/css/nhsuk-4.1.0.min.css",
                FrontendOptions.FrontendVersion.Version__5_1_0 => $"~/_content/{ns}/css/nhsuk-5.1.0.min.css",
                _ => null,
            };

            if (href != null)
            {
                var urlHelper = this.UrlHelperFactory.GetUrlHelper(this.ViewContext);
                output.Attributes.SetAttribute("rel", "stylesheet");
                output.Attributes.SetAttribute("href", urlHelper.Content(href));
            }
        }
    }
}
