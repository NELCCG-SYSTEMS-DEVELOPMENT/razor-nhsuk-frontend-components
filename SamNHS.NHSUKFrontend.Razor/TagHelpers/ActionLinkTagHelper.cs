

namespace SamNHS.NHSUKFrontend.Razor.TagHelpers
{
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Microsoft.AspNetCore.Mvc.TagHelpers;
    using Microsoft.AspNetCore.Razor.TagHelpers;
    using SamNHS.NHSUKFrontend.Razor.Helpers;
    using System.Text.Encodings.Web;
    using System.Threading.Tasks;

    [HtmlTargetElement("a", Attributes = ActionLinkAttributeName)]
    public class ActionLinkTagHelper : TagHelper
    {
        private const string ActionLinkAttributeName = "nhsuk-action-link";

        /// <summary>
        /// Format as an action link (`div` wrapper and SVG icon).
        /// </summary>
        [HtmlAttributeName(ActionLinkAttributeName)]
        public bool ActionLink { get; set; }

        private readonly IWebHostEnvironment _env;

        public ActionLinkTagHelper(IWebHostEnvironment env)
        {
            this._env = env;
        }

        public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
        {
            if (!this.ActionLink) return;

            output.AddClass("nhsuk-action-link__link", HtmlEncoder.Default);

            var wrapper = new TagBuilder("div");
            wrapper.AddCssClass("nhsuk-nhsuk-action-link");
            output.PreElement.SetHtmlContent(wrapper.RenderStartTag());

            var svgIconHelper = new SvgIconHelper(this._env);
            var svgContent = await svgIconHelper.GetSvgIconContentAsync(SvgIconHelper.SvgIcon.ArrowRightCircle);
            if (!string.IsNullOrWhiteSpace(svgContent))
            {
                output.PreContent.AppendHtml(svgContent);
            }

            var span = new TagBuilder("span");
            span.AddCssClass("nhsuk-action-link__text");
            output.PreContent.AppendHtml(span.RenderStartTag());
            output.PostContent.AppendHtml(span.RenderEndTag());

            output.PostElement.SetHtmlContent(wrapper.RenderEndTag());
        }
    }
}
