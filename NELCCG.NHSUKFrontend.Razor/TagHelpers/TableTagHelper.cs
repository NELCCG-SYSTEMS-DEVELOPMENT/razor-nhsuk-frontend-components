namespace NELCCG.NHSUKFrontend.Razor.TagHelpers
{
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Microsoft.AspNetCore.Mvc.TagHelpers;
    using Microsoft.AspNetCore.Razor.TagHelpers;
    using System;
    using System.Collections.Generic;
    using System.Text.Encodings.Web;
    using System.Threading.Tasks;

    [HtmlTargetElement("nhsuk-table")]
    public class TableTagHelper : TagHelper
    {
        /// <summary>
        /// Set the table caption.
        /// </summary>
        public string Caption { get; set; }
        /// <summary>
        /// Set the caption classes if <see cref="Caption"/> is <c>true</c>.
        /// </summary>
        public IEnumerable<string> CaptionClasses { get; set; } = Array.Empty<string>();
        /// <summary>
        /// Set the heading text if <see cref="Panel"/> is <c>true</c>.
        /// </summary>
        public string Heading { get; set; }
        /// <summary>
        /// Set the heading level if <see cref="Panel"/> is <c>true</c>.
        /// </summary>
        public int HeadingLevel { get; set; } = 3;
        /// <summary>
        /// Set to <c>true</c> if the table is in a panel with a heading.
        /// </summary>
        public bool Panel { get; set; }
        /// <summary>
        /// Set the panel classes if <see cref="Panel"/> is <c>true</c>.
        /// </summary>
        public IEnumerable<string> PanelClasses { get; set; } = Array.Empty<string>();
        /// <summary>
        /// Set to <c>true</c> if the table is responsive.
        /// </summary>
        public bool Responsive { get; set; }
        /// <summary>
        /// Set any additional classes to apply to the table.
        /// </summary>
        public IEnumerable<string> TableClasses { get; set; } = Array.Empty<string>();

        public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
        {
            output.TagName = "table";
            output.AddClass("nhsuk-table", HtmlEncoder.Default);
            foreach (var cssClass in this.TableClasses)
            {
                output.AddClass(cssClass, HtmlEncoder.Default);
            }

            context.Items.Add("table-responsive", this.Responsive);

            if (this.Responsive)
            {
                output.Attributes.SetAttribute("role", "table");
                output.AddClass("nhsuk-table-responsive", HtmlEncoder.Default);
            }
            else
            {
                output.AddClass("nhsuk-table", HtmlEncoder.Default);
            }

            if (!string.IsNullOrWhiteSpace(this.Caption))
            {
                var tableCaption = new TagBuilder("caption");
                tableCaption.AddCssClass("nhsuk-table__caption");
                foreach (var cssClass in this.CaptionClasses)
                {
                    tableCaption.AddCssClass(cssClass);
                }

                tableCaption.InnerHtml.Append(this.Caption);
                output.Content.AppendHtml(tableCaption);
            }

            var tableContainer = new TagBuilder("div");
            tableContainer.AddCssClass("nhsuk-table-container");

            var content = await output.GetChildContentAsync();
            output.Content.AppendHtml(content);

            if (this.Panel)
            {
                var panelContainer = new TagBuilder("div");
                panelContainer.AddCssClass("nhsuk-table__panel-with-heading-tab");
                foreach (var cssClass in this.PanelClasses)
                {
                    panelContainer.AddCssClass(cssClass);
                }

                var headingTag = new TagBuilder($"h{this.HeadingLevel}");
                headingTag.AddCssClass("nhsuk-table__heading-tab");
                headingTag.InnerHtml.Append(this.Heading);
                panelContainer.InnerHtml.AppendHtml(headingTag);

                output.PreElement.AppendHtml(panelContainer.RenderStartTag());
                output.PreElement.AppendHtml(panelContainer.RenderBody());
                output.PreElement.AppendHtml(tableContainer.RenderStartTag());
                output.PreElement.AppendHtml(tableContainer.RenderBody());
                output.PostElement.AppendHtml(tableContainer.RenderEndTag());
                output.PostElement.AppendHtml(panelContainer.RenderEndTag());
            }
            else
            {
                output.PreElement.AppendHtml(tableContainer.RenderStartTag());
                output.PreElement.AppendHtml(tableContainer.RenderBody());
                output.PostElement.AppendHtml(tableContainer.RenderEndTag());
            }
        }
    }
}
