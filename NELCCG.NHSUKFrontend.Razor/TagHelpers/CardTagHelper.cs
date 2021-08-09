namespace NELCCG.NHSUKFrontend.Razor.TagHelpers
{
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Microsoft.AspNetCore.Mvc.TagHelpers;
    using Microsoft.AspNetCore.Razor.TagHelpers;
    using System;
    using System.Collections.Generic;
    using System.Text.Encodings.Web;

    [HtmlTargetElement("nhsuk-card")]
    public class CardTagHelper : TagHelper
    {
        /// <summary>
        /// Make the card clickable.
        /// </summary>
        public bool Clickable { get; set; }
        /// <summary>
        /// Text description within the card content.
        /// </summary>
        public string Description { get; set; }
        /// <summary>
        /// Set this to <c>true</c> is you don't want to wrap the description in a <c>&lt;p class="nhsuk-card__description"&gt;</c>.
        /// </summary>
        public bool DescriptionIsHtml { get; set; }
        /// <summary>
        /// If <see cref="DescriptionIsHtml" /> is <c>false</c> and <see cref="Description"/> may not be safe, set this to <c>true</c>.
        /// </summary>
        public bool EncodeDescription { get; set; }
        /// <summary>
        /// If <see cref="HeadingIsHtml" /> is <c>false</c> and <see cref="Heading"/> may not be safe, set this to <c>true</c>.
        /// </summary>
        public bool EncodeHeading { get; set; }
        /// <summary>
        /// Make this card a feature card.
        /// </summary>
        public bool Feature { get; set; }
        /// <summary>
        /// Set the heading text.
        /// </summary>
        public string Heading { get; set; }
        /// <summary>
        /// Set this to <c>true</c> is you don't want to wrap the heading text in an <c>&lt;h{<see cref="HeadingLevel"/>} class="nhsuk-card__heading"&gt;</c>.
        /// </summary>
        public bool HeadingIsHtml { get; set; }
        /// <summary>
        /// Set the heading classes when <see cref="HeadingIsHtml"/> is <c>false</c>.
        /// </summary>
        public IEnumerable<string> HeadingClasses { get; set; } = System.Array.Empty<string>();
        /// <summary>
        /// Set the heading level when <see cref="HeadingIsHtml"/> is <c>false</c>.
        /// </summary>
        public int HeadingLevel { get; set; } = 2;
        /// <summary>
        /// Set heading link when <see cref="Feature"/> is set to <c>false</c>.
        /// </summary>
        public string Href { get; set; }
        /// <summary>
        /// Alternative text when <see cref="ImageUrl"/> is set.
        /// </summary>
        public string ImageAlt { get; set; }
        /// <summary>
        /// Url of the image to embed in the card.
        /// </summary>
        public string ImageUrl { get; set; }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            output.TagName = "div";
            output.AddClass("nhsuk-card", HtmlEncoder.Default);

            var content = new TagBuilder("div");
            content.AddCssClass("nhsuk-card__content");

            if (this.Clickable)
            {
                output.AddClass("nhsuk-card--clickable", HtmlEncoder.Default);
            }

            if (this.Feature)
            {
                output.AddClass("nhsuk-card--feature", HtmlEncoder.Default);
                content.AddCssClass("nhsuk-card__content--feature");
            }

            if (!string.IsNullOrWhiteSpace(this.ImageUrl))
            {
                if (!Uri.IsWellFormedUriString(this.ImageUrl, UriKind.RelativeOrAbsolute))
                {
                    throw new FormatException("Invalid format for ImageUrl");
                }
                
                var image = new TagBuilder("img");
                image.AddCssClass("nhsuk-card__img");
                image.Attributes.Add("src", this.ImageUrl);
                image.Attributes.Add("alt", this.ImageAlt ?? string.Empty);
                output.Content.AppendHtml(image);
            }

            if (!string.IsNullOrWhiteSpace(this.Heading))
            {
                if (this.HeadingIsHtml)
                {
                    content.InnerHtml.AppendHtml(this.Heading);
                }
                else
                {
                    var headingTag = new TagBuilder($"h{HeadingLevel}");
                    headingTag.AddCssClass("nhsuk-card__heading");
                    foreach (var cssClass in this.HeadingClasses)
                    {
                        headingTag.AddCssClass(cssClass);
                    }

                    if (this.Feature)
                    {
                        headingTag.AddCssClass("nhsuk-card__heading--feature");
                    }
                    
                    if (!string.IsNullOrWhiteSpace(this.Href) && !this.Feature)
                    {
                        if (!this.Href.StartsWith("#") && !Uri.IsWellFormedUriString(this.Href, UriKind.RelativeOrAbsolute))
                        {
                            throw new FormatException("Invalid format for Href");
                        }

                        var anchorTag = new TagBuilder("a");
                        anchorTag.AddCssClass("nhsuk-card__link");
                        anchorTag.Attributes.Add("href", this.Href);
                        if (this.EncodeHeading)
                        {
                            anchorTag.InnerHtml.Append(this.Heading);
                        }
                        else
                        {
                            anchorTag.InnerHtml.AppendHtml(this.Heading);
                        }

                        headingTag.InnerHtml.AppendHtml(anchorTag);
                    }
                    else if (this.EncodeHeading)
                    {
                        headingTag.InnerHtml.Append(this.Heading);
                    }
                    else
                    {
                        headingTag.InnerHtml.AppendHtml(this.Heading);
                    }
                    
                    content.InnerHtml.AppendHtml(headingTag);
                }
            }

            if (!string.IsNullOrWhiteSpace(this.Description))
            {
                if (this.DescriptionIsHtml)
                {
                    content.InnerHtml.AppendHtml(this.Description);
                }
                else
                {
                    var description = new TagBuilder("p");
                    description.AddCssClass("nhsuk-card__description");
                    if (this.EncodeDescription)
                    {
                        description.InnerHtml.Append(this.Description);
                    }
                    else
                    {
                        description.InnerHtml.AppendHtml(this.Description);
                    }

                    content.InnerHtml.AppendHtml(description);
                }
            }

            output.Content.AppendHtml(content);
        }
    }
}
