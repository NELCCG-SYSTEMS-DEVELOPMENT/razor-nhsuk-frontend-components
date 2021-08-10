namespace NELCCG.NHSUKFrontend.Razor.TagHelpers
{
    using Microsoft.AspNetCore.Razor.TagHelpers;
    using NELCCG.NHSUKFrontend.Razor.Helpers;

    public abstract class ElementTagHelperBase : TagHelper
    {
        public string Id { get; set; }
        public bool UserSetId { get; private set; }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            if (string.IsNullOrWhiteSpace(this.Id))
            {
                var prefix = $"{context.TagName}-";
                var suffix = string.Empty;
                if (context.Items.TryGetValue("id-prefix", out object p))
                {
                    prefix = p.ToString();
                }

                if (context.Items.TryGetValue("id-suffix", out object s))
                {
                    suffix = s.ToString();
                }

                this.Id = IdGenerator.GenerateId(prefix, suffix);
            }
            else
            {
                this.UserSetId = true;
            }

            output.Attributes.SetAttribute("id", this.Id);
        }
    }
}
