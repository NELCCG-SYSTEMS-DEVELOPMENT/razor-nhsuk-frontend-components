namespace NELCCG.NHSUKFrontend.Razor.TagHelpers
{
    using Microsoft.AspNetCore.Razor.TagHelpers;
    using Microsoft.AspNetCore.Mvc.TagHelpers;
    using System.Text.Encodings.Web;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using System.Collections;

    [HtmlTargetElement("nhsuk-checkbox")]
    [RestrictChildren("nhsuk-hint")]
    public class CheckboxTagHelper : FormInputElementTagHelperBase
    {
        public string Label { get; set; }
        /// <summary>
        /// Set if checked (ignored if bound to model)
        /// </summary>
        public bool Checked { get; set; }
        /// <summary>
        /// Id of conditional element
        /// </summary>
        public string AriaControls { get; set; }

        /// <summary>
        /// Set to <c>true</c> to implement 'None of the above' behaviour.
        ///   Requires v5.2.0 or later of NHS Frontend included.
        /// </summary>
        public bool Exclusive { get; set; }

        /// <summary>
        /// Items in this group are cleared if checkbox with <see cref="Exclusive"/> is checked.
        ///   Requires v5.2.0 or later of NHS Frontend included.
        /// </summary>
        public string ExclusiveGroup { get; set; }

        public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
        {
            await base.ProcessAsync(context, output);
            context.Items.Add("hint-class", "nhsuk-checkboxes__hint");
            
            output.TagName = "input";
            output.TagMode = TagMode.SelfClosing;
            output.Attributes.SetAttribute("type", "checkbox");
            output.AddClass("nhsuk-checkboxes__input", HtmlEncoder.Default);
            output.Attributes.SetAttribute("aria-describedby", $"{this.Id}-label");

            if (this.For != null)
            {
                if (this.For.Metadata.ModelType == typeof(bool))
                {
                    output.Attributes.RemoveAll("value");
                    if (this.ModelValue != null && this.ModelValue.Equals(true))
                    {
                        output.Attributes.SetAttribute("checked", "checked");
                    }
                }
                else if (this.ModelValue != null && this.ModelValue.Equals(this.Value))
                {
                    output.Attributes.SetAttribute("checked", "checked");
                }
                else if (this.ModelValue is IList list && list.Contains(this.Value))
                {
                    output.Attributes.SetAttribute("checked", "checked");
                }

                this.Label ??= this.For.Metadata.GetDisplayName();
            }
            else
            {
                if (this.Checked)
                {
                    output.Attributes.SetAttribute("checked", "checked");
                }
            }

            if (!string.IsNullOrWhiteSpace(this.AriaControls))
            {
                output.Attributes.SetAttribute("aria-controls", this.AriaControls);
                output.Attributes.SetAttribute("aria-expanded",
                    output.Attributes.TryGetAttribute("checked", out _).ToString().ToLower()
                );
            }

            if (this.Exclusive)
            {
                output.Attributes.SetAttribute("data-checkbox-exclusive", this.Exclusive);
            }

            if (!string.IsNullOrWhiteSpace(this.ExclusiveGroup))
            {
                output.Attributes.SetAttribute("data-checkbox-exclusive-group", this.ExclusiveGroup);
            }

            var wrapper = new TagBuilder("div");
            wrapper.AddCssClass("nhsuk-checkboxes__item");
            output.PreElement.SetHtmlContent(wrapper.RenderStartTag());

            var label = new TagBuilder("label");
            label.AddCssClass("nhsuk-label nhsuk-checkboxes__label");
            label.Attributes.Add("for", this.Id);
            label.InnerHtml.Append(this.Label);
            output.PostElement.SetHtmlContent(label);

            var content = await output.GetChildContentAsync();
            output.PostElement.AppendHtml(content);
            output.PostElement.AppendHtml(wrapper.RenderEndTag());
        }

        public override void SetModelValue(object value)
        {
            this.ModelValue = value;
        }
    }
}
