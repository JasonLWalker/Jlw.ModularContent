using Jlw.Web.Rcl.LocalizedContent.Areas.LocalizedGroupDataItem.Controllers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;

namespace Jlw.Web.LocalizedContent.SampleWebApp.Controllers
{
    [Route("~/Admin/[controller]")]
    [Authorize("ContentOverrideAdmin")]
    public class OverrideLocalizedDataItemsController : AdminController
    {

        public OverrideLocalizedDataItemsController(LinkGenerator linkGenerator) : base()
        {
            DefaultSettings.PageTitle = "Data Item Override Admin";
            //DefaultSettings.ApiOverrideUrl = linkGenerator.GetPathBy("~/admin/api/OverrideLocalizedDataItems") + "/";
        }


        [HttpGet]
        public override ActionResult Index(string groupKey = null, string parentKey = null)
        {
            //ViewData["groupKey"] = groupKey;
            //ViewData["apiOverrideUrl"] = Url.Content("~/admin/api/OverrideLocalizedDataItems/");
            //ViewData["parentKey"] = parentKey;
            //ViewData["PageTitle"] = "Data Item Override Admin";

            var settings = new AdminSettings()
            {
                ApiOverrideUrl = Url.Content("~/admin/api/OverrideLocalizedDataItems/"),
                IsAdmin = false,
                CanEdit = true,
                CanAdd = true,
                CanDelete = true,
                ShowId = false,
                ShowLanguage = false,
                ShowGroupKey = false,
                ShowOrder = false,
                ShowDescription = false,
                ShowData = false,
                GroupKeyOverride = "SampleRecord",
                PageTitle = "Data Item Override Admin",
                DataTableSearch = false,
                DataTableInfo = false,
                DataTablePaging = false,
            };

            return GetViewResult("Index", settings);
            //return View("~/Areas/LocalizedGroupDataItem/Views/Admin/Index.cshtml");
        }

        [HttpGet("~/Admin/LocalizedContentLanguage")]
        public ActionResult Language()
        {
            /*
            ViewData["hideId"] = true;
            ViewData["hideLanguage"] = true;
            ViewData["hideGroupKey"] = true;
            ViewData["hideOrder"] = true;
            ViewData["hideDescription"] = true;
            ViewData["hideData"] = true;

            ViewData["groupKey"] = "LocalizedContentLanguages";
            ViewData["apiOverrideUrl"] = Url.Content("~/admin/api/OverrideLocalizedContentLanguage/");
            ViewData["PageTitle"] = "Language Admin";
            */

            var settings = new AdminSettings()
            {
                ApiOverrideUrl = Url.Content("~/admin/api/OverrideLocalizedContentLanguage/"),
                ShowId = false,
                ShowLanguage = false,
                ShowGroupKey = false,
                ShowOrder = false,
                ShowDescription = false,
                ShowData = false,
                GroupKeyOverride = "LocalizedContentLanguages",
                PageTitle = "Language Admin"
            };

            return GetViewResult("Index", settings);
            //return View("~/Areas/LocalizedGroupDataItem/Views/Admin/Index.cshtml");
        }


    }
}
