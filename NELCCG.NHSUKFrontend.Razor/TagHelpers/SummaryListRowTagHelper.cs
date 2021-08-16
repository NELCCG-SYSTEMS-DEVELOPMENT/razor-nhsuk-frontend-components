namespace NELCCG.NHSUKFrontend.Razor.TagHelpers
{
    using Microsoft.AspNetCore.Mvc.TagHelpers;
    using Microsoft.AspNetCore.Razor.TagHelpers;
    using System.Text.Encodings.Web;
    using System.Threading.Tasks;

    [HtmlTargetElement("nhsuk-summary-list-row", ParentTag = "nhsuk-summary-list")]
    [RestrictChildren("nhsuk-summary-list-row-key", "nhsuk-summary-list-row-value", "nhsuk-summary-list-row-actions")]
    public class SummaryListRowTagHelper : TagHelper
    {
        public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
        {
            output.TagName = "div";
            output.AddClass("nhsuk-summary-list__row", HtmlEncoder.Default);

            var content = await output.GetChildContentAsync();
            output.Content.AppendHtml(content);
        }
    }
}
