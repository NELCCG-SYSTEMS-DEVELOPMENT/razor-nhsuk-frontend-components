namespace NELCCG.NHSUKFrontend.Razor.TagHelpers
{
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Microsoft.AspNetCore.Mvc.TagHelpers;
    using Microsoft.AspNetCore.Razor.TagHelpers;
    using System;
    using System.Text.Encodings.Web;
    using System.Threading.Tasks;

    [HtmlTargetElement("nhsuk-table-cell")]
    public class TableCellTagHelper : TagHelper
    {
        /// <summary>
        /// Number of columns this cell will cover. Does not apply is the table is responsive.
        /// </summary>
        public int? Colspan { get; set; }
        /// <summary>
        /// Cell format. Only <c>"numeric"</c> is supported, but a custom class can be created:
        ///     <c>nhsuk-table__header--format</c> and/or <c>nhsuk-table__cell--format</c>.
        /// </summary>
        public string Format { get; set; }
        /// <summary>
        /// Set if cell is <c>&lt;th&gt;</c>m when the container is a table body.
        /// </summary>
        public bool HeaderCell { get; set; }
        /// <summary>
        /// Set heading when table is set up as a responsive table.
        /// </summary>
        public string ResponsiveHeading { get; set; }
        /// <summary>
        /// Number of rows this cell will cover. Does not apply is the table is responsive.
        /// </summary>
        public int? Rowspan { get; set; }

        public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
        {
            _ = context.Items.TryGetValue("table-cell-container", out object container);
            _ = context.Items.TryGetValue("table-responsive", out object responsive);

            if (container == null)
            {
                output.SuppressOutput();
                return;
            }
            
            await base.ProcessAsync(context, output);

            if (container.Equals("thead"))
            {
                output.TagName = "th";
                output.Attributes.SetAttribute("scope", "col");
                output.Attributes.SetAttribute("role", "columnheader");
                if (!string.IsNullOrWhiteSpace(this.Format))
                {
                    output.AddClass($"nhsuk-table__header--{this.Format}", HtmlEncoder.Default);
                }
            }
            else
            {
                if (responsive != null && responsive.Equals(true))
                {
                    if (string.IsNullOrWhiteSpace(this.ResponsiveHeading))
                    {
                        throw new ArgumentException("ResponsiveHeading property is null or an empty string");
                    }

                    output.TagName = "td";
                    output.AddClass("nhsuk-table__cell", HtmlEncoder.Default);

                    var responsiveHeading = new TagBuilder("span");
                    responsiveHeading.AddCssClass("nhsuk-table-responsive__heading");
                    responsiveHeading.InnerHtml.Append(this.ResponsiveHeading);
                    output.PreContent.AppendHtml(responsiveHeading);
                }
                else
                {
                    output.TagName = this.HeaderCell ? "th" : "td";
                    output.AddClass(this.HeaderCell ? "nhsuk-table__header" : "nhsuk-table__cell", HtmlEncoder.Default);

                    if (this.Colspan.HasValue)
                    {
                        output.Attributes.Add("colspan", this.Colspan);
                    }

                    if (this.Rowspan.HasValue)
                    {
                        output.Attributes.Add("rowspan", this.Rowspan);
                    }
                }

                if (!string.IsNullOrWhiteSpace(this.Format))
                {
                    output.AddClass($"nhsuk-table__cell--{this.Format}", HtmlEncoder.Default);
                }

            }
        }
    }
}
