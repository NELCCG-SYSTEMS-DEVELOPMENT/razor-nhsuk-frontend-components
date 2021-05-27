namespace SamNHS.NHSUKFrontend.RazorComponents.Models
{
    public class TransactionalService
    {
        public string Name { get; set; }
        public string LongName { get; set; }
        public string Href { get; set; }

        public bool IsLongNameSet
        {
            get
            {
                return !string.IsNullOrWhiteSpace(this.LongName);
            }
        }
    }
}