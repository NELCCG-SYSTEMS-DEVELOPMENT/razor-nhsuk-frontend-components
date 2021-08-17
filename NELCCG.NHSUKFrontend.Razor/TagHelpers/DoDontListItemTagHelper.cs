namespace NELCCG.NHSUKFrontend.Razor.TagHelpers
{
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Razor.TagHelpers;
    using NELCCG.NHSUKFrontend.Razor.Helpers;
    using System.Threading.Tasks;

    [HtmlTargetElement("nhsuk-do-dont-list-item", ParentTag = "nhsuk-do-dont-list")]
    public class DoDontListItemTagHelper : TagHelper
    {
        private readonly IWebHostEnvironment _env;

        /// <summary>
        /// Hide <c>"do not "</c> prefix.
        /// </summary>
        public bool HidePrefix { get; set; }

        public DoDontListItemTagHelper(IWebHostEnvironment env)
        {
            this._env = env;
        }

        public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
        {
            output.TagName = "li";

            if (context.Items["list-type"] is DoDontListType listType)
            {
                var svgIconHelper = new SvgIconHelper(this._env);
                var svgContent = await svgIconHelper.GetSvgIconContentAsync(
                    listType == DoDontListType.Do ?
                    SvgIcon.Tick : SvgIcon.Cross
                );

                output.Content.AppendHtml(svgContent);

                if (!this.HidePrefix && listType == DoDontListType.Dont)
                {
                    output.Content.AppendHtml("do not ");
                }

                var content = await output.GetChildContentAsync();
                output.Content.AppendHtml(content);
            }
            else
            {
                output.SuppressOutput();
            }
        }
    }
}
