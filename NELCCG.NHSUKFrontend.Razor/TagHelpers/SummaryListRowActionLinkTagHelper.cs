namespace NELCCG.NHSUKFrontend.Razor.TagHelpers
{
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Microsoft.AspNetCore.Mvc.TagHelpers;
    using Microsoft.AspNetCore.Razor.TagHelpers;
    using System.Text.Encodings.Web;

    [HtmlTargetElement("a", Attributes = SummaryListRowActionLinkAttributeName)]
    public class SummaryListRowActionLinkTagHelper : TagHelper
    {
        private const string SummaryListRowActionLinkAttributeName = "nhsuk-summary-list-action-link";

        [HtmlAttributeName(SummaryListRowActionLinkAttributeName)]
        public bool SummaryListRowActionLink { get; set; }

        public string HiddenText { get; set; }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            if (!this.SummaryListRowActionLink) return;

            if (!string.IsNullOrWhiteSpace(this.HiddenText))
            {
                var hiddenSpan = new TagBuilder("span");
                hiddenSpan.AddCssClass("nhsuk-u-visually-hidden");
                hiddenSpan.InnerHtml.Append(this.HiddenText);
                output.PostContent.AppendHtml(hiddenSpan);
            }

        }
    }
}
