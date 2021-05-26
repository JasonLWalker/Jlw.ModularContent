using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Jlw.Web.Rcl.LocalizedContent.Areas.LocalizedGroupDataItem.Controllers 
{ 
    [Area("LocalizedGroupDataItem")]
    [Authorize("LocalizedContentUser")]
    public class AdminController : Controller 
    {
        [HttpGet] 
        // GET: Default 
        public ActionResult Index() 
        { 
            return View("Index"); 
        } 
 
    } 
}