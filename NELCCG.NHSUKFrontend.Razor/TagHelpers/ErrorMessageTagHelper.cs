namespace NELCCG.NHSUKFrontend.Razor.TagHelpers
{
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Microsoft.AspNetCore.Mvc.TagHelpers;
    using Microsoft.AspNetCore.Razor.TagHelpers;
    using System.Text.Encodings.Web;

    [HtmlTargetElement("nhsuk-error-message")]
    public class ErrorMessageTagHelper : TagHelper
    {
        /// <summary>
        /// This is set for screen reader users.
        /// </summary>
        public string VisuallyHiddenText { get; set; } = "Error: ";

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            output.TagName = "div";
            output.TagMode = TagMode.StartTagAndEndTag;
            output.AddClass("nhsuk-error-message", HtmlEncoder.Default);
            if (!string.IsNullOrWhiteSpace(this.VisuallyHiddenText))
            {
                var visuallyHidden = new TagBuilder("span");
                visuallyHidden.AddCssClass("nhsuk-u-visually-hidden");
                visuallyHidden.InnerHtml.Append(this.VisuallyHiddenText);
                output.PreContent.AppendHtml(visuallyHidden);
            }
        }
    }
}
