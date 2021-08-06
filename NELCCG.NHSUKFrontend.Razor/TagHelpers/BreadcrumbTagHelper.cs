namespace NELCCG.NHSUKFrontend.Razor.TagHelpers
{
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Microsoft.AspNetCore.Mvc.TagHelpers;
    using Microsoft.AspNetCore.Razor.TagHelpers;
    using System.Text.Encodings.Web;

    [HtmlTargetElement("nhsuk-breadcrumb")]
    public class BreadcrumbTagHelper : TagHelper
    {
        public string AriaLabel { get; set; } = "Breadcrumb";

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            output.TagName = "nav";
            output.AddClass("nhsuk-breadcrumb", HtmlEncoder.Default);

            if (!string.IsNullOrWhiteSpace(this.AriaLabel))
            {
                output.Attributes.SetAttribute("aria-label", this.AriaLabel);
            }

            var wrapper = new TagBuilder("div");
            wrapper.AddCssClass("nhsuk-width-container");

            output.PreContent.SetHtmlContent(wrapper.RenderStartTag());
            output.PostContent.SetHtmlContent(wrapper.RenderEndTag());
        }
    }
}
