using System.Collections.Generic;
using Jlw.Data.LocalizedContent;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Routing;
using Newtonsoft.Json.Linq;

namespace Jlw.Web.Rcl.LocalizedContent.Areas.ModularWizardEmailAdmin.Controllers 
{
    /// <summary>Class AdminController.
    /// Implements the <see cref="Microsoft.AspNetCore.Mvc.Controller" /></summary>
    public abstract class AdminController : Controller 
    {
        /// <summary>Email Admin UI page</summary>
        /// <param name="groupKey">The group key to filter by.</param>
        /// <param name="parentKey">The parent key to filter by.</param>
        /// <returns>ActionResult.</returns>
        [HttpGet]
        public virtual ActionResult Index(string groupKey=null, string parentKey=null)
        {
            return GetViewResult();
        }

        /// <summary>Gets the view result.</summary>
        /// <param name="viewName">Name of the view</param>
        /// <param name="settings">Settings to customize the view</param>
        /// <returns>ActionResult.</returns>
        [NonAction]
        public ActionResult GetViewResult(string viewName = null, WizardAdminSettings settings = null)
        {
            return View(viewName ?? "Index", settings ?? new WizardAdminSettings());
        }

        public class WizardAdminSettings : LocalizedContent.WizardAdminSettings
        {
            public bool ShowGroup { get; set; } = false;
            public bool ShowFieldName { get; set; } = false;
            public bool ShowLanguage { get; set; } = false;
            public bool ShowParent { get; set; } = false;
            public bool ShowKey { get; set; } = true;

            public string GroupKey { get; set; }
            public string FieldKey { get; set; }
            public string ParentKey { get; set; }
            public string Language { get; set; }

            public string DataTableClass { get; set; } = "table table-bordered table-sm table-striped table-hover";
            public bool DataTablePaging { get; set; } = true;
            public bool DataTableSearch { get; set; } = true;
            public bool DataTableInfo { get; set; } = true;
            public string DataTableAttributes { get; set; }

            public string EditView { get; set; } = "~/Areas/ModularWizardEmailAdmin/Views/Admin/_EditRecord.cshtml";
            public string DataTableListView { get; set; } = "~/Areas/ModularWizardEmailAdmin/Views/Admin/_DataTableList.cshtml";
            public string DataTableScriptView { get; set; } = "~/Areas/ModularWizardEmailAdmin/Views/Admin/_DataTableListScript.cshtml";

            public WizardAdminSettings() : this(null) {}
            public WizardAdminSettings(object o) : base(o) { }
        }
    }
}