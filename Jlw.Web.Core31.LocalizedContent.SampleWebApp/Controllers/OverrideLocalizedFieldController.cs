using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;

namespace Jlw.Web.Core31.LocalizedContent.SampleWebApp.Controllers
{
    //[Route("~/Admin/[controller]")]
    [Authorize("ContentOverrideAdmin")]
    public class OverrideLocalizedFieldController : Controller
    {
        [HttpGet("~/Admin/[controller]/{groupKey?}/{parentKey?}")]
        public ActionResult Index(string groupKey, string parentKey)
        {
            ViewData["groupKey"] = groupKey;
            ViewData["apiOverrideUrl"] = Url.Content("~/admin/api/OverrideLocalizedField/");
            ViewData["adminOverrideUrl"] = Url.Action("Index", new { groupKey = (string)null, fieldKey = (string)null });
            ViewData["adminTextOverrideUrl"] = Url.Action("Index", "OverrideLocalizedText", new { groupKey = (string)null, fieldKey = (string)null });
            ViewData["wizardOverrideUrl"] = Url.Action("Wizard", new { groupKey = (string)null, fieldKey = (string)null });
            ViewData["emailOverrideUrl"] = Url.Action("Email", new { groupKey = (string)null, fieldKey = (string)null });
            ViewData["parentKey"] = parentKey;
            ViewData["PageTitle"] = "Content Override Admin";
            return View("~/Areas/LocalizedContentField/Views/Admin/Index.cshtml");
        }

        [HttpGet("~/Admin/OverrideLocalizedWizard/{groupKey?}/{parentKey?}")]
        public ActionResult Wizard(string groupKey, string parentKey)
        {
            ViewData["groupKey"] = groupKey;
            //ViewData["apiOverrideUrl"] = Url.Content("~/admin/ContentOverride/api/");
            //ViewData["apiOverrideUrl"] = Url.Action("Index", "OverrideLocalizedFieldApi", new { groupKey = (string)null, fieldKey = (string)null }) + "/";
            ViewData["apiOverrideUrl"] = Url.Content("~/admin/api/OverrideLocalizedField/");
            ViewData["adminOverrideUrl"] = Url.Action("Index", new { groupKey = (string)null, fieldKey = (string)null });
            ViewData["adminTextOverrideUrl"] = Url.Action("Index", "OverrideLocalizedText", new { groupKey = (string)null, fieldKey = (string)null });
            ViewData["wizardOverrideUrl"] = Url.Action("Wizard",new { groupKey = (string)null, fieldKey = (string)null });
            ViewData["emailOverrideUrl"] = Url.Action("Email",new { groupKey = (string)null, fieldKey = (string)null });
            ViewData["fieldType"] = "WIZARD";
            ViewData["parentKey"] = parentKey;
            ViewData["PageTitle"] = "Wizard Override Admin";
            return View("~/Areas/LocalizedContentField/Views/Admin/Index.cshtml");
        }

        [HttpGet("~/Admin/OverrideLocalizedEmail/{groupKey?}/{parentKey?}")]
        public ActionResult Email(string groupKey, string parentKey)
        {
            ViewData["groupKey"] = groupKey;
            //ViewData["apiOverrideUrl"] = Url.Content("~/admin/ContentOverride/api/");
            //ViewData["apiOverrideUrl"] = Url.Action("Index", "OverrideLocalizedFieldApi", new { groupKey = (string)null, fieldKey = (string)null }) + "/";
            ViewData["apiOverrideUrl"] = Url.Content("~/admin/api/OverrideLocalizedField/");
            ViewData["adminOverrideUrl"] = Url.Action("Index", new { groupKey = (string)null, fieldKey = (string)null });
            ViewData["adminTextOverrideUrl"] = Url.Action("Index", "OverrideLocalizedText", new { groupKey = (string)null, fieldKey = (string)null });
            ViewData["wizardOverrideUrl"] = Url.Action("Email",new { groupKey = (string)null, fieldKey = (string)null });
            ViewData["emailOverrideUrl"] = Url.Action("Email", new { groupKey = (string)null, fieldKey = (string)null });
            ViewData["fieldType"] = "EMAIL";
            ViewData["parentKey"] = parentKey;
            ViewData["PageTitle"] = "Email Override Admin";
            return View("~/Areas/LocalizedContentField/Views/Admin/Index.cshtml");
        }


    }
}
