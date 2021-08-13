namespace NELCCG.NHSUKFrontend.Razor.TagHelpers
{
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Microsoft.AspNetCore.Mvc.TagHelpers;
    using Microsoft.AspNetCore.Razor.TagHelpers;
    using System;
    using System.Collections.Generic;
    using System.Text.Encodings.Web;

    [HtmlTargetElement("nhsuk-hero")]
    public class HeroTagHelper : TagHelper
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
        /// Url of the image to embed in the hero.
        /// </summary>
        public string ImageUrl { get; set; }
        /// <summary>
        /// Text to go under the heading text.
        /// </summary>
        public string Text { get; set; }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            output.TagName = "section";
            output.AddClass("nhsuk-hero", HtmlEncoder.Default);
            foreach (var cssClass in this.CssClasses)
            {
                output.AddClass(cssClass, HtmlEncoder.Default);
            }

            TagBuilder overlay = null;

            if (!string.IsNullOrWhiteSpace(this.ImageUrl))
            {
                if (!Uri.IsWellFormedUriString(this.ImageUrl, UriKind.RelativeOrAbsolute))
                {
                    throw new FormatException("Invalid format for ImageUrl");
                }

                output.AddClass("nhsuk-hero--image", HtmlEncoder.Default);
                output.Attributes.Add("style", $"background-image: url('{this.ImageUrl}')");

                if (!string.IsNullOrWhiteSpace(this.Heading))
                {
                    output.AddClass("nhsuk-hero--image-description", HtmlEncoder.Default);
                }

                overlay = new TagBuilder("div");
                overlay.AddCssClass("nhsuk-hero__overlay");
            }

            if (!string.IsNullOrWhiteSpace(this.Heading))
            {
                var container = new TagBuilder("div");
                container.AddCssClass("nhsuk-width-container");

                var row = new TagBuilder("div");
                row.AddCssClass("nhsuk-grid-row");

                var column = new TagBuilder("div");
                column.AddCssClass("nhsuk-grid-column-two-thirds");

                var content = new TagBuilder("div");
                content.AddCssClass(overlay != null ? "nhsuk-hero-content" : "nhsuk-hero__wrapper");

                var heading = new TagBuilder("h1");
                heading.InnerHtml.Append(this.Heading);

                if (!string.IsNullOrWhiteSpace(this.Text))
                {
                    heading.AddCssClass("nhsuk-u-margin-bottom-3");

                    var body = new TagBuilder("p");
                    body.AddCssClass("nhsuk-body-l");
                    body.AddCssClass("nhsuk-u-margin-bottom-0");
                    body.InnerHtml.Append(this.Text);

                    content.InnerHtml.AppendHtml(heading);
                    content.InnerHtml.AppendHtml(body);
                }
                else
                {
                    content.InnerHtml.AppendHtml(heading);
                }

                if (overlay != null)
                {
                    var arrow = new TagBuilder("span");
                    arrow.AddCssClass("nhsuk-hero__arrow");
                    arrow.Attributes.Add("aria-hidden", "true");

                    content.InnerHtml.AppendHtml(arrow);
                }
                else
                {
                    container.AddCssClass("nhsuk-hero--border");
                }

                column.InnerHtml.AppendHtml(content);
                row.InnerHtml.AppendHtml(column);
                container.InnerHtml.AppendHtml(row);

                if (overlay != null)
                {
                    overlay.InnerHtml.AppendHtml(container);
                    output.Content.AppendHtml(overlay);
                }
                else
                {
                    output.Content.AppendHtml(container);
                }
            }
            else if (overlay != null)
            {
                output.Content.AppendHtml(overlay);
            }
        }
    }
}
