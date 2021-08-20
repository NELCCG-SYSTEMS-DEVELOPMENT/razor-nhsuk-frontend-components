namespace NELCCG.NHSUKFrontend.Razor.TagHelpers
{
    using Microsoft.AspNetCore.Mvc.TagHelpers;
    using Microsoft.AspNetCore.Razor.TagHelpers;
    using System.Text.Encodings.Web;

    [HtmlTargetElement("nhsuk-skip-link")]
    public class SkipLinkTagHelper : TagHelper
    {
        public string Href { get; set; } = "#maincontent";

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            output.TagName = "a";
            output.TagMode = TagMode.StartTagAndEndTag;
            output.AddClass("nhsuk-skip-link", HtmlEncoder.Default);
            output.Attributes.SetAttribute("href", this.Href);
        }
    }
}
