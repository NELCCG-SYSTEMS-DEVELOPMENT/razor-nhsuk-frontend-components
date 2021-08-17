namespace NELCCG.NHSUKFrontend.Razor.TagHelpers
{
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Microsoft.AspNetCore.Mvc.TagHelpers;
    using Microsoft.AspNetCore.Razor.TagHelpers;
    using System.Net;
    using System.Text.Encodings.Web;

    [HtmlTargetElement("nhsuk-a-z")]
    public class AtoZTagHelper : TagHelper
    {
        /// <summary>
        /// Prefix when setting the anchor element link.
        ///   Set to a value like <c>#nav-</c> if numbers set in <see cref="Characters"/> (<c>#nav-1, #nav-2</c>).
        ///   Or to another page, e.g. <c>/search?startsWith=</c>
        /// </summary>
        public string AnchorPrefix { get; set; } = "#";
        public string AriaLabel { get; set; } = "A to Z Navigation";
        /// <summary>
        /// Which characters to show in the A-Z
        /// </summary>
        public string Characters { get; set; } = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
        /// <summary>
        /// Any characters which should be disabled (e.g. no values, or on page listing those items).
        /// </summary>
        public string DisableCharacters { get; set; } = "";
        public string Id { get; set; }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            output.TagName = "nav";
            output.AddClass("nhsuk-nav-a-z", HtmlEncoder.Default);
            output.Attributes.SetAttribute("role", "navigation");

            if (!string.IsNullOrWhiteSpace(this.Id))
            {
                output.Attributes.SetAttribute("id", this.Id);
            }

            var list = new TagBuilder("ol");
            list.AddCssClass("nhsuk-nav-a-z__list");
            list.Attributes.Add("role", "list");

            foreach (var c in this.Characters)
            {
                var li = new TagBuilder("li");
                li.AddCssClass("nhsuk-nav-a-z__item");
                if (this.DisableCharacters.Contains(c))
                {
                    var span = new TagBuilder("span");
                    span.AddCssClass("nhsuk-nav-a-z__link--disabled");
                    span.InnerHtml.Append(c.ToString());
                    li.InnerHtml.AppendHtml(span);
                }
                else
                {
                    var encodedChar = WebUtility.UrlEncode(c.ToString());
                    var anchorTag = new TagBuilder("a");
                    anchorTag.AddCssClass("nhsuk-nav-a-z__link");
                    anchorTag.Attributes.Add("href", $"{this.AnchorPrefix}{encodedChar}");
                    anchorTag.InnerHtml.Append(c.ToString());
                    li.InnerHtml.AppendHtml(anchorTag);
                }

                list.InnerHtml.AppendHtml(li);
            }

            output.Content.AppendHtml(list);
        }
    }
}
