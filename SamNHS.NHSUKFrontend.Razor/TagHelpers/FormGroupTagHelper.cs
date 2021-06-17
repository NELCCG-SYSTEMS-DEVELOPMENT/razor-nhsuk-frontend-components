namespace SamNHS.NHSUKFrontend.Razor.TagHelpers
{
    using Microsoft.AspNetCore.Mvc.TagHelpers;
    using Microsoft.AspNetCore.Razor.TagHelpers;
    using System.Text.Encodings.Web;

    [HtmlTargetElement("nhsuk-form-group")]
    public class FormGroupTagHelper : TagHelper
    {
        public bool ContainsErrorMessage { get; set; }
        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            output.TagName = "div";
            output.TagMode = TagMode.StartTagAndEndTag;
            output.AddClass("nhsuk-form-group", HtmlEncoder.Default);
            if (this.ContainsErrorMessage)
            {
                output.AddClass("nhsuk-form-group--error", HtmlEncoder.Default);
            }
        }
    }
}
