using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Jlw.LocalizedContent.Areas.LocalizedGroupDataItem.Controllers 
{
    /// <summary>Class AdminController.
    /// Implements the <see cref="Microsoft.AspNetCore.Mvc.Controller" /></summary>
    //[Area("LocalizedGroupDataItem")]
    //[Authorize("LocalizedContentUser")]
    public abstract class AdminController : Controller 
    {
        protected AdminSettings DefaultSettings { get; } = new AdminSettings();

        /// <summary>Default route for admin</summary>
        /// <returns>ActionResult.</returns>
        [HttpGet]
        // GET: Default 
        public virtual ActionResult Index(string groupKey = null, string parentKey = null)
        { 
            return GetViewResult("Index", DefaultSettings); 
        }

        /// <summary>Gets the view result.</summary>
        /// <param name="viewName">Name of the view</param>
        /// <param name="settings">Settings to customize the view</param>
        /// <returns>ActionResult.</returns>
        [NonAction]
        public ActionResult GetViewResult(string viewName = null, AdminSettings settings = null)
        {
            return View(GetViewPath(viewName), settings ?? DefaultSettings);
        }

        [NonAction]
        public string GetViewPath(string viewName)
        {
            return $"~/Areas/LocalizedGroupDataItem/Views/Admin/{viewName ?? "Index"}.cshtml";
        }

        public class AdminSettings
        {
            public string PageTitle { get; set; }
            public string Area { get; set; } = null;
            public string ApiOverrideUrl { get; set; } = null;
            public bool IsAdmin { get; set; } = false;
            public bool CanAdd { get; set; } = false;
            public bool CanEdit { get; set; } = false;
            public bool CanDelete { get; set; } = false;
            public bool CanView { get; set; } = false;



            public bool ShowId { get; set; } = false;
            public bool ShowLanguage { get; set; } = false;
            public bool ShowGroupKey { get; set; } = false;
            public bool ShowKey { get; set; } = true;
            public bool ShowValue { get; set; } = true;
            public bool ShowOrder { get; set; } = false;
            public bool ShowDescription { get; set; } = false;
            public bool ShowData { get; set; } = false;

            public bool EditId { get; set; } = true;
            public bool EditLanguage { get; set; } = true;
            public bool EditGroupKey { get; set; } = true;
            public bool EditKey { get; set; } = true;
            public bool EditValue { get; set; } = true;
            public bool EditOrder { get; set; } = true;
            public bool EditDescription { get; set; } = true;
            public bool EditData { get; set; } = true;
            public string GroupKeyOverride { get; set; } = null;
            public string KeyOverride { get; set; } = null;
            public string LanguageOverride { get; set; } = null;

            public List<SelectListItem> LanguageList { get; } = new List<SelectListItem>() { new SelectListItem("English", "EN") };
            //public 

            public string DataTableClass { get; set; } = "table table-bordered table-sm table-striped table-hover";
            public bool DataTablePaging { get; set; } = true;
            public bool DataTableSearch { get; set; } = true;
            public bool DataTableInfo { get; set; } = true;
            public string DataTableAttributes { get; set; } = " width='100%'";

        }

    }
}