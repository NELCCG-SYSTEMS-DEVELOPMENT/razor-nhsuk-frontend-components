namespace NELCCG.NHSUKFrontend.Razor.TagHelpers
{
    using Microsoft.AspNetCore.Mvc.TagHelpers;
    using Microsoft.AspNetCore.Razor.TagHelpers;
    using System.Text.Encodings.Web;

    [HtmlTargetElement("nhsuk-list")]
    [RestrictChildren("li")]
    public class ListTagHelper : TagHelper
    {
        public bool OrderedList { get; set; }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            output.TagName = this.OrderedList ? "ol" : "ul";
            output.AddClass("nhsuk-list", HtmlEncoder.Default);
        }
    }
}
