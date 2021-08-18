namespace NELCCG.NHSUKFrontend.Razor.TagHelpers
{
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Microsoft.AspNetCore.Mvc.TagHelpers;
    using Microsoft.AspNetCore.Razor.TagHelpers;
    using System;
    using System.Collections.Generic;
    using System.Text.Encodings.Web;
    using System.Threading.Tasks;

    [HtmlTargetElement("nhsuk-warning-callout")]
    public class WarningCalloutTagHelper : TagHelper
    {
        /// <summary>
        /// Any additional css classes to apply.
        /// </summary>
        [HtmlAttributeName("classes")]
        public IEnumerable<string> CssClasses { get; set; } = Array.Empty<string>();
        /// <summary>
        /// Set the heading text.
        /// </summary>
        public string Heading { get; set; }
        /// <summary>
        /// Set the heading level.
        /// </summary>
        public int HeadingLevel { get; set; } = 3;
        /// <summary>
        /// This is set for screen reader users.
        ///   If the default text <c>"Important"</c> is in the heading, set to <c>null</c> or <c>string.Empty</c>.
        /// </summary>
        public string VisuallyHiddenText { get; set; } = "Important: ";

        public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
        {
            output.TagName = "div";
            output.TagMode = TagMode.StartTagAndEndTag;
            output.AddClass("nhsuk-warning-callout", HtmlEncoder.Default);
            foreach (var cssClass in this.CssClasses)
            {
                output.AddClass(cssClass, HtmlEncoder.Default);
            }

            var headingTag = new TagBuilder($"h{this.HeadingLevel}");
            headingTag.AddCssClass("nhsuk-warning-callout__label");

            var headingSpan = new TagBuilder("span");
            headingSpan.Attributes.Add("role", "text");

            if (!string.IsNullOrWhiteSpace(this.VisuallyHiddenText))
            {
                var hiddenSpan = new TagBuilder("span");
                hiddenSpan.AddCssClass("nhsuk-u-visually-hidden");
                hiddenSpan.InnerHtml.Append(this.VisuallyHiddenText);
                headingSpan.InnerHtml.AppendHtml(hiddenSpan);
            }

            headingSpan.InnerHtml.Append(this.Heading);
            headingTag.InnerHtml.AppendHtml(headingSpan);

            output.Content.AppendHtml(headingTag);

            var content = await output.GetChildContentAsync();
            output.Content.AppendHtml(content);
        }
    }
}
