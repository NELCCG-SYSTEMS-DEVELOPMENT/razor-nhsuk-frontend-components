namespace NELCCG.NHSUKFrontend.Razor.TagHelpers
{
    using Microsoft.AspNetCore.Mvc.TagHelpers;
    using Microsoft.AspNetCore.Razor.TagHelpers;
    using System.Text.Encodings.Web;

    [HtmlTargetElement("nhsuk-checkbox-divider", ParentTag = "nhsuk-checkboxes")]
    [RestrictChildren("span")]
    public class CheckboxDividerTagHelper : TagHelper
    {
        public string Text { get; set; }
        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            output.TagName = "div";
            output.TagMode = TagMode.StartTagAndEndTag;
            output.AddClass("nhsuk-checkboxes__divider", HtmlEncoder.Default);
        }
    }
}
