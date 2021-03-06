namespace NELCCG.NHSUKFrontend.MvcApp.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using NELCCG.NHSUKFrontend.MvcApp.Models;
    using NELCCG.NHSUKFrontend.Razor.Helpers;
    using System;
    using System.Collections.Generic;

    public class FormController : Controller
    {

        public IActionResult Index(User model)
        {
            if (this.Request.Method == "GET")
            {
                model.Biography = "This is line 1\nThis is line 2\t\tThis is tabbed";
                model.FavouriteColour = "Other";
                model.FavouriteColourOther = "Purple";
                model.Nationality = new List<string> { "british", "other" };
                // add invalid date of birth to force an error (shown in view)
                model.DateOfBirth = DateTime.Now.AddDays(5);
            }

            if (this.Request.Method == "POST")
            {
                DateInputHelper.UpdateFromFormRequest(model, this.Request);
            }

            return View(model);
        }
    }
}
