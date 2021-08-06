namespace NELCCG.NHSUKFrontend.Razor.TagHelpers
{
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Microsoft.AspNetCore.Mvc.TagHelpers;
    using Microsoft.AspNetCore.Razor.TagHelpers;
    using System.Text.Encodings.Web;

    [HtmlTargetElement("a", Attributes = BreadcrumbItemAttributeName)]
    public class BreadcrumbItemTagHelper : TagHelper
    {
        private const string BreadcrumbItemAttributeName = "nhsuk-breadcrumb-item";

        [HtmlAttributeName(BreadcrumbItemAttributeName)]
        public bool BreadcrumbItem { get; set; }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            if (!this.BreadcrumbItem) return;

            output.AddClass("nhsuk-breadcrumb__link", HtmlEncoder.Default);

            var wrapper = new TagBuilder("li");
            wrapper.AddCssClass("nhsuk-breadcrumb__item");

            output.PreElement.SetHtmlContent(wrapper.RenderStartTag());
            output.PostElement.SetHtmlContent(wrapper.RenderEndTag());

        }
    }
}
