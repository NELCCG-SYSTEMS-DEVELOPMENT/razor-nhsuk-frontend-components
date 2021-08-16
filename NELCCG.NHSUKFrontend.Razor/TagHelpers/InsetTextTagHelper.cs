namespace NELCCG.NHSUKFrontend.Razor.TagHelpers
{
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Microsoft.AspNetCore.Mvc.TagHelpers;
    using Microsoft.AspNetCore.Razor.TagHelpers;
    using System;
    using System.Collections.Generic;
    using System.Text.Encodings.Web;
    using System.Threading.Tasks;

    [HtmlTargetElement("nhsuk-inset-text")]
    public class InsetTextTagHelper : TagHelper
    {
        /// <summary>
        /// Any additional css classes to apply.
        /// </summary>
        [HtmlAttributeName("classes")]
        public IEnumerable<string> CssClasses { get; set; } = Array.Empty<string>();
        /// <summary>
        /// Prefix text for screen readers.
        /// </summary>
        public string HiddenTextPrefix { get; set; } = "Information: ";

        public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
        {
            output.TagName = "div";
            output.AddClass("nhsuk-inset-text", HtmlEncoder.Default);
            foreach (var cssClass in this.CssClasses)
            {
                output.AddClass(cssClass, HtmlEncoder.Default);
            }

            var hiddenSpan = new TagBuilder("span");
            hiddenSpan.InnerHtml.Append(this.HiddenTextPrefix);
            hiddenSpan.AddCssClass("nhsuk-u-visually-hidden");

            output.Content.AppendHtml(hiddenSpan);

            var content = await output.GetChildContentAsync();
            output.Content.AppendHtml(content);

        }
    }
}
