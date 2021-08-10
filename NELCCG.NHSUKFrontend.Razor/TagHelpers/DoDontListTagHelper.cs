namespace NELCCG.NHSUKFrontend.Razor.TagHelpers
{
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Microsoft.AspNetCore.Mvc.TagHelpers;
    using Microsoft.AspNetCore.Razor.TagHelpers;
    using System;
    using System.Collections.Generic;
    using System.Text.Encodings.Web;
    using System.Threading.Tasks;

    [HtmlTargetElement("nhsuk-do-dont-list")]
    [RestrictChildren("nhsuk-do-dont-list-item")]
    public class DoDontListTagHelper : TagHelper
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

        public DoDontListType Type { get; set; } = DoDontListType.Do;

        public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
        {
            context.Items.Add("list-type", this.Type);

            output.TagName = "div";
            output.AddClass("nhsuk-do-dont-list", HtmlEncoder.Default);

            foreach (var cssClass in this.CssClasses)
            {
                output.AddClass(cssClass, HtmlEncoder.Default);
            }

            if (string.IsNullOrWhiteSpace(this.Heading))
            {
                this.Heading = this.Type switch
                {
                    DoDontListType.Do => "Do",
                    DoDontListType.Dont => "Don't",
                    _ => null,
                };
            }

            var headingTag = new TagBuilder($"h{this.HeadingLevel}");
            headingTag.AddCssClass("nhsuk-do-dont-list__label");
            headingTag.InnerHtml.Append(this.Heading);
            output.Content.AppendHtml(headingTag);

            var listContainer = new TagBuilder("ul");
            listContainer.AddCssClass("nhsuk-list");

            var listTypeClass = this.Type switch
            {
                DoDontListType.Do => "nhsuk-list--tick",
                DoDontListType.Dont => "nhsuk-list--cross",
                _ => null,
            };

            if (!string.IsNullOrWhiteSpace(listTypeClass))
            {
                listContainer.AddCssClass(listTypeClass);
            }

            var content = await output.GetChildContentAsync();
            listContainer.InnerHtml.AppendHtml(content);

            output.Content.AppendHtml(listContainer);
        }
    }
}
