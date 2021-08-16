namespace NELCCG.NHSUKFrontend.Razor.TagHelpers
{
    using Microsoft.AspNetCore.Mvc.TagHelpers;
    using Microsoft.AspNetCore.Razor.TagHelpers;
    using System;
    using System.Collections.Generic;
    using System.Text.Encodings.Web;
    using System.Threading.Tasks;

    [HtmlTargetElement("nhsuk-summary-list-row-key", ParentTag = "nhsuk-summary-list-row")]
    public class SummaryListRowKeyTagHelper : TagHelper
    {
        /// <summary>
        /// Any additional css classes to apply.
        /// </summary>
        [HtmlAttributeName("classes")]
        public IEnumerable<string> CssClasses { get; set; } = Array.Empty<string>();
        /// <summary>
        /// Text for key. If set, any inner HTML will be ignored.
        /// </summary>
        public string Text { get; set; }

        public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
        {
            output.TagName = "dt";
            output.AddClass("nhsuk-summary-list__key", HtmlEncoder.Default);
            foreach (var cssClass in this.CssClasses)
            {
                output.AddClass(cssClass, HtmlEncoder.Default);
            }

            if (!string.IsNullOrWhiteSpace(this.Text))
            {
                output.Content.Append(this.Text);
            }
            else
            {
                var content = await output.GetChildContentAsync();
                output.Content.AppendHtml(content);
            }
        }
    }
}
