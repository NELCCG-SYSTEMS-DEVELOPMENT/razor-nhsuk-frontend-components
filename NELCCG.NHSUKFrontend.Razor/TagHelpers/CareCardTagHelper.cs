namespace NELCCG.NHSUKFrontend.Razor.TagHelpers
{
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Microsoft.AspNetCore.Mvc.TagHelpers;
    using Microsoft.AspNetCore.Razor.TagHelpers;
    using System;
    using System.Collections.Generic;
    using System.Text.Encodings.Web;
    using System.Threading.Tasks;

    [HtmlTargetElement("nhsuk-care-card")]
    public class CareCardTagHelper : TagHelper
    {
        public enum CardType
        {
            Immediate,
            NonUrgent,
            Urgent,
        }

        public string ImmediateHiddenPrefix { get; set; } = "Immediate action required:";
        public string NonUrgentHiddenPrefix { get; set; } = "Non-urgent advice:";
        public string UrgentHiddenPrefix { get; set; } = "Urgent advice:";

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
        public int HeadingLevel { get; set; } = 2;
        /// <summary>
        /// What <see cref="CardType"/> the care card should be.
        /// </summary>
        public CardType Type { get; set; } = CardType.NonUrgent;
        

        public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
        {
            output.TagName = "div";
            output.AddClass("nhsuk-care-card", HtmlEncoder.Default);

            var cardClass = this.Type switch
            {
                CardType.Immediate => "nhsuk-care-card--immediate",
                CardType.NonUrgent => "nhsuk-care-card--non-urgent",
                CardType.Urgent => "nhsuk-care-card--urgent",
                _ => null,
            };

            if (cardClass != null)
            {
                output.AddClass(cardClass, HtmlEncoder.Default);
            }

            foreach (var cssClass in this.CssClasses)
            {
                output.AddClass(cssClass, HtmlEncoder.Default);
            }

            var headingContainer = new TagBuilder("div");
            headingContainer.AddCssClass("nhsuk-care-card__heading-container");

            var headingTag = new TagBuilder($"h{HeadingLevel}");
            headingTag.AddCssClass("nhsuk-care-card__heading");

            var headingSpan = new TagBuilder("span");
            headingSpan.Attributes.Add("role", "text");

            var hiddenSpan = new TagBuilder("span");
            hiddenSpan.AddCssClass("nhsuk-u-visually-hidden");

            var hiddenText = this.Type switch
            {
                CardType.Immediate => this.ImmediateHiddenPrefix,
                CardType.NonUrgent => this.NonUrgentHiddenPrefix,
                CardType.Urgent => this.UrgentHiddenPrefix,
                _ => this.NonUrgentHiddenPrefix,
            };

            hiddenSpan.InnerHtml.Append(hiddenText);
            headingSpan.InnerHtml.AppendHtml(hiddenSpan);
            headingSpan.InnerHtml.Append(this.Heading);
            headingTag.InnerHtml.AppendHtml(headingSpan);
            headingContainer.InnerHtml.AppendHtml(headingTag);

            var arrow = new TagBuilder("span");
            arrow.AddCssClass("nhsuk-care-card__arrow");
            arrow.Attributes.Add("aria-hidden", "true");

            headingContainer.InnerHtml.AppendHtml(arrow);

            output.Content.AppendHtml(headingContainer);

            var contentContainer = new TagBuilder("div");
            contentContainer.AddCssClass("nhsuk-care-card__content");

            var content = await output.GetChildContentAsync();
            contentContainer.InnerHtml.AppendHtml(content);

            output.Content.AppendHtml(contentContainer);
        }
    }
}
