using System.Collections.Generic;
using Jlw.Data.LocalizedContent;
using Jlw.Web.Rcl.LocalizedContent;
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
        
        public OverrideModularWizardAdminController(IWizardAdminSettings settings, IWizardFactoryRepository repository) : base(settings, repository)
        {
            _groupFilter = "Sample%";
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
