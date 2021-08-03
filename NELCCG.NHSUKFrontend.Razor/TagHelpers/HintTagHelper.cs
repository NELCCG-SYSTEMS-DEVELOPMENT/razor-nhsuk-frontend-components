namespace NELCCG.NHSUKFrontend.Razor.TagHelpers
{
    using Microsoft.AspNetCore.Mvc.TagHelpers;
    using Microsoft.AspNetCore.Razor.TagHelpers;
    using System.Text.Encodings.Web;

    [HtmlTargetElement("nhsuk-hint")]
    public class HintTagHelper : TagHelper
    {
        /// <summary>
        /// When this is set, the hint text is encoded
        /// </summary>
        public string Text { get; set; }
        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            output.TagName = "div";
            output.TagMode = TagMode.StartTagAndEndTag;
            output.AddClass("nhsuk-hint", HtmlEncoder.Default);
            if (context.Items.TryGetValue("hint-class", out object hintClass))
            {
                output.AddClass(hintClass.ToString(), HtmlEncoder.Default);
            }
            if (!string.IsNullOrWhiteSpace(this.Text))
            {
                output.Content.SetContent(this.Text);
            }
        }
    }
}
