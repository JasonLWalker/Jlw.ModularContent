using System.Collections.Generic;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.AspNetCore.Routing;
using Newtonsoft.Json.Linq;

namespace Jlw.Web.LocalizedContent.SampleWebApp.Controllers
{
    [Route("~/Admin/[controller]")]
    [Authorize("ContentOverrideAdmin")]
    public class OverrideModularWizardAdminController : Jlw.Web.Rcl.LocalizedContent.Areas.ModularWizardAdmin.Controllers.AdminController
    {
        public OverrideModularWizardAdminController(LinkGenerator linkGenerator) : base()
        {
            DefaultSettings.ApiOverrideUrl = linkGenerator.GetPathByAction("Index", "OverrideModularWizardAdminApi", new { Area = "" });
            DefaultSettings.ToolboxHeight = "calc(100vh - 58px)";
            //DefaultSettings.ShowSideNav = false;
            //DefaultSettings.SideNavDefault = false;
            //DefaultSettings.ShowWireFrame = false;
            DefaultSettings.IsAdmin = true;
            DefaultSettings.CanEdit = true;
            DefaultSettings.CanInsert = true;
            DefaultSettings.CanDelete = true;
            DefaultSettings.HiddenFilterPrefix = "Sample";
            DefaultSettings.LanguageList.Add(new SelectListItem("Chinese", "CN"));
            DefaultSettings.TinyMceSettings = JObject.Parse(@"
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
                }");

        }

        [HttpGet]
        public override ActionResult Index()
        {
            var aBreadcrumb = new List<KeyValuePair<string, string>>()
            {
                new KeyValuePair<string, string>(Url.Action("Index", "Home"), "Home"),
                new KeyValuePair<string, string>(Url.Action("Index", "Admin"), "Administration"),
            };

            //DefaultSettings.ApiOverrideUrl = Url.Action("Index", "OverrideModularWizardAdminApi", new { Area = "" });
            ViewData["Breadcrumb"] = aBreadcrumb;

            return base.Index();
        }
    }
}
