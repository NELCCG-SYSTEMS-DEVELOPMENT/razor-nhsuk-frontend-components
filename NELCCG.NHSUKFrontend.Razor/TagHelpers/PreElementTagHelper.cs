namespace NELCCG.NHSUKFrontend.Razor.TagHelpers
{
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Microsoft.AspNetCore.Mvc.ViewFeatures;
    using Microsoft.AspNetCore.Razor.TagHelpers;
    using System.Threading.Tasks;

    /// <summary>
    /// Tag helper for caching content for pre-element rendering.
    ///   This facilitate nesting when context items are needed, but the content needs to be before the element
    /// </summary>
    [HtmlTargetElement("nhsuk-pre-element")]
    public class PreElementTagHelper : TagHelper
    {
        public const string DefaultPreElementKey = "nhsuk-pre-element";

        [HtmlAttributeNotBound]
        [ViewContext]
        public ViewContext ViewContext { get; set; }

        public string Key { get; set; } = DefaultPreElementKey;

        public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
        {
            var content = await output.GetChildContentAsync();
            this.ViewContext.ViewData[this.Key] = content;
            output.SuppressOutput();
        }

        /// <summary>
        /// Add content to output PreElement.
        /// </summary>
        /// <param name="context">The <see cref="Microsoft.AspNetCore.Mvc.Rendering.ViewContext"/> containing the data.</param>
        /// <param name="output">The <see cref="TagHelperOutput"/> to render to.</param>
        /// <param name="contextKey">The context key to use.</param>
        /// <param name="removeFromContextAfterAppend">Remove the data after appending.</param>
        public static void AppendPreElementContent(
            ViewContext context,
            TagHelperOutput output,
            string contextKey = DefaultPreElementKey,
            bool removeFromContextAfterAppend = true)
        {
            if (context.ViewData.ContainsKey(contextKey))
            {
                var preElementContent = context.ViewData[contextKey] as TagHelperContent;
                output.PreElement.AppendHtml(preElementContent);
                if (removeFromContextAfterAppend)
                {
                    context.ViewData.Remove(contextKey);
                }
            }
        }
    }
}
