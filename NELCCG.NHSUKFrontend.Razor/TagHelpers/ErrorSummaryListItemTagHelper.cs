namespace NELCCG.NHSUKFrontend.Razor.TagHelpers
{
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Microsoft.AspNetCore.Razor.TagHelpers;
    using System;
    using System.Threading.Tasks;

    [HtmlTargetElement("nhsuk-error-summary-list-item", ParentTag = "nhsuk-error-summary")]
    public class ErrorSummaryListItemTagHelper : TagHelper
    {
        public string Href { get; set; }
        /// <summary>
        /// Text to display for the list item.
        /// </summary>
        public string Text { get; set; }

        public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
        {
            output.TagName = "li";
            
            if (string.IsNullOrWhiteSpace(this.Href))
            {
                throw new ArgumentException("Href property is null or an empty string");
            }
            else if (!this.Href.StartsWith("#") && !Uri.IsWellFormedUriString(this.Href, UriKind.RelativeOrAbsolute))
            {
                throw new FormatException("Invalid format for Href");
            }

            var anchorTag = new TagBuilder("a");
            anchorTag.Attributes.Add("href", this.Href);
            if (string.IsNullOrWhiteSpace(this.Text))
            {
                var content = await output.GetChildContentAsync();
                anchorTag.InnerHtml.AppendHtml(content);
            }
            else
            {
                anchorTag.InnerHtml.Append(this.Text);
            }

            output.Content.AppendHtml(anchorTag);
        }
    }
}
