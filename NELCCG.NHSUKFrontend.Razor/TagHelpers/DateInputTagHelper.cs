namespace NELCCG.NHSUKFrontend.Razor.TagHelpers
{
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Microsoft.AspNetCore.Mvc.TagHelpers;
    using Microsoft.AspNetCore.Razor.TagHelpers;
    using System;
    using System.Text.Encodings.Web;
    using System.Threading.Tasks;

    [HtmlTargetElement("nhsuk-date-input")]
    [RestrictChildren("nhsuk-hint", "nhsuk-label", "nhsuk-error-message")]
    public class DateInputTagHelper : FormInputElementTagHelperBase
    {
        public DateTime? Date { get; set; }
        public int? DateDay { get; set; }
        public int? DateMonth { get; set; }
        public int? DateYear { get; set; }
        public bool IndicateInvalid { get; set; }

        private enum InputType
        {
            Day,
            Month,
            Year
        }

        public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
        {
            await base.ProcessAsync(context, output);

            output.TagName = "div";
            output.TagMode = TagMode.StartTagAndEndTag;
            output.AddClass("nhsuk-date-input", HtmlEncoder.Default);
            output.Attributes.RemoveAll("name");

            if (this.Value is DateTime date)
            {
                this.DateDay = date.Day;
                this.DateMonth = date.Month;
                this.DateYear = date.Year;
            }

            var dayTag = CreateInputItem($"{this.Id}__Day", $"{this.Name}.Day", InputType.Day);
            var monthTag = CreateInputItem($"{this.Id}__Month", $"{this.Name}.Month", InputType.Month);
            var yearTag = CreateInputItem($"{this.Id}__Year", $"{this.Name}.Year", InputType.Year);

            output.Content.AppendHtml(dayTag);
            output.Content.AppendHtml(monthTag);
            output.Content.AppendHtml(yearTag);

            var childContent = await output.GetChildContentAsync();
            output.PreElement.AppendHtml(childContent);
        }

        private TagBuilder CreateInputItem(string id, string name, InputType inputType)
        {
            var input = new TagBuilder("input");
            input.AddCssClass("nhsuk-input nhsuk-date-input__input");
            input.Attributes.Add("name", name);
            input.Attributes.Add("id", id);

            var dateInputItem = new TagBuilder("div");
            dateInputItem.AddCssClass("nhsuk-date-input__item");

            var formGroup = new TagBuilder("div");
            formGroup.AddCssClass("nhsuk-form-group");

            var label = new TagBuilder("label");
            label.AddCssClass("nhsuk-label nhsuk-date-input__label");
            label.Attributes.Add("for", id);

            if (inputType == InputType.Day)
            {
                label.InnerHtml.Append("Day");
                input.AddCssClass("nhsuk-input--width-2");

                if (this.Date.HasValue)
                {
                    this.DateDay = this.Date.Value.Day;
                }

                if (this.DateDay.HasValue)
                {
                    input.Attributes.Add("value", this.DateDay.Value.ToString("00"));
                }
            }
            else if (inputType == InputType.Month)
            {
                label.InnerHtml.Append("Month");
                input.AddCssClass("nhsuk-input--width-2");

                if (this.Date.HasValue)
                {
                    this.DateMonth = this.Date.Value.Month;
                }

                if (this.DateMonth.HasValue)
                {
                    input.Attributes.Add("value", this.DateMonth.Value.ToString("00"));
                }
            }
            else if (inputType == InputType.Year)
            {
                label.InnerHtml.Append("Year");
                input.AddCssClass("nhsuk-input--width-4");

                if (this.Date.HasValue)
                {
                    this.DateYear = this.Date.Value.Year;
                }

                if (this.DateYear.HasValue)
                {
                    input.Attributes.Add("value", this.DateYear.Value.ToString("0000"));
                }
            }

            formGroup.InnerHtml.AppendHtml(label);
            formGroup.InnerHtml.AppendHtml(input);
            dateInputItem.InnerHtml.AppendHtml(formGroup);

            input.Attributes.TryGetValue("value", out string value);

            if (this.IndicateInvalid && !ValidValue(inputType, value))
            {
                input.AddCssClass("nhsuk-input--error");
            }

            return dateInputItem;
        }

        private bool ValidValue(InputType inputType, string value)
        {
            _ = int.TryParse(value, out int result);
            if (result < 1) return false;
            if (inputType == InputType.Day && this.DateDay.HasValue && this.DateMonth.HasValue)
            {
                return DateTime.TryParse($"{this.DateYear.GetValueOrDefault(DateTime.Now.Year)}-{this.DateMonth.Value}-{result}", out _);
            }
            return inputType switch
            {
                InputType.Day => result <= 31,
                InputType.Month => result <= 12,
                InputType.Year => result <= DateTime.MinValue.Year,
                _ => false,
            };
        }
    }
}