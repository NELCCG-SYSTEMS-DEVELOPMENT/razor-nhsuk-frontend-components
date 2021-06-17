namespace SamNHS.NHSUKFrontend.Razor.TagHelpers
{
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Microsoft.AspNetCore.Mvc.ViewFeatures;
    using Microsoft.AspNetCore.Razor.TagHelpers;
    using SamNHS.NHSUKFrontend.Razor.Helpers;

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
        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            base.Process(context, output);

            if (this.For != null)
            {
                var property = this.For.Name;
                var isNestedProperty = property.Contains(".");
                ModelExplorer modelExplorer = null;
                if (isNestedProperty)
                {
                    var parts = property.Split(".");
                    foreach (var part in parts)
                    {
                        modelExplorer = (modelExplorer ?? this.For.ModelExplorer.Container).GetExplorerForProperty(part);
                    }
                }
                else
                {
                    modelExplorer = this.For.ModelExplorer.Container.GetExplorerForProperty(property);
                }

                if (modelExplorer != null)
                {
                    var value = modelExplorer.Model;

                    this.ReadOnly = modelExplorer.Metadata.IsReadOnly;
                    if (modelExplorer.ModelType != typeof(bool))
                    {
                        this.Required = modelExplorer.Metadata.IsRequired;
                    }

                    this.Id = IdGenerator.GenerateId($"{property.Replace(".", "_")}__");
                    output.Attributes.SetAttribute("id", this.Id);
                    this.Name = property;

                    this.ModelValue = this.Value = value;
                }
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
                output.Attributes.SetAttribute("required", "required");
            }
        }
    }
}