namespace NELCCG.NHSUKFrontend.Razor.TagHelpers
{
    using Microsoft.AspNetCore.Mvc.TagHelpers;
    using Microsoft.AspNetCore.Razor.TagHelpers;
    using System.Text.Encodings.Web;
    using System.Threading.Tasks;

    [HtmlTargetElement("nhsuk-table-body", ParentTag = "nhsuk-table")]
    [RestrictChildren("nhsuk-table-body-row")]
    public class TableBodyTagHelper : TagHelper
    {
        public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
        {
            await base.ProcessAsync(context, output);

            context.Items.Add("table-cell-container", "tbody");

            output.TagName = "tbody";
            output.AddClass("nhsuk-table__body", HtmlEncoder.Default);
        }
    }
}
