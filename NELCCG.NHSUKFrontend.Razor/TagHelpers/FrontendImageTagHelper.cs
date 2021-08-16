namespace NELCCG.NHSUKFrontend.Razor.TagHelpers
{
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Microsoft.AspNetCore.Mvc.Routing;
    using Microsoft.AspNetCore.Mvc.TagHelpers;
    using Microsoft.AspNetCore.Mvc.ViewFeatures;
    using Microsoft.AspNetCore.Razor.TagHelpers;
    using System;
    using System.Collections.Generic;
    using System.Text.Encodings.Web;
    using System.Threading.Tasks;

    [HtmlTargetElement("nhsuk-image")]
    public class FrontendImageTagHelper : ImageTagHelper
    {
        public string Caption { get; set; }
        /// <summary>
        /// Any additional css classes to apply.
        /// </summary>
        [HtmlAttributeName("classes")]
        public IEnumerable<string> CssClasses { get; set; } = Array.Empty<string>();
        /// <summary>
        /// If <see cref="Caption"/> may not be safe, set this to <c>true</c>.
        /// </summary>
        public bool EncodeCaption { get; set; }

        public FrontendImageTagHelper(
            IFileVersionProvider fileVersionProvider,
            HtmlEncoder htmlEncoder,
            IUrlHelperFactory urlHelperFactory) : base(fileVersionProvider, htmlEncoder, urlHelperFactory)
        {
        }

        public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
        {
            await base.ProcessAsync(context, output);

            var figure = new TagBuilder("figure");
            figure.AddCssClass("nhsuk-image");

            foreach (var cssClass in this.CssClasses)
            {
                figure.AddCssClass(cssClass);
            }

            output.TagName = "img";
            output.TagMode = TagMode.SelfClosing;
            output.AddClass("nhsuk-image__img", HtmlEncoder.Default);

            var figCaption = new TagBuilder("figcaption");
            figCaption.AddCssClass("nhsuk-image__caption");
            if (this.EncodeCaption)
            {
                figCaption.InnerHtml.Append(this.Caption);
            }
            else
            {
                figCaption.InnerHtml.AppendHtml(this.Caption);
            }

            output.PostElement.AppendHtml(figCaption);
            
            output.PreElement.AppendHtml(figure.RenderStartTag());
            output.PostElement.AppendHtml(figure.RenderEndTag());
        }
    }
}
