using System.Collections.Generic;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.AspNetCore.Routing;

namespace Jlw.Web.LocalizedContent.SampleWebApp.Controllers
{
    [Route("~/Admin/[controller]")]
    [Authorize("ContentOverrideAdmin")]
    public class OverrideModularWizardAdminController : Jlw.Web.Rcl.LocalizedContent.Areas.ModularWizardAdmin.Controllers.AdminController
    {
        public OverrideModularWizardAdminController(LinkGenerator linkGenerator) : base()
        {
            DefaultSettings.ApiOverrideUrl = linkGenerator.GetPathByAction("Index", "OverrideModularWizardAdminApi", new { Area = "" });
            DefaultSettings.ToolboxHeight = "calc(100vh - 48px)";
            //DefaultSettings.ShowSideNav = false;
            //DefaultSettings.SideNavDefault = false;
            //DefaultSettings.ShowWireFrame = false;
            DefaultSettings.IsAdmin = true;
            DefaultSettings.CanEdit = true;
            DefaultSettings.CanInsert = true;
            DefaultSettings.CanDelete = true;
            DefaultSettings.LanguageList.Add(new SelectListItem("Chinese", "CN"));
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
