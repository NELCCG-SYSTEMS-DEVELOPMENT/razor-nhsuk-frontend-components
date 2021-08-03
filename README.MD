# NHS.UK Frontend Razor Components

This solution consists of two projects, one with the Razor Components (SamNHS.NHSUKFrontent.Razor)
and a sample application consuming those components (SamNHS.NHSUKFrontend.MvcApp). It requires .NET Core 5.0 or later.

The form components can be bound to models using `asp-for`.

## Razor Components

### Helpers

#### IdGenerator

  `IdGenerator.GenerateId()`, `IdGenerator.GenerateId("Prefix_")`, `IdGenerator.GenerateId("Prefix_", "_Suffix")`

#### DateInputHelper

  `DateInputHelper.UpdateFromFormRequest(model, this.Request);`

#### SvgIconHelper  

In `Startup.ConfigureServices`:  
```
services.AddSingleton<SvgIconHelper>();
```

In page:
```
@inject SvgIconHelper iconHelper
@inject IUrlHelperFactory urlHelperFactory
@{
    var urlHelper = urlHelperFactory.GetUrlHelper(this.ViewContext);
}
<img class="nhsuk-icon"
    src="@urlHelper.Content(iconHelper.GetSvgIconVirtualPath(SvgIconHelper.SvgIcon.ArrowLeft))"
    alt="Arrow Left" />
@Html.Raw(await iconHelper.GetSvgIconContentAsync(SvgIconHelper.SvgIcon.ArrowRight))
```

### Tag Helpers

In `_ViewImports.cshtml` add `@addTagHelper *, SamNHS.NHSUKFrontend.Razor`

#### ActionLinkTagHelper

  `<a nhsuk-action-link asp-action="Action">Action</a>`

#### BackLinkTagHelper

  `<a nhsuk-back-link asp-action="Action">Action</a>`

#### ButtonTagHelper

  `<nhsuk-button>Submit</nhsuk-button>`

#### CheckboxTagHelper

  `<nhsuk-checkbox asp-for="FieldName"></nhsuk-checkbox>`

#### CheckboxConditionalTagHelper

```
<nhsuk-checkboxes contains-conditional>
  <nhsuk-checkbox asp-for="FieldName" label="Option 1" value="@("Option 1")"></nhsuk-radio>
  <nhsuk-checkbox asp-for="FieldName" label="Option 2" value="@("Option 2")"></nhsuk-radio>
  <nhsuk-checkbox asp-for="FieldName" label="Other" aria-controls="conditional"></nhsuk-checkbox>
  <nhsuk-checkbox-conditional hidden="@(Model?.FieldName != "Other")" id="conditional">
    ...
  </nhsuk-checkbox-conditional>
</nhsuk-checkboxes>
```

#### CheckboxesTagHelper

  `<nhsuk-checkboxes>...</nhsuk-checkboxes>`

#### DateInputTagHelper

  `<nhsuk-date-input asp-for="DateField"></nhsuk-date-input>`

#### ErrorMessageTagHelper

  `<nhsuk-error-message>Message</nhsuk-error-message>`

#### FieldsetTagHelper

  `<nhsuk-fieldset>...</nhsuk-fieldset>`

#### FieldsetLegendTagHelper

```
<nhsuk-fieldset>
  <nhsuk-fieldset-legend>...</nhsuk-fieldset-legend>
</nhsuk-fieldset>
```

#### FormGroupTagHelper

  `<nhsuk-form-group>...</nhsuk-form-group>`

#### FrontendLinkTagHelper

  Generate link for frontend CSS  
  `<link nhsuk-frontend-version="Version__5_1_0" />`

#### FrontendScriptTagHelper

  Generate script tag for frontend JS   
  `<script nhsuk-frontend-version="Version__5_1_0"></script>`

#### HintTagHelper

  `<nhsuk-hint>...</nhsuk-hint>`

#### LabelTagHelper

  `<nhsuk-label asp-for="FieldName"></nhsuk-label>`

#### RadioTagHelper

  `<nhsuk-radio asp-for="FieldName"></nhsuk-radio>`

#### RadioConditionalTagHelper

```
<nhsuk-radios contains-conditional>
  <nhsuk-radio asp-for="FieldName" label="Option 1" value="@("Option 1")"></nhsuk-radio>
  <nhsuk-radio asp-for="FieldName" label="Option 2" value="@("Option 2")"></nhsuk-radio>
  <nhsuk-radio asp-for="FieldName" label="Other" aria-controls="conditional"></nhsuk-checkbox>
  <nhsuk-radio-conditional hidden="@(Model?.FieldName != "Other")" id="conditional">
    ...
  </nhsuk-radio-conditional>
</nhsuk-checkboxes>
```

#### RadiosTagHelper

  `<nhsuk-radios>...</nhsuk-radios>`

#### TextAreaTagHelper

  `<nhsuk-textarea asp-for="FieldName"></nhsuk-textarea>`

#### TextInputTagHelper

  `<nhsuk-input asp-for="FieldName"></nhsuk-input>`

### View Components

#### HeaderViewComponent

```
@{
    var headerProperties = new HeaderProperties
    {
        Organisation = new Organisation { Name = "NHS North East Lincolnshire", Descriptor = "Clinical Commissioning Group" },
        PrimaryLinks = new Link[]
        {
            new Link { Label = "About", Url = Url.Action("About", "Home") },
            new Link { Label = "Contact Us", Url = Url.Action("ContactUs", "Home") },
        },
        ShowNav = true
    };
}
```

In page `<body>`:
```
  <vc:header properties="@headerProperties"></vc:header>
```

#### FooterViewComponent

```
@{
    var footerProperties = new FooterProperties
    {
        Copyright = "© 2021 NHS North East Lincolnshire",
        Links = new Link[]
        {
            new Link { Label = "Privacy", Url = Url.Action("Privacy", "Home") }
        }
    };
}
```

In page `<body>`:
```
  <vc:footer properties="@footerProperties"></vc:footer>
```