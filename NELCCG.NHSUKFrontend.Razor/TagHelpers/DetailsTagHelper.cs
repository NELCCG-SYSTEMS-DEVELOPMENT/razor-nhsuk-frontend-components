namespace NELCCG.NHSUKFrontend.Razor.TagHelpers
{
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Microsoft.AspNetCore.Mvc.TagHelpers;
    using Microsoft.AspNetCore.Razor.TagHelpers;
    using System;
    using System.Collections.Generic;
    using System.Text.Encodings.Web;
    using System.Threading.Tasks;

    [HtmlTargetElement("nhsuk-details")]
    public class DetailsTagHelper : TagHelper
    {
        /// <summary>
        /// Any additional css classes to apply.
        /// </summary>
        [HtmlAttributeName("classes")]
        public IEnumerable<string> CssClasses { get; set; } = Array.Empty<string>();
        /// <summary>
        /// Style as an expander.
        /// </summary>
        public bool Expander { get; set; }
        /// <summary>
        /// Details will be expanded.
        /// </summary>
        public bool Open { get; set; }

        /// <summary>
        /// Summary text.
        /// </summary>
        public string Summary { get; set; }
        

        public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
        {
            output.TagName = "details";
            output.AddClass("nhsuk-details", HtmlEncoder.Default);

            if (this.Expander)
            {
                output.AddClass("nhsuk-expander", HtmlEncoder.Default);
            }

            if (this.Open)
            {
                output.Attributes.Add("open", "open");
            }

            foreach (var cssClass in this.CssClasses)
            {
                output.AddClass(cssClass, HtmlEncoder.Default);
            }

            var summaryTag = new TagBuilder("summary");
            summaryTag.AddCssClass("nhsuk-details__summary");

            var summarySpanTag = new TagBuilder("span");
            summarySpanTag.AddCssClass("nhsuk-details__summary-text");
            summarySpanTag.InnerHtml.Append(this.Summary);

            summaryTag.InnerHtml.AppendHtml(summarySpanTag);
            output.Content.AppendHtml(summaryTag);

            var textContainer = new TagBuilder("div");
            textContainer.AddCssClass("nhsuk-details__text");

            var content = await output.GetChildContentAsync();
            textContainer.InnerHtml.AppendHtml(content);

            output.Content.AppendHtml(textContainer);
        }
    }
}
