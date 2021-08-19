namespace NELCCG.NHSUKFrontend.Razor.TagHelpers
{
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Microsoft.AspNetCore.Mvc.TagHelpers;
    using Microsoft.AspNetCore.Razor.TagHelpers;
    using System.Text.Encodings.Web;
    using System.Threading.Tasks;

    [HtmlTargetElement("nhsuk-table-head", ParentTag = "nhsuk-table")]
    public class TableHeadTagHelper : TagHelper
    {
        public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
        {
            await base.ProcessAsync(context, output);

            context.Items.Add("table-cell-container", "thead");

            output.TagName = "thead";
            output.AddClass("nhsuk-table__head", HtmlEncoder.Default);
            output.Attributes.SetAttribute("role", "rowgroup");

            var tr = new TagBuilder("tr");
            tr.AddCssClass("nhsuk-table__row");
            tr.Attributes.Add("role", "row");

            output.PreContent.AppendHtml(tr.RenderStartTag());
            output.PostContent.AppendHtml(tr.RenderEndTag());
        }
    }
}
