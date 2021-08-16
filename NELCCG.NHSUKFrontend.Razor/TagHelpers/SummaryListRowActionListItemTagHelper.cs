namespace NELCCG.NHSUKFrontend.Razor.TagHelpers
{
    using Microsoft.AspNetCore.Mvc.TagHelpers;
    using Microsoft.AspNetCore.Razor.TagHelpers;
    using System.Text.Encodings.Web;
    using System.Threading.Tasks;

    [HtmlTargetElement("nhsuk-summary-list-row-action-list-item", ParentTag = "nhsuk-summary-list-row-action-list")]
    [RestrictChildren("a")]
    public class SummaryListRowActionListItemTagHelper : TagHelper
    {
        public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
        {
            output.TagName = "li";
            output.AddClass("nhsuk-summary-list__actions-list-item", HtmlEncoder.Default);

            var content = await output.GetChildContentAsync();
            output.Content.AppendHtml(content);
        }
    }
}
