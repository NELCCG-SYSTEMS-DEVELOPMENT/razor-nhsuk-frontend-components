namespace SamNHS.NHSUKFrontend.Razor.TagHelpers
{
    using Microsoft.AspNetCore.Mvc.TagHelpers;
    using Microsoft.AspNetCore.Razor.TagHelpers;
    using System.Text.Encodings.Web;

    [HtmlTargetElement("nhsuk-fieldset")]
    public class FieldsetTagHelper : TagHelper
    {
        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            output.TagName = "fieldset";
            output.TagMode = TagMode.StartTagAndEndTag;
            output.AddClass("nhsuk-fieldset", HtmlEncoder.Default);
        }
    }
}
