namespace NELCCG.NHSUKFrontend.MvcApp.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class User
    {
        [Display(Name = "Given name")]
        public string GivenName { get; set; }

        public string Surname { get; set; }

        [Display(Name = "Date of birth")]
        public DateTime? DateOfBirth { get; set; }

        [Display(Name = "Favourite Colour")]
        public string FavouriteColour { get; set; }

        [Display(Name = "Favourite Colour (Other)")]
        public string FavouriteColourOther { get; set; }

        public List<string> Nationality { get; set; } = new List<string>();

        public List<string> ContactPreferences { get; set; } = new List<string>();

        public string ContactPreferenceEmail { get; set; }

        public string ContactPreferenceTelephone { get; set; }

        public string ContactPreferenceTextMessage { get; set; }

        public string Biography { get; set; }

        public string Role { get; set; }

    }
}
