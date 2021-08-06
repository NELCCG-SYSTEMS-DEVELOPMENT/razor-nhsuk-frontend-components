namespace NELCCG.NHSUKFrontend.Razor.TagHelpers
{
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Microsoft.AspNetCore.Mvc.TagHelpers;
    using Microsoft.AspNetCore.Razor.TagHelpers;
    using System.Text.Encodings.Web;

    [HtmlTargetElement("a", Attributes = BreadcrumbBackLinkAttributeName)]
    public class BreadcrumbBacklinkTagHelper : TagHelper
    {
        private const string BreadcrumbBackLinkAttributeName = "nhsuk-breadcrumb-backlink";

        [HtmlAttributeName(BreadcrumbBackLinkAttributeName)]
        public bool BreadcrumbBacklink { get; set; }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            if (!this.BreadcrumbBacklink) return;

            output.AddClass("nhsuk-breadcrumb__backlink", HtmlEncoder.Default);

            var wrapper = new TagBuilder("p");
            wrapper.AddCssClass("nhsuk-breadcrumb__back");

            output.PreElement.SetHtmlContent(wrapper.RenderStartTag());
            output.PostElement.SetHtmlContent(wrapper.RenderEndTag());
        }
    }
}
