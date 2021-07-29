namespace SamNHS.NHSUKFrontend.Razor.TagHelpers
{
    using Microsoft.AspNetCore.Razor.TagHelpers;
    using Microsoft.AspNetCore.Mvc.TagHelpers;
    using System.Text.Encodings.Web;
    using System.Threading.Tasks;

    [HtmlTargetElement("nhsuk-button")]
    public class ButtonTagHelper : ElementTagHelperBase
    {
        public enum ButtonType
        {
            Button,
            Reset,
            Submit,
        }

        public bool Reverse { get; set; }
        public bool Secondary { get; set; }
        public ButtonType Type { get; set; } = ButtonType.Submit;

        public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
        {
            await base.ProcessAsync(context, output);
            output.TagName = "button";
            output.TagMode = TagMode.StartTagAndEndTag;
            switch (this.Type)
            {
                case ButtonType.Button:
                    output.Attributes.SetAttribute("type", "button");
                    break;
                case ButtonType.Reset:
                    output.Attributes.SetAttribute("type", "reset");
                    break;
                case ButtonType.Submit:
                    output.Attributes.SetAttribute("type", "submit");
                    break;
                default:
                    break;
            }

            output.AddClass("nhsuk-button", HtmlEncoder.Default);

            if (this.Secondary)
            {
                output.AddClass("nhsuk-button--secondary", HtmlEncoder.Default);
            }

            if (this.Reverse)
            {
                output.AddClass("nhsuk-button--reverse", HtmlEncoder.Default);
            }
        }
    }
}
