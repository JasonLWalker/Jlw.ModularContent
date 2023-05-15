using System.Collections.Generic;
using Jlw.Web.Rcl.LocalizedContent.Areas.ModularWizardEmailAdmin.Controllers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Jlw.Web.LocalizedContent.SampleWebApp.Controllers
{
    [Route("~/Admin/[controller]")]
    [Authorize("ContentOverrideAdmin")]
    public class OverrideModularWizardEmailAdminController : AdminController
    {
        [HttpGet("{groupKey?}/{parentKey?}")]
        public override ActionResult Index(string groupKey=null, string parentKey=null)
        {
            string adminApiUrl = Url.Action("Index", "OverrideModularWizardEmailAdminApi", new { Area = "" }) + "/";

            var aBreadcrumb = new List<KeyValuePair<string, string>>()
            {
                new KeyValuePair<string, string>(Url.Action("Index", "Home"), "Home"),
                new KeyValuePair<string, string>(Url.Action("Index", "Admin"), "Administration"),
            };

            ViewData["Breadcrumb"] = aBreadcrumb;
            var settings = new AdminController.WizardAdminSettings()
            {
                PageTitle = "Email Admin",
                IsAdmin = true,
                CanEdit = true,
                CanInsert = true,
                CanDelete = true,
                GroupKey = "Sample0",
                Language = (groupKey == null || parentKey == null) ? null : "*",
                FieldKey = parentKey,
                ApiOverrideUrl = adminApiUrl,
                AdminUrl = Url.Action("Index", "OverrideModularWizardEmailAdmin", new { Area="", groupKey = (string)null, parentKey = (string)null }),
                TinyMceSettings = JObject.Parse(@"
                {
                    useTinyMce: true,
                    plugins: 'image imagetools paste link code help',
                    height: 'calc(90vh - 350px)',
                    image_advtab: true,
                    paste_data_images: true,
                    toolbar: 'undo redo | styleselect | bold italic | alignleft aligncenter alignright alignjustify | outdent indent | code ',
                    menu: {
                        file: { title: 'File', items: 'newdocument ' },
                        edit: { title: 'Edit', items: 'undo redo | cut copy paste | selectall | searchreplace' },
                        view: { title: 'View', items: 'code | visualaid visualchars visualblocks | spellchecker | preview fullscreen' },
                        insert: { title: 'Insert', items: 'image link media template codesample inserttable | charmap emoticons hr | pagebreak nonbreaking anchor toc | insertdatetime' },
                        format: { title: 'Format', items: 'bold italic underline strikethrough superscript subscript codeformat | formats blockformats fontformats fontsizes align lineheight | forecolor backcolor | removeformat' },
                        tools: { title: 'Tools', items: 'spellchecker spellcheckerlanguage | code wordcount' },
                        table: { title: 'Table', items: 'inserttable | cell row column | tableprops deletetable' },
                        help: { title: 'Help', items: 'help' }
                    }
                }"),
            };
            settings.LanguageList.Add(new SelectListItem("Spanish", "SP"));
            settings.LanguageList.Add(new SelectListItem("French", "FR"));

            return GetViewResult("~/Areas/ModularWizardEmailAdmin/Views/Admin/Index.cshtml", settings);

        }
    }
}
