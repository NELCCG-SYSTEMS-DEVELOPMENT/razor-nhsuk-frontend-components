namespace NELCCG.NHSUKFrontend.Razor.TagHelpers
{
    using Microsoft.AspNetCore.Mvc.TagHelpers;
    using Microsoft.AspNetCore.Razor.TagHelpers;
    using System.Text.Encodings.Web;

    [HtmlTargetElement("nhsuk-breadcrumb-list", ParentTag = "nhsuk-breadcrumb")]
    public class BreadcrumbListTagHelper : TagHelper
    {
        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            output.TagName = "ol";
            output.AddClass("nhsuk-breadcrumb__list", HtmlEncoder.Default);
        }
    }
}
