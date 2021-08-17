

namespace NELCCG.NHSUKFrontend.Razor.TagHelpers
{
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Microsoft.AspNetCore.Mvc.TagHelpers;
    using Microsoft.AspNetCore.Razor.TagHelpers;
    using NELCCG.NHSUKFrontend.Razor.Helpers;
    using System.Text.Encodings.Web;
    using System.Threading.Tasks;

    [HtmlTargetElement("a", Attributes = BackLinkAttributeName)]
    public class BackLinkTagHelper : TagHelper
    {
        private const string BackLinkAttributeName = "nhsuk-back-link";

        /// <summary>
        /// Format as a back link (`div` wrapper and SVG icon).
        /// </summary>
        [HtmlAttributeName(BackLinkAttributeName)]
        public bool BackLink { get; set; }

        private readonly IWebHostEnvironment _env;

        public BackLinkTagHelper(IWebHostEnvironment env)
        {
            this._env = env;
        }

        public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
        {
            if (!this.BackLink) return;

            output.AddClass("nhsuk-back-link__link", HtmlEncoder.Default);

            var wrapper = new TagBuilder("div");
            wrapper.AddCssClass("nhsuk-nhsuk-back-link");
            output.PreElement.SetHtmlContent(wrapper.RenderStartTag());

            var svgIconHelper = new SvgIconHelper(this._env);
            var svgContent = await svgIconHelper.GetSvgIconContentAsync(SvgIcon.ChevronLeft);
            if (!string.IsNullOrWhiteSpace(svgContent))
            {
                output.PreContent.AppendHtml(svgContent);
            }

            output.PostElement.SetHtmlContent(wrapper.RenderEndTag());
        }
    }
}
