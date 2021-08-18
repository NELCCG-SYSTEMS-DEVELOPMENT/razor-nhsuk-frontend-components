namespace NELCCG.NHSUKFrontend.Razor.TagHelpers
{
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Microsoft.AspNetCore.Mvc.TagHelpers;
    using Microsoft.AspNetCore.Razor.TagHelpers;
    using System.Net;
    using System.Text.Encodings.Web;

    [HtmlTargetElement("nhsuk-tag")]
    public class TagTagHelper : TagHelper
    {
        /// <summary>
        /// Variant of tag (e.g. colour, or custom class <c>nhsuk-tag--variant</c>).
        /// 
        /// <list type="bullet">
        ///   <item>
        ///    <description>white</description>
        ///   </item>
        ///   <item>
        ///     <description>grey</description>
        ///   </item>
        ///   <item>
        ///     <description>green</description>
        ///   </item>
        ///   <item>
        ///     <description>aqua-green</description>
        ///   </item>
        ///   <item>
        ///     <description>blue</description>
        ///   </item>
        ///   <item>
        ///     <description>purple</description>
        ///   </item>
        ///   <item>
        ///     <description>pink</description>
        ///   </item>
        ///   <item>
        ///     <description>red</description>
        ///   </item>
        ///   <item>
        ///     <description>orange</description>
        ///   </item>
        ///   <item>
        ///     <description>yellow</description>
        ///   </item>
        /// </list>
        /// </summary>

        public string Variant { get; set; }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            output.TagName = "strong";
            output.AddClass("nhsuk-tag", HtmlEncoder.Default);

            if (!string.IsNullOrWhiteSpace(this.Variant))
            {
                output.AddClass($"nhsuk-tag--{this.Variant}", HtmlEncoder.Default);
            }
        }
    }
}
