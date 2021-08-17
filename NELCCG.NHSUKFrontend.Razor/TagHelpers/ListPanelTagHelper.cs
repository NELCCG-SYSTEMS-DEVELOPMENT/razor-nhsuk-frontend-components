namespace NELCCG.NHSUKFrontend.Razor.TagHelpers
{
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Microsoft.AspNetCore.Mvc.TagHelpers;
    using Microsoft.AspNetCore.Razor.TagHelpers;
    using NELCCG.NHSUKFrontend.Razor.Helpers;
    using System;
    using System.Collections.Generic;
    using System.Text.Encodings.Web;
    using System.Threading.Tasks;

    [HtmlTargetElement("nhsuk-list-panel", ParentTag = "nhsuk-list")]
    [RestrictChildren("nhsuk-list-panel-item")]
    public class ListPanelTagHelper : TagHelper
    {
        public string BackToTopLink { get; set; }
        /// <summary>
        /// Any additional css classes to apply.
        /// </summary>
        [HtmlAttributeName("classes")]
        public IEnumerable<string> CssClasses { get; set; } = Array.Empty<string>();
        /// <summary>
        /// Disable panel (no results to show).
        /// </summary>
        public bool Disable { get; set; }
        /// <summary>
        /// Set the heading level when <see cref="Label"/> is set.
        /// </summary>
        public int HeadingLevel { get; set; } = 2;
        public string Id { get; set; }
        /// <summary>
        /// Set message when <see cref="Disable"/> is <c>true</c>.
        /// </summary>
        public string Message { get; set; }
        public string Label { get; set; }

        private readonly IWebHostEnvironment _env;

        public ListPanelTagHelper(IWebHostEnvironment env)
        {
            this._env = env;
        }

        public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
        {
            output.TagName = "div";
            output.AddClass("nhsuk-list-panel", HtmlEncoder.Default);
            foreach (var cssClass in this.CssClasses)
            {
                output.AddClass(cssClass, HtmlEncoder.Default);
            }

            output.PreElement.SetHtmlContent("<li>");
            output.PostElement.SetHtmlContent("</li>");

            var hasLabel = !string.IsNullOrWhiteSpace(this.Label);

            if (hasLabel)
            {
                var heading = new TagBuilder($"h{this.HeadingLevel}");
                heading.AddCssClass("nhsuk-list-panel__label");
                if (!string.IsNullOrWhiteSpace(this.Id))
                {
                    heading.Attributes.Add("id", this.Id);
                }

                heading.InnerHtml.Append(this.Label);
                output.Content.AppendHtml(heading);
            }

            if (this.Disable)
            {
                var box = new TagBuilder("div");
                box.AddCssClass("nhsuk-list-panel__box");
                if (hasLabel)
                {
                    box.AddCssClass("nhsuk-list-panel__box--with-label");
                }

                var messageContainer = new TagBuilder("p");
                messageContainer.AddCssClass("nhsuk-list-panel--results-items__no-results");
                messageContainer.InnerHtml.Append(this.Message);
                box.InnerHtml.AppendHtml(messageContainer);
                output.Content.AppendHtml(box);
            }
            else
            {
                var list = new TagBuilder("ul");
                list.AddCssClass("nhsuk-list-panel__list");
                if (hasLabel)
                {
                    list.AddCssClass("nhsuk-list-panel__list--with-label");
                }

                output.Content.AppendHtml(list.RenderStartTag());
                var content = await output.GetChildContentAsync();
                output.Content.AppendHtml(content);
                output.Content.AppendHtml(list.RenderEndTag());
            }

            if (!string.IsNullOrWhiteSpace(this.BackToTopLink))
            {
                if (!this.BackToTopLink.StartsWith("#") && !Uri.IsWellFormedUriString(this.BackToTopLink, UriKind.RelativeOrAbsolute))
                {
                    throw new FormatException("Invalid format for BackToTopLink");
                }

                var container = new TagBuilder("div");
                container.AddCssClass("nhsuk-back-to-top");

                var anchorTag = new TagBuilder("a");
                anchorTag.AddCssClass("nhsuk-back-to-top__link");
                anchorTag.Attributes.Add("href", this.BackToTopLink);

                var svgIconHelper = new SvgIconHelper(this._env);
                var svgContent = await svgIconHelper.GetSvgIconContentAsync(SvgIconHelper.SvgIcon.ArrowRight);
                if (!string.IsNullOrWhiteSpace(svgContent))
                {
                    anchorTag.InnerHtml.AppendHtml(svgContent);
                }

                anchorTag.InnerHtml.Append("Back to top");
                container.InnerHtml.AppendHtml(anchorTag);
                output.Content.AppendHtml(container);
            }
        }
    }
}
