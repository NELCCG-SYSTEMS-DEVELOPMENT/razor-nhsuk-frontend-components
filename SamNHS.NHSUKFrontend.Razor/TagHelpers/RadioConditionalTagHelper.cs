namespace SamNHS.NHSUKFrontend.Razor.TagHelpers
{
    using Microsoft.AspNetCore.Mvc.TagHelpers;
    using Microsoft.AspNetCore.Razor.TagHelpers;
    using System.Text.Encodings.Web;

    [HtmlTargetElement("nhsuk-radio-conditional", ParentTag = "nhsuk-radios")]
    public class RadioConditionalTagHelper : ElementTagHelperBase
    {
        public bool Hidden { get; set; }
        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            base.Process(context, output);

            output.TagName = "div";
            output.TagMode = TagMode.StartTagAndEndTag;
            output.AddClass("nhsuk-radios__conditional", HtmlEncoder.Default);

            if (this.Hidden)
            {
                output.AddClass("nhsuk-radios__conditional--hidden", HtmlEncoder.Default);
            }

            output.Attributes.SetAttribute("aria-expanded", (!this.Hidden).ToString().ToLower());

            if (context.Items.ContainsKey("input-id"))
            {
                context.Items.Remove("input-id");
            }

        }

    }
}
