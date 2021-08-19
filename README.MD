# NHS.UK Frontend Razor Components

This solution consists of two projects, one with the Razor Components (NELCCG.NHSUKFrontent.Razor)
and a sample application consuming those components (NELCCG.NHSUKFrontend.MvcApp). It requires .NET Core 5.0 or later.

It includes the [NHS.UK Frontend](https://github.com/nhsuk/nhsuk-frontend) assets (CSS, favicons, SVG icons and logos) for both version 4.1.0 and 5.1.0.
There are tag helpers for including the CSS `FrontendLinkTagHelper` and JavaScript `FrontendScriptTagHelper`, although not currently the favicons or logos.  

The form components can be bound to models using `asp-for`.

## Razor Components

### Helpers

#### IdGenerator

  `IdGenerator.GenerateId()`, `IdGenerator.GenerateId("Prefix_")`, `IdGenerator.GenerateId("Prefix_", "_Suffix")`

#### DateInputHelper

  `DateInputHelper.UpdateFromFormRequest(model, this.Request);`

#### SvgIconHelper  

In `Startup.ConfigureServices`:  
```csharp
services.AddSingleton<SvgIconHelper>();
```

In page:
```razor
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

In `_ViewImports.cshtml` add `@addTagHelper *, NELCCG.NHSUKFrontend.Razor`

#### ActionLinkTagHelper

  `<a nhsuk-action-link asp-action="Action">Action</a>`

#### AtoZTagHelper

```razor
<nhsuk-a-z></nhsuk-a-z>
<nhsuk-a-z disable-characters="JQWXZ"></nhsuk-a-z>
<nhsuk-a-z anchor-prefix="?character=" characters="@("$#?%\"&123ÁÄÊ")"></nhsuk-a-z>
```

#### BackLinkTagHelper

  `<a nhsuk-back-link asp-action="Action">Action</a>`

#### BreadcrumbTagHelper, BreadcrumbListTagHelper, BreadcrumbItemTagHelper, BreadcrumbBacklinkTagHelper

```razor
<nhsuk-breadcrumb>
    <nhsuk-breadcrumb-list>
        <a nhsuk-breadcrumb-item asp-action="Index">Home</a>
        <a nhsuk-breadcrumb-item asp-controller="Publications" asp-action="Index">Publications</a>
    </nhsuk-breadcrumb-list>
    <a nhsuk-breadcrumb-backlink asp-controller="Publications" asp-action="Index">Back to Publications</a>
</nhsuk-breadcrumb>
```

#### ButtonTagHelper

  `<nhsuk-button>Submit</nhsuk-button>`

#### CardTagHelper

```razor
<nhsuk-card heading="Encoded description and <em>heading</em>"
            encode-heading
            description="This is <script>alert('hi')</script> unsafe content"
            encode-description></nhsuk-card>

<nhsuk-card heading="Unencoded description <em>and</em> heading" heading-classes="@(new[] { "class-1", "class-2" })"
            description="This is <strong>safe</strong> content. Custom heading classes are applied."></nhsuk-card>

<nhsuk-card heading="Card with image" image-url="https://placekitten.com/g/970/450"
            description="This card has an image. Source: <a href='https://placekitten.com/'>placekitten.com</a>"></nhsuk-card>

<nhsuk-card heading="Clickable card" clickable href="#"
            description="This card is clickable"></nhsuk-card>

<nhsuk-card heading="Feature card" feature
            description="This is a feature card"></nhsuk-card>
```

#### CareCardTagHelper

```razor
<nhsuk-care-card heading="Speak to a GP if:" type="NonUrgent">
    <ul>
        <li>Symptom one</li>
        <li>Symptom two</li>
        <li>Symptom three</li>
        <li>Symptom four</li>
    </ul>
</nhsuk-care-card>

<nhsuk-care-card heading="Ask for an urgent GP appointment if:" type="Urgent" classes="@(new[] { "open-q", "close-q" })">
    <ul>
        <li>Symptom one</li>
        <li>Symptom two</li>
        <li>Symptom three</li>
        <li>Symptom four</li>
    </ul>
</nhsuk-care-card>

<nhsuk-care-card heading="Call 999 if:" type="Immediate">
    <ul>
        <li>Symptom one</li>
        <li>Symptom two</li>
        <li>Symptom three</li>
        <li>Symptom four</li>
    </ul>
</nhsuk-care-card>
```

#### CheckboxTagHelper

  `<nhsuk-checkbox asp-for="FieldName"></nhsuk-checkbox>`

#### CheckboxConditionalTagHelper

```razor
<nhsuk-checkboxes contains-conditional>
  <nhsuk-checkbox asp-for="FieldName" label="Option 1" value="@("Option 1")"></nhsuk-checkbox>
  <nhsuk-checkbox asp-for="FieldName" label="Option 2" value="@("Option 2")"></nhsuk-checkbox>
  <nhsuk-checkbox asp-for="FieldName" label="Other" aria-controls="conditional"></nhsuk-checkbox>
  <nhsuk-checkbox-conditional hidden="@(Model?.FieldName != "Other")" id="conditional">
    ...
  </nhsuk-checkbox-conditional>
</nhsuk-checkboxes>
```

#### CheckboxesTagHelper

  `<nhsuk-checkboxes>...</nhsuk-checkboxes>`

#### ContentsListTagHelper, ContentsListItemTagHelper

```razor
<nhsuk-contents-list>
    <nhsuk-contents-list-item text="Guidance" href="@Url.Action("Guidance")"></nhsuk-contents-list-item>
    <nhsuk-contents-list-item text="Meeting Minutes" current></nhsuk-contents-list-item>
    <nhsuk-contents-list-item text="Policies" href="@Url.Action("Policies")"></nhsuk-contents-list-item>
</nhsuk-contents-list>
```

#### DateInputTagHelper

  `<nhsuk-date-input asp-for="DateField"></nhsuk-date-input>`

#### DetailsTagHelper

```razor
<nhsuk-details summary="Details hidden">
    <p>Details go here</p>
</nhsuk-details>

<nhsuk-details summary="Details shown" open>
    <p>Details go here</p>
</nhsuk-details>

<nhsuk-details summary="Styled as expander" expander>
    <p>Details go here</p>
</nhsuk-details>
```

#### DoDontListTagHelper, DoDontListItemTagHelper

```razor
<nhsuk-do-dont-list type="Do">
    <nhsuk-do-dont-list-item>cover blisters that are likely to burst with a soft plaster or dressing</nhsuk-do-dont-list-item>
    <nhsuk-do-dont-list-item>wash your hands before touching a burst blister</nhsuk-do-dont-list-item>
    <nhsuk-do-dont-list-item>allow the fluid in a burst blister to drain before covering it with a plaster or dressing</nhsuk-do-dont-list-item>
</nhsuk-do-dont-list>

<nhsuk-do-dont-list type="Dont">
    <nhsuk-do-dont-list-item>burst a blister yourself</nhsuk-do-dont-list-item>
    <nhsuk-do-dont-list-item>peel the skin off a burst blister</nhsuk-do-dont-list-item>
    <nhsuk-do-dont-list-item hide-prefix>do not pick at the edges of the remaining skin</nhsuk-do-dont-list-item>
    <nhsuk-do-dont-list-item>wear the shoes or use the equipment that caused your blister until it heals</nhsuk-do-dont-list-item>
</nhsuk-do-dont-list>
```

#### ErrorMessageTagHelper

  `<nhsuk-error-message>Message</nhsuk-error-message>`

#### ErrorSummaryTagHelper, ErrorSummaryListItemTagHelper

```razor
@if (hasErrors)
{
    <nhsuk-error-summary description="The following errors need correcting">
        @if (emptySurname)
        {
            <nhsuk-error-summary-list-item href="#Surname">Surname is required</nhsuk-error-summary-list-item>
        }
        @if (invalidDoB)
        {
            <nhsuk-error-summary-list-item href="#DateOfBirth__Day">Date of birth must be in the past</nhsuk-error-summary-list-item>
        }
    </nhsuk-error-summary>
}
```

#### FieldsetTagHelper

  `<nhsuk-fieldset>...</nhsuk-fieldset>`

#### FieldsetLegendTagHelper

```razor
<nhsuk-fieldset>
  <nhsuk-fieldset-legend>...</nhsuk-fieldset-legend>
</nhsuk-fieldset>
```

#### FormGroupTagHelper

  `<nhsuk-form-group>...</nhsuk-form-group>`

#### FrontendImageTagHelper

Inherits from built in [`ImageTagHelper`](https://docs.microsoft.com/en-us/aspnet/core/mvc/views/tag-helpers/built-in/image-tag-helper).
Wraps `<img>` with `<figure>` and adds `<figcaption>`

```razor
<nhsuk-image classes="@(new[] { "nhsuk-u-margin-top-0" })" src="https://placekitten.com/600/300" caption="Image caption" />

<nhsuk-image src="https://placehold.co/320x150/333333/fff?text=small"
                sizes="(min-width: 1020px) 320px, (min-width: 768px) 50vw, 100vw"
                srcset="https://placehold.co/640x300/333333/fff?text=medium 640w, https://placehold.co/800x375/333333/fff?text=large 800w"
                caption="<code>srcset</code> test. Resize or use responsive mode to see the different images" />
```

#### FrontendLinkTagHelper

  Generate link for frontend CSS  
  `<link nhsuk-frontend-version="Version__5_1_0" />`

#### FrontendScriptTagHelper

  Generate script tag for frontend JS   
  `<script nhsuk-frontend-version="Version__5_1_0"></script>`

#### HeroTagHelper

```razor
<nhsuk-hero image-url="path/to/image.png"></nhsuk-hero>
<nhsuk-hero heading="Hero heading"></nhsuk-hero>
<nhsuk-hero heading="Hero heading and text" text="Hero text"></nhsuk-hero>
<nhsuk-hero heading="Hero heading and image" image-url="path/to/image.png"></nhsuk-hero>
<nhsuk-hero heading="Hero heading, text and image" text="Hero text" image-url="path/to/image.png"></nhsuk-hero>
```

#### HintTagHelper

  `<nhsuk-hint>...</nhsuk-hint>`

#### LabelTagHelper

  `<nhsuk-label asp-for="FieldName"></nhsuk-label>`

#### ListTagHelper

```razor
<nhsuk-list ordered-list>
    <li>Ordered 1</li>
    <li>Ordered 2</li>
    <li>Ordered 3</li>
</nhsuk-list>

<nhsuk-list>
    <li>Unordered 1</li>
    <li>Unordered 2</li>
    <li>Unordered 3</li>
</nhsuk-list>
```

#### ListPanelTagHelper, ListPanelItemTagHelper

```razor
<nhsuk-list>
    <nhsuk-list-panel back-to-top-link="#nhsuk-nav-a-z" id="A" label="A">
        <nhsuk-list-panel-item href="@Url.Action("AAA")">
            AAA
        </nhsuk-list-panel-item>
        <nhsuk-list-panel-item href="@Url.Action("AbdominalAorticAneurysm")">
            Abdominal aortic aneurysm
        </nhsuk-list-panel-item>
        <nhsuk-list-panel-item href="@Url.Action("Abscess")">
            Abscess
        </nhsuk-list-panel-item>
    </nhsuk-list-panel>
    <nhsuk-list-panel disable back-to-top-link="#nhsuk-nav-a-z" id="B" label="B" message="There are currently no conditions listed"></nhsuk-list-panel>
    <nhsuk-list-panel back-to-top-link="#nhsuk-nav-a-z" id="C" label="C">
        <nhsuk-list-panel-item href="@Url.Action("ChestPain")">
            Chest Pain
        </nhsuk-list-panel-item>
        <nhsuk-list-panel-item href="@Url.Action("ColdSore")">
            Cold Sore
        </nhsuk-list-panel-item>
    </nhsuk-list-panel>
</nhsuk-list>
```

#### PaginationTagHelper

```razor
<nhsuk-pagination previous-page="Previous Page Name" previous-url="?page=previous"
                  next-page="Next Page Name" next-url="?page=next" />
```

#### RadioTagHelper

  `<nhsuk-radio asp-for="FieldName"></nhsuk-radio>`

#### RadioConditionalTagHelper

```razor
<nhsuk-radios contains-conditional>
  <nhsuk-radio asp-for="FieldName" label="Option 1" value="@("Option 1")"></nhsuk-radio>
  <nhsuk-radio asp-for="FieldName" label="Option 2" value="@("Option 2")"></nhsuk-radio>
  <nhsuk-radio asp-for="FieldName" label="Other" aria-controls="conditional"></nhsuk-radio>
  <nhsuk-radio-conditional hidden="@(Model?.FieldName != "Other")" id="conditional">
    ...
  </nhsuk-radio-conditional>
</nhsuk-radios>
```

#### RadiosTagHelper

  `<nhsuk-radios>...</nhsuk-radios>`

#### SummaryListTagHelper

Related helpers:  
**SummaryListRowTagHelper, SummaryListRowKeyTagHelper, SummaryListRowValueTagHelper,
SummaryListRowActionsTagHelper, SummaryListRowActionLinkTagHelper,
SummaryListRowActionListTagHelper, SummaryListRowActionListItemTagHelper**

```razor
<nhsuk-summary-list>
    <nhsuk-summary-list-row>
        <nhsuk-summary-list-row-key text="Item 1"><strong>HTML content</strong> ignored</nhsuk-summary-list-row-key>
        <nhsuk-summary-list-row-value>HTML <strong>content</strong> used as no <code>text</code> attribute set</nhsuk-summary-list-row-value>
    </nhsuk-summary-list-row>
    <nhsuk-summary-list-row>
        <nhsuk-summary-list-row-key>Item <em>2</em></nhsuk-summary-list-row-key>
        <nhsuk-summary-list-row-value text="Value set via <code>text</code> attribute"></nhsuk-summary-list-row-value>
    </nhsuk-summary-list-row>
</nhsuk-summary-list>

<nhsuk-summary-list>
    <nhsuk-summary-list-row>
        <nhsuk-summary-list-row-key>Name</nhsuk-summary-list-row-key>
        <nhsuk-summary-list-row-value text="Jo Smith"></nhsuk-summary-list-row-value>
        <nhsuk-summary-list-row-actions>
            <a nhsuk-summary-list-action-link hidden-text="name" href="#">Change</a>
        </nhsuk-summary-list-row-actions>
    </nhsuk-summary-list-row>
    <nhsuk-summary-list-row>
        <nhsuk-summary-list-row-key>Documents</nhsuk-summary-list-row-key>
        <nhsuk-summary-list-row-value text="Document 1, Document 2"></nhsuk-summary-list-row-value>
        <nhsuk-summary-list-row-actions>
            <nhsuk-summary-list-row-action-list>
                <nhsuk-summary-list-row-action-list-item>
                    <a nhsuk-summary-list-action-link hidden-text="documents" href="#">Manage</a>
                </nhsuk-summary-list-row-action-list-item>
                <nhsuk-summary-list-row-action-list-item>
                    <a nhsuk-summary-list-action-link hidden-text="documents" href="#">Export</a>
                </nhsuk-summary-list-row-action-list-item>
            </nhsuk-summary-list-row-action-list>
        </nhsuk-summary-list-row-actions>
    </nhsuk-summary-list-row>
</nhsuk-summary-list>
```

#### TableTagHelper, TableHeadTagHelper, TableBodyTagHelper, TableBodyRowTagHelper, TableCellTagHelper

```razor
<nhsuk-table caption="Plain table caption">
    <nhsuk-table-head>
        <nhsuk-table-cell>Column 1</nhsuk-table-cell>
        <nhsuk-table-cell>Column 2</nhsuk-table-cell>
    </nhsuk-table-head>
    <nhsuk-table-body>
        <nhsuk-table-body-row>
            <nhsuk-table-cell header-cell>Row 1 Column 1</nhsuk-table-cell>
            <nhsuk-table-cell>Row 1 Column 2</nhsuk-table-cell>
        </nhsuk-table-body-row>
        <nhsuk-table-body-row>
            <nhsuk-table-cell colspan="2">The contents of this cell is set up to span two columns</nhsuk-table-cell>
        </nhsuk-table-body-row>
        <nhsuk-table-body-row>
            <nhsuk-table-cell header-cell rowspan="2">Row 3 and 4 Column 1</nhsuk-table-cell>
            <nhsuk-table-cell>Row 3 Column 2</nhsuk-table-cell>
        </nhsuk-table-body-row>
        <nhsuk-table-body-row>
            <nhsuk-table-cell>Row 4 Column 2</nhsuk-table-cell>
        </nhsuk-table-body-row>
    </nhsuk-table-body>
</nhsuk-table>
        
<nhsuk-table responsive caption="Responsive table">
    <nhsuk-table-head>
        <nhsuk-table-cell>Column 1</nhsuk-table-cell>
        <nhsuk-table-cell format="numeric">Numeric column</nhsuk-table-cell>
    </nhsuk-table-head>
    <nhsuk-table-body>
        <nhsuk-table-body-row>
            <nhsuk-table-cell responsive-heading="Column 1">Row 1 Column 1</nhsuk-table-cell>
            <nhsuk-table-cell responsive-heading="Numeric column" format="numeric">21</nhsuk-table-cell>
        </nhsuk-table-body-row>
        <nhsuk-table-body-row>
            <nhsuk-table-cell responsive-heading="Column 1">Row 2 Column 1</nhsuk-table-cell>
            <nhsuk-table-cell responsive-heading="Numeric column" format="numeric">43</nhsuk-table-cell>
        </nhsuk-table-body-row>
        <nhsuk-table-body-row>
            <nhsuk-table-cell responsive-heading="Column 1">Row 3 Column 1</nhsuk-table-cell>
            <nhsuk-table-cell responsive-heading="Numeric column" format="numeric">76</nhsuk-table-cell>
        </nhsuk-table-body-row>
    </nhsuk-table-body>
</nhsuk-table>
        
<nhsuk-table panel heading="In panel" caption="Also with caption">
    <nhsuk-table-head>
        <nhsuk-table-cell>Column 1</nhsuk-table-cell>
        <nhsuk-table-cell format="numeric">Numeric column</nhsuk-table-cell>
    </nhsuk-table-head>
    <nhsuk-table-body>
        <nhsuk-table-body-row>
            <nhsuk-table-cell>Row 1 Column 1</nhsuk-table-cell>
            <nhsuk-table-cell format="numeric">21</nhsuk-table-cell>
        </nhsuk-table-body-row>
        <nhsuk-table-body-row>
            <nhsuk-table-cell>Row 2 Column 1</nhsuk-table-cell>
            <nhsuk-table-cell format="numeric">43</nhsuk-table-cell>
        </nhsuk-table-body-row>
        <nhsuk-table-body-row>
            <nhsuk-table-cell>Row 3 Column 1</nhsuk-table-cell>
            <nhsuk-table-cell format="numeric">76</nhsuk-table-cell>
        </nhsuk-table-body-row>
    </nhsuk-table-body>
</nhsuk-table>
```

#### TagTagHelper

```razor
<nhsuk-tag>
  Active
</nhsuk-tag>
<nhsuk-tag variant="white">
  Started
</nhsuk-tag>
```

#### TextAreaTagHelper

  `<nhsuk-textarea asp-for="FieldName"></nhsuk-textarea>`

#### TextInputTagHelper

  `<nhsuk-input asp-for="FieldName"></nhsuk-input>`

#### WarningCalloutTagHelper

```razor
<nhsuk-warning-callout heading="School, nursery or work">
    <p>Stay away from school, nursery or work until all the spots have crusted over. This is usually 5 days after the spots first appeared.</p>
</nhsuk-warning-callout>
```

### View Components

#### HeaderViewComponent

```razor
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
```razor
  <vc:header properties="@headerProperties"></vc:header>
```

#### FooterViewComponent

```razor
@{
    var footerProperties = new FooterProperties
    {
        Copyright = "� 2021 NHS North East Lincolnshire",
        Links = new Link[]
        {
            new Link { Label = "Privacy", Url = Url.Action("Privacy", "Home") }
        }
    };
}
```

In page `<body>`:
```razor
  <vc:footer properties="@footerProperties"></vc:footer>
```
