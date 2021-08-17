namespace NELCCG.NHSUKFrontend.Razor.TagHelpers
{
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Microsoft.AspNetCore.Mvc.TagHelpers;
    using Microsoft.AspNetCore.Razor.TagHelpers;
    using System;
    using System.Text.Encodings.Web;
    using System.Threading.Tasks;

    [HtmlTargetElement("nhsuk-list-panel-item", ParentTag = "nhsuk-list-panel")]
    public class ListPanelItemTagHelper : TagHelper
    {
        public string Href { get; set; }

        public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
        {
            output.TagName = "li";
            output.AddClass("nhsuk-list-panel__item", HtmlEncoder.Default);

            var content = await output.GetChildContentAsync();

            if (!string.IsNullOrWhiteSpace(this.Href))
            {
                if (!this.Href.StartsWith("#") && !Uri.IsWellFormedUriString(this.Href, UriKind.RelativeOrAbsolute))
                {
                    throw new FormatException("Invalid format for Href");
                }

                var anchorTag = new TagBuilder("a");
                anchorTag.AddCssClass("nhsuk-list-panel__link");
                anchorTag.Attributes.Add("href", this.Href);
                anchorTag.InnerHtml.AppendHtml(content);
                output.Content.AppendHtml(anchorTag);
            }
            else
            {
                output.Content.AppendHtml(content);
            }
        }
    }
}
