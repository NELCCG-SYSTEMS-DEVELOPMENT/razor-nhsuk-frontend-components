namespace NELCCG.NHSUKFrontend.Razor.Models
{
    using System.Collections.Generic;

    public class FooterProperties
    {
        public string A11yHeading { get; set; } = "Support links";
        public string Copyright { get; set; }
        public IEnumerable<Link> Links { get; set; }
        public IEnumerable<string> CssClasses { get; set; }
        public IEnumerable<string> ListCssClasses { get; set; }
    }
}