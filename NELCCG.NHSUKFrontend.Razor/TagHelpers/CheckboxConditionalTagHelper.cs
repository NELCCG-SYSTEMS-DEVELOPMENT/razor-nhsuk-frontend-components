namespace NELCCG.NHSUKFrontend.Razor.TagHelpers
{
    using Microsoft.AspNetCore.Mvc.TagHelpers;
    using Microsoft.AspNetCore.Razor.TagHelpers;
    using System.Text.Encodings.Web;

    [HtmlTargetElement("nhsuk-checkbox-conditional", ParentTag = "nhsuk-checkboxes")]
    public class CheckboxConditionalTagHelper : ElementTagHelperBase
    {
        public bool Hidden { get; set; }
        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            base.Process(context, output);

            output.TagName = "div";
            output.TagMode = TagMode.StartTagAndEndTag;
            output.AddClass("nhsuk-checkboxes__conditional", HtmlEncoder.Default);

            if (this.Hidden)
            {
                output.AddClass("nhsuk-checkboxes__conditional--hidden", HtmlEncoder.Default);
            }

            output.Attributes.SetAttribute("aria-expanded", (!this.Hidden).ToString().ToLower());

            if (context.Items.ContainsKey("input-id"))
            {
                context.Items.Remove("input-id");
            }

        }

    }
}
