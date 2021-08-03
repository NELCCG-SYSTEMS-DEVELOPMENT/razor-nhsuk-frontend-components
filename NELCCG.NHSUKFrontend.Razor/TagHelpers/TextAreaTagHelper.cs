namespace NELCCG.NHSUKFrontend.Razor.TagHelpers
{
    using Microsoft.AspNetCore.Razor.TagHelpers;
    using Microsoft.AspNetCore.Mvc.TagHelpers;
    using System.Text.Encodings.Web;
    using System.Threading.Tasks;

    [HtmlTargetElement("nhsuk-textarea")]
    public class TextAreaTagHelper : FormInputElementTagHelperBase
    {
        public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
        {
            await base.ProcessAsync(context, output);

            output.TagName = "textarea";
            output.TagMode = TagMode.StartTagAndEndTag;
            output.AddClass("nhsuk-textarea", HtmlEncoder.Default);
            if (this.Value != null)
            {
                output.Attributes.RemoveAll("value");
                output.Content.SetContent(this.Value.ToString());
            }

            var childContent = await output.GetChildContentAsync();
            output.PreElement.AppendHtml(childContent);
        }
    }
}
