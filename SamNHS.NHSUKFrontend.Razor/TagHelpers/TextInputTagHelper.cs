namespace SamNHS.NHSUKFrontend.Razor.TagHelpers
{
    using Microsoft.AspNetCore.Razor.TagHelpers;
    using Microsoft.AspNetCore.Mvc.TagHelpers;
    using System.Text.Encodings.Web;
    using System.Threading.Tasks;

    [HtmlTargetElement("nhsuk-input")]
    public class TextInputTagHelper : FormInputElementTagHelperBase
    {
        public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
        {
            await base.ProcessAsync(context, output);

            output.TagName = "input";
            output.TagMode = TagMode.SelfClosing;
            output.Attributes.SetAttribute("type", "text");
            output.AddClass("nhsuk-input", HtmlEncoder.Default);

            if (this.For != null)
            {
                if (this.For.Metadata.ModelType == typeof(int) || this.For.Metadata.ModelType == typeof(uint))
                {
                    output.Attributes.SetAttribute("inputmode", "numeric");
                    output.Attributes.SetAttribute("pattern", "[0-9]*");
                }
            }

            var childContent = await output.GetChildContentAsync();
            output.PreElement.AppendHtml(childContent);
        }
    }
}
