namespace SamNHS.NHSUKFrontend.Razor.TagHelpers
{
    using Microsoft.AspNetCore.Mvc.TagHelpers;
    using Microsoft.AspNetCore.Razor.TagHelpers;
    using System.Text.Encodings.Web;

    [HtmlTargetElement("nhsuk-fieldset-legend", ParentTag = "nhsuk-fieldset")]
    public class FieldsetLegendTagHelper : TagHelper
    {
        public enum LegendSize
        {
            ExtraLarge,
            Large,
            Medium,
            Small
        }

        public int HeadingLevel { get; set; } = 1;
        public bool IsPageHeading { get; set; }
        public LegendSize? Size { get; set; }
        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            output.TagName = "legend";
            output.TagMode = TagMode.StartTagAndEndTag;
            output.AddClass("nhsuk-fieldset__legend", HtmlEncoder.Default);

            if (this.IsPageHeading)
            {
                if (this.Size == null)
                {
                    this.Size = this.HeadingLevel switch
                    {
                        1 => LegendSize.ExtraLarge,
                        2 => LegendSize.Large,
                        3 => LegendSize.Medium,
                        _ => LegendSize.Small,
                    };
                }
                output.PreContent.AppendHtml($"<h{this.HeadingLevel} class=\"nhsuk-fieldset__heading\">");
                output.PostContent.AppendHtml($"</h{this.HeadingLevel}>");
            }

            switch (this.Size)
            {
                case LegendSize.ExtraLarge:
                    output.AddClass("nhsuk-fieldset__legend--xl", HtmlEncoder.Default);
                    break;
                case LegendSize.Large:
                    output.AddClass("nhsuk-fieldset__legend--l", HtmlEncoder.Default);
                    break;
                case LegendSize.Medium:
                    output.AddClass("nhsuk-fieldset__legend--m", HtmlEncoder.Default);
                    break;
                case LegendSize.Small:
                    output.AddClass("nhsuk-fieldset__legend--s", HtmlEncoder.Default);
                    break;
                default:
                    break;
            }
        }
    }
}
