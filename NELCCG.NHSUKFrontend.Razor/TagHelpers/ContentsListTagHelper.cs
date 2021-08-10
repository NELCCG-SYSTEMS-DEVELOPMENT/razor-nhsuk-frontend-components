namespace NELCCG.NHSUKFrontend.Razor.TagHelpers
{
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Microsoft.AspNetCore.Mvc.TagHelpers;
    using Microsoft.AspNetCore.Razor.TagHelpers;
    using System;
    using System.Collections.Generic;
    using System.Text.Encodings.Web;
    using System.Threading.Tasks;

    [HtmlTargetElement("nhsuk-contents-list")]
    [RestrictChildren("nhsuk-contents-list-item")]
    public class ContentsListTagHelper : TagHelper
    {
        public string AriaLabel { get; set; }
        /// <summary>
        /// Any additional css classes to apply.
        /// </summary>
        [HtmlAttributeName("classes")]
        public IEnumerable<string> CssClasses { get; set; } = Array.Empty<string>();
        /// <summary>
        /// Set the hidden heading level.
        /// </summary>
        public int HiddenHeadingLevel { get; set; } = 2;
        /// <summary>
        /// Set the hidden heading text.
        /// </summary>
        public string HiddenHeadingText { get; set; } = "Contents";
        

        public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
        {
            output.TagName = "nav";
            output.AddClass("nhsuk-contents-list", HtmlEncoder.Default);

            foreach (var cssClass in this.CssClasses)
            {
                output.AddClass(cssClass, HtmlEncoder.Default);
            }

            output.Attributes.Add("role", "navigation");

            if (!string.IsNullOrWhiteSpace(this.AriaLabel))
            {
                output.Attributes.Add("aria-label", this.AriaLabel);
            }

            if (!string.IsNullOrWhiteSpace(this.HiddenHeadingText))
            {
                var headingTag = new TagBuilder($"h{this.HiddenHeadingLevel}");
                headingTag.AddCssClass("nhsuk-u-visually-hidden");
                headingTag.InnerHtml.Append(this.HiddenHeadingText);
                output.Content.AppendHtml(headingTag);
            }

            var contentContainer = new TagBuilder("ol");
            contentContainer.AddCssClass("nhsuk-contents-list__list");

            var content = await output.GetChildContentAsync();
            contentContainer.InnerHtml.AppendHtml(content);

            output.Content.AppendHtml(contentContainer);
        }
    }
}
