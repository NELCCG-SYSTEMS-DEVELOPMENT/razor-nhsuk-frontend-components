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

    [HtmlTargetElement("nhsuk-pagination")]
    public class PaginationTagHelper : TagHelper
    {
        private readonly IWebHostEnvironment _env;

        public PaginationTagHelper(IWebHostEnvironment env)
        {
            this._env = env;
        }

        public string AriaLabel { get; set; } = "Pagination";
        /// <summary>
        /// Any additional css classes to apply.
        /// </summary>
        [HtmlAttributeName("classes")]
        public IEnumerable<string> CssClasses { get; set; } = Array.Empty<string>();
        /// <summary>
        /// The name of the next page.
        /// </summary>
        public string NextPage { get; set; }
        /// <summary>
        /// The title before the name of the next page.
        /// </summary>
        public string NextTitle { get; set; } = "Next";
        /// <summary>
        /// The next url to link to.
        /// </summary>
        public string NextUrl { get; set; }
        /// <summary>
        /// The name of the previous page.
        /// </summary>
        public string PreviousPage { get; set; }
        /// <summary>
        /// The title before the name of the previous page.
        /// </summary>
        public string PreviousTitle { get; set; } = "Previous";
        /// <summary>
        /// The previous url to link to.
        /// </summary>
        public string PreviousUrl { get; set; }

        public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
        {
            output.TagName = "nav";
            output.TagMode = TagMode.StartTagAndEndTag;
            output.AddClass("nhsuk-pagination", HtmlEncoder.Default);
            foreach (var cssClass in this.CssClasses)
            {
                output.AddClass(cssClass, HtmlEncoder.Default);
            }

            output.Attributes.SetAttribute("role", "navigation");
            output.Attributes.SetAttribute("aria-label", this.AriaLabel);

            var list = new TagBuilder("ul");
            list.AddCssClass("nhsuk-list");
            list.AddCssClass("nhsuk-pagination__list");

            var previousSet = !string.IsNullOrWhiteSpace(this.PreviousPage) && !string.IsNullOrWhiteSpace(this.PreviousUrl);
            var nextSet = !string.IsNullOrWhiteSpace(this.NextPage) && !string.IsNullOrWhiteSpace(this.NextUrl);

            if (previousSet)
            {
                await AddPaginationLink(list, "previous", this.PreviousUrl, this.PreviousTitle, this.PreviousPage);
            }

            if (nextSet)
            {
                await AddPaginationLink(list, "next", this.NextUrl, this.NextTitle, this.NextPage);
            }

            output.Content.AppendHtml(list);
        }

        private async Task AddPaginationLink(TagBuilder list, string type, string url, string title, string page)
        {
            if (!url.StartsWith("#") && !Uri.IsWellFormedUriString(url, UriKind.RelativeOrAbsolute))
            {
                throw new FormatException($"Invalid format for {type} url");
            }

            var li = new TagBuilder("li");
            li.AddCssClass($"nhsuk-pagination-item--{type}"); // 'previous' or 'next'

            var anchorTag = new TagBuilder("a");
            anchorTag.AddCssClass("nhsuk-pagination__link");
            anchorTag.AddCssClass($"nhsuk-pagination__link--{type[..4]}"); // 'prev' or 'next'
            anchorTag.Attributes.Add("href", url);

            var titleSpan = new TagBuilder("span");
            titleSpan.AddCssClass("nhsuk-pagination__title");
            titleSpan.InnerHtml.Append(title);

            var hiddenSpan = new TagBuilder("span");
            hiddenSpan.AddCssClass("nhsuk-u-visually-hidden");
            hiddenSpan.InnerHtml.Append(":");

            var pageSpan = new TagBuilder("span");
            pageSpan.AddCssClass("nhsuk-pagination__page");
            pageSpan.InnerHtml.Append(page);

            anchorTag.InnerHtml.AppendHtml(titleSpan);
            anchorTag.InnerHtml.AppendHtml(hiddenSpan);
            anchorTag.InnerHtml.AppendHtml(pageSpan);

            if (type is "previous" or "next")
            {
                var svgIconHelper = new SvgIconHelper(this._env);
                var svgContent = await svgIconHelper.GetSvgIconContentAsync(
                    type == "previous" ? SvgIconHelper.SvgIcon.ArrowLeft
                    : SvgIconHelper.SvgIcon.ArrowRight
                );
                if (!string.IsNullOrWhiteSpace(svgContent))
                {
                    anchorTag.InnerHtml.AppendHtml(svgContent);
                }
            }

            li.InnerHtml.AppendHtml(anchorTag);
            list.InnerHtml.AppendHtml(li);
        }
    }
}