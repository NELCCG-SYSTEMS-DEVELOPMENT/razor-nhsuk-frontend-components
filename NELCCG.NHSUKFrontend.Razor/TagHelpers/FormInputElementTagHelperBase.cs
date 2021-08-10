namespace NELCCG.NHSUKFrontend.Razor.TagHelpers
{
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Microsoft.AspNetCore.Mvc.ViewFeatures;
    using Microsoft.AspNetCore.Razor.TagHelpers;
    using NELCCG.NHSUKFrontend.Razor.Helpers;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;

    public abstract class FormInputElementTagHelperBase : ElementTagHelperBase
    {
        public bool Disabled { get; set; }

        [HtmlAttributeName("asp-for")]
        public ModelExpression For { get; set; }
        public object ModelValue { get; set; }
        public string Name { get; set; }
        public bool ReadOnly { get; set; }
        public bool Required { get; set; }
        public object Value { get; set; }
        public bool InputError { get; set; }
        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            base.Process(context, output);

            if (this.For != null)
            {
                var value = this.For.Model;

                this.ReadOnly = this.For.Metadata.IsReadOnly;
                if (this.For.Metadata.ModelType != typeof(bool))
                {
                    this.Required = this.For.Metadata.IsRequired;
                    var requiredAttribute = this.For.Metadata.ValidatorMetadata.OfType<RequiredAttribute>().FirstOrDefault();
                    if (requiredAttribute != null)
                    {
                        output.Attributes.SetAttribute("data-required-message", requiredAttribute.FormatErrorMessage(this.For.Metadata.GetDisplayName()));
                    }
                }

                if (!this.UserSetId)
                {
                    this.Id = IdGenerator.GenerateId($"{this.For.Name.Replace(".", "_")}__");
                    output.Attributes.SetAttribute("id", this.Id);
                }
                
                this.Name = this.For.Name;
                this.SetModelValue(value);
            }

            if (string.IsNullOrWhiteSpace(this.Name))
            {
                this.Name = this.Id;
            }

            if (this.For == null)
            {
                this.Name = TagBuilder.CreateSanitizedId(this.Name, "-");
            }

            output.Attributes.SetAttribute("name", this.Name);
            if (this.Value != null)
            {
                output.Attributes.SetAttribute("value", this.Value);
            }
            context.Items.Add("input-id", this.Id);

            if (this.Disabled)
            {
                context.Items.Add("input-disabled", true);
                output.Attributes.SetAttribute("disabled", "disabled");
            }

            if (this.ReadOnly)
            {
                context.Items.Add("input-readonly", true);
                output.Attributes.SetAttribute("readonly", "readonly");
            }

            if (this.Required)
            {
                context.Items.Add("input-required", true);
                output.Attributes.SetAttribute("data-required", "true");
            }

            if (this.InputError)
            {
                this.SetInputErrorClass(context, output);
            }
        }

        public virtual void SetInputErrorClass(TagHelperContext context, TagHelperOutput output)
        {
            output.Attributes.SetAttribute("class", "nhsuk-input--error");
        }

        public virtual void SetModelValue(object value)
        {
            this.ModelValue = this.Value = value;
        }
    }
}