using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Jlw.Web.Rcl.LocalizedContent.Areas.LocalizedGroupDataItem.Controllers 
{
    /// <summary>Class AdminController.
    /// Implements the <see cref="Microsoft.AspNetCore.Mvc.Controller" /></summary>
    [Area("LocalizedGroupDataItem")]
    [Authorize("LocalizedContentUser")]
    public abstract class AdminController : Controller 
    {
        /// <summary>Default route for admin</summary>
        /// <returns>ActionResult.</returns>
        [HttpGet]
        // GET: Default 
        public ActionResult Index() 
        { 
            return View("Index"); 
        } 
 
    } 
}