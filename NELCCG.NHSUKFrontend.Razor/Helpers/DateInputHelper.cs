namespace NELCCG.NHSUKFrontend.Razor.Helpers
{
    using Microsoft.AspNetCore.Http;
    using System;
    using System.Reflection;

    public class DateInputHelper
    {
        /// <summary>
        /// Update date properties from posted form data.
        /// </summary>
        /// <param name="model">The model to update.</param>
        /// <param name="request">The request to get the values from.</param>
        /// <param name="prefix">The prefix to apply when requesting form values.</param>
        public static void UpdateFromFormRequest(object model, HttpRequest request, string prefix = "")
        {
            if (model == null) return;

            foreach (PropertyInfo propertyInfo in model.GetType().GetProperties())
            {
                if (!propertyInfo.CanWrite) continue;
                if (propertyInfo.PropertyType == typeof(DateTime?) || propertyInfo.PropertyType == typeof(DateTime))
                {
                    var day = request.Form[$"{prefix}{propertyInfo.Name}.Day"];
                    var month = request.Form[$"{prefix}{propertyInfo.Name}.Month"];
                    var year = request.Form[$"{prefix}{propertyInfo.Name}.Year"];
                    if (DateTime.TryParse($"{year}-{month}-{day}", out DateTime date))
                    {
                        propertyInfo.SetValue(model, date);
                    }
                }
            }
        }
    }
}
