namespace SamNHS.NHSUKFrontend.Razor.TagHelpers
{
    using Microsoft.AspNetCore.Mvc.TagHelpers;
    using Microsoft.AspNetCore.Razor.TagHelpers;
    using System.Text.Encodings.Web;
    using System.Threading.Tasks;

    [HtmlTargetElement("nhsuk-checkboxes")]
    [RestrictChildren("nhsuk-checkbox", "nhsuk-checkbox-conditional")]
    public class CheckboxesTagHelper : ElementTagHelperBase
    {
        public bool ContainsConditional { get; set; }

        public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
        {
            await base.ProcessAsync(context, output);

            output.TagName = "div";
            output.AddClass("nhsuk-checkboxes", HtmlEncoder.Default);
            if (this.ContainsConditional)
            {
                output.AddClass("nhsuk-checkboxes--conditional", HtmlEncoder.Default);
            }
        }
    }
}
