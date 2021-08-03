namespace NELCCG.NHSUKFrontend.Razor.Models
{
    using System.Collections.Generic;

    public class HeaderProperties
    {
        public Organisation Organisation { get; set; }
        public TransactionalService TransactionalService { get; set; }
        public SearchOptions SearchOptions { get; set; }
        public IEnumerable<Link> PrimaryLinks { get; set; }
        public string HomeText { get; set; }
        public bool WhiteHeader { get; set; }
        public bool ShowNav { get; set; }
        public bool ShowSearch { get; set; }
        public bool SvgFallbackImage { get; set; }
    }
}
