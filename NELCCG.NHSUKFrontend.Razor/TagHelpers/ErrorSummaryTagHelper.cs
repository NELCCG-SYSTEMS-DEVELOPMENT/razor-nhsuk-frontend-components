namespace NELCCG.NHSUKFrontend.Razor.TagHelpers
{
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Microsoft.AspNetCore.Mvc.TagHelpers;
    using Microsoft.AspNetCore.Razor.TagHelpers;
    using System;
    using System.Collections.Generic;
    using System.Text.Encodings.Web;
    using System.Threading.Tasks;

    [HtmlTargetElement("nhsuk-error-summary")]
    [RestrictChildren("nhsuk-error-summary-list-item")]
    public class ErrorSummaryTagHelper : TagHelper
    {
        /// <summary>
        /// Any additional css classes to apply.
        /// </summary>
        [HtmlAttributeName("classes")]
        public IEnumerable<string> CssClasses { get; set; } = Array.Empty<string>();
        /// <summary>
        /// Description of the errors.
        /// </summary>
        public string Description { get; set; }
        /// <summary>
        /// Encode the description if it may not be safe content.
        /// </summary>
        public bool EncodeDescription { get; set; }
        /// <summary>
        /// Encode the heading if it may not be safe content.
        /// </summary>
        public bool EncodeHeading { get; set; }
        /// <summary>
        /// Set the heading text.
        /// </summary>
        public string Heading { get; set; } = "There is a problem";
        /// <summary>
        /// Set the heading level.
        /// </summary>
        public int HeadingLevel { get; set; } = 2;

        public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
        {
            output.TagName = "div";
            output.AddClass("nhsuk-error-summary", HtmlEncoder.Default);

            foreach (var cssClass in this.CssClasses)
            {
                output.AddClass(cssClass, HtmlEncoder.Default);
            }

            output.Attributes.Add("aria-labelledby", "error-summary-title");
            output.Attributes.Add("role", "alert");
            output.Attributes.Add("tabindex", "-1");

            var headingTag = new TagBuilder($"h{this.HeadingLevel}");
            headingTag.AddCssClass("nhsuk-error-summary__title");
            headingTag.Attributes.Add("id", "error-summary-title");
            if (this.EncodeHeading)
            {
                headingTag.InnerHtml.Append(this.Heading);
            }
            else
            {
                headingTag.InnerHtml.AppendHtml(this.Heading);
            }

            output.Content.AppendHtml(headingTag);

            var contentContainer = new TagBuilder("div");
            contentContainer.AddCssClass("nhsuk-error-summary__body");
            if (!string.IsNullOrWhiteSpace(this.Description))
            {
                var descriptionTag = new TagBuilder("p");
                if (this.EncodeDescription)
                {
                    descriptionTag.InnerHtml.Append(this.Description);
                }
                else
                {
                    descriptionTag.InnerHtml.AppendHtml(this.Description);
                }

                contentContainer.InnerHtml.AppendHtml(descriptionTag);
            }

            var listContainer = new TagBuilder("ul");
            listContainer.AddCssClass("nhsuk-list");
            listContainer.AddCssClass("nhsuk-error-summary__list");

            var content = await output.GetChildContentAsync();
            listContainer.InnerHtml.AppendHtml(content);

            contentContainer.InnerHtml.AppendHtml(listContainer);

            output.Content.AppendHtml(contentContainer);
        }
    }
}
