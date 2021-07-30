namespace SamNHS.NHSUKFrontend.Razor.TagHelpers
{
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Microsoft.AspNetCore.Mvc.TagHelpers;
    using Microsoft.AspNetCore.Mvc.ViewFeatures;
    using Microsoft.AspNetCore.Razor.TagHelpers;
    using System.Text.Encodings.Web;
    using System.Threading.Tasks;

    [HtmlTargetElement("nhsuk-label")]
    public class LabelTagHelper : TagHelper
    {
        public enum LabelSize
        {
            ExtraLarge,
            Large,
            Medium,
            Small
        }

        [HtmlAttributeName("asp-for")]
        public ModelExpression For { get; set; }

        public int HeadingLevel { get; set; } = 1;
        public bool IsPageHeading { get; set; }
        public bool IsBold { get; set; }
        public LabelSize? Size { get; set; }
        public string VisuallyHiddenText { get; set; }
        public bool LabelIsVisuallyHidden { get; set; }

        public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
        {
            output.TagName = "label";
            output.AddClass("nhsuk-label", HtmlEncoder.Default);
            if (this.IsBold)
            {
                output.AddClass("nhsuk-label--s", HtmlEncoder.Default);
            }

            if (this.LabelIsVisuallyHidden)
            {
                output.AddClass("nhsuk-u-visually-hidden", HtmlEncoder.Default);
            }

            if (this.IsPageHeading)
            {
                output.PreContent.AppendHtml($"<h{this.HeadingLevel} class=\"nhsuk-fieldset__heading\">");
                output.PostContent.AppendHtml($"</h{this.HeadingLevel}>");
            }

            switch (this.Size)
            {
                case LabelSize.ExtraLarge:
                    output.AddClass("nhsuk-label--xl", HtmlEncoder.Default);
                    break;
                case LabelSize.Large:
                    output.AddClass("nhsuk-label--l", HtmlEncoder.Default);
                    break;
                case LabelSize.Medium:
                    output.AddClass("nhsuk-label--m", HtmlEncoder.Default);
                    break;
                case LabelSize.Small:
                    output.AddClass("nhsuk-label--s", HtmlEncoder.Default);
                    break;
                default:
                    break;
            }

            if (context.Items.TryGetValue("input-id", out object inputId))
            {
                output.Attributes.SetAttribute("for", inputId);
            }

            if (this.For != null)
            {
                var displayName = this.For.Metadata.GetDisplayName();
                output.Content.Append(displayName);
            }

            var content = await output.GetChildContentAsync();
            output.Content.AppendHtml(content);

            if (!string.IsNullOrWhiteSpace(this.VisuallyHiddenText))
            {
                var hiddenText = new TagBuilder("span");
                hiddenText.AddCssClass("nhsuk-u-visually-hidden");
                hiddenText.InnerHtml.Append(this.VisuallyHiddenText);
                output.Content.AppendHtml(hiddenText);
            }
        }
    }
}
