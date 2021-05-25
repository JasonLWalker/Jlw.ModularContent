using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Jlw.Web.Rcl.LocalizedContent.Areas.LocalizedContentField.Controllers 
{
    [Area("LocalizedContentField")]
    [Authorize("LocalizedContentUser")] 
    public class AdminController : Controller 
    {
        [HttpGet]
        public ActionResult Index(string groupKey, string parentKey)
        {
            ViewData["groupKey"] = groupKey;
            ViewData["parentKey"] = parentKey;
            ViewData["groupFilter"] = null;
            ViewData["PageTitle"] = "Localized Content Field Admin";
            return GetViewResult();
        }

        [HttpGet]
        public ActionResult Wizard(string groupKey, string parentKey)
        {
            ViewData["fieldType"] = "WIZARD";
            ViewData["groupKey"] = groupKey;
            ViewData["groupFilter"] = null;
            ViewData["parentKey"] = parentKey;
            ViewData["PageTitle"] = "Localized Wizard Admin";
            return GetViewResult();
        }

        [HttpGet]
        public ActionResult Email(string groupKey, string parentKey)
        {
            ViewData["fieldType"] = "EMAIL";
            ViewData["groupKey"] = groupKey;
            ViewData["groupFilter"] = null;
            ViewData["parentKey"] = parentKey;
            ViewData["PageTitle"] = "Localized Email Admin";
            return GetViewResult();
        }

        [NonAction]
        public ActionResult GetViewResult(string viewName = "Index")
        {
            return View(viewName);
        }

    }
}