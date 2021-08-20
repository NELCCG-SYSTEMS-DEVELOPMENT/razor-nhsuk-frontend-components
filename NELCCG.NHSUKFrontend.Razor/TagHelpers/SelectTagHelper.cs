namespace NELCCG.NHSUKFrontend.Razor.TagHelpers
{
    using Microsoft.AspNetCore.Razor.TagHelpers;
    using Microsoft.AspNetCore.Mvc.TagHelpers;
    using System.Text.Encodings.Web;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc.ViewFeatures;
    using Microsoft.AspNetCore.Mvc.Rendering;

    [HtmlTargetElement("select", Attributes = SelectAttributeName)]
    public class SelectTagHelper : FormInputElementTagHelperBase
    {
        private const string SelectAttributeName = "nhsuk-select";

        /// <summary>
        /// Format as an NHS Frontend select
        /// </summary>
        [HtmlAttributeName(SelectAttributeName)]
        public bool NhsSelect { get; set; }

        [HtmlAttributeNotBound]
        [ViewContext]
        public ViewContext ViewContext { get; set; }

        public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
        {
            if (!this.NhsSelect) return;

            await base.ProcessAsync(context, output);

            // process any child content, which may include pre-element content to be added
            _ = await output.GetChildContentAsync();

            PreElementTagHelper.AppendPreElementContent(this.ViewContext, output);

            output.AddClass("nhsuk-select", HtmlEncoder.Default);
        }
    }
}
