using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Jlw.LocalizedContent.Areas.LocalizedContentField.Controllers 
{
    /// <summary>Class AdminController.
    /// Implements the <see cref="Microsoft.AspNetCore.Mvc.Controller" /></summary>
    [Area("LocalizedContentField")]
    [Authorize("LocalizedContentUser")] 
    public abstract class AdminController : Controller 
    {
        /// <summary>Admin UI Index page.</summary>
        /// <param name="groupKey">The group key to filter by.</param>
        /// <param name="parentKey">The parent key to filter by.</param>
        /// <returns>ActionResult.</returns>
        [HttpGet]
        public ActionResult Index(string groupKey, string parentKey)
        {
            ViewData["groupKey"] = groupKey;
            ViewData["parentKey"] = parentKey;
            ViewData["groupFilter"] = null;
            ViewData["PageTitle"] = "Localized Content Field Admin";
            return GetViewResult();
        }

        /// <summary>Admin Wizard UI page.</summary>
        /// <param name="groupKey">The group key to filter by.</param>
        /// <param name="parentKey">The parent key to filter by.</param>
        /// <returns>ActionResult.</returns>
        [HttpGet]
        public ActionResult Wizard(string groupKey, string parentKey)
        {
            ViewData["fieldType"] = "WIZARD";
            ViewData["groupKey"] = groupKey;
            ViewData["parentKey"] = parentKey;
            ViewData["groupFilter"] = null;
            ViewData["PageTitle"] = "Localized Wizard Admin";
            return GetViewResult();
        }

        /// <summary>Email Admin UI page</summary>
        /// <param name="groupKey">The group key to filter by.</param>
        /// <param name="parentKey">The parent key to filter by.</param>
        /// <returns>ActionResult.</returns>
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

        /// <summary>Gets the view result.</summary>
        /// <param name="viewName">Name of the view.</param>
        /// <returns>ActionResult.</returns>
        [NonAction]
        public ActionResult GetViewResult(string viewName = "Index")
        {
            return View(viewName);
        }

    }
}