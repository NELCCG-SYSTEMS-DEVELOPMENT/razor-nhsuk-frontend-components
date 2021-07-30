namespace SamNHS.NHSUKFrontend.Razor.TagHelpers
{
    using Microsoft.AspNetCore.Mvc.Razor.TagHelpers;
    using Microsoft.AspNetCore.Mvc.Routing;
    using Microsoft.AspNetCore.Razor.TagHelpers;
    using SamNHS.NHSUKFrontend.Razor.Models;
    using System.Text.Encodings.Web;

    [HtmlTargetElement("script")]
    public class FrontendScriptTagHelper : UrlResolutionTagHelper
    {
        public FrontendScriptTagHelper(IUrlHelperFactory urlHelperFactory, HtmlEncoder htmlEncoder) : base(urlHelperFactory, htmlEncoder)
        {
        }

        [HtmlAttributeName("nhsuk-frontend-version")]
        public FrontendOptions.FrontendVersion Version { get; set; }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            if (!context.AllAttributes.ContainsName("nhsuk-frontend-version")) return;

            var ns = System.IO.Path.GetFileNameWithoutExtension(this.GetType().Module.Name);

            switch (Version)
            {
                case FrontendOptions.FrontendVersion.Version__4_1_0:
                    output.Attributes.Add("src", this.UrlHelperFactory.GetUrlHelper(this.ViewContext).Content(
                        $"~/_content/{ns}/js/nhsuk-4.1.0.min.js"
                    ));
                    
                    break;
                case FrontendOptions.FrontendVersion.Version__5_1_0:
                    output.Attributes.Add("src", this.UrlHelperFactory.GetUrlHelper(this.ViewContext).Content(
                        $"~/_content/{ns}/js/nhsuk-5.1.0.min.js"
                    ));
                    break;
                default:
                    break;
            }
            
        }
    }
}
