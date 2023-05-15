using Jlw.Extensions.DataParsing;
using Microsoft.AspNetCore.Mvc;

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

            public WizardAdminSettings(object o) : base(o)
            {
                if (o is null)
                    return;

                ShowGroup = o.ParseTo<bool>(nameof(ShowGroup));
                ShowFieldName = o.ParseTo<bool>(nameof(ShowFieldName));
                ShowLanguage = o.ParseTo<bool>(nameof(ShowLanguage));
                ShowParent = o.ParseTo<bool>(nameof(ShowParent));
                ShowKey = o.ParseTo<bool>(nameof(ShowKey));
                GroupKey = o.ParseTo<string>(nameof(GroupKey));
                FieldKey = o.ParseTo<string>(nameof(FieldKey));
                ParentKey = o.ParseTo<string>(nameof(ParentKey));
                Language = o.ParseTo<string>(nameof(Language));
                DataTableClass = o.NullIfWhiteSpace(nameof(DataTableClass)) ?? DataTableClass;
                DataTablePaging = o.ParseTo<bool>(nameof(DataTablePaging));
                DataTableSearch = o.ParseTo<bool>(nameof(DataTableSearch));
                DataTableInfo = o.ParseTo<bool>(nameof(DataTableInfo));
                DataTableAttributes = o.ParseTo<string>(nameof(DataTableAttributes));

                EditView = o.NullIfWhiteSpace(nameof(EditView)) ?? EditView;
                DataTableListView = o.NullIfWhiteSpace(nameof(DataTableListView)) ?? DataTableListView;
                DataTableScriptView = o.NullIfWhiteSpace(nameof(DataTableScriptView)) ?? DataTableScriptView;

            }
        }
    }
}