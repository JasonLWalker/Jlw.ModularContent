<!-- Do not edit this file directly. The README.md file is auto-generated during the build process, and any changes you make will be overwritten. If you need to make changes to this file, update the /build-scripts/templates/ReadmeTemplate.md file.  -->
<!--  -->
# Jlw.ModularContent

## Pipeline Status

| Test | Alpha | Staging | Release |
|-----|-----|-----|-----|
| [![Build and Test](https://github.com/JasonLWalker/Jlw.ModularContent/actions/workflows/build-test.yml/badge.svg)](https://github.com/JasonLWalker/Jlw.ModularContent/actions/workflows/build-test.yml) | [![Build and Deploy - Alpha](https://github.com/JasonLWalker/Jlw.ModularContent/actions/workflows/build-deploy-alpha.yml/badge.svg)](https://github.com/JasonLWalker/Jlw.ModularContent/actions/workflows/build-deploy-alpha.yml) | [![Build and Deploy - RC](https://github.com/JasonLWalker/Jlw.ModularContent/actions/workflows/build-deploy-rc.yml/badge.svg?branch=staging)](https://github.com/JasonLWalker/Jlw.ModularContent/actions/workflows/build-deploy-rc.yml) |[![Build and Deploy](https://github.com/JasonLWalker/Jlw.ModularContent/actions/workflows/build-deploy.yml/badge.svg)](https://github.com/JasonLWalker/Jlw.ModularContent/actions/workflows/build-deploy.yml) | 


# Data Repository
<!--  -->
[![Nuget](https://img.shields.io/nuget/v/Jlw.ModularContent.Data?label=Jlw.ModularContent.Data%20%28release%29)](https://www.nuget.org/packages/Jlw.ModularContent.Data/#versions-body-tab) [![Nuget (with prereleases)](https://img.shields.io/nuget/vpre/Jlw.ModularContent.Data?label=Jlw.ModularContent.Data%20%28preview%29)](https://www.nuget.org/packages/Jlw.ModularContent.Data/#versions-body-tab)

## Information / Requirements
|||
|-----|-----|
|Namespace|Jlw.ModularContent.Data|
|Target Framework|netstandard2.1|
|Author(s)|Jason L. Walker|
|Copyright|Copyright �2012-20223 Jason L. Walker|


## Dependencies

|Dependency|Version|License|Purpose|
|-----|-----|-----|-----|
|Jlw.Utilities.Data|4.8.8531.6086|[MIT](https://licenses.nuget.org/MIT)|A utility package providing static string manipulation and conversion utilities, as well as a modular database framework meant to provide a way to decouple database implementations when working with data repositories.|
|[Microsoft.Extensions.Options](https://dot.net/)|6.0.0|[MIT](https://licenses.nuget.org/MIT)|Provides a strongly typed way of specifying and accessing settings using dependency injection.|
|[Newtonsoft.Json](https://www.newtonsoft.com/json)|13.0.3|[MIT](https://licenses.nuget.org/MIT)|Json.NET is a popular high-performance JSON framework for .NET|


# Razor Class Library
<!--  -->
[![Nuget](https://img.shields.io/nuget/v/jlw.ModularContent.Rcl?label=jlw.ModularContent.Rcl%20%28release%29)](https://www.nuget.org/packages/jlw.ModularContent.Rcl/#versions-body-tab) [![Nuget (with prereleases)](https://img.shields.io/nuget/vpre/jlw.ModularContent.Rcl?label=jlw.ModularContent.Rcl%20%28preview%29)](https://www.nuget.org/packages/jlw.ModularContent.Rcl/#versions-body-tab)
## Information / Requirements

|||
|-----|-----|
|Namespace|Jlw.ModularContent.Rcl|
|Target Framework|net6.0|
|Author(s)|Jason L. Walker|
|Copyright|Copyright �2019-2023 Jason L. Walker|


## Back-End Dependencies

|Dependency|Version|License|Purpose|
|-----|-----|-----|-----|
|BuildWebCompiler|1.12.405|[...](https://github.com/madskristensen/WebCompiler/blob/master/LICENSE)|Compiles LESS, Sass, JSX and CoffeeScript files|
|BuildBundlerMinifier|3.2.449|[...](https://github.com/madskristensen/BundlerMinifier/blob/master/LICENSE)|Bundles and minifies CSS, JS and HTML files|
|Jlw.Extensions.DataParsing|4.8.8531.6086|[MIT](https://licenses.nuget.org/MIT)|A utility extension providing generic parsing of any object to another data type via the DataUtility class.|
|[Microsoft.Extensions.FileProviders.Embedded](https://asp.net/)|6.0.16|[MIT](https://licenses.nuget.org/MIT)|File provider for files in embedded resources for Microsoft.Extensions.FileProviders.|
|Jlw.Utilities.WebApiUtility|1.5.7928.8091|[MIT](https://licenses.nuget.org/MIT)|WebApi Utility class designed to work with the libJlwAppBuilder and libJlwUtility Npm/javascript libraries.|


## Front-end Dependencies
|Dependency|Version|License|Purpose|
|-----|-----|-----|-----|
|@jasonlwalker/jlwappbuilder|[1.0.32](https://github.com/JasonLWalker/Jlw.Package.Releases.git)|MIT|Utility library for building HTML5 web applications|
|@jasonlwalker/jlwutility|[1.2.11](https://github.com/JasonLWalker/Jlw.Package.Releases.git)|MIT|Utility library for HTML5 web applications that use JQuery, Bootstrap, Bootbox, Font-Awesome, and Toastr|
|[bootbox.js](http://bootboxjs.com)|[6.0.0](https://github.com/makeusabrew/bootbox.git)|MIT|Wrappers for JavaScript alert(), confirm() and other flexible dialogs using the Bootstrap framework|
|[bootswatch](http://bootswatch.com/)|[5.2.2](https://github.com/thomaspark/bootswatch.git)|MIT|Bootswatch is a collection of themes for Bootstrap.|
|[datatables.net](https://datatables.net)|[1.12.1](https://github.com/DataTables/Dist-DataTables.git)|MIT|jQuery DataTables|
|[datatables.net-bs5](https://datatables.net)|[1.12.1](https://github.com/DataTables/Dist-DataTables-Bootstrap5.git)|MIT|DataTables for jQuery with styling for [Bootstrap 5](http://getbootstrap.com/)|
|[font-awesome](https://fontawesome.com/)|[5.15.4](https://github.com/FortAwesome/Font-Awesome.git)|(OFL-1.1 OR MIT OR CC-BY-4.0)|The iconic SVG, font, and CSS toolkit|
|[jquery](http://jquery.com/)|[3.6.0](https://github.com/jquery/jquery.git)|MIT|Framework library used by other libraries for HTML, DOM, Event, and AJAX manipulation|
|mar10/fancytree|mar10/fancytree@2.38.2|| |
|[popper.js](https://popper.js.org/)|[2.11.6](https://github.com/popperjs/popper-core.git)|MIT|A kickass library to manage your poppers|
|[prism](http://prismjs.com/)|[1.28.0](https://github.com/PrismJS/prism.git)|MIT|Lightweight, robust, elegant syntax highlighting. A spin-off project from Dabblet.|
|[prism-themes](https://github.com/prismjs/prism-themes#readme)|[1.9.0](https://github.com/prismjs/prism-themes.git)|MIT|Additional themes for the Prism syntax highlighting library.|
|[Sortable](http://rubaxa.github.io/Sortable/)|[1.15.0](https://github.com/rubaxa/Sortable.git)|MIT|Minimalist JavaScript library for reorderable drag-and-drop lists on modern browsers and touch devices. No jQuery. Supports AngularJS and any CSS library, e.g. Bootstrap.|
|swisnl/jQuery-contextMenu|swisnl/jQuery-contextMenu@2.9.2|| |
|[tinymce](http://www.tinymce.com)|[5.8.1](https://github.com/tinymce/tinymce.git)|LGPL-2.0|TinyMCE rich text editor|
|[toastr.js](http://www.toastrjs.com)|[2.1.4](https://github.com/CodeSeven/toastr.git)|MIT|ToastrJS is a JavaScript library for Gnome / Growl type non-blocking notifications. jQuery is required. The goal is to create a simple core library that can be customized and extended.|
|[twitter-bootstrap](http://getbootstrap.com/)|[5.2.2](https://github.com/twbs/bootstrap)|MIT|Responsive UI, layout, and design framework|


<!--  -->
# SQL Schema

## Tables:

|Server|Database|Table|Purpose|
|-----|-----|-----|-----|
|(localdb)SqlLocalDb-SampleApp|LocalizedContent|[dbo.DatabaseAuditTrail](_git//?path=SqlSchema/Table/dbo.DatabaseAuditTrail.sql)|Used to store an audit trail of records in JSON format|
|(localdb)SqlLocalDb-SampleApp|LocalizedContent|[dbo.LocalizedContentFields](_git//?path=SqlSchema/Table/dbo.LocalizedContentFields.sql)|Used to store individual elements and metadata to be used by a wizard screen|
|(localdb)SqlLocalDb-SampleApp|LocalizedContent|[dbo.LocalizedContentText](_git//?path=SqlSchema/Table/dbo.LocalizedContentText.sql)|Used to store text, label, and error metadata to be used by individual fields on a wizard screen|
|(localdb)SqlLocalDb-SampleApp|LocalizedContent|[dbo.LocalizedGroupDataItems](_git//?path=SqlSchema/Table/dbo.LocalizedGroupDataItems.sql)|Used to store sets of data that may be grouped and sorted logically|


## Stored Procedures:

|Server|Database|Stored Procedure|Purpose|
|-----|-----|-----|-----|
|(localdb)SqlLocalDb-SampleApp|LocalizedContent|[dbo.sp_AuditTrailSave_LocalizedContentField](_git//?path=SqlSchema/StoredProcedure/dbo.sp_AuditTrailSave_LocalizedContentField.sql)|Used to insert or update an audit trail record for [LocalizedContentFields] |
|(localdb)SqlLocalDb-SampleApp|LocalizedContent|[dbo.sp_AuditTrailSave_LocalizedContentText](_git//?path=SqlSchema/StoredProcedure/dbo.sp_AuditTrailSave_LocalizedContentText.sql)|Used to insert or update an audit trail record for [LocalizedContentText] |
|(localdb)SqlLocalDb-SampleApp|LocalizedContent|[dbo.sp_AuditTrailSave_LocalizedGroupDataItems](_git//?path=SqlSchema/StoredProcedure/dbo.sp_AuditTrailSave_LocalizedGroupDataItems.sql)|Used to insert or update an audit trail record for [LocalizedGroupDataItems] |
|(localdb)SqlLocalDb-SampleApp|LocalizedContent|[dbo.sp_DeleteLocalizedContentFieldRecord](_git//?path=SqlSchema/StoredProcedure/dbo.sp_DeleteLocalizedContentFieldRecord.sql)|Used to remove matching record from [LocalizedContentFields] |
|(localdb)SqlLocalDb-SampleApp|LocalizedContent|[dbo.sp_DeleteLocalizedContentTextRecord](_git//?path=SqlSchema/StoredProcedure/dbo.sp_DeleteLocalizedContentTextRecord.sql)|Used to remove the matching record from [LocalizedContentText] |
|(localdb)SqlLocalDb-SampleApp|LocalizedContent|[dbo.sp_DeleteLocalizedGroupDataItemRecord](_git//?path=SqlSchema/StoredProcedure/dbo.sp_DeleteLocalizedGroupDataItemRecord.sql)|Used to remove matching record from [LocalizedGroupDataItems] |
|(localdb)SqlLocalDb-SampleApp|LocalizedContent|[dbo.sp_DeleteWizardFieldRecursive](_git//?path=SqlSchema/StoredProcedure/dbo.sp_DeleteWizardFieldRecursive.sql)|Used to recursively remove the matching records from [LocalizedContentFields] and [LocalizedContentText] |
|(localdb)SqlLocalDb-SampleApp|LocalizedContent|[dbo.sp_GetComponentList](_git//?path=SqlSchema/StoredProcedure/dbo.sp_GetComponentList.sql)|Helper method to generate a pre-defined list of components to be used in a wizard|
|(localdb)SqlLocalDb-SampleApp|LocalizedContent|[dbo.sp_GetFormFields](_git//?path=SqlSchema/StoredProcedure/dbo.sp_GetFormFields.sql)|Used to retrieve all matching form fields|
|(localdb)SqlLocalDb-SampleApp|LocalizedContent|[dbo.sp_GetLocalizedContentFieldRecord](_git//?path=SqlSchema/StoredProcedure/dbo.sp_GetLocalizedContentFieldRecord.sql)|Used to retrieve the first matching record from [LocalizedContentFields] |
|(localdb)SqlLocalDb-SampleApp|LocalizedContent|[dbo.sp_GetLocalizedContentFieldRecordByName](_git//?path=SqlSchema/StoredProcedure/dbo.sp_GetLocalizedContentFieldRecordByName.sql)|Used to retrieve the first matching record from [LocalizedContentFields] |
|(localdb)SqlLocalDb-SampleApp|LocalizedContent|[dbo.sp_GetLocalizedContentFieldsDt](_git//?path=SqlSchema/StoredProcedure/dbo.sp_GetLocalizedContentFieldsDt.sql)|Used to generate a DataTable list of matching records from [LocalizedContentFields] |
|(localdb)SqlLocalDb-SampleApp|LocalizedContent|[dbo.sp_GetLocalizedContentTextDt](_git//?path=SqlSchema/StoredProcedure/dbo.sp_GetLocalizedContentTextDt.sql)|Used to generate a DataTable list of matching records from [LocalizedContentText] |
|(localdb)SqlLocalDb-SampleApp|LocalizedContent|[dbo.sp_GetLocalizedContentTextRecord](_git//?path=SqlSchema/StoredProcedure/dbo.sp_GetLocalizedContentTextRecord.sql)|Used to retrieve the first matching record from [LocalizedContentText] |
|(localdb)SqlLocalDb-SampleApp|LocalizedContent|[dbo.sp_GetLocalizedGroupDataItemRecord](_git//?path=SqlSchema/StoredProcedure/dbo.sp_GetLocalizedGroupDataItemRecord.sql)|Used to retrieve the first matching record from [LocalizedGroupDataItems] |
|(localdb)SqlLocalDb-SampleApp|LocalizedContent|[dbo.sp_GetLocalizedGroupDataItems](_git//?path=SqlSchema/StoredProcedure/dbo.sp_GetLocalizedGroupDataItems.sql)|Used to retrieve the matching records from [LocalizedGroupDataItems] |
|(localdb)SqlLocalDb-SampleApp|LocalizedContent|[dbo.sp_GetLocalizedGroupDataItemsDt](_git//?path=SqlSchema/StoredProcedure/dbo.sp_GetLocalizedGroupDataItemsDt.sql)|Used to generate a DataTable list of matching records from [LocalizedGroupDataItems] |
|(localdb)SqlLocalDb-SampleApp|LocalizedContent|[dbo.sp_GetLocalizedGroupDataItemValue](_git//?path=SqlSchema/StoredProcedure/dbo.sp_GetLocalizedGroupDataItemValue.sql)|Used to retrieve the first matching record from [LocalizedGroupDataItems] |
|(localdb)SqlLocalDb-SampleApp|LocalizedContent|[dbo.sp_GetWizardContentFieldRecord](_git//?path=SqlSchema/StoredProcedure/dbo.sp_GetWizardContentFieldRecord.sql)|Used to retrieve the first matching record from [LocalizedContentFields] |
|(localdb)SqlLocalDb-SampleApp|LocalizedContent|[dbo.sp_GetWizardFields](_git//?path=SqlSchema/StoredProcedure/dbo.sp_GetWizardFields.sql)|Used to retrieve a grouped list of wizard fields with localized text|
|(localdb)SqlLocalDb-SampleApp|LocalizedContent|[dbo.sp_GetWizardModelFields](_git//?path=SqlSchema/StoredProcedure/dbo.sp_GetWizardModelFields.sql)|Used to retrieve a list of the Model Fields that will be passed by the API during wizard interactions.|
|(localdb)SqlLocalDb-SampleApp|LocalizedContent|[dbo.sp_RenameWizardFieldRecursive](_git//?path=SqlSchema/StoredProcedure/dbo.sp_RenameWizardFieldRecursive.sql)|Used to recursively rename the matching records from [LocalizedContentFields] and [LocalizedContentText] by changing the [FieldKey] column|
|(localdb)SqlLocalDb-SampleApp|LocalizedContent|[dbo.sp_SaveLocalizedContentFieldData](_git//?path=SqlSchema/StoredProcedure/dbo.sp_SaveLocalizedContentFieldData.sql)|Used to retrieve the first matching record from [LocalizedContentFields] |
|(localdb)SqlLocalDb-SampleApp|LocalizedContent|[dbo.sp_SaveLocalizedContentFieldParentOrder](_git//?path=SqlSchema/StoredProcedure/dbo.sp_SaveLocalizedContentFieldParentOrder.sql)|Used to retrieve the first matching record from [LocalizedContentFields] |
|(localdb)SqlLocalDb-SampleApp|LocalizedContent|[dbo.sp_SaveLocalizedContentFieldRecord](_git//?path=SqlSchema/StoredProcedure/dbo.sp_SaveLocalizedContentFieldRecord.sql)|Used to retrieve the first matching record from [LocalizedContentFields] |
|(localdb)SqlLocalDb-SampleApp|LocalizedContent|[dbo.sp_SaveLocalizedContentTextRecord](_git//?path=SqlSchema/StoredProcedure/dbo.sp_SaveLocalizedContentTextRecord.sql)|Used to retrieve the first matching record from [LocalizedContentText] |
|(localdb)SqlLocalDb-SampleApp|LocalizedContent|[dbo.sp_SaveLocalizedGroupDataItemRecord](_git//?path=SqlSchema/StoredProcedure/dbo.sp_SaveLocalizedGroupDataItemRecord.sql)|Used to insert or update a matching record in [LocalizedGroupDataItems] |


## Functions:

|Server|Database|Function|Purpose|
|-----|-----|-----|-----|
|(localdb)SqlLocalDb-SampleApp|LocalizedContent|[dbo.fnGetWizardTreeNodes](_git//?path=SqlSchema/UserDefinedFunction/dbo.fnGetWizardTreeNodes.sql)|Used to recursively find the matching records from [LocalizedContentFields] by walking the tree of nodes, starting with @id|


## Views:

|Server|Database|View|Purpose|
|-----|-----|-----|-----|
|(localdb)SqlLocalDb-SampleApp|LocalizedContent|[dbo.vwLocalizedContentField_Audit](_git//?path=SqlSchema/View/dbo.vwLocalizedContentField_Audit.sql)|Used to retrieve the Audit records for the table [LocalizedContentFields] |
|(localdb)SqlLocalDb-SampleApp|LocalizedContent|[dbo.vwLocalizedContentText_Audit](_git//?path=SqlSchema/View/dbo.vwLocalizedContentText_Audit.sql)|Used to retrieve the Audit records for the table [LocalizedContentText] |



