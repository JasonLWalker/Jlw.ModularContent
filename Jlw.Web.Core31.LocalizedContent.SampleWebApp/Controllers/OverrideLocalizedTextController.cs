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
    public class OverrideLocalizedTextController : Controller
    {
        [HttpGet("~/Admin/[controller]/{groupKey?}/{parentKey?}")]
        public ActionResult Index(string groupKey, string parentKey)
        {
            ViewData["groupKey"] = groupKey;
            ViewData["apiOverrideUrl"] = Url.Content("~/admin/api/OverrideLocalizedText/");
            ViewData["adminOverrideUrl"] = Url.Action("Index", "OverrideLocalizedText", new { groupKey = (string)null, fieldKey = (string)null });
            ViewData["adminFieldOverrideUrl"] = Url.Action("Index", "OverrideLocalizedField", new { groupKey = (string)null, fieldKey = (string)null });
            ViewData["fieldKey"] = parentKey;
            ViewData["PageTitle"] = "Content Override Admin";
            return View("~/Areas/LocalizedContentText/Views/Admin/Index.cshtml");
        }

    }
}
