namespace NELCCG.NHSUKFrontend.Razor.TagHelpers
{
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Microsoft.AspNetCore.Mvc.TagHelpers;
    using Microsoft.AspNetCore.Razor.TagHelpers;
    using System;
    using System.Text.Encodings.Web;

    [HtmlTargetElement("nhsuk-contents-list-item", ParentTag = "nhsuk-contents-list")]
    public class ContentsListItemTagHelper : TagHelper
    {
        /// <summary>
        /// Set the <c>aria-current</c> attribute. Default is <c>"page"</c>, but may be other values, like <c>"step"</c>.
        /// </summary>
        public string AriaCurrent { get; set; } = "page";
        /// <summary>
        /// Is the current active item.
        /// </summary>
        public bool Current { get; set; }
        /// <summary>
        /// Link for the list item. Not used if <see cref="Current"/> is set to <c>true</c>.
        /// </summary>
        public string Href { get; set; }
        /// <summary>
        /// Text to display for the list item.
        /// </summary>
        public string Text { get; set; }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            output.TagName = "li";
            output.AddClass("nhsuk-contents-list__item", HtmlEncoder.Default);

            if (this.Current)
            {
                var spanTag = new TagBuilder("span");
                spanTag.AddCssClass("nhsuk-contents-list__current");
                spanTag.InnerHtml.Append(this.Text);
                output.Attributes.Add("aria-current", this.AriaCurrent);
                output.Content.AppendHtml(spanTag);
            }
            else
            {
                if (string.IsNullOrWhiteSpace(this.Href))
                {
                    throw new ArgumentException("Href property is null or an empty string");
                }
                else if (!this.Href.StartsWith("#") && !Uri.IsWellFormedUriString(this.Href, UriKind.RelativeOrAbsolute))
                {
                    throw new FormatException("Invalid format for Href");
                }

                var anchorTag = new TagBuilder("a");
                anchorTag.AddCssClass("nhsuk-contents-list__link");
                anchorTag.Attributes.Add("href", this.Href);
                anchorTag.InnerHtml.Append(this.Text);
                output.Content.AppendHtml(anchorTag);
                
            }
        }
    }
}
