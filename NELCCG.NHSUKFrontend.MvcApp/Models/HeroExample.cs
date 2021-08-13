using System.ComponentModel.DataAnnotations;

namespace NELCCG.NHSUKFrontend.MvcApp.Models
{
    public class HeroExample
    {
        [Display(Name = "Heading")]
        public string HeroTitle { get; set; }
        
        [Display(Name = "Text")]
        public string HeroMessage { get; set; }

        [Display(Name = "Image Url")]
        public string HeroImage { get; set; }
    }
}
