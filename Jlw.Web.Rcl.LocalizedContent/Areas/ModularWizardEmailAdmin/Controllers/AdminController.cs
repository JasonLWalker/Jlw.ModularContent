using System.Collections.Generic;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json.Linq;

namespace Jlw.Web.Rcl.LocalizedContent.Areas.ModularWizardEmailAdmin.Controllers 
{
    /// <summary>Class AdminController.
    /// Implements the <see cref="Microsoft.AspNetCore.Mvc.Controller" /></summary>
    public abstract class AdminController : Controller 
    {
        protected readonly IDictionary<string, string> _languageList = new Dictionary<string, string>() { { "EN", "English" } };

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

        public class WizardAdminSettings
        {
            public bool IsAdmin { get; set; }
            public bool CanEdit { get; set; }
            public bool CanDelete { get; set; }
            public bool CanInsert { get; set; }

            public bool UseWysiwyg { get; set; } = true;
            public bool ShowGroup { get; set; } = false;
            public bool ShowFieldName { get; set; } = false;
            public bool ShowLanguage { get; set; } = false;
            public bool ShowParent { get; set; } = false;
            public bool ShowKey { get; set; } = true;
            public JToken TinyMceSettings { get; set; }
            public string PageTitle { get; set; }
            public string ExtraCss { get; set; }
            public string ExtraScript { get; set; }
            public string Area { get; set; }
            public string AdminUrl { get; set; }
            public string ApiOverrideUrl { get; set; }
            public string JsRoot { get; set; }

            public string GroupKey { get; set; }
            public string FieldKey { get; set; }
            public string ParentKey { get; set; }
            public string Language { get; set; }

            public List<SelectListItem> LanguageList { get; } = new List<SelectListItem>() { new SelectListItem("English", "EN") };

            public string DataTableClass { get; set; } = "table table-bordered table-sm table-striped table-hover";
            public bool DataTablePaging { get; set; } = true;
            public bool DataTableSearch { get; set; } = true;
            public bool DataTableInfo { get; set; } = true;
            public string DataTableAttributes { get; set; }
        }

    }
}