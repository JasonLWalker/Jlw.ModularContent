using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Jlw.Web.LocalizedContent.SampleWebApp.Controllers
{
    //[Route("~/Admin/[controller]")]
    [Authorize("ContentOverrideAdmin")]
    public class OverrideLocalizedDataItemsController : Controller
    {
        [HttpGet("~/Admin/[controller]/{groupKey?}/{parentKey?}")]
        public ActionResult Index(string groupKey, string parentKey)
        {
            ViewData["groupKey"] = groupKey;
            ViewData["apiOverrideUrl"] = Url.Content("~/admin/api/OverrideLocalizedDataItems/");
            ViewData["parentKey"] = parentKey;
            ViewData["PageTitle"] = "Data Item Override Admin";
            return View("~/Areas/LocalizedGroupDataItem/Views/Admin/Index.cshtml");
        }

        [HttpGet("~/Admin/LocalizedContentLanguage")]
        public ActionResult Language()
        {
            ViewData["hideId"] = true;
            ViewData["hideLanguage"] = true;
            ViewData["hideGroupKey"] = true;
            ViewData["hideOrder"] = true;
            ViewData["hideDescription"] = true;
            ViewData["hideData"] = true;

            ViewData["groupKey"] = "LocalizedContentLanguages";
            ViewData["apiOverrideUrl"] = Url.Content("~/admin/api/OverrideLocalizedContentLanguage/");
            ViewData["PageTitle"] = "Language Admin";
            return View("~/Areas/LocalizedGroupDataItem/Views/Admin/Index.cshtml");
        }


    }
}
