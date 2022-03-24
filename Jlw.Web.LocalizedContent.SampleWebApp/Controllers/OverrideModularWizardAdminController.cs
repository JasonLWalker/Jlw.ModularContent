using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;

namespace Jlw.Web.Core31.LocalizedContent.SampleWebApp.Controllers
{
    [Route("~/Admin/[controller]")]
    [Authorize("ContentOverrideAdmin")]
    public class OverrideModularWizardAdminController : Jlw.Web.Rcl.LocalizedContent.Areas.ModularWizardAdmin.Controllers.AdminController
    {
        [HttpGet]
        public override ActionResult Index()
        {
            string adminApiUrl = Url.Action("Index", "OverrideModularWizardAdminApi", new { Area = "" });

            var aBreadcrumb = new List<KeyValuePair<string, string>>()
            {
                new KeyValuePair<string, string>(Url.Action("Index", "Home"), "Home"),
                new KeyValuePair<string, string>(Url.Action("Index", "Admin"), "Administration"),
            };

            ViewData["Breadcrumb"] = aBreadcrumb;
            ViewData["apiOverrideUrl"] = adminApiUrl;

            return base.Index();
        }
    }
}
