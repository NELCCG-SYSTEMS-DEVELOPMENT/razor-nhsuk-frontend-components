namespace SamNHS.NHSUKFrontend.Razor.TagHelpers
{
    using Microsoft.AspNetCore.Mvc.Razor.TagHelpers;
    using Microsoft.AspNetCore.Mvc.Routing;
    using Microsoft.AspNetCore.Razor.TagHelpers;
    using SamNHS.NHSUKFrontend.Razor.Models;
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
            output.Attributes.SetAttribute("rel", "stylesheet");

            switch (Version)
            {
                case FrontendOptions.FrontendVersion.Version__4_1_0:
                    output.Attributes.Add("href", this.UrlHelperFactory.GetUrlHelper(this.ViewContext).Content(
                        $"~/_content/{ns}/css/nhsuk-4.1.0.min.css"
                    ));
                    
                    break;
                case FrontendOptions.FrontendVersion.Version__5_1_0:
                    output.Attributes.Add("href", this.UrlHelperFactory.GetUrlHelper(this.ViewContext).Content(
                        $"~/_content/{ns}/css/nhsuk-5.1.0.min.css"
                    ));
                    break;
                default:
                    break;
            }
            
        }
    }
}
