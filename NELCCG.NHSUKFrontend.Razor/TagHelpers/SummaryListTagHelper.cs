namespace NELCCG.NHSUKFrontend.Razor.TagHelpers
{
    using Microsoft.AspNetCore.Mvc.TagHelpers;
    using Microsoft.AspNetCore.Razor.TagHelpers;
    using System.Text.Encodings.Web;
    using System.Threading.Tasks;

    [HtmlTargetElement("nhsuk-summary-list")]
    [RestrictChildren("nhsuk-summary-list-row")]
    public class SummaryListTagHelper : TagHelper
    {
        public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
        {
            output.TagName = "dl";
            output.AddClass("nhsuk-summary-list", HtmlEncoder.Default);

            var content = await output.GetChildContentAsync();
            output.Content.AppendHtml(content);
        }
    }
}
