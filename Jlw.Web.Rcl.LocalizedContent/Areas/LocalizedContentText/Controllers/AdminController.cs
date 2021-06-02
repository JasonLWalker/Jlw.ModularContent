using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Jlw.Web.Rcl.LocalizedContent.Areas.LocalizedContentText.Controllers 
{ 
    [Area("LocalizedContentText")]
    [Authorize("LocalizedContentUser")]
    public class AdminController : Controller 
    {
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

        [NonAction]
        public ActionResult GetViewResult(string viewName = "Index")
        {
            return View(viewName);
        }


    }
}