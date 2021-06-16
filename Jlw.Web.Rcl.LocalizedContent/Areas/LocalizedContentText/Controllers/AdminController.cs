using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Jlw.Web.Rcl.LocalizedContent.Areas.LocalizedContentText.Controllers 
{
    /// <summary>Class AdminController.
    /// Implements the <see cref="Microsoft.AspNetCore.Mvc.Controller" /></summary>
    [Area("LocalizedContentText")]
    [Authorize("LocalizedContentUser")]
    public class AdminController : Controller 
    {
        /// <summary>Default route for the controller</summary>
        /// <param name="groupKey">The group key to filter by.</param>
        /// <param name="fieldKey">The field key  to filter by.</param>
        /// <param name="language">The language  to filter by.</param>
        /// <returns>ActionResult.</returns>
        [HttpGet]
        //[HttpGet("[area]/{groupKey?}/{fieldKey?}/{language?}")]
        // GET: Default 
        public ActionResult Index(string groupKey, string fieldKey, string language) 
        {
            ViewData["groupKey"] = groupKey;
            ViewData["fieldKey"] = fieldKey;
            ViewData["language"] = language;
            ViewData["groupFilter"] = null;
            ViewData["PageTitle"] = "Localized Content Text Admin";
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