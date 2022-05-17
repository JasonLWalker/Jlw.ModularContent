<!-- Do not edit this file directly. The README.md file is auto-generated during the build process, and any changes you make will be overwritten. If you need to make changes to this file, update the /build-scripts/templates/ReadmeTemplate.md file.  -->
<!--  -->
# Jlw.LocalizedContent

## Pipeline Status

| Test | Alpha | Staging | Release |
|-----|-----|-----|-----|
| [![Build and Test](https://github.com/JasonLWalker/Jlw.LocalizedContent/actions/workflows/build-test.yml/badge.svg)](https://github.com/JasonLWalker/Jlw.LocalizedContent/actions/workflows/build-test.yml) | [![Build and Deploy - Alpha](https://github.com/JasonLWalker/Jlw.LocalizedContent/actions/workflows/build-deploy-alpha.yml/badge.svg)](https://github.com/JasonLWalker/Jlw.LocalizedContent/actions/workflows/build-deploy-alpha.yml) | [![Build and Deploy - RC](https://github.com/JasonLWalker/Jlw.LocalizedContent/actions/workflows/build-deploy-rc.yml/badge.svg)](https://github.com/JasonLWalker/Jlw.LocalizedContent/actions/workflows/build-deploy-rc.yml) |[![Build and Deploy](https://github.com/JasonLWalker/Jlw.LocalizedContent/actions/workflows/build-deploy.yml/badge.svg)](https://github.com/JasonLWalker/Jlw.LocalizedContent/actions/workflows/build-deploy.yml) | 


# Data Repository
<!--  -->

## Information / Requirements
|||
|-----|-----|
|Namespace|Jlw.Data.LocalizedContent|
|Target Framework|netstandard2.1|
|Author(s)|Jason L. Walker|
|Copyright|Copyright ©2012-2021 Jason L. Walker|
|Version|0.0.0.1|


## Dependencies

|Dependency|Version|License|Purpose|
|-----|-----|-----|-----|
|Jlw.Utilities.Data|4.3.7948.6501|[MIT](https://licenses.nuget.org/MIT)||
|[Microsoft.Extensions.Options](https://dot.net/)|6.0.0|[MIT](https://licenses.nuget.org/MIT)||
|[Newtonsoft.Json](https://www.newtonsoft.com/json)|13.0.1|[MIT](https://licenses.nuget.org/MIT)||


# Razor Class Library
<!--  -->
## Information / Requirements

|||
|-----|-----|
|Namespace|Jlw.Web.Rcl.LocalizedContent|
|Target Framework|net6.0|
|Author(s)|Jason L. Walker|
|Copyright|Copyright ©2019-2021 Jason L. Walker|
|Version|0.0.0.1|


## Back-End Dependencies

|Dependency|Version|License|Purpose|
|-----|-----|-----|-----|
|BuildWebCompiler|1.12.405|[...](https://github.com/madskristensen/WebCompiler/blob/master/LICENSE)||
|[Microsoft.Extensions.FileProviders.Composite](https://dot.net/)|6.0.0|[MIT](https://licenses.nuget.org/MIT)||
|[Microsoft.Extensions.FileProviders.Embedded](https://asp.net/)|6.0.3|[MIT](https://licenses.nuget.org/MIT)||
|Jlw.Utilities.WebApiUtility|1.5.7928.8091|[MIT](https://licenses.nuget.org/MIT)||
|BuildBundlerMinifier|3.2.449|[...](https://github.com/madskristensen/BundlerMinifier/blob/master/LICENSE)||
|BundlerMinifier.Core|3.2.449|[...](https://github.com/madskristensen/BundlerMinifier/blob/master/LICENSE)||
|[Microsoft.Web.LibraryManager.Build](https://github.com/aspnet/LibraryManager)|2.1.161|[License.txt](https://aka.ms/deprecateLicenseUrl)||


## Front-end Dependencies
|Dependency|Version|License|Purpose|
|-----|-----|-----|-----|
|@jasonlwalker/jlwappbuilder|[0.0.34](https://github.com/JasonLWalker/Jlw.Package.Releases.git)|MIT||
|@jasonlwalker/jlwutility|[0.0.34](https://github.com/JasonLWalker/Jlw.Package.Releases.git)|MIT||
|[bootbox.js](http://bootboxjs.com)|[5.4.0](https://github.com/makeusabrew/bootbox.git)|MIT||
|[bootswatch](http://bootswatch.com/)|[4.5.2](https://github.com/thomaspark/bootswatch.git)|MIT||
|[datatables](http://datatables.net)|[1.10.21](https://github.com/DataTables/DataTables.git)|MIT|Display tables in user-friendly way|
|[font-awesome](https://fontawesome.com/)|[5.15.4](https://github.com/FortAwesome/Font-Awesome.git)|(OFL-1.1 OR MIT OR CC-BY-4.0)||
|jquery.fancytree|[2.38.0](https://github.com/mar10/fancytree)|MIT||
|[jquery](http://jquery.com/)|[3.3.1](https://github.com/jquery/jquery.git)|MIT|Framework library used by other libraries for HTML, DOM, Event, and AJAX manipulation|
|[popper.js](https://popper.js.org/)|[1.16.1](https://github.com/popperjs/popper-core.git)|MIT||
|[prism-themes](https://github.com/prismjs/prism-themes#readme)|[1.7.0](https://github.com/prismjs/prism-themes.git)|MIT||
|[prism](http://prismjs.com/)|[1.7.0](https://github.com/PrismJS/prism.git)|MIT||
|[Sortable](http://rubaxa.github.io/Sortable/)|[1.14.0](https://github.com/rubaxa/Sortable.git)|MIT||
|[tinymce](http://www.tinymce.com)|[5.8.1](https://github.com/tinymce/tinymce.git)|LGPL-2.0||
|[toastr.js](http://www.toastrjs.com)|[2.1.4](https://github.com/CodeSeven/toastr.git)|MIT||
|[twitter-bootstrap](http://getbootstrap.com/)|[4.5.2](https://github.com/twbs/bootstrap)|MIT|Responsive UI, layout, and design framework|


<!--  -->
# SQL Schema

## Tables:

|Server|Database|Table|Purpose|
|-----|-----|-----|-----|
|(localdb)SqlLocalDb-SampleApp|LocalizedContent|[dbo.DatabaseAuditTrail](_git//?path=SqlSchema/Table/dbo.DatabaseAuditTrail.sql)||
|(localdb)SqlLocalDb-SampleApp|LocalizedContent|[dbo.LocalizedContentFields](_git//?path=SqlSchema/Table/dbo.LocalizedContentFields.sql)||
|(localdb)SqlLocalDb-SampleApp|LocalizedContent|[dbo.LocalizedContentText](_git//?path=SqlSchema/Table/dbo.LocalizedContentText.sql)||
|(localdb)SqlLocalDb-SampleApp|LocalizedContent|[dbo.LocalizedGroupDataItems](_git//?path=SqlSchema/Table/dbo.LocalizedGroupDataItems.sql)||


## Stored Procedures:

|Server|Database|Stored Procedure|Purpose|
|-----|-----|-----|-----|
|(localdb)SqlLocalDb-SampleApp|LocalizedContent|[dbo.sp_AuditTrailSave_LocalizedContentField](_git//?path=SqlSchema/StoredProcedure/dbo.sp_AuditTrailSave_LocalizedContentField.sql)||
|(localdb)SqlLocalDb-SampleApp|LocalizedContent|[dbo.sp_AuditTrailSave_LocalizedContentText](_git//?path=SqlSchema/StoredProcedure/dbo.sp_AuditTrailSave_LocalizedContentText.sql)||
|(localdb)SqlLocalDb-SampleApp|LocalizedContent|[dbo.sp_AuditTrailSave_LocalizedGroupDataItems](_git//?path=SqlSchema/StoredProcedure/dbo.sp_AuditTrailSave_LocalizedGroupDataItems.sql)||
|(localdb)SqlLocalDb-SampleApp|LocalizedContent|[dbo.sp_DeleteLocalizedContentFieldRecord](_git//?path=SqlSchema/StoredProcedure/dbo.sp_DeleteLocalizedContentFieldRecord.sql)||
|(localdb)SqlLocalDb-SampleApp|LocalizedContent|[dbo.sp_DeleteLocalizedContentTextRecord](_git//?path=SqlSchema/StoredProcedure/dbo.sp_DeleteLocalizedContentTextRecord.sql)||
|(localdb)SqlLocalDb-SampleApp|LocalizedContent|[dbo.sp_DeleteLocalizedGroupDataItemRecord](_git//?path=SqlSchema/StoredProcedure/dbo.sp_DeleteLocalizedGroupDataItemRecord.sql)||
|(localdb)SqlLocalDb-SampleApp|LocalizedContent|[dbo.sp_DeleteWizardFieldRecursive](_git//?path=SqlSchema/StoredProcedure/dbo.sp_DeleteWizardFieldRecursive.sql)||
|(localdb)SqlLocalDb-SampleApp|LocalizedContent|[dbo.sp_GetComponentList](_git//?path=SqlSchema/StoredProcedure/dbo.sp_GetComponentList.sql)||
|(localdb)SqlLocalDb-SampleApp|LocalizedContent|[dbo.sp_GetFormFields](_git//?path=SqlSchema/StoredProcedure/dbo.sp_GetFormFields.sql)||
|(localdb)SqlLocalDb-SampleApp|LocalizedContent|[dbo.sp_GetLocalizedContentFieldRecord](_git//?path=SqlSchema/StoredProcedure/dbo.sp_GetLocalizedContentFieldRecord.sql)||
|(localdb)SqlLocalDb-SampleApp|LocalizedContent|[dbo.sp_GetLocalizedContentFieldsDt](_git//?path=SqlSchema/StoredProcedure/dbo.sp_GetLocalizedContentFieldsDt.sql)||
|(localdb)SqlLocalDb-SampleApp|LocalizedContent|[dbo.sp_GetLocalizedContentTextDt](_git//?path=SqlSchema/StoredProcedure/dbo.sp_GetLocalizedContentTextDt.sql)||
|(localdb)SqlLocalDb-SampleApp|LocalizedContent|[dbo.sp_GetLocalizedContentTextRecord](_git//?path=SqlSchema/StoredProcedure/dbo.sp_GetLocalizedContentTextRecord.sql)||
|(localdb)SqlLocalDb-SampleApp|LocalizedContent|[dbo.sp_GetLocalizedGroupDataItemRecord](_git//?path=SqlSchema/StoredProcedure/dbo.sp_GetLocalizedGroupDataItemRecord.sql)||
|(localdb)SqlLocalDb-SampleApp|LocalizedContent|[dbo.sp_GetLocalizedGroupDataItems](_git//?path=SqlSchema/StoredProcedure/dbo.sp_GetLocalizedGroupDataItems.sql)||
|(localdb)SqlLocalDb-SampleApp|LocalizedContent|[dbo.sp_GetLocalizedGroupDataItemsDt](_git//?path=SqlSchema/StoredProcedure/dbo.sp_GetLocalizedGroupDataItemsDt.sql)||
|(localdb)SqlLocalDb-SampleApp|LocalizedContent|[dbo.sp_GetLocalizedGroupDataItemValue](_git//?path=SqlSchema/StoredProcedure/dbo.sp_GetLocalizedGroupDataItemValue.sql)||
|(localdb)SqlLocalDb-SampleApp|LocalizedContent|[dbo.sp_GetWizardContentFieldRecord](_git//?path=SqlSchema/StoredProcedure/dbo.sp_GetWizardContentFieldRecord.sql)||
|(localdb)SqlLocalDb-SampleApp|LocalizedContent|[dbo.sp_GetWizardFields](_git//?path=SqlSchema/StoredProcedure/dbo.sp_GetWizardFields.sql)||
|(localdb)SqlLocalDb-SampleApp|LocalizedContent|[dbo.sp_SaveLocalizedContentFieldData](_git//?path=SqlSchema/StoredProcedure/dbo.sp_SaveLocalizedContentFieldData.sql)||
|(localdb)SqlLocalDb-SampleApp|LocalizedContent|[dbo.sp_SaveLocalizedContentFieldParentOrder](_git//?path=SqlSchema/StoredProcedure/dbo.sp_SaveLocalizedContentFieldParentOrder.sql)||
|(localdb)SqlLocalDb-SampleApp|LocalizedContent|[dbo.sp_SaveLocalizedContentFieldRecord](_git//?path=SqlSchema/StoredProcedure/dbo.sp_SaveLocalizedContentFieldRecord.sql)||
|(localdb)SqlLocalDb-SampleApp|LocalizedContent|[dbo.sp_SaveLocalizedContentTextRecord](_git//?path=SqlSchema/StoredProcedure/dbo.sp_SaveLocalizedContentTextRecord.sql)||
|(localdb)SqlLocalDb-SampleApp|LocalizedContent|[dbo.sp_SaveLocalizedGroupDataItemRecord](_git//?path=SqlSchema/StoredProcedure/dbo.sp_SaveLocalizedGroupDataItemRecord.sql)||


## Views:

|Server|Database|View|Purpose|
|-----|-----|-----|-----|
|(localdb)SqlLocalDb-SampleApp|LocalizedContent|[dbo.vwLocalizedContentField_Audit](_git//?path=SqlSchema/View/dbo.vwLocalizedContentField_Audit.sql)||
|(localdb)SqlLocalDb-SampleApp|LocalizedContent|[dbo.vwLocalizedContentText_Audit](_git//?path=SqlSchema/View/dbo.vwLocalizedContentText_Audit.sql)||



