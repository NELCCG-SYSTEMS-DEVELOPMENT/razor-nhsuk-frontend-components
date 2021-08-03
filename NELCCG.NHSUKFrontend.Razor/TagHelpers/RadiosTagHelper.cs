namespace NELCCG.NHSUKFrontend.Razor.TagHelpers
{
    using Microsoft.AspNetCore.Mvc.TagHelpers;
    using Microsoft.AspNetCore.Razor.TagHelpers;
    using System.Text.Encodings.Web;
    using System.Threading.Tasks;

    [HtmlTargetElement("nhsuk-radios")]
    [RestrictChildren("nhsuk-radio", "nhsuk-radio-conditional", "nhsuk-radio-divider")]
    public class RadiosTagHelper : ElementTagHelperBase
    {
        public bool ContainsConditional { get; set; }

        public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
        {
            await base.ProcessAsync(context, output);

            output.TagName = "div";
            output.AddClass("nhsuk-radios", HtmlEncoder.Default);
            if (this.ContainsConditional)
            {
                output.AddClass("nhsuk-radios--conditional", HtmlEncoder.Default);
            }
        }
    }
}
